using Microsoft.Extensions.Localization;
using Mix.Core.Localization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Mix.Windows.WPF
{
    [MarkupExtensionReturnType(typeof(string))]
    public class LocalizerExtension : MarkupExtension
    {
        private readonly IStringLocalizerFactory _localizerFactory;
        public string Key { get; }

        public LocalizerExtension(string key)
        {
            Key = key;
            //_localizerFactory = DependencyResolver.Current.ResolveService<IStringLocalizerFactory>();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider.GetService(typeof(IProvideValueTarget)) is not IProvideValueTarget provideValueTarget)
                throw new ArgumentException(
                    $"The {nameof(serviceProvider)} must implement {nameof(IProvideValueTarget)} interface.");

            if (provideValueTarget.TargetObject.GetType().FullName == "System.Windows.SharedDp") return this;

            var frameworkElement = provideValueTarget.TargetObject is DependencyObject dependencyObject
                ? dependencyObject as FrameworkElement ?? dependencyObject.TryFindParent<FrameworkElement>()
                : null;

            //return new Binding(nameof(I18nSource.Value))
            //{
            //    Source = new I18nSource(key, frameworkElement),
            //    Mode = BindingMode.OneWay
            //}.ProvideValue(serviceProvider);
            var fac = new JsonStringLocalizerFactory();
            var localizer = fac.Create("", "");

            return localizer[Key].Value;
        }
    }
}