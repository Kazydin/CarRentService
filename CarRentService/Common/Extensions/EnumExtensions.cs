using CarRentService.Common.Attributes;

using System;
using System.Reflection;

namespace CarRentService.Common.Extensions;
public static class EnumExtensions
{
    public static Type GetPageType(this Enum value)
    {
        var type = value.GetType();
        var memberInfo = type.GetMember(value.ToString());

        if (memberInfo.Length > 0)
        {
            var attribute = memberInfo[0].GetCustomAttribute<PageTypeAttribute>();
            if (attribute != null)
            {
                return attribute.PageType;
            }
        }

        throw new InvalidOperationException($"Атрибут PageTypeAttribute отсутствует для значения {value}.");
    }
}