using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Extension.Attributes;

namespace System.Windows.Extension.Converter
{
    public class MultiValue2ModelConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrWhiteSpace(parameter?.ToString()))
            {
                var t = Type.GetType(parameter?.ToString());
                if (t != null)
                {
                    object value = null;
                    try
                    {
                        value = Activator.CreateInstance(t);
                    }
                    catch
                    { }
                    if (value != null)
                    {
                        var props = t.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).
                            Where(q => q.GetCustomAttributes(true).Any(q1 => q1 is IndexAttribute)).
                            Select(q => new
                            {
                                Property = q,
                                Index = (IndexAttribute)q.GetCustomAttributes(true).FirstOrDefault(q1 => q1 is IndexAttribute)
                            }).OrderBy(q => q.Index.Value).Select(q => q.Property).ToArray();

                        if (props.Length == values.Length)
                        {
                            try
                            {
                                for (int i = 0; i < props.Length; i++)
                                {
                                    props[i].SetValue(value, System.Convert.ChangeType(values[i], props[i].PropertyType));
                                }
                                return value;
                            }
                            catch (Exception e)
                            {
                            }
                        }
                    }
                }
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var props = value.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).
                    Where(q => q.GetCustomAttributes(true).Any(q1 => q1 is IndexAttribute)).
                    Select(q => new
                    {
                        Property = q,
                        Index = (IndexAttribute)q.GetCustomAttributes(true).FirstOrDefault(q1 => q1 is IndexAttribute)
                    }).OrderBy(q => q.Index.Value).Select(q => q.Property).ToArray();
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(value);
                }
                return values;
            }
            return new object[] { };
        }
    }
}
