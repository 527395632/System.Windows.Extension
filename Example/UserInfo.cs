using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Extension.Attributes;

namespace Example
{
    public class UserInfo
    {
        [Index(1)]
        public int ID { get; set; }

        [Index(2)]
        public string Name { get; set; }

        [Index(3)]
        public bool Sex { get; set; }
    }
}