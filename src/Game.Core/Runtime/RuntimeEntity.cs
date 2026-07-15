namespace Game.Core.Runtime;

public sealed class RuntimeEntity
{
    public string Id { get; init; } = "";
    public string Name { get; init; } = "";
    public double X { get; set; }
    public double Y { get; set; }
    public double Rotation { get; init; }
    public double ScaleX { get; init; } = 1;
    public double ScaleY { get; init; } = 1;
    public string SpriteAsset { get; init; } = "placeholder";
    public string Color { get; init; } = "#ffffff";
    public double Radius { get; init; } = 16;
    public bool IsPlayer { get; init; }
    public bool IsGoal { get; init; }
    public double MoveSpeed { get; init; }
    public double GoalRadius { get; init; }
    public string GoalMessage { get; init; } = "Complete";
}
