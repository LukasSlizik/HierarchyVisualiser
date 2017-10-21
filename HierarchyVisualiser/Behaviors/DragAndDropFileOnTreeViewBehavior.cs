using HierarchyVisualiser.Contracts;
using System.Windows;
using System.Windows.Controls;

namespace HierarchyVisualiser.Behaviors
{
    /// <summary>
    /// Enables to drop .dll files directly onto the assembly tree.
    /// </summary>
    public static class DragAndDropFileOnTreeViewBehavior
    {
        #region IsDraggableProperty

        /// <summary>
        /// Set this property to allow Drag and Drop of .NET Assemblies in the Assembly Tree.
        /// </summary>
        public static readonly DependencyProperty CanFileBeDroppedProperty = DependencyProperty.RegisterAttached(
                                                                                                    "CanFileBeDropped",
                                                                                                    typeof(bool),
                                                                                                    typeof(DragAndDropFileOnTreeViewBehavior),
                                                                                                    new PropertyMetadata(false, OnCanFileBeDropped));

        public static bool GetCanFileBeDropped(TreeView el)
        {
            return (bool)el.GetValue(CanFileBeDroppedProperty);
        }

        public static void SetCanFileBeDropped(TreeView el, bool value)
        {
            el.SetValue(CanFileBeDroppedProperty, value);
        }

        #endregion

        private static void OnCanFileBeDropped(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var el = (UIElement)d;
            UnregisterEventHandlers(el);

            if ((bool)args.NewValue)
                RegisterEventHandlers(el);
        }

        private static void RegisterEventHandlers(UIElement d)
        {
            d.AllowDrop = true;
            d.Drop += OnDrop;
        }

        private static void UnregisterEventHandlers(UIElement d)
        {
            d.AllowDrop = false;
            d.Drop -= OnDrop;
        }

        /// <summary>
        /// File is dropped onto the Assembly Tree.
        /// </summary>
        private static void OnDrop(object sender, DragEventArgs args)
        {
            if (args.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var el = (FrameworkElement)sender;
                var ctx = (IAssemblyFileLoader)el.DataContext;

                string[] droppedFiles = (string[])args.Data.GetData(DataFormats.FileDrop);
                foreach (var file in droppedFiles)
                    ctx.TryLoadAssemblyFromFile(file);
            }
        }
    }
}
