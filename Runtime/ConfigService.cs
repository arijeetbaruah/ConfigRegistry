using Core.Config;
using Core.Service;
using System;
using UnityEngine;

namespace Core.ConfigSystem
{
    public class ConfigService : IService
    {
        private ConfigRegistry configRegistry = null;

        public ConfigService(ConfigRegistry configRegistry)
        {
            this.configRegistry = configRegistry;
        }

        public T GetConfig<T>() where T : class, IConfig, new()
        {
            if (configRegistry.configs.TryGetValue(typeof(T).FullName, out ScriptableObject config))
            {
                return (T)Convert.ChangeType(config, typeof(T));
            }

            return null;
        }
    }
}
