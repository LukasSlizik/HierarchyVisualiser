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

        public bool CanExecute(object p) => CanExecute();
        public bool CanExecute() => _canExecute();

        public void Execute(object p) => Execute();
        public void Execute() => _execute();
    }
}
