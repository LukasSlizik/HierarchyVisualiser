using System.Windows;
using System.Windows.Controls.Primitives;
using HierarchyVisualiser.ViewModels;

namespace HierarchyVisualiser.Behaviors
{
    /// <summary>
    /// Enables dragging of the Thumbs representing the classes, interfaces, ...
    /// </summary>
    public static class ThumbDragBehavior
    {
        /// <summary>
        /// Set this Property to True to enable dragging of the Thumb.
        /// </summary>
        public static readonly DependencyProperty ThumbCanBeDraggedProperty = DependencyProperty.RegisterAttached(
                                                                                            "ThumbCanBeDragged",
                                                                                            typeof(bool),
                                                                                            typeof(ThumbDragBehavior),
                                                                                            new PropertyMetadata(false, OnCanBeDraggedChanged));

        public static bool GetThumbCanBeDragged(Thumb el)
        {
            return (bool)el.GetValue(ThumbCanBeDraggedProperty);
        }

        public static void SetThumbCanBeDragged(Thumb el, bool value)
        {
            el.SetValue(ThumbCanBeDraggedProperty, value);
        }

        /// <summary>
        /// Registers an EventHandler for DragDelta Event.
        /// </summary>
        private static void OnCanBeDraggedChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var thumb = (Thumb)d;

            thumb.DragDelta -= OnDragDelta;
            thumb.DragDelta += OnDragDelta;
        }

        /// <summary>
        /// Updates the [X,Y] Coordinates in the DataContext.
        /// </summary>
        private static void OnDragDelta(object sender, DragDeltaEventArgs args)
        {
            var n = (TypeViewModel)((FrameworkElement)sender).DataContext;
            n.X += args.HorizontalChange;
            n.Y += args.VerticalChange;
        }
    }
}
