using UnityEngine;

namespace Core.Config
{
    public interface IConfig
    {    
        string ID { get; }
        void Initialize();
    }

    public interface IConfigData
    {
        string ID { get; }
    }

    public abstract class BaseConfig<U> : ScriptableObject, IConfig where U : class, IConfig, new()
    {
        public string ID => typeof(U).FullName;

        public virtual void Initialize() { }
    }
}
