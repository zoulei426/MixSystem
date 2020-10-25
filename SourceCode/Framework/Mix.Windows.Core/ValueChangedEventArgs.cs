using System;

namespace Mix.Windows.Core
{
    /// <summary>
    /// ValueChangedEventArgs
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ValueChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the name of the key.
        /// </summary>
        /// <value>
        /// The name of the key.
        /// </value>
        public string KeyName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueChangedEventArgs"/> class.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        public ValueChangedEventArgs(string keyName) => KeyName = keyName;
    }
}