using HandyControl.Controls;

namespace Mix.Windows.Controls
{
    public static class Notify
    {
        public static void Info(string message)
        {
            Growl.InfoGlobal(message);
        }

        public static void Success(string message)
        {
            Growl.SuccessGlobal(message);
        }

        public static void Warning(string message)
        {
            Growl.WarningGlobal(message);
        }

        public static void Error(string message)
        {
            Growl.ErrorGlobal(message);
        }
    }
}
