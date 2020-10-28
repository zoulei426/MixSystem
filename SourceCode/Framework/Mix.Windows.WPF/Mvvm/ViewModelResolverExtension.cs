using Mix.Core;
using Mix.Windows.Core;
using Prism.Ioc;
using System;
using System.Linq;
using System.Windows;

namespace Mix.Windows.WPF
{
    /// <summary>
    /// ViewModelResolverExtension
    /// </summary>
    public static class ViewModelResolverExtension
    {
        /// <summary>
        /// Uses the default configure.
        /// </summary>
        /// <param name="this">The this.</param>
        /// <returns></returns>
        public static IViewModelResolver UseDefaultConfigure(this IViewModelResolver @this) => @this
            .IfInheritsFrom<ViewModelBase>((view, viewModel) =>
            {
                viewModel.Dispatcher = view.Dispatcher;
            })
            .IfInheritsFrom<IViewLoadedAndUnloadedAware>((view, viewModel) =>
            {
                view.Loaded += (sender, e) => viewModel.OnLoaded();
                view.Unloaded += (sender, e) => viewModel.OnUnloaded();
            })
            .IfInheritsFrom(typeof(IViewLoadedAndUnloadedAware<>), (view, viewModel, interfaceInstance) =>
            {
                var viewType = view.GetType();
                if (interfaceInstance.GenericArguments.Single() != viewType)
                {
                    throw new InvalidOperationException();
                }

                var onLoadedMethod = interfaceInstance.GetMethod<Action<object>>("OnLoaded", viewType);
                var onUnloadedMethod = interfaceInstance.GetMethod<Action<object>>("OnUnloaded", viewType);

                view.Loaded += (sender, args) => onLoadedMethod(sender);
                view.Unloaded += (sender, args) => onUnloadedMethod(sender);
            });

        /// <summary>
        /// Ifs the inherits from.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="this">The this.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IViewModelResolver IfInheritsFrom<TViewModel>(this IViewModelResolver @this, Action<FrameworkElement, TViewModel> configuration)
        {
            Guards.ThrowIfNull(@this, configuration);

            return @this.IfInheritsFrom<FrameworkElement, TViewModel>((view, viewModel, container) => configuration(view, viewModel));
        }

        /// <summary>
        /// Ifs the inherits from.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="this">The this.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IViewModelResolver IfInheritsFrom<TViewModel>(this IViewModelResolver @this, Action<FrameworkElement, TViewModel, IContainerProvider> configuration)
        {
            Guards.ThrowIfNull(@this, configuration);

            return @this.IfInheritsFrom(configuration);
        }

        /// <summary>
        /// Ifs the inherits from.
        /// </summary>
        /// <param name="this">The this.</param>
        /// <param name="genericInterfaceType">Type of the generic interface.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IViewModelResolver IfInheritsFrom(this IViewModelResolver @this, Type genericInterfaceType, Action<FrameworkElement, object, IGenericInterface> configuration)
        {
            Guards.ThrowIfNull(@this, configuration);

            return @this.IfInheritsFrom<FrameworkElement>(
                genericInterfaceType,
                (view, viewModel, interfaceInstance, container) => configuration(view, viewModel, interfaceInstance));
        }
    }
}