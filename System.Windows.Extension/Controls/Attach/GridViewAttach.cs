﻿using System.Windows;
using System.Windows.Extension.Data;

namespace System.Windows.Extension.Controls
{
    public class GridViewAttach
    {
        public static readonly DependencyProperty ColumnHeaderHeightProperty = DependencyProperty.RegisterAttached(
            "ColumnHeaderHeight", typeof(double), typeof(GridViewAttach), new PropertyMetadata(ValueBoxes.Double0Box));

        public static void SetColumnHeaderHeight(DependencyObject element, double value)
            => element.SetValue(ColumnHeaderHeightProperty, value);

        public static double GetColumnHeaderHeight(DependencyObject element)
            => (double)element.GetValue(ColumnHeaderHeightProperty);
    }
}