namespace Mix.Core.Notify
{
    /// <summary>
    /// 通知器
    /// </summary>
    public interface INotifier
    {
        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(string message);

        /// <summary>
        /// Successes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Success(string message);

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warning(string message);

        /// <summary>
        /// Errors this instance.
        /// </summary>
        void Error(string message);

        /// <summary>
        /// Asks this instance.
        /// </summary>
        //void Ask();
    }
}