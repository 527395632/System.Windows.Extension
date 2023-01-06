﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Extension.Mvvm
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CommandBindingAttribute : Attribute
    {
        public CommandBindingAttribute(string execute)
            : this(execute, null)
        {
        }

        public CommandBindingAttribute(string execute, string canExecute)
        {
            Execute = execute;
            CanExecute = canExecute;
        }

        public string Execute { get; }
        public string CanExecute { get; }
    }
}
