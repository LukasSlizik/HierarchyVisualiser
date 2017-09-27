using HierarchyVisualiser.ViewModels;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace HierarchyVisualiser.Converters
{
    /// <summary>
    /// Converts the Position of the ClassView to Geometry Data for the Connection Line.
    /// </summary>
    public class ClassViewPosition2ConnectionLineConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var childX = (double)values[0];
            var childY = (double)values[1];
            var childWidth = (double)values[2];
            var parentVm = (ClassViewModel)values[3];

            var startPositionX = childX + (childWidth/2);
            var startPositionY = childY;

            if (parentVm != null)
            {
                try
                {
                    var parentX = (double)values[4];
                    var parentY = (double)values[5];
                    var parentWidth = (double)values[6];
                    var parentHeight = (double)values[7];

                    var endPositionX = parentX + (parentWidth / 2);
                    var endPositionY = parentY + parentHeight;

                    return new LineGeometry(new System.Windows.Point(startPositionX, startPositionY), new System.Windows.Point(endPositionX, endPositionY));
                }
                catch
                {
                    return string.Empty;
                }
            }

            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
