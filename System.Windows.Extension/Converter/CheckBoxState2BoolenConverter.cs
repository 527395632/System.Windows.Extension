using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Extension.Data;

namespace System.Windows.Extension.Converter
{
    public class CheckBoxState2BoolenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CheckBoxState state)
            {
                switch (state)
                {
                    case CheckBoxState.Unchecked: return false;
                    case CheckBoxState.Checked: return true;
                    case CheckBoxState.Partial: return null;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return CheckBoxState.Partial;
            if (value is bool v)
            {
                return v ? CheckBoxState.Checked : CheckBoxState.Unchecked;
            }
            return CheckBoxState.Unchecked;
        }
    }
}
