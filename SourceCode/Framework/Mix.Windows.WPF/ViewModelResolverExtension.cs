using Mix.Core;
using Mix.Windows.Core;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mix.Windows.WPF
{
    public static class ViewModelResolverExtension
    {
        public static IViewModelResolver UseDefaultConfigure(this IViewModelResolver @this) => @this
            .IfInheritsFrom<ViewModelBase>((view, viewModel) =>
            {
                viewModel.Dispatcher = view.Dispatcher;
            })
            //.IfInheritsFrom<ILocalizable>((view, viewModel) =>
            //{
            //    viewModel.I18nManager = I18nManager.Instance;
            //    view.Loaded += (sender, args) => I18nManager.Instance.CurrentUICultureChanged += viewModel.OnCurrentUICultureChanged;
            //    view.Unloaded += (sender, args) => I18nManager.Instance.CurrentUICultureChanged -= viewModel.OnCurrentUICultureChanged;
            //})
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
            //.IfInheritsFrom<INotificable>((view, viewModel, container) =>
            //{
            //    viewModel.GlobalMessageQueue = container.Resolve<ISnackbarMessageQueue>();
            //});
            //.IfInheritsFrom<IAwareTabItemSelectionChanged>((view, viewModel) =>
            //{
            //    TabControlHelper.SetAwareSelectionChanged(view, true);
            //});

        public static IViewModelResolver IfInheritsFrom<TViewModel>(this IViewModelResolver @this, Action<FrameworkElement, TViewModel> configuration)
        {
            Guards.ThrowIfNull(@this, configuration);

            return @this.IfInheritsFrom<FrameworkElement, TViewModel>((view, viewModel, container) => configuration(view, viewModel));
        }

        public static IViewModelResolver IfInheritsFrom<TViewModel>(this IViewModelResolver @this, Action<FrameworkElement, TViewModel, IContainerProvider> configuration)
        {
            Guards.ThrowIfNull(@this, configuration);

            return @this.IfInheritsFrom(configuration);
        }

        public static IViewModelResolver IfInheritsFrom(this IViewModelResolver @this, Type genericInterfaceType, Action<FrameworkElement, object, IGenericInterface> configuration)
        {
            Guards.ThrowIfNull(@this, configuration);

            return @this.IfInheritsFrom<FrameworkElement>(
                genericInterfaceType,
                (view, viewModel, interfaceInstance, container) => configuration(view, viewModel, interfaceInstance));
        }
    }
}
