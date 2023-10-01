using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Config
{
    public abstract class BaseMultiConfig<T, U> : BaseConfig<U> where T : class, IConfigData where U : class, IConfig, new()
    {
        [SerializeField]
        private List<T> data;

        private Dictionary<string, T> dataRaw;
        public Dictionary<string, T> Data => dataRaw;

        public override void Initialize()
        {
            base.Initialize();

            dataRaw = data.ToDictionary(d => d.ID);
        }
    }
}
