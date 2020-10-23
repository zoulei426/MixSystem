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

        public event PropertyChangedEventHandler PropertyChanged;

        public LocalizerSource(string key, FrameworkElement element = null)
        {
            _key = key;

            if (element != null)
            {
                element.Loaded += OnLoaded;
                element.Unloaded += OnUnloaded;
            }
        }

        public string Value => LocalizerManager.Instance.Get(_key);

        private void RaiseValue()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            RaiseValue();
            LocalizerManager.Instance.CurrentUICultureChanged += RaiseValue;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            RaiseValue();
            LocalizerManager.Instance.CurrentUICultureChanged -= RaiseValue;
        }

        public static implicit operator LocalizerSource(string resourceKey) => new LocalizerSource(resourceKey);
    }
}
