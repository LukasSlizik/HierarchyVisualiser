﻿using System;
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
            var x = (double)values[0];
            var y = (double)values[1];

            var members = (ItemsControl)values[2];
            var guidToFind = (System.Guid)values[3];

            //var memberToFind = members.Items.ToList().Where(item => item.Tag == guidToFind);
            foreach(var item in members.Items)
            {
                if ((ClassView)item)
            }

            var linePositionX = x;
            var linePositionY = y;

            return new LineGeometry(new System.Windows.Point(linePositionX, linePositionY), new System.Windows.Point(linePositionX, linePositionY - 100));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
