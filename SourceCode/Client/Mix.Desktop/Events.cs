using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Desktop
{
    internal class MainWindowLoadingEvent : PubSubEvent<bool> { }

    internal class SignUpSuccessEvent : PubSubEvent<SignUpArgs> { }
}