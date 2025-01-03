using Microsoft.UI.Xaml.Data;

using System;

namespace CarRentService.Common.Converters;

public class EnumToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is Enum enumValue)
        {
            return enumValue.ToString(); // Преобразуем в строку
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is string stringValue && Enum.IsDefined(targetType, stringValue))
        {
            return Enum.Parse(targetType, stringValue);
        }
        return null;
    }
}