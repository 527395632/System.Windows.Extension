using System;

namespace System.Windows.Extension.Controls
{
    public interface ISingleOpen : IDisposable
    {
        bool CanDispose { get; }
    }
}