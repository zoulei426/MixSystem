namespace Mix.Windows.WPF
{
    public interface IViewLoadedAndUnloadedAware
    {
        void OnLoaded();

        void OnUnloaded();
    }

    public interface IViewLoadedAndUnloadedAware<in TView>
    {
        void OnLoaded(TView view);

        void OnUnloaded(TView view);
    }
}