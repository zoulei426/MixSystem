using Mix.Windows.Core;
using Prism.Ioc;
using System;

namespace Mix.Windows.WPF
{
    public interface IViewModelResolver
    {
        object ResolveViewModelForView(object view, Type viewModelType);

        IViewModelResolver IfInheritsFrom<TView, TViewModel>(Action<TView, TViewModel, IContainerProvider> configuration);

        IViewModelResolver IfInheritsFrom<TView>(Type genericInterfaceType, Action<TView, object, IGenericInterface, IContainerProvider> configuration);
    }
}
