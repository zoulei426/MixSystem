using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Mix.Windows.WPF.Commands
{
    /// <summary>
    /// RelayCommandAsync
    /// </summary>
    /// <seealso cref="Mix.Windows.WPF.Commands.RelayCommandBase" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class RelayCommandAsync : RelayCommandBase, INotifyPropertyChanged
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;
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
        /// Initializes a new instance of the <see cref="RelayCommandAsync"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public RelayCommandAsync(Func<Task> execute, Func<bool> canExecute = null)
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
        protected override bool CanExecute(object parameter) => CanExecute() && !IsExecuting;

        /// <summary>
        /// Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected override async void Execute(object parameter) => await Execute();

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        public bool CanExecute() => _canExecute?.Invoke() ?? true;

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public async Task Execute()
        {
            IsExecuting = true;
            var invoke = _execute?.Invoke();
            if (invoke != null) await invoke;
            IsExecuting = false;
        }
    }
}