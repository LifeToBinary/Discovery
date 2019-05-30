﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Discovery.Core.UI.AttachedProperties
{
    public class ButtonBrush
    {
        public static readonly DependencyProperty ButtonPressBackgroundProperty =
            DependencyProperty.RegisterAttached(
                "ButtonPressBackground",
                typeof(Brush),
                typeof(ButtonBrush),
                new PropertyMetadata(default(Brush)));
        public static void SetButtonPressBackground(
            DependencyObject element,
            Brush value)
            => element.SetValue(ButtonPressBackgroundProperty, value);
        public static Brush GetButtonPressBackground(
            DependencyObject element)
            => element.GetValue(ButtonPressBackgroundProperty) as Brush;
    }
}
