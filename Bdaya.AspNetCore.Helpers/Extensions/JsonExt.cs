using System.Text.Json;

namespace System;

public static class JsonExt
{
    public static string ToJson<T>(this T src, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(src, options);
    }
}
