using Prism.Logging;
using System;
using NLog;
using System.Threading;

namespace Mix.Windows.Core
{
    public class Logger : ILoggerFacade, IDisposable
    {
       

        public void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
                    break;
                case Category.Exception:
                    break;
                case Category.Info:
                    break;
                case Category.Warn:
                    break;
                default:
                    break;
            }
        }

        static readonly object locker = new object();
        NLog.Logger _logger;

        private Logger(NLog.Logger logger)
        {
            _logger = logger;
        }

        public Logger(string name) : this(LogManager.GetLogger(name))
        {

        }

        public static Logger Default { get; private set; }
        static Logger()
        {
            Default = new Logger(NLog.LogManager.GetCurrentClassLogger());
        }

        public static void WriteLog1(string name, string msg)
        {
            LogModels lm = new LogModels();
            lm.Name = name;
            lm.Msg = msg;

            ParameterizedThreadStart tStart = new ParameterizedThreadStart(ThreadWriteLog);
            Thread thread = new Thread(tStart);
            thread.Start(lm);//传递参数 
        }

        public static void ThreadWriteLog(object arg)
        {
            lock (locker)
            {
                LogModels lm = (LogModels)arg;
                LogManager.Configuration.Variables["LogDir"] = lm.Name;
                Logger logger = new Logger("ESDLog");
                logger.Info(lm.Msg);
            }
        }

        #region Debug
        public void Debug(string msg, params object[] args)
        {
            _logger.Debug(msg, args);
        }

        public void Debug(string msg, Exception err)
        {
            _logger.Debug(err, msg);
        }
        #endregion

        #region Info
        public void Info(string msg, params object[] args)
        {
            _logger.Info(msg, args);
        }

        public void Info(string msg, Exception err)
        {
            _logger.Info(err, msg);
        }
        #endregion
        #region Custom
        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class MyLogEventInfo : LogEventInfo
    {
        public MyLogEventInfo() { }
        public MyLogEventInfo(LogLevel level, string loggerName, string message) : base(level, loggerName, message)
        { }

        public override string ToString()
        {
            //Message format
            //Log Event: Logger='XXX' Level=Info Message='XXX' SequenceID=5
            return FormattedMessage;
        }
    }

    public class LogModels
    {
        public string Name { set; get; }
        public string Msg { set; get; }
    }
}
