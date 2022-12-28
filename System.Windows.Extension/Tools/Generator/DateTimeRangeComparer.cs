using System.Collections.Generic;
using System.Windows.Extension.Data;

namespace System.Windows.Extension.Tools
{
    public class DateTimeRangeComparer : IComparer<DateTimeRange>
    {
        public int Compare(DateTimeRange x, DateTimeRange y)
        {
            if (x.Start > y.Start && x.End > y.End) return 1;
            if (x.Start < y.Start && x.End < y.End) return -1;
            return 0;
        }
    }
}