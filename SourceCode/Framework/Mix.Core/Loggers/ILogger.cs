namespace Mix.Core.Loggers
{
    /// <summary>
    /// ILogger
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error(string message);

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The message.</param>
        /// <param name="propertyValue">The property value.</param>
        void Error<T>(string message, T propertyValue);

        /// <summary>
        /// Infomations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Infomation(string message);

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warning(string message);
    }
}