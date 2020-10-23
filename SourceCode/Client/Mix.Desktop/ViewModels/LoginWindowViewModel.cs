/*
 *  ┌─────────────────────────────────────────────────────────────┐
 *  │┌───┬───┬───┬───┬───┬───┬───┬───┬───┬───┬───┬───┬───┬───┬───┐│
 *  ││Esc│!1 │@2 │#3 │$4 │%5 │^6 │&7 │*8 │(9 │)0 │_- │+= │|\ │`~ ││
 *  │├───┴─┬─┴─┬─┴─┬─┴─┬─┴─┬─┴─┬─┴─┬─┴─┬─┴─┬─┴─┬─┴─┬─┴─┬─┴─┬─┴───┤│
 *  ││ Tab │ Q │ W │ E │ R │ T │ Y │ U │ I │ O │ P │{[ │}] │ BS  ││
 *  │├─────┴┬──┴┬──┴┬──┴┬──┴┬──┴┬──┴┬──┴┬──┴┬──┴┬──┴┬──┴┬──┴─────┤│
 *  ││ Ctrl │ A │ S │ D │ F │ G │ H │ J │ K │ L │: ;│" '│ Enter  ││
 *  │├──────┴─┬─┴─┬─┴─┬─┴─┬─┴─┬─┴─┬─┴─┬─┴─┬─┴─┬─┴─┬─┴─┬─┴────┬───┤│
 *  ││ Shift  │ Z │ X │ C │ V │ B │ N │ M │< ,│> .│? /│Shift │Fn ││
 *  │└─────┬──┴┬──┴──┬┴───┴───┴───┴───┴───┴──┬┴───┴┬──┴┬─────┴───┘│
 *  │      │Fn │ Alt │         Space         │ Alt │Win│   HHKB   │
 *  │      └───┴─────┴───────────────────────┴─────┴───┘          │
 *  └─────────────────────────────────────────────────────────────┘
 */

using Mix.Windows.WPF;
using Prism.Ioc;
using System.Windows.Controls;

namespace Mix.Desktop
{
    public class LoginWindowViewModel : ViewModelBase, IViewLoadedAndUnloadedAware<LoginWindow>
    {
        #region Fields

        private TabItem SignInTabItem;

        #endregion Fields

        #region Properties

        public bool IsLoading
        {
            get { return _IsLoading; }
            set { SetProperty(ref _IsLoading, value); }
        }

        private bool _IsLoading;


        #endregion Properties

        #region Ctor

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="container"></param>
        public LoginWindowViewModel(IContainerExtension container) : base(container)
        {
            EventAggregator.GetEvent<MainWindowLoadingEvent>().Subscribe(e => IsLoading = e);
            EventAggregator.GetEvent<SignUpSuccessEvent>().Subscribe(signUpInfo => SignInTabItem.IsSelected = true);
        }

        public void OnLoaded(LoginWindow view)
        {
            this.SignInTabItem = view.FindName("SignInTabItem") as TabItem;

        }

        public void OnUnloaded(LoginWindow view)
        {
        }

        #endregion Ctor
    }
}