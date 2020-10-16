using Prism.Events;

namespace Mix.Desktop
{
    internal class MainWindowLoadingEvent : PubSubEvent<bool> { }

    internal class SignUpSuccessEvent : PubSubEvent<SignUpArgs> { }
}