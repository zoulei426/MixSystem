using Mix.Desktop.Modules.Enterprise.Views;
using Mix.Windows.WPF.Mvvm;
using Prism.Ioc;
using Prism.Events;
using Unity;
using Prism.Regions;
using Prism.Unity;
using Mix.Windows.WPF;
using Mix.Desktop.Modules.Enterprise.ViewModels;
using Refit;
using Prism.Modularity;

namespace Mix.Desktop.Modules.Enterprise
{
    //[Module(ModuleName = "EnterpriseModule", OnDemand = true)]
    public class EnterpriseModule : ModuleBase
    {
        private readonly IRegionManager _regionManager;

        public EnterpriseModule(IUnityContainer container, IRegionManager regionManager) : base(container)
        {
            _regionManager = regionManager;
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register for container
            //containerRegistry.RegisterInstance(FileTransferService.GetDownloaderManager("net-disk"));

            // Register for region
            containerRegistry.RegisterInstance(RestService.For<IMixApi>("https://localhost:5002"));
            _regionManager.RegisterViewWithRegion(SystemRegionNames.MainTabRegion, typeof(EnterpriseComponent));
        }

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            //containerProvider
            //    .Resolve<IEventAggregator>()
            //    .GetEvent<ApplicationExiting>()
            //    .Subscribe(
            //        () =>
            //        {
            //            if (containerProvider.GetContainer().IsRegistered<IAcceleriderUser>())
            //            {
            //                containerProvider.Resolve<IAcceleriderUser>().SaveToLocalDisk();
            //            }
            //        },
            //        true);
        }
    }
}