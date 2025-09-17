namespace Heroes.Element.JsonConverters;

/// <summary>
/// Converter to convert the hero units to and from JSON.
/// </summary>
public class HeroUnitsConverter : JsonConverter<IDictionary<string, Unit>>
{
    /// <inheritdoc/>
    public override IDictionary<string, Unit>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        IDictionary<string, Unit>? unitByIds = JsonSerializer.Deserialize<IDictionary<string, Unit>>(ref reader, options);
        if (unitByIds is null)
            return null;

        foreach (KeyValuePair<string, Unit> unitById in unitByIds)
        {
            unitById.Value.Id = unitById.Key;
        }

        return unitByIds;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, IDictionary<string, Unit> value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}