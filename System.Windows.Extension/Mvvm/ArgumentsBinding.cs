using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace System.Windows.Extension.Mvvm
{
    public class ArgumentsBinding : MultiBinding, IMultiValueConverter
    {
        public ArgumentsBinding()
        {
            base.Converter = this;
        }

        #region Obsolete
        [Obsolete]
        public new IMultiValueConverter Converter
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        [Obsolete]
        public new CultureInfo ConverterCulture
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        [Obsolete]
        public new object ConverterParameter
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }
        #endregion



        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new Arguments(values);
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is Arguments arguments)
            {
                var values = new object[arguments.Count];
                for (int i = 0; i < arguments.Count; i++)
                {
                    values[i] = arguments[i];
                }
                return values;
            }
            return new object[] { };
        }
    }
}
