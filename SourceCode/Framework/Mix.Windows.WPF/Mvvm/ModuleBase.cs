using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;

namespace Mix.Windows.WPF.Mvvm
{
    public abstract class ModuleBase : IModule
    {

        private readonly IRegionManager _RegionManager;

        protected IUnityContainer Container { get; }

        /// <summary>
        /// 区域管理器
        /// </summary>
        protected IRegionManager RegionManager => _RegionManager;


        protected ModuleBase(IUnityContainer container)
        {
            Container = container;
            _RegionManager = container.Resolve<IRegionManager>();
        }

        public virtual void RegisterTypes(IContainerRegistry containerRegistry) { }

        public virtual void OnInitialized(IContainerProvider containerProvider) { }
    }
}
