using System;
using CarRentService.Common.Extensions;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace CarRentService.Common.Converters;

public class EnumToDescriptionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is Enum enumValue)
        {
            return enumValue.GetDescription();
        }

        return value?.ToString() ?? string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (targetType.IsEnum && value is string stringValue)
        {
            // Использование рефлексии для создания вызова обобщённого метода
            var method = typeof(EnumExtensions).GetMethod("ToEnumFromDescription");
            var genericMethod = method.MakeGenericMethod(targetType);

            // Вызов метода с передачей stringValue как параметра
            return genericMethod.Invoke(null, [stringValue])!;
        }

        return DependencyProperty.UnsetValue;
    }
}
