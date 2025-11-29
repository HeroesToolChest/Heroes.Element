using GstInnerProperty = Heroes.Element.JsonTypeInfoResolvers.GameStringModifier.GameStringTextExtractorProperties;

namespace Heroes.Element.JsonTypeInfoResolvers;

/// <summary>
/// Class for the custom heroes json type info resolver.
/// </summary>
public class HeroesElementResolver : DefaultJsonTypeInfoResolver
{
    /// <inheritdoc/>
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

        // attach owner to nested value objects
        if (!typeof(Unit).IsAssignableFrom(type) && !typeof(Map).IsAssignableFrom(type))
            return jsonTypeInfo;

        foreach (JsonPropertyInfo p in jsonTypeInfo.Properties)
        {
            if (IsNotGstInnerProperty(p))
                continue;

            Func<object, object?>? originalGet = p.Get;
            if (originalGet is null)
                continue;

            string typeName = type.Name.ToLowerInvariant();

            p.Get = obj =>
            {
                object? value = originalGet(obj);

                if (value is not null && obj is IElementObject owner)
                {
                    // Handle collections of MapObjective differently
                    if (value is IList<MapObjective> mapObjectives)
                    {
                        foreach (MapObjective mapObjective in mapObjectives)
                        {
                            GameStringTextExtractor.SetOwner(mapObjective, owner, typeName);
                        }
                    }
                    else
                    {
                        GameStringTextExtractor.SetOwner(value, owner, typeName);
                    }
                }

                return value;
            };
        }

        return jsonTypeInfo;
    }

    private static bool IsNotGstInnerProperty(JsonPropertyInfo p)
    {
        if (p.DeclaringType == typeof(Unit) || p.DeclaringType == typeof(Hero))
        {
            return p.PropertyType != GstInnerProperty.UnitLife.Type && p.PropertyType != GstInnerProperty.UnitEnergy.Type && p.PropertyType != GstInnerProperty.UnitShield.Type;
        }
        else if (p.DeclaringType == typeof(Map))
        {
            return p.PropertyType != GstInnerProperty.MapObjectiveTitle.Type && p.PropertyType != GstInnerProperty.MapObjectiveDescription.Type;
        }

        return false;
    }
}
