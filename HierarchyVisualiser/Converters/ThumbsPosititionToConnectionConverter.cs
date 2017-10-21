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
            var startX = (double)values[0];
            var startY = (double)values[1];
            var startWidth = (double)values[2];
            var startHeight = (double)values[3];
            var endX = (double)values[4];
            var endY = (double)values[5];
            var endWidth = (double)values[6];
            var endHeight = (double)values[7];

            return new LineGeometry(new System.Windows.Point(startX + (startWidth / 2), startY), new System.Windows.Point(endX + (endWidth / 2), endY + endHeight));
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}