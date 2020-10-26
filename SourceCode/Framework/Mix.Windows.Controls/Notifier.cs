using HandyControl.Controls;
using Mix.Core.Notify;

namespace Mix.Windows.Controls
{
    /// <summary>
    /// 通知器
    /// </summary>
    public class Notifier : INotifier
    {
        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(string message)
        {
            Growl.InfoGlobal(message);
        }

        /// <summary>
        /// Successes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Success(string message)
        {
            Growl.SuccessGlobal(message);
        }

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warning(string message)
        {
            Growl.WarningGlobal(message);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(string message)
        {
            Growl.ErrorGlobal(message);
        }
    }
}