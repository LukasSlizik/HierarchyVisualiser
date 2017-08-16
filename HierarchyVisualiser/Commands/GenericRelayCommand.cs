using System;
using System.Windows.Input;

namespace HierarchyVisualiser.Commands
{
    public class GenericRelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;
        Action<T> _execute;

        public GenericRelayCommand(Action<T> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
