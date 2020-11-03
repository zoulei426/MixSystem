using HandyControl.Controls;
using Mix.Core.Notify;
using System.Windows;

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
            Application.Current.Dispatcher.Invoke(() =>
            {
                Growl.InfoGlobal(message);
            });
        }

        /// <summary>
        /// Successes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Success(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Growl.SuccessGlobal(message);
            });
        }

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warning(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Growl.WarningGlobal(message);
            });
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Growl.ErrorGlobal(message);
            });
        }
    }
}