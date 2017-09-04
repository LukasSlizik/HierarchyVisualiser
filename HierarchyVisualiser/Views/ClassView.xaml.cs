using HierarchyVisualiser.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using HierarchyVisualiser.ViewModels;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var path1 = new Path();
            path1.Stroke = Brushes.Black;
            path1.StrokeThickness = 1;

            var ctx = (ClassViewModel)((ClassView)sender).DataContext;
            ctx.Connection = "M 250,0 L 250,100";

            double left = Canvas.GetLeft(this);
            double top = Canvas.GetTop(this);

            //myCanvas.Children.Add(path1);

            //LineGeometry line1 = new LineGeometry();
            //path1.Data = line1;

            //myThumb1.StartLines.Add(line1);
            //myThumb2.EndLines.Add(line1);

            //UpdateLines(myThumb1);
        }

        private void UpdateLines(MyThumb thumb)
        {
            double left = Canvas.GetLeft(thumb);
            double top = Canvas.GetTop(thumb);

            for (int i = 0; i < thumb.StartLines.Count; i++)
                thumb.StartLines[i].StartPoint = new Point(left + thumb.ActualWidth / 2, top + thumb.ActualHeight / 2);

            for (int i = 0; i < thumb.EndLines.Count; i++)
                thumb.EndLines[i].EndPoint = new Point(left + thumb.ActualWidth / 2, top + thumb.ActualHeight / 2);
        }
    }
}
