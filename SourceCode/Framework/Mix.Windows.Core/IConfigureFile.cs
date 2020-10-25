using System;

namespace Mix.Windows.Core
{
    /// <summary>
    /// IConfigureFile
    /// </summary>
    public interface IConfigureFile
    {
        /// <summary>
        /// Occurs when [value changed].
        /// </summary>
        event EventHandler<ValueChangedEventArgs> ValueChanged;

        /// <summary>
        /// Determines whether this instance contains the object.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        bool Contains(string key);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T GetValue<T>(string key);

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        IConfigureFile SetValue<T>(string key, T value);

        /// <summary>
        /// Loads the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        IConfigureFile Load(string filePath = null);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns></returns>
        IConfigureFile Clear();

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        void Delete();
    }
}