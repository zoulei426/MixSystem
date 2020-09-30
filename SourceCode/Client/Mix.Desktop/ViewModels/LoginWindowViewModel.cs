using Mix.Windows.WPF;
using Prism.Ioc;
using Unity;

namespace Mix.Desktop
{
    public class LoginWindowViewModel : ViewModelBase, IViewLoadedAndUnloadedAware<LoginWindow>
    {
        #region Ctor

      
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="container"></param>
        protected LoginWindowViewModel(IContainerExtension container) : base(container)
        {
            Logger.Info("登录");
            Logger.Error("错误", new System.Exception("Error"));
        }

        public void OnLoaded(LoginWindow view)
        {
        }

        public void OnUnloaded(LoginWindow view)
        {
        }

        #endregion

        #region Commands


        #endregion

    }
}
