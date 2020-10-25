namespace Mix.Windows.WPF
{
    /// <summary>
    /// IViewLoadedAndUnloadedAware
    /// </summary>
    public interface IViewLoadedAndUnloadedAware
    {
        /// <summary>
        /// Called when [loaded].
        /// </summary>
        void OnLoaded();

        /// <summary>
        /// Called when [unloaded].
        /// </summary>
        void OnUnloaded();
    }

    /// <summary>
    /// IViewLoadedAndUnloadedAware
    /// </summary>
    /// <typeparam name="TView">The type of the view.</typeparam>
    public interface IViewLoadedAndUnloadedAware<in TView>
    {
        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="view">The view.</param>
        void OnLoaded(TView view);

        /// <summary>
        /// Called when [unloaded].
        /// </summary>
        /// <param name="view">The view.</param>
        void OnUnloaded(TView view);
    }
}