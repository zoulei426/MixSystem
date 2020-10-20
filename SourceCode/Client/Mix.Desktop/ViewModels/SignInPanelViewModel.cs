using Grpc.Core;
using Grpc.Net.Client;
using IdentityModel.Client;
using Mix.Library.Entity.Protos;
using Mix.Windows.Controls;
using Mix.Windows.Core;
using Mix.Windows.WPF;
using Mix.Windows.WPF.Commands;
using Prism.Ioc;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
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
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set { SetProperty(ref _Password, value); }
        }

        private string _Password;

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

        private SignUpArgs signUpArgs;
        private ChannelBase channel;
        private AccountsClient accountsClient;

        #endregion Properties

        #region Commands

        public ICommand SignInCommand { get; set; }

        #endregion Commands

        #region Ctor

        public SignInPanelViewModel(IContainerExtension container) : base(container)
        {
            channel = GrpcChannel.ForAddress("https://localhost:5001");
            //channel = new Channel("localhost:5001", ChannelCredentials.Insecure);
            accountsClient = new AccountsClient(channel);

            EventAggregator.GetEvent<SignUpSuccessEvent>().Subscribe(signUpArgs =>
            this.signUpArgs = signUpArgs);
        }

        #endregion Ctor

        #region Methods

        public void OnLoaded(SignInPanel view)
        {
            // 1. Login info from SignUpView
            if (signUpArgs != null)
            {
                IsRememberMe = false;
                IsAutoSignIn = false;
                Email = signUpArgs.Username;
                Password = signUpArgs.Password;

                SignInCommand.Execute(null);
                signUpArgs = null;
                return;
            }

            // 2. If there is some residual information on username or password text box, no login information is loaded from elsewhere.
            if (!string.IsNullOrEmpty(Email) || !string.IsNullOrEmpty(Password)) return;

            // 3. No login info from config file.
            if (!CanSignIn(ConfigureFile.GetValue<string>(ConfigureKeys.Username), ConfigureFile.GetValue<string>(ConfigureKeys.Password))) return;

            // 4. Login info from config file.
            IsRememberMe = true;
            IsAutoSignIn = ConfigureFile.GetValue<bool>(ConfigureKeys.AutoSignIn);
            Email = ConfigureFile.GetValue<string>(ConfigureKeys.Username);
            Password = ConfigureFile.GetValue<string>(ConfigureKeys.Password).DecryptByDes();

            if (IsAutoSignIn)
            {
                SignInCommand.Execute(null);
            }
        }

        public void OnUnloaded(SignInPanel view)
        {
        }

        protected override void RegisterCommands()
        {
            SignInCommand = new RelayCommand(ExecuteSignIn, () => CanSignIn(Email, Password));
        }

        private static bool CanSignIn(string username, string password) => !username.IsNullOrEmpty() && !password.IsNullOrEmpty();

        private async void ExecuteSignIn()
        {
            var passwordMd5 = Password == ConfigureFile.GetValue<string>(ConfigureKeys.Password).DecryptByDes()
                            ? Password
                            : Password.ToMd5();

            await SignInAsync(Email, passwordMd5);
        }

        private async Task SignInAsync(string username, string passwordMd5)
        {
            EventAggregator.GetEvent<MainWindowLoadingEvent>().Publish(true);

            var httpClient = new HttpClient();
            var disco = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5999/");
            if (disco.IsError)
            {
                return;
            }

            var token = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A",
                Scope = "api"
            });
            var tokenValue = "Bearer " + token.AccessToken;
            var metadata = new Metadata
    {
        { "Authorization", tokenValue }
    };
            var callOptions = new CallOptions(metadata);

            LoginResponse response = null;
            try
            {
                response = await accountsClient.LoginAsync(new Library.Entity.Protos.LoginRequest
                {
                    Username = Email,
                    Password = passwordMd5
                }, callOptions);

                Notify.Success(Localizer["Login Success"]);
            }
            catch (Exception ex)
            {
                Notify.Error(ex.Message);
                EventAggregator.GetEvent<MainWindowLoadingEvent>().Publish(false);
                ConfigureFile.SetValue(ConfigureKeys.AutoSignIn, false);
                return;
            }

            //await Container.Resolve<ModuleResolver>().LoadAsync();

            // Saves data.
            ConfigureFile.SetValue(ConfigureKeys.Username, IsRememberMe ? username : string.Empty);
            ConfigureFile.SetValue(ConfigureKeys.Password, IsRememberMe ? passwordMd5.EncryptByDes() : string.Empty);
            ConfigureFile.SetValue(ConfigureKeys.AutoSignIn, IsAutoSignIn);

            // Launches main window and closes itself.
            ShellManager.Switch<LoginWindow, MainWindow>();
        }

        #endregion Methods
    }
}