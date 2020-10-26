using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;

namespace Mix.Windows.WPF.Mvvm
{
    /// <summary>
    /// 模块基类
    /// </summary>
    /// <seealso cref="Prism.Modularity.IModule" />
    public abstract class ModuleBase : IModule
    {
        private readonly IRegionManager _RegionManager;

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        protected IUnityContainer Container { get; }

        /// <summary>
        /// 区域管理器
        /// </summary>
        protected IRegionManager RegionManager => _RegionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleBase"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        protected ModuleBase(IUnityContainer container)
        {
            Container = container;
            _RegionManager = container.Resolve<IRegionManager>();
        }

        /// <summary>
        /// Used to register types with the container that will be used by your application.
        /// </summary>
        /// <param name="containerRegistry"></param>
        public virtual void RegisterTypes(IContainerRegistry containerRegistry) { }

        /// <summary>
        /// Notifies the module that it has been initialized.
        /// </summary>
        /// <param name="containerProvider"></param>
        public virtual void OnInitialized(IContainerProvider containerProvider) { }
    }
}