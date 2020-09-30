using Prism.Logging;
using System;
using NLog;
using System.Threading;

namespace Mix.Windows.Core
{
    public class Logger : ILogger
    {
       
        readonly NLog.Logger _Logger;

        private Logger(NLog.Logger logger)
        {
            _Logger = logger;
        }

        public Logger(string name) : this(LogManager.GetLogger(name))
        {

        }

        public static Logger Default { get; private set; }
        static Logger()
        {
            Default = new Logger(NLog.LogManager.GetCurrentClassLogger());
        }

        public void Debug(string message)
        {
            _Logger.Debug(message);
        }

        public void Info(string message)
        {
            _Logger.Info(message);
        }

        public void Warn(string message)
        {
            _Logger.Warn(message);
        }

        public void Trace(string message)
        {
            _Logger.Trace(message);
        }

        public void Error(string message, Exception exception)
        {
            _Logger.Error(message, exception);
        }

        public void Fatal(string message, Exception exception)
        {
            _Logger.Fatal(message, exception);
        }
    }

}
