using System.Text.Json;

namespace Game.Core.Content;

public sealed class SceneDocument
{
    public int Version { get; set; } = 1;
    public string Id { get; set; } = "scene";
    public string Name { get; set; } = "Scene";
    public SceneSettings Settings { get; set; } = new();
    public List<EntityDocument> Entities { get; set; } = [];
}

public sealed class SceneSettings
{
    public string BackgroundColor { get; set; } = "#15191f";
    public double PixelsPerUnit { get; set; } = 32;
    public double WorldWidth { get; set; } = 960;
    public double WorldHeight { get; set; } = 540;
}

public sealed class EntityDocument
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = "Entity";
    public string? Prefab { get; set; }
    public Transform2D Transform { get; set; } = new();
    public Dictionary<string, JsonElement> Components { get; set; } = [];
}

public sealed class Transform2D
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Rotation { get; set; }
    public double ScaleX { get; set; } = 1;
    public double ScaleY { get; set; } = 1;
}
