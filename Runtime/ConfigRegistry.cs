using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Core.Config
{
    [CreateAssetMenu(fileName = "Config Registry", menuName = "Core/Config/Config Registry")]
    public class ConfigRegistry : ScriptableObject
    {
        [SerializeField, FolderPath] private string path;

        public GenericDictionary<ScriptableObject> configs = new GenericDictionary<ScriptableObject>();

        [Button]
        public void Refresh()
        {
#if UNITY_EDITOR
            Type type = typeof(IConfig);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsGenericType && !p.IsInterface && !p.IsAbstract);

            foreach(var t in types)
            {
                if (!configs.ContainsKey(t.FullName))
                {
                    ScriptableObject so = ScriptableObject.CreateInstance(t);
                    
                    UnityEditor.AssetDatabase.CreateAsset(so, Path.Combine(path, $"{t.Name}.asset"));
                    configs.Add(t.FullName, so);
                }
            }

            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        [Serializable]
        public class GenericDictionary<TValue> : UnitySerializedDictionary<string, TValue> { }
    }

    public abstract class UnitySerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector]
        private List<TKey> keyData = new List<TKey>();

        [SerializeField, HideInInspector]
        private List<TValue> valueData = new List<TValue>();

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.Clear();
            for (int i = 0; i < this.keyData.Count && i < this.valueData.Count; i++)
            {
                this[this.keyData[i]] = this.valueData[i];
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.keyData.Clear();
            this.valueData.Clear();

            foreach (var item in this)
            {
                this.keyData.Add(item.Key);
                this.valueData.Add(item.Value);
            }
        }
    }
}
