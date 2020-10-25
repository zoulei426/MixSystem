using Mix.Core;
using Mix.Windows.Core;
using Prism.Ioc;
using System;

namespace Mix.Windows.WPF
{
    /// <summary>
    /// ViewModelResolver
    /// </summary>
    /// <seealso cref="Mix.Windows.WPF.IViewModelResolver" />
    public class ViewModelResolver : IViewModelResolver
    {
        private readonly Func<IContainerProvider> _containerFactory;
        private Action<object, object, IContainerProvider> _configureViewAndViewModel;
        private IContainerProvider _container;

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public IContainerProvider Container => _container ??= _containerFactory();

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelResolver"/> class.
        /// </summary>
        /// <param name="containerFactory">The container factory.</param>
        public ViewModelResolver(Func<IContainerProvider> containerFactory)
        {
            Guards.ThrowIfNull(containerFactory);

            _containerFactory = containerFactory;
        }

        /// <summary>
        /// Resolves the view model for view.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="viewModelType">Type of the view model.</param>
        /// <returns></returns>
        public object ResolveViewModelForView(object view, Type viewModelType)
        {
            var viewModel = Container.Resolve(viewModelType);
            _configureViewAndViewModel?.Invoke(view, viewModel, Container);

            return viewModel;
        }

        /// <summary>
        /// Ifs the inherits from.
        /// </summary>
        /// <typeparam name="TView">The type of the view.</typeparam>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public IViewModelResolver IfInheritsFrom<TView, TViewModel>(Action<TView, TViewModel, IContainerProvider> configuration)
        {
            var previousAction = _configureViewAndViewModel;
            _configureViewAndViewModel = (view, viewModel, container) =>
            {
                previousAction?.Invoke(view, viewModel, container);
                if (view is TView tView && viewModel is TViewModel tViewModel)
                {
                    configuration?.Invoke(tView, tViewModel, container);
                }
            };
            return this;
        }

        /// <summary>
        /// Ifs the inherits from.
        /// </summary>
        /// <typeparam name="TView">The type of the view.</typeparam>
        /// <param name="genericInterfaceType">Type of the generic interface.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public IViewModelResolver IfInheritsFrom<TView>(Type genericInterfaceType, Action<TView, object, IGenericInterface, IContainerProvider> configuration)
        {
            var previousAction = _configureViewAndViewModel;
            _configureViewAndViewModel = (view, viewModel, container) =>
            {
                previousAction?.Invoke(view, viewModel, container);
                var interfaceInstance = viewModel.AsGenericInterface(genericInterfaceType);
                if (view is TView tView && interfaceInstance != null)
                {
                    configuration?.Invoke(tView, viewModel, interfaceInstance, container);
                }
            };
            return this;
        }
    }
}