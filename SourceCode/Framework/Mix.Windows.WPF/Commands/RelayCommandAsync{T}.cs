using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Mix.Windows.WPF.Commands
{
    /// <summary>
    /// RelayCommandAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Mix.Windows.WPF.Commands.RelayCommandBase" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class RelayCommandAsync<T> : RelayCommandBase, INotifyPropertyChanged
    {
        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool> _canExecute;
        private bool _isExecuting;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets a value indicating whether this instance is executing.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is executing; otherwise, <c>false</c>.
        /// </value>
        public bool IsExecuting
        {
            get => _isExecuting;
            private set
            {
                if (_isExecuting == value) return;
                _isExecuting = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsExecuting)));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommandAsync{T}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public RelayCommandAsync(Func<T, Task> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///   <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        protected override bool CanExecute(object parameter) => CanExecute((T)parameter) && !IsExecuting;

        /// <summary>
        /// Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected override async void Execute(object parameter) => await Execute((T)parameter);

        /// <summary>
        /// Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///   <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanExecute(T parameter) => _canExecute?.Invoke(parameter) ?? true;

        /// <summary>
        /// Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public async Task Execute(T parameter)
        {
            IsExecuting = true;
            var invoke = _execute?.Invoke(parameter);
            if (invoke != null) await invoke;
            IsExecuting = false;
        }
    }
}