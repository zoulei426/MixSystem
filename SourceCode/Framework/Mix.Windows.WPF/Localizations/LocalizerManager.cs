using Microsoft.Extensions.Localization;
using Mix.Core;
using Mix.Windows.Core;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Mix.Windows.WPF.Localizations
{
    /// <summary>
    /// LocalizerManager
    /// </summary>
    public class LocalizerManager
    {
        private readonly IConfigureFile configure;
        private readonly IStringLocalizerFactory localizerFactory;

        /// <summary>
        /// Occurs when [current UI culture changed].
        /// </summary>
        public event Action CurrentUICultureChanged;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static LocalizerManager Instance { get; private set; }

        /// <summary>
        /// Initializes the specified configure.
        /// </summary>
        /// <param name="configure">The configure.</param>
        /// <param name="localizerFactory">The localizer factory.</param>
        public static void Initialize(IConfigureFile configure, IStringLocalizerFactory localizerFactory)
        {
            Instance = new LocalizerManager(configure, localizerFactory);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizerManager"/> class.
        /// </summary>
        /// <param name="configure">The configure.</param>
        /// <param name="localizerFactory">The localizer factory.</param>
        private LocalizerManager(IConfigureFile configure, IStringLocalizerFactory localizerFactory)
        {
            this.configure = configure;
            this.localizerFactory = localizerFactory;
        }

        /// <summary>
        /// Gets the available culture infos.
        /// </summary>
        /// <value>
        /// The available culture infos.
        /// </value>
        public IEnumerable<CultureInfo> AvailableCultureInfos => new[]
        {
            new CultureInfo("zh-CN"),
            new CultureInfo("en-US")
        };

        /// <summary>
        /// Gets or sets the current UI culture.
        /// </summary>
        /// <value>
        /// The current UI culture.
        /// </value>
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

        /// <summary>
        /// Called when [current UI culture changed].
        /// </summary>
        private void OnCurrentUICultureChanged() => CurrentUICultureChanged?.Invoke();

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string Get(string key)
        {
            var localizer = localizerFactory.Create("", "");
            return localizer[key].Value;
        }
    }
}