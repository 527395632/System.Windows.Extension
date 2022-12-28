using System;

namespace System.Windows.Extension.Interactivity
{
    public class PreviewInvokeEventArgs : EventArgs
    {
        public bool Cancelling { get; set; }
    }
}