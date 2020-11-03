using IdentityModel.Client;
using Mix.Desktop.Modules.Enterprise.Views;
using Mix.Windows.WPF;
using Mix.Windows.WPF.Mvvm;
using Prism.Ioc;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Unity;

namespace Mix.Desktop.Modules.Enterprise
{
    public class EnterpriseModule : ModuleBase
    {
        public EnterpriseModule(IUnityContainer container) : base(container)
        {
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register for container
            //containerRegistry.RegisterInstance(FileTransferService.GetDownloaderManager("net-disk"));

            // Register for region

            RegionManager.RegisterViewWithRegion(SystemRegionNames.MainTabRegion, typeof(EnterpriseComponent));
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