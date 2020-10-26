using Mix.Library.Entities.DtoParameters;
using Mix.Library.Entities.Dtos;
using Mix.Windows.WPF;
using Mix.Windows.WPF.Commands;
using Prism.Ioc;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mix.Desktop.Modules.Enterprise.ViewModels
{
    public class EnterpriseComponentViewModel : ViewModelBase, IViewLoadedAndUnloadedAware
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
            set 
            {
                if (value is null) return;
                if(!SetProperty(ref _CurrentCompany, value)) return;

                GetEmployeesForCompany();
            }
        }

        private CompanyDto _CurrentCompany;

        public ObservableCollection<EmployeeDto> Employees
        {
            get { return _Employees; }
            set { SetProperty(ref _Employees, value); }
        }

        private ObservableCollection<EmployeeDto> _Employees;
        #endregion

        #region Fields

        private readonly IMixApi mixApi;
        #endregion
        #region Commands


        #endregion Commands
        #region Ctor

        public EnterpriseComponentViewModel(IContainerExtension container) : base(container)
        {
            mixApi = Container.Resolve<IMixApi>();
            Companies = new ObservableCollection<CompanyDto>();
            Employees = new ObservableCollection<EmployeeDto>();

        }
        #endregion

        #region Methods

        public async void OnLoaded()
        {
            var parameters = new CompanyDtoParameters();
            var result = await mixApi.GetCompaniesAsync(parameters);
            foreach (var item in result)
            {
                Companies.Add(item);
            }
        }

        public void OnUnloaded()
        {
        }


        private async void GetEmployeesForCompany()
        {
            Employees.Clear();
            var result = await mixApi.GetEmployeesForCompany(CurrentCompany.Id);
            foreach (var item in result)
            {
                Employees.Add(item);
            }
        }
        #endregion
    }
}
