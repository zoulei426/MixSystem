using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Core.Log
{
    public class Logger : ILogger
    {
        public void Infomation(string message)
        {
            Serilog.Log.Information(message);
        }

        public void Warning(string message)
        {
            Serilog.Log.Warning(message);
        }

        public void Error(string message)
        {
            Serilog.Log.Error(message);
        }

        public void Error<T>(string message, T propertyValue)
        {
            Serilog.Log.Error(message, propertyValue);
        }
    }
}
