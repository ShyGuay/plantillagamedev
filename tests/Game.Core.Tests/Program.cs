using Game.Core;
using Game.Core.Content;
using Game.Core.Runtime;

var tests = new List<(string Name, Action Run)>
{
    ("scene json roundtrip keeps entities", SceneRoundTripKeepsEntities),
    ("runtime reaches goal", RuntimeReachesGoal),
    ("runtime clamps player to world", RuntimeClampsPlayerToWorld)
};

foreach (var test in tests)
{
    test.Run();
    Console.WriteLine($"passed: {test.Name}");
}

static void SceneRoundTripKeepsEntities()
{
    var scene = SampleContent.CreateScene();
    var json = SceneJson.Serialize(scene);
    var loaded = SceneJson.Deserialize(json);

    Assert(loaded.Entities.Count == 2, "Expected two entities after roundtrip.");
    Assert(loaded.Entities.Any(entity => entity.Id == "player"), "Expected player entity.");
    Assert(loaded.Entities.Any(entity => entity.Id == "goal"), "Expected goal entity.");
}

static void RuntimeReachesGoal()
{
    var scene = SampleContent.CreateScene();
    scene.Entities.Single(entity => entity.Id == "player").Transform.X = 730;

    var runtime = new GameRuntime();
    runtime.Reset(scene);
    runtime.Step(new InputSnapshot(Up: false, Down: false, Left: false, Right: true), 0.2);

    Assert(runtime.State.HasWon, "Expected runtime to mark the slice complete.");
}

static void RuntimeClampsPlayerToWorld()
{
    var scene = SampleContent.CreateScene();
    scene.Entities.Single(entity => entity.Id == "player").Transform.X = 10;

    var runtime = new GameRuntime();
    runtime.Reset(scene);
    runtime.Step(new InputSnapshot(Up: false, Down: false, Left: true, Right: false), 2);

    var player = runtime.State.Entities.Single(entity => entity.IsPlayer);
    Assert(player.X >= player.Radius, "Expected player to stay inside the left world bound.");
}

static void Assert(bool condition, string message)
{
    if (!condition)
    {
        throw new InvalidOperationException(message);
    }
}
