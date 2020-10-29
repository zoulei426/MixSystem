using MaterialDesignThemes.Wpf;
using Mix.Data.Pagable;
using Mix.Library.Entities.DtoParameters;
using Mix.Library.Entities.Dtos;
using Mix.Windows.WPF;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Ioc;
using PropertyChanged;
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
    [AddINotifyPropertyChangedInterface]
    public class CompaniesPanelViewModel : EnterpriseViewModel, IViewLoadedAndUnloadedAware
    {
        #region Properties

        public IList<Card> CompanyCards { get; set; }

        #endregion Properties

        #region Fields

        private ResourceDictionary ButtonResource = new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Button.xaml") };

        private PaginationMetadata pagination;
        private CompanyDtoParameters companyParameters;

        #endregion Fields

        #region Commands

        public ICommand GetCompaniesCommand { get; private set; }

        public ICommand GetEmployeesForCompanyCommand { get; private set; }

        #endregion Commands

        #region Ctor

        public CompaniesPanelViewModel(IContainerExtension container) : base(container)
        {
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
            CompanyCards = new ObservableCollection<Card>();
            companyParameters = new CompanyDtoParameters();

            await Task.Run(() => GetCompanies());
        }

        public void OnUnloaded()
        {
            CompanyCards?.Clear();
            pagination = null;
            companyParameters = null;
        }

        private async void GetCompanies()
        {
            if (companyParameters is null)
                return;

            if (pagination is not null && companyParameters.PageNumber >= pagination.TotalPages)
                return;

            var response = await EnterpriseApi.GetCompaniesAsync(companyParameters);
            response.Headers.TryGetValues(PaginationMetadata.KEY, out IEnumerable<string> values);
            if (values is not null && values.Any())
            {
                pagination = values.First().DeserializeJson<PaginationMetadata>();
                companyParameters.PageNumber = pagination.CurrentPage + 1;
            }
            var result = await response.Content.ReadAsStringAsync();
            //var companies = result.DeserializeJson<IEnumerable<CompanyDto>>();
            var companies = JsonConvert.DeserializeObject<IEnumerable<CompanyDto>>(result);

            //System.Threading.Thread.Sleep(1000);

            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var item in companies)
                {
                    AddCompanyCard(item);
                }
            });
        }

        private void GetEmployeesForCompany(CompanyDto obj)
        {
            EventAggregator.GetEvent<GetEmployeesForCompanyEvent>().Publish(obj);
        }

        private void AddCompanyCard(CompanyDto company)
        {
            var card = new Card();
            card.Width = 200;
            card.Height = 400;
            card.Margin = new Thickness(10, 10, 10, 10);

            var grid = new Grid();
            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            //row1.Height = new GridLength(100);

            grid.RowDefinitions.Add(row1);
            grid.RowDefinitions.Add(row2);
            grid.RowDefinitions.Add(row3);

            var colorZone = new ColorZone
            {
                Content = new PackIcon() { Kind = PackIconKind.People, Height = 100, Width = 100 },
                Width = 200,
                Height = 180,
                Mode = ColorZoneMode.PrimaryLight,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            grid.Children.Add(colorZone);
            Grid.SetRow(colorZone, 0);

            var btn = CreatePackIconButton("员工信息", "MaterialDesignFloatingActionMiniAccentButton",
                PackIconKind.People, GetEmployeesForCompanyCommand, company);
            btn.Margin = new Thickness(0, 0, 16, -20);
            btn.HorizontalAlignment = HorizontalAlignment.Right;
            btn.VerticalAlignment = VerticalAlignment.Bottom;
            grid.Children.Add(btn);
            Grid.SetRow(btn, 0);

            var sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            sp.Children.Add(new TextBlock { Text = company.Name });
            sp.Children.Add(new TextBlock { Text = company.Introduction });
            grid.Children.Add(sp);
            Grid.SetRow(sp, 1);

            var spBottom = new StackPanel();
            spBottom.Orientation = Orientation.Horizontal;
            spBottom.HorizontalAlignment = HorizontalAlignment.Right;
            spBottom.Margin = new Thickness(8, 8, 8, 8);
            spBottom.Children.Add(CreatePackIconButton("分享", "MaterialDesignToolButton",
                PackIconKind.ShareVariant, null, null));
            spBottom.Children.Add(CreatePackIconButton("关注", "MaterialDesignToolButton",
                PackIconKind.Heart, null, null));
            grid.Children.Add(spBottom);
            Grid.SetRow(spBottom, 2);

            card.Content = grid;

            CompanyCards.Add(card);
        }

        private Button CreatePackIconButton(string content,
            string style, PackIconKind icon,
            ICommand command, object parameter)
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
            btn.Style = ButtonResource[style] as Style;
            btn.Foreground = Brushes.GreenYellow;
            btn.Margin = new Thickness(10, 0, 10, 0);

            return btn;
        }

        #endregion Methods
    }
}