using Grpc.Core;
using Grpc.Net.Client;
using Mix.Windows.WPF;
using Mix.Windows.WPF.Commands;
using Prism.Ioc;
using PropertyChanged;
using System.Linq;
using System.Windows.Input;
using static Mix.Library.Entities.Protos.Accounts;

namespace Mix.Desktop
{
    /// <summary>
    /// 注册
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class SignUpPanelViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        public int RemainingTimeBasedSecond { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode { get; set; }

        private ChannelBase channel;
        private AccountsClient accountsClient;

        #endregion Properties

        #region Commands

        /// <summary>
        /// 注册
        /// </summary>
        public ICommand SignUpCommand { get; set; }

        /// <summary>
        /// 发送验证码
        /// </summary>
        public ICommand SendVerificationCodeCommand { get; set; }

        #endregion Commands

        #region Ctor

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="container"></param>
        public SignUpPanelViewModel(IContainerExtension container) : base(container)
        {
            channel = GrpcChannel.ForAddress("https://localhost:5001");
            accountsClient = new AccountsClient(channel);
        }

        #endregion Ctor

        #region Methods

        #region Methods - Override

        protected override void RegisterCommands()
        {
            SignUpCommand = new RelayCommand(ExecuteSignUp, CanSignUp);
        }

        #endregion Methods - Override

        #region Methods - Private

        private bool CanSignUp() => new[]
        {
            Email,
            UserName,
            Password,
        }.All(field => !string.IsNullOrEmpty(field));

        private async void ExecuteSignUp()
        {
            EventAggregator.GetEvent<MainWindowLoadingEvent>().Publish(true);

            var response = await accountsClient.RegisterAsync(new Library.Entities.Protos.RegisterRequest
            {
                Email = Email,
                Username = UserName,
                Password = Password,
            });

            if (response.Response.Code == Library.Entities.Protos.ErrorCode.Success)
            {
                //Password = string.Empty;

                Notifier.Success(Localizer["Register Success"].Value);
                EventAggregator.GetEvent<SignUpSuccessEvent>().Publish(new SignUpArgs
                {
                    Username = Email,
                    Password = Password
                });
            }

            EventAggregator.GetEvent<MainWindowLoadingEvent>().Publish(false);
        }

        #endregion Methods - Private

        #endregion Methods
    }
}