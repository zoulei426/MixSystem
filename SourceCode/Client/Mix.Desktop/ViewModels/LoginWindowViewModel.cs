using Mix.Windows.WPF;
using Prism.Ioc;
using System.Windows.Controls;

namespace Mix.Desktop
{
    public class LoginWindowViewModel : ViewModelBase, IViewLoadedAndUnloadedAware<LoginWindow>
    {
        #region Fields

        private TabItem _SignInTabItem;

        #endregion Fields

        #region Ctor

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="container"></param>
        public LoginWindowViewModel(IContainerExtension container) : base(container)
        {
        }

        public void OnLoaded(LoginWindow view)
        {
            //Navigate()
        }

        public void OnUnloaded(LoginWindow view)
        {
        }

        #endregion Ctor
    }
}