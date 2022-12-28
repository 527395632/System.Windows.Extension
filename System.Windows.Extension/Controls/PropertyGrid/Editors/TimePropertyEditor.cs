using System.Windows;

namespace System.Windows.Extension.Controls
{
    public class TimePropertyEditor : PropertyEditorBase
    {
        public override FrameworkElement CreateElement(PropertyItem propertyItem) => new DateTimePicker
        {
            IsEnabled = !propertyItem.IsReadOnly
        };

        public override DependencyProperty GetDependencyProperty() => TimePicker.SelectedTimeProperty;
    }
}