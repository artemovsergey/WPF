using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace BindingExample
{
    public class DateTimeToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            // если с параметром
            if (parameter != null && parameter.ToString() == "EN")
                return ((DateTime)value).ToString("MM-dd-yyyy");

            return ((DateTime)value).ToString("dd.MM.yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
