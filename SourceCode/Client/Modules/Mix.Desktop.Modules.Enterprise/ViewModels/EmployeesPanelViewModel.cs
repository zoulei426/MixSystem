using MaterialDesignThemes.Wpf;
using Mix.Data.Pagable;
using Mix.Library.Entities.DtoParameters;
using Mix.Library.Entities.Dtos;
using Mix.Windows.WPF;
using Mix.Windows.WPF.Commands;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Mix.Desktop.Modules.Enterprise.ViewModels
{
    public class EmployeesPanelViewModel : EnterpriseViewModel, IViewLoadedAndUnloadedAware
    {
        #region Properties


        public ObservableCollection<EmployeeDto> Employees
        {
            get { return _Employees; }
            set { SetProperty(ref _Employees, value); }
        }

        private ObservableCollection<EmployeeDto> _Employees;
        #endregion

        #region Fields

        #endregion

        #region Commands

        #endregion Commands
        #region Ctor

        public EmployeesPanelViewModel(IContainerExtension container) : base(container)
        {
            
        }
        #endregion

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



        #endregion
    }
}
