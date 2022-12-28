using System.Windows;

namespace System.Windows.Extension.Controls
{
    public interface ISelectable
    {
        event RoutedEventHandler Selected;

        bool IsSelected { get; set; }
    }
}