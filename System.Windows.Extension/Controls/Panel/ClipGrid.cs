﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Extension.Data;

namespace System.Windows.Extension.Controls
{
    public class ClipGrid : Grid
    {
        public static readonly DependencyProperty IsClipEnabledProperty = DependencyProperty.Register(
            nameof(IsClipEnabled), typeof(bool), typeof(ClipGrid), new PropertyMetadata(ValueBoxes.TrueBox));

        public bool IsClipEnabled
        {
            get => (bool) GetValue(IsClipEnabledProperty);
            set => SetValue(IsClipEnabledProperty, ValueBoxes.BooleanBox(value));
        }

        protected override Geometry GetLayoutClip(Size layoutSlotSize)
        {
            if (IsClipEnabled)
            {
                return base.GetLayoutClip(layoutSlotSize);
            }

            return null;
        }
    }
}
