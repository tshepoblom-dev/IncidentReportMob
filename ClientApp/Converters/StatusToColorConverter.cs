using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace ClientApp.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                return status.ToLower() switch
                {
                    "pending" => Color.FromArgb("#2196F3"),
                    "in_progress" => Color.FromArgb("#4CAF50"),
                    "resolved" => Color.FromArgb("#F44336"),
                    _ => Color.FromArgb("#9E9E9E")
                };
            }

            return Colors.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

}
