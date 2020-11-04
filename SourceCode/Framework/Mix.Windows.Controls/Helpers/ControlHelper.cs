using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Mix.Windows.Controls.Helpers
{
    /// <summary>
    /// ControlHelper
    /// </summary>
    public class ControlHelper
    {
        /// <summary>
        /// The mouse over background property
        /// </summary>
        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.RegisterAttached("MouseOverBackground", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(null));

        /// <summary>
        /// Gets the mouse over background.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Brush GetMouseOverBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverBackgroundProperty);
        }

        /// <summary>
        /// Sets the mouse over background.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetMouseOverBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverBackgroundProperty, value);
        }

        /// <summary>
        /// The mouse over foreground property
        /// </summary>
        public static readonly DependencyProperty MouseOverForegroundProperty = DependencyProperty.RegisterAttached("MouseOverForeground", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(null));

        /// <summary>
        /// Gets the mouse over foreground.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Brush GetMouseOverForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverForegroundProperty);
        }

        /// <summary>
        /// Sets the mouse over foreground.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetMouseOverForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverForegroundProperty, value);
        }

        /// <summary>
        /// The follow target property
        /// </summary>
        public static readonly DependencyProperty FollowTargetProperty = DependencyProperty.RegisterAttached("FollowTarget", typeof(DependencyObject), typeof(ControlHelper), new PropertyMetadata(null, FollowTargetChanged));

        /// <summary>
        /// Gets the follow target.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static DependencyObject GetFollowTarget(DependencyObject obj)
        {
            return (DependencyObject)obj.GetValue(FollowTargetProperty);
        }

        /// <summary>
        /// Sets the follow target.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetFollowTarget(DependencyObject obj, DependencyObject value)
        {
            obj.SetValue(FollowTargetProperty, value);
        }

        /// <summary>
        /// Follows the target changed.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void FollowTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is DependencyObject target &&
                Window.GetWindow(target) is Window window &&
                d is Popup popup)
            {
                window.LocationChanged += (sender, eventArgs) =>
                {
                    var backup = popup.HorizontalOffset;
                    popup.HorizontalOffset++;
                    popup.HorizontalOffset = backup;
                };
            }
        }
    }
}