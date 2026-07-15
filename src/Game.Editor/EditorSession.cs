using System.Text.Json;
using System.Text.Json.Nodes;
using Game.Core.Content;
using Game.Core.Runtime;

namespace Game.Editor;

public sealed class EditorSession
{
    public EditorSession(SceneDocument scene)
    {
        Scene = SceneJson.Clone(scene);
        SelectedEntityId = Scene.Entities.FirstOrDefault()?.Id;
    }

    public SceneDocument Scene { get; private set; }
    public GameRuntime Runtime { get; } = new();
    public bool IsPlaying { get; private set; }
    public string? SelectedEntityId { get; private set; }
    public string LastExportedJson { get; private set; } = "";

    public EntityDocument? SelectedEntity => Scene.Entities.FirstOrDefault(entity => entity.Id == SelectedEntityId);

    public void ReplaceScene(SceneDocument scene)
    {
        Scene = SceneJson.Clone(scene);
        SelectedEntityId = Scene.Entities.FirstOrDefault()?.Id;
        StopPlay();
    }

    public void Select(string entityId)
    {
        SelectedEntityId = entityId;
    }

    public void StartPlay()
    {
        Runtime.Reset(Scene);
        IsPlaying = true;
    }

    public void StopPlay()
    {
        Runtime.Stop();
        IsPlaying = false;
    }

    public void ResetPlay()
    {
        StartPlay();
    }

    public void Step(InputSnapshot input, double deltaSeconds)
    {
        if (!IsPlaying)
        {
            return;
        }

        Runtime.Step(input, deltaSeconds);
        if (!Runtime.State.IsPlaying)
        {
            IsPlaying = false;
        }
    }

    public EntityDocument AddGoal()
    {
        var goal = new EntityDocument
        {
            Id = $"goal-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}",
            Name = "Goal",
            Prefab = "marker.goal",
            Transform = new Transform2D { X = Scene.Settings.WorldWidth * 0.75, Y = Scene.Settings.WorldHeight * 0.5 },
            Components =
            {
                ["sprite"] = JsonSerializer.SerializeToElement(new { asset = "placeholder-goal", color = "#7bd88f", radius = 24 }, SceneJson.Options),
                ["goal"] = JsonSerializer.SerializeToElement(new { radius = 34, message = "Slice completada" }, SceneJson.Options)
            }
        };

        Scene.Entities.Add(goal);
        SelectedEntityId = goal.Id;
        return goal;
    }

    public void DuplicateSelected()
    {
        if (SelectedEntity is null)
        {
            return;
        }

        var duplicate = SceneJson.Deserialize(SceneJson.Serialize(new SceneDocument { Entities = [SelectedEntity] })).Entities[0];
        duplicate.Id = $"{SelectedEntity.Id}-copy-{Scene.Entities.Count + 1}";
        duplicate.Name = $"{SelectedEntity.Name} Copy";
        duplicate.Transform.X += 32;
        duplicate.Transform.Y += 32;
        Scene.Entities.Add(duplicate);
        SelectedEntityId = duplicate.Id;
    }

    public void DeleteSelected()
    {
        if (SelectedEntity is null || ComponentReader.HasComponent(SelectedEntity, "playerController"))
        {
            return;
        }

        Scene.Entities.Remove(SelectedEntity);
        SelectedEntityId = Scene.Entities.FirstOrDefault()?.Id;
    }

    public void MoveSelected(double dx, double dy)
    {
        if (SelectedEntity is null)
        {
            return;
        }

        SelectedEntity.Transform.X = Math.Clamp(SelectedEntity.Transform.X + dx, 0, Scene.Settings.WorldWidth);
        SelectedEntity.Transform.Y = Math.Clamp(SelectedEntity.Transform.Y + dy, 0, Scene.Settings.WorldHeight);
    }

    public string ExportJson()
    {
        LastExportedJson = SceneJson.Serialize(Scene);
        return LastExportedJson;
    }

    public double GetNumber(EntityDocument entity, string component, string property, double fallback)
    {
        return ComponentReader.GetDouble(entity, component, property, fallback);
    }

    public string GetText(EntityDocument entity, string component, string property, string fallback)
    {
        return ComponentReader.GetString(entity, component, property, fallback);
    }

    public bool HasComponent(EntityDocument entity, string component)
    {
        return ComponentReader.HasComponent(entity, component);
    }

    public void SetNumber(EntityDocument entity, string component, string property, double value)
    {
        SetComponentProperty(entity, component, property, value);
    }

    public void SetText(EntityDocument entity, string component, string property, string value)
    {
        SetComponentProperty(entity, component, property, value);
    }

    private static void SetComponentProperty(EntityDocument entity, string component, string property, object value)
    {
        var node = entity.Components.TryGetValue(component, out var existing) && existing.ValueKind == JsonValueKind.Object
            ? JsonNode.Parse(existing.GetRawText())?.AsObject() ?? []
            : [];

        node[property] = value switch
        {
            int intValue => JsonValue.Create(intValue),
            double doubleValue => JsonValue.Create(doubleValue),
            bool boolValue => JsonValue.Create(boolValue),
            _ => JsonValue.Create(value.ToString())
        };

        entity.Components[component] = JsonSerializer.SerializeToElement(node, SceneJson.Options);
    }
}
