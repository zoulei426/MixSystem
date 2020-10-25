namespace Mix.Windows.WPF
{
    /// <summary>
    /// INotificable
    /// </summary>
    public interface INotificable
    {
        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(string message);

        /// <summary>
        /// Errors this instance.
        /// </summary>
        void Error();

        /// <summary>
        /// Asks this instance.
        /// </summary>
        void Ask();
    }
}