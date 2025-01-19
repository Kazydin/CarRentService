using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using CarRentService.Common.Extensions;
using Microsoft.UI.Xaml.Data;

namespace CarRentService.Common.Converters;

public class EnumCollectionToDescriptionsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        // Проверяем, является ли value коллекцией
        if (value is IEnumerable enumCollection)
        {
            var descriptionCollection = new ObservableCollection<string>();

            // Получаем тип элементов коллекции
            var elementType = enumCollection.GetType().GetGenericArguments().FirstOrDefault();

            // Проверяем, что тип элементов является enum
            if (elementType != null && elementType.IsEnum)
            {
                foreach (var enumValue in enumCollection)
                {
                    // Получаем описание для каждого элемента enum
                    descriptionCollection.Add(((Enum)enumValue).GetDescription());
                }

                return descriptionCollection;
            }
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        return null;
    }
}