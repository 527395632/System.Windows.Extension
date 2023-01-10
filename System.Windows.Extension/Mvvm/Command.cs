using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace System.Windows.Extension.Mvvm
{
    public sealed class Command : ICommand
    {
        private Delegate _execute;
        private Delegate _canExecute;

        public Command(Delegate execute, Delegate canExecute)
        {
            var executeArgs = execute.Method.GetParameters();
            var canExecuteArgs = canExecute?.Method.GetParameters();
            if (canExecuteArgs != null)
            {
                if (executeArgs.Length != canExecuteArgs.Length ||
                    !canExecute.Method.ReturnType.Equals(typeof(bool)))
                    throw new ArgumentException("Command错误! (Command的Execute和CanExecute的参数必须一致, 且CanExecute的返回值必须为bool类型)");
                for (int i = 0; i < executeArgs.Length; i++)
                {
                    if (!executeArgs[i].ParameterType.Equals(canExecuteArgs[i].ParameterType))
                        throw new ArgumentException("Command错误! (Command的Execute和CanExecute的参数必须一致, 且CanExecute的返回值必须为bool类型)");
                }
            }
            _execute = execute;
            _canExecute = canExecute;
        }

        public void RaiseCanExecuteChanged() => _canExecuteChanged.Invoke(this, EventArgs.Empty);

        #region ICommand
        private EventHandler _canExecuteChanged;
        event EventHandler ICommand.CanExecuteChanged
        {
            add => _canExecuteChanged += value;
            remove => _canExecuteChanged -= value;
        }

        void ICommand.Execute(object parameter)
        {
            if (_execute == null) return;
            if (parameter is Arguments arguments)
            {
                var args = _execute.Method.GetParameters();
                if (args.Length == arguments.Count)
                {
                    var values = new object[args.Length];
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = Convert.ChangeType(arguments[i], args[i].ParameterType);
                    }
                    _execute.DynamicInvoke(values);
                    return;
                }
            }
            if (_execute.Method.GetParameters().Length == 0)
            {
                _execute.DynamicInvoke();
            }
            else
            {
                _execute.DynamicInvoke(parameter);
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (_canExecute == null) return true;
            if (parameter is Arguments arguments)
            {
                var args = _canExecute.Method.GetParameters();
                if (args.Length == arguments.Count)
                {
                    var values = new object[args.Length];
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = Convert.ChangeType(arguments[i], args[i].ParameterType);
                    }
                    return (bool)_canExecute.DynamicInvoke(values);
                }
            }

            return _canExecute.Method.GetParameters().Length == 0 ?
                (bool)_canExecute.DynamicInvoke() :
                (bool)_canExecute.DynamicInvoke(parameter);
        }
        #endregion
    }
}
