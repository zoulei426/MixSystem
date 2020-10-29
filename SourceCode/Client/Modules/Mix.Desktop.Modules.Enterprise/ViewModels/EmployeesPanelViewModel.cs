using Mix.Library.Entities.DtoParameters;
using Mix.Library.Entities.Dtos;
using Mix.Windows.WPF;
using Prism.Ioc;
using PropertyChanged;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mix.Desktop.Modules.Enterprise.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class EmployeesPanelViewModel : EnterpriseViewModel, IViewLoadedAndUnloadedAware
    {
        #region Properties

        public IList<EmployeeDto> Employees { get; set; }

        #endregion Properties

        #region Fields

        #endregion Fields

        #region Commands

        #endregion Commands

        #region Ctor

        public EmployeesPanelViewModel(IContainerExtension container) : base(container)
        {
        }

        #endregion Ctor

        #region Methods

        protected override void RegisterCommands()
        {
        }

        protected override void SubscribeEvents()
        {
            EventAggregator.GetEvent<GetEmployeesForCompanyEvent>().Subscribe(GetEmployeesForCompany);
        }

        public void OnLoaded()
        {
            Employees = new ObservableCollection<EmployeeDto>();
        }

        public void OnUnloaded()
        {
            Employees?.Clear();
        }

        private async void GetEmployeesForCompany(CompanyDto obj)
        {
            Employees.Clear();

            var parameters = new EmployeeDtoParameters();
            var result = await EnterpriseApi.GetEmployeesForCompany(obj.Id, parameters);
            foreach (var item in result)
            {
                Employees.Add(item);
            }
        }

        #endregion Methods
    }
}