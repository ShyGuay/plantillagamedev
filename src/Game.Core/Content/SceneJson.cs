using System.Text.Json;

namespace Game.Core.Content;

public static class SceneJson
{
    public static readonly JsonSerializerOptions Options = new()
    {
        AllowTrailingCommas = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        ReadCommentHandling = JsonCommentHandling.Skip,
        WriteIndented = true
    };

    public static SceneDocument Deserialize(string json)
    {
        return JsonSerializer.Deserialize<SceneDocument>(json, Options)
            ?? throw new InvalidOperationException("Scene JSON did not produce a document.");
    }

    public static string Serialize(SceneDocument scene)
    {
        return JsonSerializer.Serialize(scene, Options);
    }

    public static SceneDocument Clone(SceneDocument scene)
    {
        return Deserialize(Serialize(scene));
    }
}
