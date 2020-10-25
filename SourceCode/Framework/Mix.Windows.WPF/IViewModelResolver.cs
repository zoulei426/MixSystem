using Mix.Windows.Core;
using Prism.Ioc;
using System;

namespace Mix.Windows.WPF
{
    /// <summary>
    /// IViewModelResolver
    /// </summary>
    public interface IViewModelResolver
    {
        /// <summary>
        /// Resolves the view model for view.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="viewModelType">Type of the view model.</param>
        /// <returns></returns>
        object ResolveViewModelForView(object view, Type viewModelType);

        /// <summary>
        /// Ifs the inherits from.
        /// </summary>
        /// <typeparam name="TView">The type of the view.</typeparam>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        IViewModelResolver IfInheritsFrom<TView, TViewModel>(Action<TView, TViewModel, IContainerProvider> configuration);

        /// <summary>
        /// Ifs the inherits from.
        /// </summary>
        /// <typeparam name="TView">The type of the view.</typeparam>
        /// <param name="genericInterfaceType">Type of the generic interface.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        IViewModelResolver IfInheritsFrom<TView>(Type genericInterfaceType, Action<TView, object, IGenericInterface, IContainerProvider> configuration);
    }
}