using System;
using System.Windows.Input;

namespace HierarchyVisualiser.Commands
{
    /// <summary>
    /// Non-Generic Implementation of the Command.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private Action _execute = () => {};
        private Func<bool> _canExecute = () => true;

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object p)
        {
            return CanExecute();
        }

        public void Execute(object p)
        {
            Execute();
        }

        public bool CanExecute()
        {
            return _canExecute();
        }

        public void Execute()
        {
            _execute();
        }
    }
}
