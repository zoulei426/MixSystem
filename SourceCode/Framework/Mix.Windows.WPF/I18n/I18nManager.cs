using Mix.Core;
using Mix.Windows.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Windows;

namespace Mix.Windows.WPF
{
    public class I18nManager
    {
        private readonly IConfigureFile _Configure;
        private readonly ConcurrentDictionary<string, ResourceManager> _ResourceManagerStorage = new ConcurrentDictionary<string, ResourceManager>();

        public event Action CurrentUICultureChanged;

        public static I18nManager Instance { get; private set; }

        public static void Initialize(IConfigureFile configure)
        {
            Instance = new I18nManager(configure);
        }

        private I18nManager(IConfigureFile configure)
        {
            _Configure = configure;
        }

        public CultureInfo CurrentUICulture
        {
            get => CultureInfo.DefaultThreadCurrentUICulture;
            set
            {
                if (EqualityComparer<CultureInfo>.Default.Equals(value, CultureInfo.DefaultThreadCurrentUICulture)) return;

                CultureInfo.DefaultThreadCurrentUICulture = value;
                _Configure.SetValue(SystemConst.LANGUAGE, value);
                OnCurrentUICultureChanged();
            }
        }

        /// <summary>
        /// 已支持的语言
        /// </summary>
        public IEnumerable<CultureInfo> AvailableCultureInfos => new[]
        {
            new CultureInfo("zh-CN"),
            new CultureInfo("en-US")
        };

        public void AddResourceManager(ResourceManager resourceManager)
        {
            if (_ResourceManagerStorage.ContainsKey(resourceManager.BaseName))
                throw new ArgumentException("", nameof(resourceManager));

            _ResourceManagerStorage[resourceManager.BaseName] = resourceManager;
        }

        private void OnCurrentUICultureChanged() => CurrentUICultureChanged?.Invoke();

        public object Get(ComponentResourceKey key)
        {
            return GetCurrentResourceManager(key.TypeInTargetAssembly.FullName)?.GetObject(key.ResourceId.ToString(), CurrentUICulture)
                   ?? $"<MISSING: {key}>";
        }

        private ResourceManager GetCurrentResourceManager(string key)
        {
            return _ResourceManagerStorage.TryGetValue(key, out var value) ? value : null;
        }
    }
}