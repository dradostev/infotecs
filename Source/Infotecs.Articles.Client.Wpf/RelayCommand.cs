namespace Infotecs.Articles.Client.Wpf
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Universal relay command.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> canExecute;
        private readonly Action<object> execute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="canExecute">Predicate determines if command can be executed.</param>
        /// <param name="execute">Action to execute.</param>
        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }
        
        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested += value;
        }

        /// <inheritdoc/>
        public bool CanExecute(object parameter) => this.canExecute(parameter);

        /// <inheritdoc/>
        public void Execute(object parameter) => this.execute(parameter);
    }
}
