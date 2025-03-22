using System.Globalization;
using System.Windows.Data;

namespace Lab4.PersonList.Convertors;

public class BooleanToDisplayValueConverter : IValueConverter
{
    public string TrueDisplayValue { get; set; } = true.ToString();
    public string FalseDisplayValue { get; set; } = false.ToString();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue) return boolValue ? TrueDisplayValue : FalseDisplayValue;

        return FalseDisplayValue;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}