using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace SEW03_Projekt
{
    public class HealthToProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int health)
            {
                return health / 100.0; // Konvertiert den Gesundheitswert in einen Fortschrittswert
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}