using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace ClientApp.Converters
{
    public class StepToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int step && int.TryParse(parameter?.ToString(), out int index))
            {
                return step >= index ? Color.FromArgb("#2196F3") : Color.FromArgb("#BDBDBD");
            }
            return Colors.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
