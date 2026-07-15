namespace Game.Core.Runtime;

public sealed class RuntimeState
{
    public bool IsPlaying { get; set; }
    public bool HasWon { get; set; }
    public double ElapsedSeconds { get; set; }
    public string Message { get; set; } = "Ready";
    public double WorldWidth { get; set; } = 960;
    public double WorldHeight { get; set; } = 540;
    public string BackgroundColor { get; set; } = "#15191f";
    public List<RuntimeEntity> Entities { get; } = [];
}
