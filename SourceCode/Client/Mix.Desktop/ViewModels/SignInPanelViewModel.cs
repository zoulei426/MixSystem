using Grpc.Core;
using Grpc.Net.Client;
using IdentityModel.Client;
using Mix.Library.Entities.Protos;
using Mix.Windows.Core;
using Mix.Windows.WPF;
using Mix.Windows.WPF.Commands;
using Prism.Ioc;
using PropertyChanged;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using static Mix.Library.Entities.Protos.Accounts;

namespace Mix.Desktop
{
    [AddINotifyPropertyChangedInterface]
    public class SignInPanelViewModel : ViewModelBase, IViewLoadedAndUnloadedAware<SignInPanel>
    {
        #region Properties

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 是否记住密码
        /// </summary>
        public bool IsRememberMe { get; set; }

        /// <summary>
        /// 是否自动登录
        /// </summary>
        public bool IsAutoSignIn { get; set; }

        private SignUpArgs signUpArgs;
        private readonly ChannelBase channel;
        private readonly AccountsClient accountsClient;

        #endregion Properties

        #region Commands

        public ICommand SignInCommand { get; set; }

        #endregion Commands

        #region Ctor

        public SignInPanelViewModel(IContainerExtension container) : base(container)
        {
            channel = GrpcChannel.ForAddress("https://localhost:5001");
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

            var disco = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5000/");
            if (disco.IsError)
            {
                Notifier.Error(disco.Error);
                EventAggregator.GetEvent<MainWindowLoadingEvent>().Publish(false);
                return;
            }

            var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "wpf client",
                ClientSecret = "77DAABEF-697A-4CC1-A400-3CC561B9AD83",
                Scope = "api1 openid profile",
                UserName = "alice",
                Password = "123456"
            });

            if (tokenResponse.IsError)
            {
                Notifier.Error(tokenResponse.Error);
                EventAggregator.GetEvent<MainWindowLoadingEvent>().Publish(false);
                return;
            }

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync(disco.UserInfoEndpoint);

            if (response.IsSuccessStatusCode)
            {
                System.Windows.Application.Current.Properties["AccessToken"] = tokenResponse.AccessToken;
                var content = await response.Content.ReadAsStringAsync();
                Notifier.Success(Localizer["Login Success"]);
            }
            else
            {
                Notifier.Error(Localizer["Login Failed"]);
                EventAggregator.GetEvent<MainWindowLoadingEvent>().Publish(false);
                ConfigureFile.SetValue(ConfigureKeys.AutoSignIn, false);
                return;
            }

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