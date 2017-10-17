using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace HierarchyVisualiser.Converters
{
    class ConnectionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var startPositionX = (double)values[0];
            var startPositionY = (double)values[1];
            var endPositionX = (double)values[2];
            var endPositionY = (double)values[3];

            return new LineGeometry(new System.Windows.Point(startPositionX, startPositionY), new System.Windows.Point(endPositionX, endPositionY));
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

//var childX = (double)values[0];
//var childY = (double)values[1];
//var childWidth = (double)values[2];
//var parentVm = (ClassViewModel)values[3];

//var startPositionX = childX + (childWidth / 2);
//var startPositionY = childY;

//            if (parentVm != null)
//            {
//                try
//                {
//                    var parentX = (double)values[4];
//var parentY = (double)values[5];
//var parentWidth = (double)values[6];
//var parentHeight = (double)values[7];

//var endPositionX = parentX + (parentWidth / 2);
//var endPositionY = parentY + parentHeight;

//                    return new LineGeometry(new System.Windows.Point(startPositionX, startPositionY), new System.Windows.Point(endPositionX, endPositionY));
//                }
//                catch
//                {
//                    return string.Empty;
//                }
//            }

//            return string.Empty;