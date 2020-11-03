using Serilog;

namespace Mix.Core.Loggers
{
    /// <summary>
    /// Logger
    /// </summary>
    /// <seealso cref="Mix.Core.Loggers.ILogger" />
    public class Logger : ILogger
    {
        /// <summary>
        /// Infomations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Infomation(string message)
        {
            Log.Information(message);
        }

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warning(string message)
        {
            Log.Warning(message);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(string message)
        {
            Log.Error(message);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The message.</param>
        /// <param name="propertyValue">The property value.</param>
        public void Error<T>(string message, T propertyValue)
        {
            Log.Error(message, propertyValue);
        }
    }
}