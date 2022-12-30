using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Extension.Attributes;

namespace System.Windows.Extension.Tools
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
            return new List<object>(values);
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is IList list)
            {
                var values = new object[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    values[i] = list[i];
                }
                return values;
            }
            return new object[] { };
        }


        //public Type ModelType { get; set; }

        //object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        //{
        //    if (ModelType != null)
        //    {
        //        object value = null;
        //        try
        //        {
        //            value = Activator.CreateInstance(ModelType);
        //        }
        //        catch (Exception e)
        //        {
        //            throw new Exception($"Model[{ModelType.FullName}]实例化失败!", e);
        //        }
        //        if (value != null)
        //        {
        //            var props = ModelType.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).
        //                Where(q => q.GetCustomAttributes(true).Any(q1 => q1 is IndexAttribute)).
        //                Select(q => new
        //                {
        //                    Property = q,
        //                    Index = (IndexAttribute)q.GetCustomAttributes(true).FirstOrDefault(q1 => q1 is IndexAttribute)
        //                }).OrderBy(q => q.Index.Value).Select(q => q.Property).ToArray();

        //            if (props.Length == values.Length)
        //            {
        //                for (int i = 0; i < props.Length; i++)
        //                {
        //                    object v = null;
        //                    try
        //                    {
        //                        v = Convert.ChangeType(values[i], props[i].PropertyType);
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        throw new Exception($"数据[{values[i]}]转换到 Model[{ModelType.FullName}].[{props[i].Name}({props[i].PropertyType.FullName})]失败!", e);
        //                    }
        //                    props[i].SetValue(value, v);
        //                }
        //                return value;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return new List<object>(values);
        //    }
        //    return null;

        //}

        //object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        //{
        //    if (value != null)
        //    {
        //        var props = value.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).
        //            Where(q => q.GetCustomAttributes(true).Any(q1 => q1 is IndexAttribute)).
        //            Select(q => new
        //            {
        //                Property = q,
        //                Index = (IndexAttribute)q.GetCustomAttributes(true).FirstOrDefault(q1 => q1 is IndexAttribute)
        //            }).OrderBy(q => q.Index.Value).Select(q => q.Property).ToArray();
        //        var values = new object[props.Length];
        //        for (int i = 0; i < props.Length; i++)
        //        {
        //            values[i] = props[i].GetValue(value);
        //        }
        //        return values;
        //    }
        //    return new object[] { };
        //}
    }
}
