using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace System.Windows.Extension.Interactivity
{
    public abstract class CommandBase : ICommand
    {
        private SynchronizationContext _synchronizationContext;

        protected CommandBase()
        {
            _synchronizationContext = SynchronizationContext.Current;
        }

        protected abstract void Execute(object parameter);
        protected abstract bool CanExecute(object parameter);

        public void RaiseCanExecuteChanged() => OnCanExecuteChanged();
        void ICommand.Execute(object parameter) => Execute(parameter);
        bool ICommand.CanExecute(object parameter) => CanExecute(parameter);

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
