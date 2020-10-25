using Mix.Windows.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Mix.Windows.Controls
{
    /// <summary>
    /// TabControlHelper
    /// </summary>
    public class TabControlHelper
    {
        #region Switch Aware

        /// <summary>
        /// The aware selection changed property
        /// </summary>
        public static readonly DependencyProperty AwareSelectionChangedProperty = DependencyProperty.RegisterAttached(
            "AwareSelectionChanged", typeof(bool), typeof(TabControlHelper), new PropertyMetadata(default(bool), OnAwareSelectionChangedChanged));

        /// <summary>
        /// Sets the aware selection changed.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void SetAwareSelectionChanged(DependencyObject element, bool value)
        {
            element.SetValue(AwareSelectionChangedProperty, value);
        }

        /// <summary>
        /// Gets the aware selection changed.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static bool GetAwareSelectionChanged(DependencyObject element)
        {
            return (bool)element.GetValue(AwareSelectionChangedProperty);
        }

        /// <summary>
        /// Called when [aware selection changed changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnAwareSelectionChangedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var frameworkElement = (FrameworkElement)d;

            if ((bool)e.NewValue)
                frameworkElement.Loaded += OnLoaded;
            else
                frameworkElement.Loaded -= OnLoaded;
        }

        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private static void OnLoaded(object sender, RoutedEventArgs e)
        {
            var contentControl = (ContentControl)sender;
            var tab = contentControl.TryFindParent<TabControl>();

            if (tab == null) return;

            tab.SelectionChanged -= OnSelectionChanged;
            tab.SelectionChanged += OnSelectionChanged;
        }

        /// <summary>
        /// Called when [selection changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private static void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource != sender) return;

            var toTabItem = e.AddedItems.Cast<FrameworkElement>().SingleOrDefault();
            var fromTabItem = e.RemovedItems.Cast<FrameworkElement>().SingleOrDefault();

            if (toTabItem == null || fromTabItem == null) return;

            if (GetAwareSelectionChanged(fromTabItem))
                (fromTabItem.DataContext as ITabItemSelectionChangedAware)?.OnUnselected();

            if (GetAwareSelectionChanged(toTabItem))
                (toTabItem.DataContext as ITabItemSelectionChangedAware)?.OnSelected();
        }

        #endregion Switch Aware

        #region Switch Animation

        private const string LeftToRightMovedEventTriggerResourceKey = "LeftToRightMovedEventTrigger";
        private const string RightToLeftMovedEventTriggerResourceKey = "RightToLeftMovedEventTrigger";

        private static readonly Uri TabControlResourceDictionaryUri = new Uri("pack://application:,,,/Mix.Windows.Controls;component/Styles/MixTheme.TabControl.xaml");

        /// <summary>
        /// The left to right animation property
        /// </summary>
        public static readonly DependencyProperty LeftToRightAnimationProperty = DependencyProperty.RegisterAttached("LeftToRightAnimation", typeof(Storyboard), typeof(TabControlHelper), new PropertyMetadata(null, OnLeftToRightAnimationChanged));

        /// <summary>
        /// Gets the left to right animation.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Storyboard GetLeftToRightAnimation(DependencyObject obj)
        {
            return (Storyboard)obj.GetValue(LeftToRightAnimationProperty);
        }

        /// <summary>
        /// Sets the left to right animation.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetLeftToRightAnimation(DependencyObject obj, Storyboard value)
        {
            obj.SetValue(LeftToRightAnimationProperty, value);
        }

        /// <summary>
        /// The right to left animation property
        /// </summary>
        public static readonly DependencyProperty RightToLeftAnimationProperty = DependencyProperty.RegisterAttached("RightToLeftAnimation", typeof(Storyboard), typeof(TabControlHelper), new PropertyMetadata(null, OnRightToLeftAnimationChanged));

        /// <summary>
        /// Gets the right to left animation.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Storyboard GetRightToLeftAnimation(DependencyObject obj)
        {
            return (Storyboard)obj.GetValue(RightToLeftAnimationProperty);
        }

        /// <summary>
        /// Sets the right to left animation.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetRightToLeftAnimation(DependencyObject obj, Storyboard value)
        {
            obj.SetValue(RightToLeftAnimationProperty, value);
        }

        /// <summary>
        /// The left to right moved event
        /// </summary>
        public static readonly RoutedEvent LeftToRightMovedEvent = EventManager.RegisterRoutedEvent("LeftToRightMoved", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(TabControlHelper));

        /// <summary>
        /// Adds the left to right moved handler.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void AddLeftToRightMovedHandler(DependencyObject obj, RoutedEventHandler value)
        {
            if (obj is UIElement element)
            {
                element.AddHandler(LeftToRightMovedEvent, value);
            }
        }

        /// <summary>
        /// Removes the left to right moved handler.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void RemoveLeftToRightMovedHandler(DependencyObject obj, RoutedEventHandler value)
        {
            if (obj is UIElement element)
            {
                element.RemoveHandler(LeftToRightMovedEvent, value);
            }
        }

        /// <summary>
        /// The right to left moved event
        /// </summary>
        public static readonly RoutedEvent RightToLeftMovedEvent = EventManager.RegisterRoutedEvent("RightToLeftMoved", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(TabControlHelper));

        /// <summary>
        /// Adds the right to left moved handler.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void AddRightToLeftMovedHandler(DependencyObject obj, RoutedEventHandler value)
        {
            if (obj is UIElement element)
            {
                element.AddHandler(RightToLeftMovedEvent, value);
            }
        }

        /// <summary>
        /// Removes the right to left moved handler.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void RemoveRightToLeftMovedHandler(DependencyObject obj, RoutedEventHandler value)
        {
            if (obj is UIElement element)
            {
                element.RemoveHandler(RightToLeftMovedEvent, value);
            }
        }

        private static readonly HashSet<int> _initializedTabControlSet = new HashSet<int>();

        /// <summary>
        /// Called when [left to right animation changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnLeftToRightAnimationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TabControl tabControl &&
                !_initializedTabControlSet.Contains(tabControl.GetHashCode()))
            {
                ModifyTabControl(tabControl);
            }
        }

        /// <summary>
        /// Called when [right to left animation changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnRightToLeftAnimationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TabControl tabControl &&
                !_initializedTabControlSet.Contains(tabControl.GetHashCode()))
            {
                ModifyTabControl(tabControl);
            }
        }

        /// <summary>
        /// Modifies the tab control.
        /// </summary>
        /// <param name="tabControl">The tab control.</param>
        private static void ModifyTabControl(Selector tabControl)
        {
            tabControl.SelectionChanged += OnSelectionChangedForSwitchAnimation;
            tabControl.Loaded += OnLoadedForSwitchAnimation;

            _initializedTabControlSet.Add(tabControl.GetHashCode());
        }

        /// <summary>
        /// Called when [loaded for switch animation].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        /// <exception cref="TranslateTransform"></exception>
        private static void OnLoadedForSwitchAnimation(object sender, RoutedEventArgs e)
        {
            if (sender is not TabControl tabControl) return;

            foreach (TabItem tabItem in tabControl.Items)
            {
                if (tabItem.Content is not FrameworkElement content) break;

                // 1. Sets TranslateTransform instance to RenderTransform Property if it does not exist.
                switch (content.RenderTransform)
                {
                    case TranslateTransform _:
                        break;

                    case TransformGroup group:
                        if (group.Children.FirstOrDefault(item => item is TranslateTransform) == null)
                            group.Children.Add(new TranslateTransform());
                        break;

                    case MatrixTransform _:
                        content.RenderTransform = new TranslateTransform();
                        break;

                    default:
                        throw new Exception("the RenderTransform property already has a value, and the value is not TranslateTransform type.");
                }

                // 2. Add animation to EventTrigger property.
                var resourceDictionary = new ResourceDictionary { Source = TabControlResourceDictionaryUri };
                content.Triggers.Add(resourceDictionary[LeftToRightMovedEventTriggerResourceKey] as TriggerBase);
                content.Triggers.Add(resourceDictionary[RightToLeftMovedEventTriggerResourceKey] as TriggerBase);
            }
            tabControl.Loaded -= OnLoaded;
        }

        /// <summary>
        /// Called when [selection changed for switch animation].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        private static void OnSelectionChangedForSwitchAnimation(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource || e.RemovedItems.Count == 0 || e.AddedItems.Count == 0) return;

            var fromTabItem = e.RemovedItems.Cast<TabItem>().Single();
            var toTabItem = e.AddedItems.Cast<TabItem>().Single();
            if (sender is TabControl tabControl &&
                toTabItem.Content is UIElement content)
            {
                content.RaiseEvent(
                    tabControl.Items.IndexOf(fromTabItem) > tabControl.Items.IndexOf(toTabItem)
                    ? new RoutedEventArgs(RightToLeftMovedEvent, content)
                    : new RoutedEventArgs(LeftToRightMovedEvent, content));
            }
        }

        #endregion Switch Animation
    }
}