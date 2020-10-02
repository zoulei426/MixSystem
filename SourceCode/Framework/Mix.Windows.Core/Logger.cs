using NLog;
using System;

namespace Mix.Windows.Core
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Logger : ILogger
    {
        private readonly NLog.Logger _Logger;

        public Logger()
        {
            if (_Logger == null)
            {
                _Logger = LogManager.GetCurrentClassLogger();
            }
        }

        public void Debug(string message)
        {
            _Logger.Debug(message);
        }

        public void Debug(string message, params object[] args)
        {
            _Logger.Debug(message, args);
        }

        public void Debug(string message, Exception exception)
        {
            _Logger.Debug(exception, message);
        }

        public void Info(string message)
        {
            _Logger.Info(message);
        }

        public void Info(string message, params object[] args)
        {
            _Logger.Info(message, args);
        }

        public void Info(string message, Exception exception)
        {
            _Logger.Info(exception, message);
        }

        public void Warn(string message)
        {
            _Logger.Warn(message);
        }

        public void Warn(string message, params object[] args)
        {
            _Logger.Warn(message, args);
        }

        public void Warn(string message, Exception exception)
        {
            _Logger.Warn(exception, message);
        }

        public void Error(string message)
        {
            _Logger.Error(message);
        }

        public void Error(string message, params object[] args)
        {
            _Logger.Error(message, args);
        }

        public void Error(string message, Exception exception)
        {
            _Logger.Error(exception, message);
        }

        public void Fatal(string message, Exception exception)
        {
            _Logger.Fatal(exception, message);
        }

        public void Trace(string message)
        {
            _Logger.Trace(message);
        }
    }
}