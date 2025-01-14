using System.ComponentModel;
using System.Reflection;

namespace CarRentService.Common.Extensions;

public static class EnumExtensions
{
    public static T ToEnum<T>(this string value) where T : struct, Enum
    {
        // Преобразуем строку в enum с использованием Enum.TryParse
        if (Enum.TryParse(value, true, out T result)
            && Enum.IsDefined(typeof(T), result))
        {
            return result;
        }

        // Если строка не соответствует ни одному значению enum, выбрасываем исключение
        throw new ArgumentException($"Невозможно преобразовать строку '{value}' в {typeof(T).Name}.");
    }

    public static T? ToEnumNullable<T>(this string value) where T : struct, Enum
    {
        // Преобразуем строку в enum с использованием Enum.TryParse
        if (Enum.TryParse(value, true, out T result)
            && Enum.IsDefined(typeof(T), result))
        {
            return result;
        }

        return null;
    }

    public static string GetDescription(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());

        if (field != null)
        {
            DescriptionAttribute attribute =
                (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            if (attribute != null)
            {
                return attribute.Description;
            }
        }

        return value.ToString();
    }

    public static T ToEnumFromDescription<T>(this string value) where T : struct, Enum
    {
        // Преобразуем строку в enum с использованием Enum.TryParse
        var values = Enum.GetValues<T>().ToDictionary(k => k.GetDescription(), v => v);

        if (values.TryGetValue(value, out T enumValue))
        {
            return enumValue;
        }

        // Если строка не соответствует ни одному значению enum, выбрасываем исключение
        throw new ArgumentException($"Невозможно преобразовать строку '{value}' в {typeof(T).Name}.");
    }
}