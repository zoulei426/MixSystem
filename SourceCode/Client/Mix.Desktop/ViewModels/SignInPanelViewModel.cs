using Grpc.Core;
using Grpc.Net.Client;
using Mix.Core;
using Mix.Windows.Core;
using Mix.Windows.WPF;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using static Mix.Library.Entity.Protos.Accounts;

namespace Mix.Desktop
{
    public class SignInPanelViewModel : ViewModelBase, IViewLoadedAndUnloadedAware<SignInPanel>
    {
        #region Properties

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            get { return _Email; }
            set { SetProperty(ref _Email, value); }
        }

        private string _Email;

        /// <summary>
        /// 是否记住密码
        /// </summary>
        public bool IsRememberMe
        {
            get { return _IsRememberMe; }
            set { if (SetProperty(ref _IsRememberMe, value) && !value) IsAutoSignIn = false; }
        }

        private bool _IsRememberMe;

        /// <summary>
        /// 是否自动登录
        /// </summary>
        public bool IsAutoSignIn
        {
            get { return _IsAutoSignIn; }
            set { if (SetProperty(ref _IsAutoSignIn, value) && value) IsRememberMe = true; }
        }

        private bool _IsAutoSignIn;

        private ChannelBase channel;
        private AccountsClient accountsClient;

        #endregion Properties

        #region Commands

        public ICommand SignInCommand { get; set; }

        #endregion Commands

        public SignInPanelViewModel(IContainerExtension container) : base(container)
        {
            channel = GrpcChannel.ForAddress("https://localhost:5001");
            //channel = new Channel("localhost:5001", ChannelCredentials.Insecure);
            accountsClient = new AccountsClient(channel);
        }

        protected override void RegisterCommands()
        {
            SignInCommand = new RelayCommand<PasswordBox>(ExecuteSignIn, passwordBox => CanSignIn(Email, passwordBox.Password));
        }

        private static bool CanSignIn(string username, string password) => !username.IsNullOrEmpty() && !password.IsNullOrEmpty();

        private async void ExecuteSignIn(PasswordBox password)
        {
            var passwordMd5 = password.Password == ConfigureFile.GetValue<string>(SystemConfigKeys.Password).DecryptByDes()
                            ? password.Password
                            : password.Password.ToMd5();

            var response = await accountsClient.LoginAsync(new Library.Entity.Protos.LoginRequest
            {
                Username = Email,
                Password = password.Password
                
            });

            ShellManager.Switch<LoginWindow, MainWindow>();
            //await SignInAsync(Email, passwordMd5);
        }

        private async Task SignInAsync(string username, string passwordMd5)
        {
            EventAggregator.GetEvent<MainWindowLoadingEvent>().Publish(true);

            if (!await AuthenticateAsync(username, passwordMd5))
            {
                EventAggregator.GetEvent<MainWindowLoadingEvent>().Publish(false);
                ConfigureFile.SetValue(SystemConfigKeys.AutoSignIn, false);
                return;
            }

            //await Container.Resolve<ModuleResolver>().LoadAsync();

            // Saves data.
            ConfigureFile.SetValue(SystemConfigKeys.Username, IsRememberMe ? username : string.Empty);
            ConfigureFile.SetValue(SystemConfigKeys.Password, IsRememberMe ? passwordMd5.EncryptByDes() : string.Empty);
            ConfigureFile.SetValue(SystemConfigKeys.AutoSignIn, IsAutoSignIn);

            // Launches main window and closes itself.
            ShellManager.Switch<LoginWindow, MainWindow>();
        }

        private Task<bool> AuthenticateAsync(string username, string passwordMd5)
        {
            throw new NotImplementedException();
        }

        public void OnLoaded(SignInPanel view)
        {
        }

        public void OnUnloaded(SignInPanel view)
        {
        }
    }
}