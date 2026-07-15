using System.Text.Json;
using Game.Core.Content;

namespace Game.Core;

public static class SampleContent
{
    public static SceneDocument CreateScene()
    {
        return new SceneDocument
        {
            Id = "template-main",
            Name = "Template Main Scene",
            Settings = new SceneSettings
            {
                BackgroundColor = "#15191f",
                WorldWidth = 960,
                WorldHeight = 540,
                PixelsPerUnit = 32
            },
            Entities =
            [
                new EntityDocument
                {
                    Id = "player",
                    Name = "Player",
                    Prefab = "actor.player",
                    Transform = new Transform2D { X = 160, Y = 270 },
                    Components =
                    {
                        ["sprite"] = JsonSerializer.SerializeToElement(new { asset = "placeholder-player", color = "#4aa3ff", radius = 18 }, SceneJson.Options),
                        ["playerController"] = JsonSerializer.SerializeToElement(new { moveSpeed = 180 }, SceneJson.Options)
                    }
                },
                new EntityDocument
                {
                    Id = "goal",
                    Name = "Goal",
                    Prefab = "marker.goal",
                    Transform = new Transform2D { X = 780, Y = 270 },
                    Components =
                    {
                        ["sprite"] = JsonSerializer.SerializeToElement(new { asset = "placeholder-goal", color = "#7bd88f", radius = 24 }, SceneJson.Options),
                        ["goal"] = JsonSerializer.SerializeToElement(new { radius = 34, message = "Slice completada" }, SceneJson.Options)
                    }
                }
            ]
        };
    }
}
