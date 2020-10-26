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

        void Success(string message);

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