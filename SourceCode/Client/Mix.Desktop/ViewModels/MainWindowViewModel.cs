using Mix.Desktop.WebApis;
using Mix.Library.Entities.Models;
using Mix.Windows.WPF;
using Prism.Commands;
using Prism.Ioc;
using Refit;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mix.Desktop
{
    public class MainWindowViewModel : ViewModelBase, IViewLoadedAndUnloadedAware
    {
        #region Properties

        public ObservableCollection<CompanyDto> Companies
        {
            get { return _Companies; }
            set { SetProperty(ref _Companies, value); }
        }
        private ObservableCollection<CompanyDto> _Companies;



        #endregion Properties

        #region Commands

        public ICommand LogoutCommand { get; set; }

        #endregion Commands

        #region Ctor

        public MainWindowViewModel(IContainerExtension container) : base(container)
        {
            Companies = new ObservableCollection<CompanyDto>();
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

        public async void OnLoaded()
        {
            var mixApi = RestService.For<IMixApi>("https://localhost:5002");
            var result = await mixApi.GetCompaniesAsync();
            foreach (var item in result)
            {
                Companies.Add(item);
            }
        }

        public void OnUnloaded()
        {

        }

        #endregion Methods
    }
}