using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using HierarchyVisualiser.ViewModels;
using System.Windows.Shapes;
using System.Windows.Media;

namespace HierarchyVisualiser.Converters
{
    public class ConnectionConverter : IMultiValueConverter
    {
        // Todo: Refactoring + Comments
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var width = (double)values[0];
            var height = (double)values[1];
            var x = (double)values[2];
            var y = (double)values[3];

            var linePositionX = (int)((x + width) / 2);
            var linePositionY = (int)y;

            return new LineGeometry(new System.Windows.Point(linePositionX, linePositionY), new System.Windows.Point(linePositionX, linePositionY - 100));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
