using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace SEW03_Projekt
{
    // Diese Klasse konvertiert den Gesundheitswert eines Spielcharakters in einen Fortschrittswert für die Anzeige in einer Healthbar
    public class HealthToProgressConverter : IValueConverter
    {
        // Konvertiert den Gesundheitswert (int) in einen Prozentsatz (double) zur Anzeige in der Healthbar
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Überprüfen, ob der Eingabewert ein Integer-Wert ist
            if (value is int health)
            {
                // Konvertiert den Gesundheitswert in einen Prozentsatz und gibt diesen zurück
                return health / 100.0;
            }
            // Wenn der Eingabewert nicht korrekt ist, wird 0 zurückgegeben
            return 0;
        }

        // Diese Methode ist nicht implementiert, da eine Rückkonvertierung nicht benötigt wird
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Eine Ausnahme wird ausgelöst, um anzuzeigen, dass diese Methode nicht unterstützt wird
            throw new NotImplementedException();
        }
    }
}
