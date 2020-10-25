using HandyControl.Controls;

namespace Mix.Windows.Controls
{
    /// <summary>
    /// 通知类
    /// </summary>
    public static class Notify
    {
        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Info(string message)
        {
            Growl.InfoGlobal(message);
        }

        /// <summary>
        /// Successes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Success(string message)
        {
            Growl.SuccessGlobal(message);
        }

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Warning(string message)
        {
            Growl.WarningGlobal(message);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Error(string message)
        {
            Growl.ErrorGlobal(message);
        }
    }
}