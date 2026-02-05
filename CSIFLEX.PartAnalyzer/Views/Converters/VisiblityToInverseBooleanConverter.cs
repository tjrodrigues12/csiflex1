using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CSIFLEX.PartAnalyzer.Views.Converters
{
    public class VisiblityToInverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vis = (Visibility)value;
            return vis == Visibility.Visible
                ? true
                : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vis = (bool)value;
            return vis
                ? Visibility.Visible
                : Visibility.Hidden;
        }
    }
}
