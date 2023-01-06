using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Extension.Mvvm
{
    public class Arguments : List<object>
    {
        public Arguments(IEnumerable<object> collection)
            : base(collection)
        {
        }
    }
}