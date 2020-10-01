﻿using Mix.Windows.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows;

namespace Mix.Windows.WPF
{
    public class I18nManager
    {
        private readonly IConfigureFile _configure;
        private readonly ConcurrentDictionary<string, ResourceManager> _resourceManagerStorage = new ConcurrentDictionary<string, ResourceManager>();

        public event Action CurrentUICultureChanged;

        public static I18nManager Instance { get; private set; }

        public static void Initialize(IConfigureFile configure)
        {
            Instance = new I18nManager(configure);
        }

        private I18nManager(IConfigureFile configure)
        {
            _configure = configure;
        }

        public CultureInfo CurrentUICulture
        {
            get => CultureInfo.DefaultThreadCurrentUICulture;
            set
            {
                if (EqualityComparer<CultureInfo>.Default.Equals(value, CultureInfo.DefaultThreadCurrentUICulture)) return;

                CultureInfo.DefaultThreadCurrentUICulture = value;
                _configure.SetValue("language", value);
                OnCurrentUICultureChanged();
            }
        }

        public IEnumerable<CultureInfo> AvailableCultureInfos => new[]
        {
            new CultureInfo("zh-CN"),
            new CultureInfo("en-US")
        };

        public void AddResourceManager(ResourceManager resourceManager)
        {
            if (_resourceManagerStorage.ContainsKey(resourceManager.BaseName))
                throw new ArgumentException("", nameof(resourceManager));

            _resourceManagerStorage[resourceManager.BaseName] = resourceManager;
        }

        private void OnCurrentUICultureChanged() => CurrentUICultureChanged?.Invoke();

        public object Get(ComponentResourceKey key)
        {
            return GetCurrentResourceManager(key.TypeInTargetAssembly.FullName)?.GetObject(key.ResourceId.ToString(), CurrentUICulture)
                   ?? $"<MISSING: {key}>";
        }

        private ResourceManager GetCurrentResourceManager(string key)
        {
            return _resourceManagerStorage.TryGetValue(key, out var value) ? value : null;
        }
    }
}
