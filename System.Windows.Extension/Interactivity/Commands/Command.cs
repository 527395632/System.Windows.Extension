using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace System.Windows.Extension.Interactivity
{
    public class Command : ICommand
    {
        private Action _action;
        public Command(Action action)
        {
            _action = action;
        }

        private EventHandler _canExecuteChanged;
        event EventHandler ICommand.CanExecuteChanged
        {
            add => _canExecuteChanged += value;
            remove => _canExecuteChanged -= value;
        }

        public virtual bool CanExecute(object parameter) => true;

        void ICommand.Execute(object parameter)
        {
            _action?.Invoke();
        }
    }

    public class Command<T> : ICommand
    {
        private Action<T> _action;
        public Command(Action<T> action)
        {
            _action = action;
        }

        private EventHandler _canExecuteChanged;
        event EventHandler ICommand.CanExecuteChanged
        {
            add => _canExecuteChanged += value;
            remove => _canExecuteChanged -= value;
        }

        public virtual bool CanExecute(object parameter) => true;

        void ICommand.Execute(object parameter)
        {
            _action?.Invoke((T)parameter);
        }
    }
}
