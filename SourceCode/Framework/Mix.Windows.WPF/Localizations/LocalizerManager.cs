using Microsoft.Extensions.Localization;
using Mix.Core;
using Mix.Windows.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Mix.Windows.WPF.Localizations
{
    public class LocalizerManager
    {
        private readonly IConfigureFile configure;
        private readonly IStringLocalizerFactory localizerFactory;

        public event Action CurrentUICultureChanged;

        public static LocalizerManager Instance { get; private set; }

        public static void Initialize(IConfigureFile configure, IStringLocalizerFactory localizerFactory)
        {
            Instance = new LocalizerManager(configure, localizerFactory);
        }

        private LocalizerManager(IConfigureFile configure, IStringLocalizerFactory localizerFactory)
        {
            this.configure = configure;
            this.localizerFactory = localizerFactory;
        }

        public IEnumerable<CultureInfo> AvailableCultureInfos => new[]
        {
            new CultureInfo("zh-CN"),
            new CultureInfo("en-US")
        };

        public CultureInfo CurrentUICulture
        {
            get => CultureInfo.CurrentUICulture;
            set
            {
                if (EqualityComparer<CultureInfo>.Default.Equals(value, CultureInfo.CurrentUICulture)) return;

                CultureInfo.CurrentUICulture = value;
                //CultureInfo.DefaultThreadCurrentUICulture = value;
                configure.SetValue(SystemConst.LANGUAGE, value);
                OnCurrentUICultureChanged();
            }
        }

        private void OnCurrentUICultureChanged() => CurrentUICultureChanged?.Invoke();

        public string Get(string key)
        {
            var localizer = localizerFactory.Create("", "");
            return localizer[key].Value;

        }
    }
}
