using Mix.Windows.WPF;
using Prism.Commands;
using Prism.Ioc;
using PropertyChanged;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace Mix.Desktop
{
    public class MainWindowViewModel : ViewModelBase, IViewLoadedAndUnloadedAware
    {
        #region Properties

        public bool AppStoreIsDisplayed
        {
            get { return _AppStoreIsDisplayed; }
            set
            {
                if (_AppStoreIsDisplayed) return;
                if (!SetProperty(ref _AppStoreIsDisplayed, value)) return;

                if (!RegionManager.Regions.ContainsRegionWithName(SystemRegionNames.MainTabRegion)) return;

                var region = RegionManager.Regions[SystemRegionNames.MainTabRegion];
                foreach (var activeView in region.ActiveViews)
                {
                    region.Deactivate(activeView);
                }
            }
        }

        private bool _AppStoreIsDisplayed;

        #endregion Properties

        #region Fileds

        #endregion Fileds

        #region Commands

        public ICommand LogoutCommand { get; set; }

        #endregion Commands

        #region Ctor

        public MainWindowViewModel(IContainerExtension container) : base(container)
        {
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

        public void OnLoaded()
        {
            if (!RegionManager.Regions.ContainsRegionWithName(SystemRegionNames.MainTabRegion)) return;

            var region = RegionManager.Regions[SystemRegionNames.MainTabRegion];
            region.ActiveViews.CollectionChanged += OnActiveViewsChanged;
            if (!region.Views.Any()) AppStoreIsDisplayed = true;
        }

        public void OnUnloaded()
        {
        }

        //[SuppressPropertyChangedWarnings]
        private void OnActiveViewsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add) return;

            _AppStoreIsDisplayed = false;
            RaisePropertyChanged(nameof(AppStoreIsDisplayed));
        }

        #endregion Methods
    }
}