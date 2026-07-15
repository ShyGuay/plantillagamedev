using Game.Core.Content;

namespace Game.Core.Runtime;

public sealed class GameRuntime
{
    private SceneDocument _source = new();

    public RuntimeState State { get; } = new();

    public void Reset(SceneDocument scene)
    {
        _source = SceneJson.Clone(scene);
        State.Entities.Clear();
        State.IsPlaying = true;
        State.HasWon = false;
        State.ElapsedSeconds = 0;
        State.Message = "Reach the goal";
        State.WorldWidth = Math.Max(320, _source.Settings.WorldWidth);
        State.WorldHeight = Math.Max(240, _source.Settings.WorldHeight);
        State.BackgroundColor = _source.Settings.BackgroundColor;

        foreach (var entity in _source.Entities)
        {
            State.Entities.Add(ToRuntimeEntity(entity));
        }
    }

    public void Stop()
    {
        State.IsPlaying = false;
        State.Message = "Stopped";
    }

    public void Step(InputSnapshot input, double deltaSeconds)
    {
        if (input.Restart)
        {
            Reset(_source);
            return;
        }

        if (!State.IsPlaying || State.HasWon)
        {
            return;
        }

        State.ElapsedSeconds += Math.Clamp(deltaSeconds, 0, 0.05);
        var player = State.Entities.FirstOrDefault(entity => entity.IsPlayer);
        if (player is null)
        {
            State.Message = "No player entity";
            return;
        }

        MovePlayer(player, input, deltaSeconds);
        CheckGoal(player);
    }

    private void MovePlayer(RuntimeEntity player, InputSnapshot input, double deltaSeconds)
    {
        var x = (input.Right ? 1 : 0) - (input.Left ? 1 : 0);
        var y = (input.Down ? 1 : 0) - (input.Up ? 1 : 0);
        var length = Math.Sqrt(x * x + y * y);

        if (length > 0)
        {
            x /= length;
            y /= length;
        }

        player.X = Math.Clamp(player.X + x * player.MoveSpeed * deltaSeconds, player.Radius, State.WorldWidth - player.Radius);
        player.Y = Math.Clamp(player.Y + y * player.MoveSpeed * deltaSeconds, player.Radius, State.WorldHeight - player.Radius);
    }

    private void CheckGoal(RuntimeEntity player)
    {
        foreach (var goal in State.Entities.Where(entity => entity.IsGoal))
        {
            var dx = player.X - goal.X;
            var dy = player.Y - goal.Y;
            var distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance <= player.Radius + goal.GoalRadius)
            {
                State.HasWon = true;
                State.IsPlaying = false;
                State.Message = goal.GoalMessage;
                return;
            }
        }
    }

    private static RuntimeEntity ToRuntimeEntity(EntityDocument entity)
    {
        var isPlayer = ComponentReader.HasComponent(entity, "playerController");
        var isGoal = ComponentReader.HasComponent(entity, "goal");

        return new RuntimeEntity
        {
            Id = entity.Id,
            Name = entity.Name,
            X = entity.Transform.X,
            Y = entity.Transform.Y,
            Rotation = entity.Transform.Rotation,
            ScaleX = entity.Transform.ScaleX,
            ScaleY = entity.Transform.ScaleY,
            SpriteAsset = ComponentReader.GetString(entity, "sprite", "asset", entity.Prefab ?? "placeholder"),
            Color = ComponentReader.GetString(entity, "sprite", "color", isPlayer ? "#4aa3ff" : "#ffffff"),
            Radius = ComponentReader.GetDouble(entity, "sprite", "radius", isGoal ? 24 : 18),
            IsPlayer = isPlayer,
            IsGoal = isGoal,
            MoveSpeed = ComponentReader.GetDouble(entity, "playerController", "moveSpeed", 160),
            GoalRadius = ComponentReader.GetDouble(entity, "goal", "radius", 32),
            GoalMessage = ComponentReader.GetString(entity, "goal", "message", "Slice complete")
        };
    }
}
