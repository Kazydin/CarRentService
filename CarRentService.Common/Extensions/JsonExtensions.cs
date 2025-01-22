using System.Text.Json;

namespace CarRentService.Common.Extensions;

public static class JsonExtensions
{
    public static T DeepClone<T>(this T obj) where T : class?
    {
        var options = new JsonSerializerOptions
            { ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles };
        var json = JsonSerializer.Serialize(obj, options);

        return JsonSerializer.Deserialize<T>(json, options);
    }
}