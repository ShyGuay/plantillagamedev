namespace Game.Core.Runtime;

public readonly record struct InputSnapshot(
    bool Up,
    bool Down,
    bool Left,
    bool Right,
    bool PrimaryAction = false,
    bool Restart = false);
