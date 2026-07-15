using System.Text.Json;

namespace Game.Core.Content;

public static class ComponentReader
{
    public static bool HasComponent(EntityDocument entity, string componentName)
    {
        return entity.Components.ContainsKey(componentName);
    }

    public static double GetDouble(EntityDocument entity, string componentName, string propertyName, double fallback)
    {
        if (!entity.Components.TryGetValue(componentName, out var component))
        {
            return fallback;
        }

        if (component.ValueKind != JsonValueKind.Object ||
            !component.TryGetProperty(propertyName, out var property))
        {
            return fallback;
        }

        return property.ValueKind == JsonValueKind.Number && property.TryGetDouble(out var value)
            ? value
            : fallback;
    }

    public static string GetString(EntityDocument entity, string componentName, string propertyName, string fallback)
    {
        if (!entity.Components.TryGetValue(componentName, out var component))
        {
            return fallback;
        }

        if (component.ValueKind != JsonValueKind.Object ||
            !component.TryGetProperty(propertyName, out var property))
        {
            return fallback;
        }

        return property.ValueKind == JsonValueKind.String
            ? property.GetString() ?? fallback
            : fallback;
    }
}
