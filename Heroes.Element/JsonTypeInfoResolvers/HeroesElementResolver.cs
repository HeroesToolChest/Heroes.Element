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

        // attach owner (Unit/Hero) to nested value objects
        if (typeof(Unit).IsAssignableFrom(type))
        {
            foreach (JsonPropertyInfo p in jsonTypeInfo.Properties)
            {
                if (p.PropertyType == GstInnerProperty.UnitLife.Type || p.PropertyType == GstInnerProperty.UnitEnergy.Type || p.PropertyType == GstInnerProperty.UnitShield.Type)
                {
                    Func<object, object?>? originalGet = p.Get;
                    if (originalGet is null)
                        continue;

                    string typeName = type.Name.ToLowerInvariant();

                    p.Get = obj =>
                    {
                        object? value = originalGet(obj);
                        if (value is not null && obj is IElementObject owner)
                        {
                            GameStringTextExtractor.SetOwner(value, owner, typeName);
                        }

                        return value;
                    };
                }
            }
        }

        return jsonTypeInfo;
    }
}
