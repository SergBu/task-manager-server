using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaskManagerServer.Lib.App.Extensions;

public static class EnumExtensions
{
    public static string? FromEnum<T>(this T? enumValue) where T : struct, Enum
    {
        return enumValue?.FromEnum();
    }
    
    public static string FromEnum<T>(this T enumValue) where T : struct, Enum
    {
        return JsonSerializer.Serialize(enumValue, GetOptions()).Replace("\"", "");
    }
    
    public static T? ToEnumOrDefault<T>(this string? enumStringValue) where T : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(enumStringValue))
            return null;

        return enumStringValue.ToEnum<T>();
    }
    
    public static T ToEnum<T>(this string enumStringValue) where T : struct, Enum
    {
        return JsonSerializer.Deserialize<T>($"\"{enumStringValue}\"", GetOptions());
    }

    private static JsonSerializerOptions GetOptions()
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new JsonStringEnumConverter());
        return options;
    }
}