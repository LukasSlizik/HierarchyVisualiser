using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HierarchyVisualiser.Converters
{
    /// <summary>
    /// Converts the Position of the Class to the Thickness Object (Margin)
    /// </summary>
    public class ClassPositionToMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var x = (double)values[0] > 0 ? (double)values[0] : 0;
            var y = (double)values[1] > 0 ? (double)values[1] : 0;

            return new Thickness(x, y, 0, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

