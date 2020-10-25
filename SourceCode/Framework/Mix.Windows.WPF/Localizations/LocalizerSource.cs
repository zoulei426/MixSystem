using System.ComponentModel;
using System.Windows;

namespace Mix.Windows.WPF.Localizations
{
    /// <summary>
    /// 国际化数据源，用于绑定XAML界面，实现动态切换语言
    /// </summary>
    public class LocalizerSource : INotifyPropertyChanged
    {
        private readonly string _key;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizerSource"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="element">The element.</param>
        public LocalizerSource(string key, FrameworkElement element = null)
        {
            _key = key;

            if (element != null)
            {
                element.Loaded += OnLoaded;
                element.Unloaded += OnUnloaded;
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value => LocalizerManager.Instance.Get(_key);

        /// <summary>
        /// Raises the value.
        /// </summary>
        private void RaiseValue()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }

        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            RaiseValue();
            LocalizerManager.Instance.CurrentUICultureChanged += RaiseValue;
        }

        /// <summary>
        /// Called when [unloaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            RaiseValue();
            LocalizerManager.Instance.CurrentUICultureChanged -= RaiseValue;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="LocalizerSource"/>.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator LocalizerSource(string resourceKey) => new LocalizerSource(resourceKey);
    }
}