using HierarchyVisualiser.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace HierarchyVisualiser.Views
{
    /// <summary>
    /// Interaction logic for ClassView.xaml
    /// </summary>
    public partial class ClassView : UserControl
    {
        public ClassView()
        {
            InitializeComponent();
        }

        public void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            var n = (ClassViewModel)((FrameworkElement)sender).DataContext;
            n.X += e.HorizontalChange;
            n.Y += e.VerticalChange;
        }

    }
}
