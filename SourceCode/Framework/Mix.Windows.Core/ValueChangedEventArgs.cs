using System;

namespace Mix.Windows.Core
{
    public class ValueChangedEventArgs : EventArgs
    {
        public string KeyName { get; }

        public ValueChangedEventArgs(string keyName) => KeyName = keyName;
    }
}