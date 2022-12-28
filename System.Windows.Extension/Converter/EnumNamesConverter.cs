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
    public class EnumNamesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum e)
            {
                var type = value.GetType();

                return Enum.GetNames(type).Select(q =>
                {
                    var description = type.GetField(q).GetCustomAttributes(true).Where(q1 => q1 is DescriptionAttribute).
                    Select(q1 => (DescriptionAttribute)q1).FirstOrDefault()?.Description;
                    return string.IsNullOrWhiteSpace(description) ? q : description;
                }).ToArray();
            }
            return new string[] { };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
