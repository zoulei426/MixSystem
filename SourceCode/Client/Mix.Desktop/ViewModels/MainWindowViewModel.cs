using Mix.Windows.WPF;
using Prism.Commands;
using Prism.Ioc;
using System.Windows.Input;

namespace Mix.Desktop
{
    public class MainWindowViewModel : ViewModelBase, IViewLoadedAndUnloadedAware
    {
        #region Properties

        #endregion Properties

        #region Commands

        public ICommand LogoutCommand { get; set; }

        #endregion Commands

        #region Ctor

        public MainWindowViewModel(IContainerExtension container) : base(container)
        {
        }

        #endregion Ctor

        #region Methods

        protected override void RegisterCommands()
        {
            LogoutCommand = new DelegateCommand(() =>
            {
                ShellManager.Switch<MainWindow, LoginWindow>();
            });
        }

        public void OnLoaded()
        {

        }

        public void OnUnloaded()
        {

        }

        #endregion Methods
    }
}