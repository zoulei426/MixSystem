using Mix.Desktop.Modules.Index.Views;
using Mix.Windows.WPF;
using Mix.Windows.WPF.Mvvm;
using Prism.Ioc;
using Refit;
using Unity;

namespace Mix.Desktop.Modules.Index
{
    // TODO 增加加载顺序
    public class IndexModule : ModuleBase
    {
        public IndexModule(IUnityContainer container) : base(container)
        {
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register for region
            containerRegistry.RegisterInstance(RestService.For<IMixApi>("https://localhost:5002"));
            RegionManager.RegisterViewWithRegion(SystemRegionNames.MainTabRegion, typeof(IndexComponent));
        }

        public override void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}