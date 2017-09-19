using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Linq;
using HierarchyVisualiser.Views;

namespace HierarchyVisualiser.Converters
{
    /// <summary>
    /// Converts the Position of the ClassView to Geometry Data for the Connection Line.
    /// </summary>
    public class ClassViewPosition2ConnectionLineConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var allShownItems = ((ItemsControl)values[0]).Items;
            var x = (double)values[1];
            var y = (double)values[2];
            var guidToFind = (Guid)values[3];

            var searchedObject = allShownItems.Cast<ClassView>().SingleOrDefault(item => (Guid)item.Tag == guidToFind);
            var width = searchedObject.ActualWidth;
            var height = searchedObject.ActualHeight;

            var linePositionX = x + (width / 2);
            var linePositionY = y;

            return new LineGeometry(new System.Windows.Point(linePositionX, linePositionY), new System.Windows.Point(linePositionX, linePositionY - 100));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
