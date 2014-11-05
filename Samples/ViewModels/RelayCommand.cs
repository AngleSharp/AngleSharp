namespace Samples.ViewModels
{
    using System;
    using System.Windows.Input;

    sealed class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        readonly Action<Object> _action;

        public RelayCommand(Action action)
            : this(_ => action())
        {
        }

        public RelayCommand(Action<Object> action)
        {
            _action = action;
        }

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public void Execute(Object parameter)
        {
            _action(parameter);
        }
    }
}
