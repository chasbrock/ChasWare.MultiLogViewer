using System;
using System.Windows.Input;

namespace ChasWare.MultiLogViewer.Common.Helpers
{
    /// <summary>
    ///     simple implementation of ICommand
    /// </summary>
    public class SimpleCommand : ICommand
    {
        #region Constants and fields 

        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SimpleCommand" /> class.
        ///     Creates instance of ICommand implementation
        /// </summary>
        /// <param name="execute">
        ///     delegate called when action is required
        /// </param>
        /// <param name="canExecute">
        ///     delegate should return true if execution is allowed.
        ///     Supplying null will cause can execute to always return true;
        /// </param>
        public SimpleCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _canExecute = canExecute ?? AlwaysAllowExecution;
            _execute = execute;
        }

        #endregion

        #region public events, delegates and enums

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        #endregion

        #region public methods

        /// <inheritdoc />
        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        ///     true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion

        #region other methods

        private static bool AlwaysAllowExecution(object parameter)
        {
            return true;
        }

        #endregion
    }
}