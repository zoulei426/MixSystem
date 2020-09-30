﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Windows.Core
{
    public interface ILogger
    {
        void Debug(string message);
        
        void Info(string message);

        void Warn(string message);

        void Trace(string message);

        void Error(string message, Exception exception);

        void Fatal(string message, Exception exception);
    }
}
