using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace System.Windows.Extension.Converter
{
    public class EnumNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum e)
            {
                var type = value.GetType();
                var description = type.GetField(value?.ToString())?.GetCustomAttributes(true).Where(q1 => q1 is DescriptionAttribute).
                           Select(q1 => (DescriptionAttribute)q1).FirstOrDefault()?.Description;
                return string.IsNullOrWhiteSpace(description) ? value?.ToString() : description;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object retValue = null;
            try
            {
                retValue = Enum.Parse(targetType, value?.ToString());
            }
            catch
            { }

            if (retValue == null)
                retValue = Enum.Parse(targetType,
                    Enum.GetNames(targetType).FirstOrDefault(q => targetType.GetField(q).GetCustomAttributes(true).Any(q1 => q1 is DescriptionAttribute desc && (desc.Description?.Equals(value?.ToString()) ?? false))));
            return retValue;
        }
    }
}
