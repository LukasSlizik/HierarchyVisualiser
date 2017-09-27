using HierarchyVisualiser.ViewModels;
using System.Windows;
using System.Windows.Controls;

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

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var classView = (ClassView)sender;
            var classViewModel = (ClassViewModel)classView.DataContext;
            classViewModel.Width = e.NewSize.Width;
        }
    }
}
