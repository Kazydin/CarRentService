using System;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace CarRentService.Common.Converters;

public class MatchValuesBackgroundConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is string sortOrder && parameter is string currentOrder)
        {
            // Сравниваем текущее значение с параметром
            if (sortOrder == currentOrder)
            {
                return new SolidColorBrush(Color.FromArgb(100, 125, 125, 125));
            }
        }

        return new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}