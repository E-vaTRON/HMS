using System.Globalization;

namespace HMS;

public class DateTimeToSimpleDateTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime dateTime)
        {
            // Convert DateTime to string in "dd/MM/yy" format
            return dateTime.ToString("dd/MM/yy", CultureInfo.InvariantCulture);
        }

        // Handle other cases or return null if appropriate
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
