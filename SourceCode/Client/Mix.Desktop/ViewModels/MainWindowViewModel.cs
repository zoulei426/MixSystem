using Mix.Desktop.WebApis;
using Mix.Library.Entities.Dtos;
using Mix.Windows.WPF;
using Mix.Windows.WPF.Commands;
using Prism.Commands;
using Prism.Ioc;
using Refit;
using System;
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

        public CompanyDto CurrentCompany
        {
            get { return _CurrentCompany; }
            set { SetProperty(ref _CurrentCompany, value); }
        }

        private CompanyDto _CurrentCompany;

        public ObservableCollection<EmployeeDto> Employees
        {
            get { return _Employees; }
            set { SetProperty(ref _Employees, value); }
        }

        private ObservableCollection<EmployeeDto> _Employees;

        #endregion Properties

        #region Fileds

        private readonly IMixApi mixApi;

        #endregion Fileds

        #region Commands

        public ICommand LogoutCommand { get; set; }

        public ICommand GetEmployeesForCompanyCommand { get; set; }

        #endregion Commands

        #region Ctor

        public MainWindowViewModel(IContainerExtension container) : base(container)
        {
            mixApi = RestService.For<IMixApi>("https://localhost:5002");
            Companies = new ObservableCollection<CompanyDto>();
            Employees = new ObservableCollection<EmployeeDto>();
        }

        #endregion Ctor

        #region Methods

        protected override void RegisterCommands()
        {
            LogoutCommand = new DelegateCommand(() =>
            {
                ShellManager.Switch<MainWindow, LoginWindow>();
            });

            GetEmployeesForCompanyCommand = new RelayCommand(ExecuteGetEmployeesForCompany, CanGetEmployeesForCompany);
        }

        public async void OnLoaded()
        {
            var result = await mixApi.GetCompaniesAsync();
            foreach (var item in result)
            {
                Companies.Add(item);
            }
        }

        public void OnUnloaded()
        {
        }

        private bool CanGetEmployeesForCompany()
        {
            return CurrentCompany is not null;
        }

        private async void ExecuteGetEmployeesForCompany()
        {
            Employees.Clear();
            var result = await mixApi.GetEmployeesForCompany(CurrentCompany.Id);
            foreach (var item in result)
            {
                Employees.Add(item);
            }
        }

        #endregion Methods
    }
}