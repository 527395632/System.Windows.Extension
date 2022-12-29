using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Extension.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IndexAttribute : Attribute
    {
        public IndexAttribute(int index)
        {
            Value = index;
        }

        public int Value { get; }
    }
}
