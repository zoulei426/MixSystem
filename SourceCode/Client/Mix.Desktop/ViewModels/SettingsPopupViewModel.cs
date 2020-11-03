using MaterialDesignThemes.Wpf;
using Mix.Desktop.Views;
using Mix.Windows.WPF;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mix.Desktop.ViewModels
{
    public class SettingsPopupViewModel : ViewModelBase, IViewLoadedAndUnloadedAware<SettingsPopup>
    {
        #region Fields

        private readonly Dictionary<Type, object> _dialogDictionary = new Dictionary<Type, object>();
        private SettingsPopup _view;

        #endregion Fields

        #region Commands

        public ICommand ChangeProfileCommand { get; set; }

        public ICommand OpenSettingsPanelCommand { get; set; }

        public ICommand HelpCommand { get; set; }

        public ICommand OpenOfficialSiteCommand { get; set; }

        public ICommand AboutCommand { get; set; }

        public ICommand SignOutCommand { get; set; }

        #endregion Commands

        #region Ctor

        public SettingsPopupViewModel(IContainerExtension container) : base(container)
        {
        }

        #endregion Ctor

        #region Methods

        protected override void RegisterCommands()
        {
            SignOutCommand = new DelegateCommand(() =>
            {
                ShellManager.Switch<MainWindow, LoginWindow>();
            });
        }

        public void OnLoaded(SettingsPopup view)
        {
            _view = view;
        }

        public void OnUnloaded(SettingsPopup view)
        {
        }

        #region Methods - Private

        private async void OpenDialog<T>() where T : new()
        {
            var type = typeof(T);

            if (!_dialogDictionary.ContainsKey(type))
                _dialogDictionary[type] = new T();

            _view.SetValue(System.Windows.Controls.Primitives.Popup.IsOpenProperty, false);
            await DialogHost.Show(_dialogDictionary[type], "RootDialog");
        }

        #endregion Methods - Private

        #endregion Methods
    }
}