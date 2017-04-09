using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HierarchyVisualiser.Behaviors
{
    /// <summary>
    /// Enables Drag and Drop Functionality on UserControl objects.
    /// </summary>
    public static class DragAndDropBehavior
    {
        private static bool isBeingDragged = false;
        private static Point clickPosition;

        #region IsDraggableProperty

        /// <summary>
        /// Set this property to move the UserControl around.
        /// </summary>
        public static readonly DependencyProperty CanBeDraggedProperty = DependencyProperty.RegisterAttached(
                                                                                                    "CanBeDragged", 
                                                                                                    typeof(bool), 
                                                                                                    typeof(DragAndDropBehavior), 
                                                                                                    new PropertyMetadata(false, OnIsDraggableChanged));

        public static bool GetCanBeDragged(UserControl uc)
        {
            return (bool)uc.GetValue(CanBeDraggedProperty);
        }

        public static void SetCanBeDragged(UserControl uc, bool value)
        {
            uc.SetValue(CanBeDraggedProperty, value);
        }

        #endregion

        /// <summary>
        /// All needed EventHandlers are attached.
        /// </summary>
        private static void RegisterEventHandlers(UserControl uc)
        {
            uc.MouseLeftButtonDown += OnMouseLeftButtonDown;
            uc.MouseLeftButtonUp += OnMouseLeftButtonUp;
            uc.MouseMove += OnMouseMove;
        }

        /// <summary>
        /// All needed EventHandlers are detached.
        /// </summary>
        private static void UnregisterEventHandlers(UserControl uc)
        {
            uc.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            uc.MouseLeftButtonUp -= OnMouseLeftButtonUp;
            uc.MouseMove -= OnMouseMove;
        }

        /// <summary>
        /// Registers or unregisters all attached Event Handlers.
        /// </summary>
        private static void OnIsDraggableChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var uc = (UserControl)sender;
            UnregisterEventHandlers(uc);

            if ((bool)args.NewValue)
                RegisterEventHandlers(uc);
        }

        /// <summary>
        /// UserControl selected to be dragged.
        /// </summary>
        private static void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isBeingDragged = true;
            var controlToBeDragged = sender as UserControl;
            clickPosition = e.GetPosition(controlToBeDragged);
            controlToBeDragged.CaptureMouse();
        }

        /// <summary>
        /// UserControl is released.
        /// </summary>
        private static void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isBeingDragged = false;
            var draggable = sender as UserControl;
            draggable.ReleaseMouseCapture();
        }

        /// <summary>
        /// UserControl is being dragged.
        /// </summary>
        private static void OnMouseMove(object sender, MouseEventArgs e)
        {
            var draggableControl = sender as UserControl;
            if (isBeingDragged && draggableControl != null)
            {
                Point currentPosition = e.GetPosition(draggableControl.Parent as Canvas);

                var transform = draggableControl.RenderTransform as TranslateTransform;
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    draggableControl.RenderTransform = transform;
                }

                transform.X = currentPosition.X - clickPosition.X;
                transform.Y = currentPosition.Y - clickPosition.Y;
            }
        }
    }
}