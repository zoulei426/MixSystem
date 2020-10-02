namespace Mix.Windows.WPF
{
    public interface INotificable
    {
        void Info(string message);

        void Error();

        void Ask();
    }
}