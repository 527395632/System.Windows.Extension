using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace System.Windows.Extension.Interactivity
{
    public class Command : CommandBase
    {
        Action _executeMethod;
        Func<bool> _canExecuteMethod;

        public Command(Action executeMethod)
            : this(executeMethod, () => true)
        {

        }

        public Command(Action executeMethod, Func<bool> canExecuteMethod)
            : base()
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException(nameof(executeMethod), "DelegatesCannotBeNull");

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public void Execute()
        {
            _executeMethod();
        }

        public bool CanExecute()
        {
            return _canExecuteMethod();
        }

        protected override void Execute(object parameter)
        {
            Execute();
        }

        protected override bool CanExecute(object parameter)
        {
            return CanExecute();
        }
    }

    public class Command<T> : CommandBase
    {
        readonly Action<T> _executeMethod;
        Func<T, bool> _canExecuteMethod;

        public Command(Action<T> executeMethod)
            : this(executeMethod, (o) => true)
        {
        }

        public Command(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
            : base()
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException(nameof(executeMethod), "DelegatesCannotBeNull");

            TypeInfo genericTypeInfo = typeof(T).GetTypeInfo();

            if (genericTypeInfo.IsValueType)
            {
                if ((!genericTypeInfo.IsGenericType) || (!typeof(Nullable<>).GetTypeInfo().IsAssignableFrom(genericTypeInfo.GetGenericTypeDefinition().GetTypeInfo())))
                {
                    throw new InvalidCastException("CommandInvalidGenericPayloadType");
                }
            }

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public void Execute(T parameter)
        {
            _executeMethod(parameter);
        }

        public bool CanExecute(T parameter)
        {
            return _canExecuteMethod(parameter);
        }

        protected override void Execute(object parameter)
        {
            Execute((T)parameter);
        }

        protected override bool CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }
    }
}
