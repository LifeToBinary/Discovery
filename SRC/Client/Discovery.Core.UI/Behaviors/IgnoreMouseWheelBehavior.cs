using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Discovery.Core.UI.Behaviors
{
    public class IgnoreMouseWheelBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewMouseWheel += AssociatedObjectPreviewMouseWheel;
        }


        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseWheel -= AssociatedObjectPreviewMouseWheel;
            base.OnDetaching();
        }

        private void AssociatedObjectPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            var e2 = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            e2.RoutedEvent = UIElement.MouseWheelEvent;
            AssociatedObject.RaiseEvent(e2);
        }
    }
}
