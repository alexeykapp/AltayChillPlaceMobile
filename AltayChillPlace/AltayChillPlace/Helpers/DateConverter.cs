using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AltayChillPlace.Helpers
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty; // или любое значение по умолчанию

            DateTime date;
            if (DateTime.TryParse((string)value, out date))
            {
                return date.ToString("dd-MM-yyyy");
            }

            throw new ArgumentException("Invalid date format");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty; // или любое значение по умолчанию

            DateTime date;
            if (DateTime.TryParse((string)value, out date))
            {
                return date.ToString("yyyy-MM-dd");
            }

            throw new ArgumentException("Invalid date format");
        }
    }
}
