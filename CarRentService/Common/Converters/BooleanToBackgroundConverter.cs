using System;
using Windows.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

namespace CarRentService.Common.Converters;

public class BooleanToBackgroundConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is true)
        {
            return new SolidColorBrush(Color.FromArgb(100, 125, 125, 125));
        }
        return new SolidColorBrush(Color.FromArgb(0, 0 , 0, 0));
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}