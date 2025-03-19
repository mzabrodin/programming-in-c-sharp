using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Lab2.PersonBirthdayApplication.Convertors;

public class NullToVisibilityConverter : IValueConverter
{
    public Visibility Visibility { get; set; } = Visibility.Collapsed;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return Visibility;
        }
        else
        {
            return Visibility.Visible;
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}