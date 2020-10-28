using MaterialDesignThemes.Wpf;
using Mix.Data.Pagable;
using Mix.Library.Entities.DtoParameters;
using Mix.Library.Entities.Dtos;
using Mix.Windows.WPF;
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
    public class EnterpriseComponentViewModel : ViewModelBase, IViewLoadedAndUnloadedAware
    {
        #region Properties

        public ObservableCollection<Card> CompanyCards
        {
            get { return _CompanyCards; }
            set { SetProperty(ref _CompanyCards, value); }
        }

        private ObservableCollection<Card> _CompanyCards;

        public ObservableCollection<EmployeeDto> Employees
        {
            get { return _Employees; }
            set { SetProperty(ref _Employees, value); }
        }

        private ObservableCollection<EmployeeDto> _Employees;

        #endregion Properties

        #region Fields

        private ResourceDictionary ButtonResource = new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Button.xaml") };
        private readonly IMixApi mixApi;
        private PaginationMetadata pagination;
        private CompanyDtoParameters companyParameters;

        #endregion Fields

        #region Commands

        public ICommand GetCompaniesCommand { get; set; }

        public ICommand GetEmployeesForCompanyCommand { get; set; }

        #endregion Commands

        #region Ctor

        public EnterpriseComponentViewModel(IContainerExtension container) : base(container)
        {
            mixApi = Container.Resolve<IMixApi>();
        }

        #endregion Ctor

        #region Methods

        protected override void RegisterCommands()
        {
            GetCompaniesCommand = new DelegateCommand(GetCompanies);
            GetEmployeesForCompanyCommand = new DelegateCommand<CompanyDto>(GetEmployeesForCompany);
        }

        public async void OnLoaded()
        {
            Employees = new ObservableCollection<EmployeeDto>();

            CompanyCards = new ObservableCollection<Card>();
            companyParameters = new CompanyDtoParameters();

            await Task.Run(() => GetCompanies());
        }

        public void OnUnloaded()
        {
            CompanyCards?.Clear();
            Employees?.Clear();
            pagination = null;
            companyParameters = null;
        }

        private async void GetCompanies()
        {
            if (pagination is not null && companyParameters.PageNumber >= pagination.TotalPages)
                return;

            var response = await mixApi.GetCompaniesAsync(companyParameters);
            response.Headers.TryGetValues(PaginationMetadata.KEY, out IEnumerable<string> values);
            if (values is not null && values.Any())
            {
                pagination = values.First().DeserializeJson<PaginationMetadata>();
                companyParameters.PageNumber = pagination.CurrentPage + 1;
            }
            var result = await response.Content.ReadAsStringAsync();
            //var companies = result.DeserializeJson<IEnumerable<CompanyDto>>();
            var companies = JsonConvert.DeserializeObject<IEnumerable<CompanyDto>>(result);
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var item in companies)
                {
                    AddCompanyCard(item);
                }
            });
        }

        private async void GetEmployeesForCompany(CompanyDto obj)
        {
            Employees.Clear();

            var parameters = new EmployeeDtoParameters();
            var result = await mixApi.GetEmployeesForCompany(obj.Id, parameters);
            foreach (var item in result)
            {
                Employees.Add(item);
            }
        }

        private void AddCompanyCard(CompanyDto company)
        {
            var card = new Card();
            var sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            sp.Children.Add(new TextBlock { Text = company.Name });
            sp.Children.Add(new TextBlock { Text = company.Introduction });
            sp.Children.Add(CreatePackIconButton("员工信息", PackIconKind.People, GetEmployeesForCompanyCommand, company));

            card.Width = 180;
            card.Height = 200;
            card.Margin = new Thickness(10, 10, 10, 10);
            card.Content = sp;

            CompanyCards.Add(card);
        }

        private Button CreatePackIconButton(string content, PackIconKind icon, ICommand command, object parameter)
        {
            var btn = new Button();
            var pi = new PackIcon();
            pi.Kind = icon;
            pi.Width = 25;
            pi.Height = 25;
            btn.Content = pi;
            btn.ToolTip = content;
            btn.Command = command;
            btn.CommandParameter = parameter;
            btn.Style = ButtonResource["MaterialDesignToolButton"] as Style;
            btn.Foreground = Brushes.GreenYellow;
            btn.Margin = new Thickness(10, 0, 10, 0);

            return btn;
        }

        #endregion Methods
    }
}