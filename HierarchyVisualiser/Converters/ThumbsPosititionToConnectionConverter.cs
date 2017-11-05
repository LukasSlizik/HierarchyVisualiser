using System;
using System.Globalization;
using System.Windows;
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
            // convert input
            var startX = (double)values[0];
            var startY = (double)values[1];
            var startWidth = (double)values[2];
            var startHeight = (double)values[3];
            var endX = (double)values[4];
            var endY = (double)values[5];
            var endWidth = (double)values[6];
            var endHeight = (double)values[7];

            // calculate positions
            var startXMiddle = startX + (startWidth / 2);
            var endXMiddle = endX + (endWidth / 2);
            var middleBetweenStartAndEnd = (startY + (endY + endHeight)) / 2;

            // calculate points for the PathGeometry
            var point1 = new Point(startXMiddle, startY);
            var point2 = new Point(startXMiddle, middleBetweenStartAndEnd);
            var point3 = new Point(endXMiddle, middleBetweenStartAndEnd);
            var point4 = new Point(endXMiddle, endY + endHeight);

            var segments = new[] { new LineSegment(point2, true), new LineSegment(point3, true), new LineSegment(point4, true) };
            var myPathFigure = new PathFigure(point1, segments, false);

            PathGeometry myPathGeometry = new PathGeometry(new[] { myPathFigure });
            return myPathGeometry;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}