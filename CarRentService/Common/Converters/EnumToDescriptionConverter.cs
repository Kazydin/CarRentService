using System;
using System.Collections.Generic;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Enum;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace CarRentService.Common.Converters;

public class EnumToDescriptionConverter : IValueConverter
{
    private static readonly Dictionary<string, Type> EnumNamespaceMap = new()
    {
        { "RentalTariffEnum", typeof(RentalTariffEnum) },
        { "ManagerRoleEnum", typeof(ManagerRoleEnum) },
        { "InsuranceTypeEnum", typeof(InsuranceTypeEnum) },
        { "CarStatusEnum", typeof(CarStatusEnum) },
        { "RentalStatusEnum", typeof(RentalStatusEnum) },
        { "PaymentMethodEnum", typeof(PaymentMethodEnum) },
    };


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
        if (value is string stringValue && parameter is string enumName)
        {
            if (EnumNamespaceMap.TryGetValue(enumName, out var enumType))
            {
                if (enumType.IsEnum)
                {
                    var method = typeof(EnumExtensions).GetMethod(nameof(EnumExtensions.ToEnumFromDescription));
                    var genericMethod = method.MakeGenericMethod(enumType);

                    return genericMethod.Invoke(null, new object[] { stringValue });
                }
            }
            else
            {
                throw new InvalidOperationException("Неизвестный тип перечисления для конвертирования в Enum");
            }
        }

        return DependencyProperty.UnsetValue;
    }
}
