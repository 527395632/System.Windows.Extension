using System.Windows;
using System.Windows.Extension.Data;

namespace System.Windows.Extension.Controls
{
    public class RegularItemsControl : SimpleItemsControl
    {
        public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(
            nameof(ItemWidth), typeof(double), typeof(RegularItemsControl), new PropertyMetadata(ValueBoxes.Double200Box));

        public double ItemWidth
        {
            get => (double)GetValue(ItemWidthProperty);
            set => SetValue(ItemWidthProperty, value);
        }

        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(
            nameof(ItemHeight), typeof(double), typeof(RegularItemsControl), new PropertyMetadata(ValueBoxes.Double200Box));

        public double ItemHeight
        {
            get => (double)GetValue(ItemHeightProperty);
            set => SetValue(ItemHeightProperty, value);
        }

        public static readonly DependencyProperty ItemMarginProperty = DependencyProperty.Register(
            nameof(ItemMargin), typeof(Thickness), typeof(RegularItemsControl), new PropertyMetadata(default(Thickness)));

        public Thickness ItemMargin
        {
            get => (Thickness)GetValue(ItemMarginProperty);
            set => SetValue(ItemMarginProperty, value);
        }
    }
}