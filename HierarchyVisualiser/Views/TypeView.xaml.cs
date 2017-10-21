using HierarchyVisualiser.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace HierarchyVisualiser.Views
{
    /// <summary>
    /// Interaction logic for ClassView.xaml
    /// </summary>
    public partial class TypeView : UserControl
    {
        public TypeView()
        {
            InitializeComponent();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var classView = (TypeView)sender;
            var classViewModel = (TypeViewModel)classView.DataContext;
            classViewModel.Width = e.NewSize.Width;
            classViewModel.Height = e.NewSize.Height;
        }
    }
}
