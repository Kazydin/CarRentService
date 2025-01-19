using System.Globalization;
using System;
using Microsoft.UI.Xaml.Data;

namespace CarRentService.Common.Converters;

public class DateTimeToDateTimeOffsetConverter : IValueConverter
{
    // Преобразование из DateTime в DateTimeOffset
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is DateTime dateTime)
        {
            return new DateTimeOffset(dateTime);
        }
        return null;
    }

    // Преобразование из DateTimeOffset в DateTime
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.DateTime;
        }
        return null;
    }
}