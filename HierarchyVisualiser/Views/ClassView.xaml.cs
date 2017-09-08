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

        // Todo: remove from the code behind
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // ActualWidth and Height are 0.0 -> not loaded yet
            //https://stackoverflow.com/questions/1695101/why-are-actualwidth-and-actualheight-0-0-in-this-case

            var path1 = new Path();
            path1.Stroke = Brushes.Black;
            path1.StrokeThickness = 1;

            var classView = (ClassView)sender;
            var ctx = (ClassViewModel)((ClassView)sender).DataContext;
            var left = ctx.X;
            var top = ctx.Y;

            var right = left + classView.ActualWidth;
            var bottom = top + classView.ActualHeight;

            var startX = (left + right) / 2;
            var startY = top;

            var endX = startX;
            var endY = endX - 100;

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
