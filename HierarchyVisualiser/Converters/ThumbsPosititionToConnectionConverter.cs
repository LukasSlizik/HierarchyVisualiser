using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace HierarchyVisualiser.Converters
{
    /// <summary>
    /// Converts the positions and dimensions of two Thumbs into Geometry Data of the connection line.
    /// </summary>
    class ThumbsPosititionToConnectionConverter : IMultiValueConverter
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