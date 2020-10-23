using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Mix.Windows.WPF.Localizations
{
    /// <summary>
    /// 国际化Markup
    /// </summary>
    [MarkupExtensionReturnType(typeof(string))]
    public class LocalizerExtension : MarkupExtension
    {
        public string Key { get; }

        public LocalizerExtension(string key)
        {
            Key = key;
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

            return new Binding(nameof(LocalizerSource.Value))
            {
                Source = new LocalizerSource(Key, frameworkElement),
                Mode = BindingMode.OneWay
            }.ProvideValue(serviceProvider);
        }
    }
}