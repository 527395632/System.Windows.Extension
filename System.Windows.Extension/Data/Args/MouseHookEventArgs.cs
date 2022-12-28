using System;
using System.Windows.Extension.Tools.Interop;

namespace System.Windows.Extension.Data
{
    internal class MouseHookEventArgs : EventArgs
    {
        public MouseHookMessageType MessageType { get; set; }

        public InteropValues.POINT Point { get; set; }
    }
}