using System;
using System.Collections.Generic;
using System.Windows.Extension.Collections;
using System.Windows.Extension.Data;

namespace System.Windows.Extension.Tools
{
    public class ComparerGenerator
    {
        private static readonly Dictionary<Type, ComparerTypeCode> TypeCodeDic = new()
        {
            [typeof(DateTimeRange)] = ComparerTypeCode.DateTimeRange,
        };

        public static IComparer<T> GetComparer<T>()
        {
            if (TypeCodeDic.TryGetValue(typeof(T), out var comparerType))
            {
                if (comparerType == ComparerTypeCode.DateTimeRange)
                {
                    return (IComparer<T>)new DateTimeRangeComparer();
                }

                return null;
            }

            return null;
        }

        private enum ComparerTypeCode
        {
            DateTimeRange
        }
    }
}