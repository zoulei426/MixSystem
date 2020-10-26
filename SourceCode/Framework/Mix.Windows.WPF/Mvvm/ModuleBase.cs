using Prism.Ioc;
using Prism.Modularity;
using Unity;

namespace Mix.Windows.WPF.Mvvm
{
    public abstract class ModuleBase : IModule
    {
        protected IUnityContainer Container { get; }

        protected ModuleBase(IUnityContainer container) => Container = container;

        public virtual void RegisterTypes(IContainerRegistry containerRegistry) { }

        public virtual void OnInitialized(IContainerProvider containerProvider) { }
    }
}
