using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace System.Windows.Extension.Interactivity
{
    public delegate void CommandExecuteHandler(object parameter);
    public delegate bool CommandCanExecuteHandler(object parameter);

    public class Command : ICommand
    {
        private SynchronizationContext _synchronizationContext;

        public Command()
        {
            _synchronizationContext = SynchronizationContext.Current;
        }

        public event CommandExecuteHandler OnCommandExecute;
        public event CommandCanExecuteHandler OnCommandCanExecute;

        public void RaiseCanExecuteChanged() => OnCanExecuteChanged();
        
        void ICommand.Execute(object parameter) => OnCommandExecute?.Invoke(parameter);
        bool ICommand.CanExecute(object parameter) => OnCommandCanExecute?.Invoke(parameter) ?? true;

        public virtual event EventHandler CanExecuteChanged;
        protected virtual void OnCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                if (_synchronizationContext != null && _synchronizationContext != SynchronizationContext.Current)
                    _synchronizationContext.Post((o) => handler.Invoke(this, EventArgs.Empty), null);
                else
                    handler.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
