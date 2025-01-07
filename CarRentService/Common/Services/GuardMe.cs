using System;

namespace CarRentService.Common.Services;

public class GuardMe
{
    public static void NotNull<T>(T? value, string name, string message) where T : struct
    {
        if (!value.HasValue)
        {
            throw new ArgumentNullException(name, message);
        }
    }
}