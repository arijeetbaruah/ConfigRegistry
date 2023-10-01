using UnityEngine;

namespace Core.Config
{
    public abstract class BaseSingleConfig<T, U> : BaseConfig<U> where T : class, IConfigData where U : class, IConfig, new()
    {
        [SerializeField]
        private T data;

        public T Data => data;
    }
}
