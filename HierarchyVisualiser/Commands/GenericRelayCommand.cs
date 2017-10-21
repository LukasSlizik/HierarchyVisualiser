using System;
using System.Windows.Input;

namespace HierarchyVisualiser.Commands
{
    /// <summary>
    /// Generic Implementation of Command.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;
        Func<T, bool> _canExecute = (p) => true;
        Action<T> _execute = (p) => { };

        public GenericRelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object p) => _canExecute((T)p);
        public void Execute(object p) => _execute((T)p);

    }
}
