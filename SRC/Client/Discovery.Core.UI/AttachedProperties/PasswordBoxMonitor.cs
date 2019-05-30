using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Discovery.Core.UI.AttachedProperties
{
    public class PasswordBoxMonitor : DependencyObject
    {
        public static readonly DependencyProperty IsMonitoringProperty =
            DependencyProperty.RegisterAttached(
                "IsMonitoring",
                typeof(bool),
                typeof(PasswordBoxMonitor),
                new UIPropertyMetadata(false, OnIsMonitoringChanged));

        public static readonly DependencyProperty PasswordLengthProperty =
            DependencyProperty.RegisterAttached(
                "PasswordLength",
                typeof(int),
                typeof(PasswordBoxMonitor),
                new UIPropertyMetadata(0));

        public static int GetPasswordLength(DependencyObject element)
            => (int)element.GetValue(PasswordLengthProperty);

        public static void SetPasswordLength(DependencyObject element, int value)
            => element.SetValue(PasswordLengthProperty, value);

        public static bool GetIsMonitoring(DependencyObject element)
            => (bool)element.GetValue(IsMonitoringProperty);

        public static void SetIsMonitoring(DependencyObject element, bool value)
            => element.SetValue(IsMonitoringProperty, value);

        private static void OnIsMonitoringChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                if ((bool)e.NewValue)
                {
                    passwordBox.PasswordChanged += PasswordChanged;
                    return;
                }
                passwordBox.PasswordChanged -= PasswordChanged;
            }

            void PasswordChanged(object sender, RoutedEventArgs _)
            {
                if (sender is PasswordBox pwdBox)
                {
                    SetPasswordLength(pwdBox, pwdBox.Password.Length);
                }
            }
        }
    }
}
