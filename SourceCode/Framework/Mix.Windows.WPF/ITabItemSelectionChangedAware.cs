namespace Mix.Windows.WPF
{
    public interface ITabItemSelectionChangedAware
    {
        void OnSelected();

        void OnUnselected();
    }
}