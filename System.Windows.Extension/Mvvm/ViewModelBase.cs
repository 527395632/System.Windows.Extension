using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Extension.Interactivity;
using System.Windows.Extension.Tools;

namespace System.Windows.Extension.Mvvm
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Constructors
        public ViewModelBase()
        {
            var t = this.GetType();
            var methods = t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            foreach (var item in t.GetProperties())
            {
                CmdBindingAttribute attribute = null;
                if (item.PropertyType.Equals(typeof(Command)) &&
                    !item.PropertyType.IsAbstract &&
                    (attribute = item.GetCustomAttribute<CmdBindingAttribute>(true)) != null &&
                    !string.IsNullOrWhiteSpace(attribute.Execute))
                {
                    var execute = methods.FirstOrDefault(q => q.Name.Equals(attribute.Execute))?.CreateDelegate(this);
                    var canExecute = methods.FirstOrDefault(q => q.Name.Equals(attribute.CanExecute))?.CreateDelegate(this);
                    if (execute != null)
                    {
                        var cmd = (Command)Activator.CreateInstance(item.PropertyType, new object[] { execute, canExecute });
                        item.SetValue(this, cmd);
                    }
                }
                else if (typeof(IList).IsAssignableFrom(item.PropertyType) && !item.PropertyType.IsAbstract)
                {
                    item.SetValue(this, Activator.CreateInstance(item.PropertyType));
                }
            }
        }
        #endregion

        #region INotifyPropertyChanged
        private PropertyChangedEventHandler _notify;
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => _notify += value;
            remove => _notify -= value;
        }
        #endregion

        #region Get & Set
        private Dictionary<string, object> _values = new Dictionary<string, object>();
        protected T Get<T>(string propertyName)
        {
            if (_values.ContainsKey(propertyName) &&
                _values[propertyName] is T value)
            {
                return value;
            }
            return default;
        }

        protected void Set(string propertyName, object value)
        {
            if (!_values.ContainsKey(propertyName) ||
                _values[propertyName] == null || !_values[propertyName].Equals(value))
            {
                _values[propertyName] = value;
                _notify?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}