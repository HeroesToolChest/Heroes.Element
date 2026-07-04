namespace Heroes.Element.JsonConverters;

/// <summary>
/// Converter to convert the hero units to and from JSON.
/// </summary>
public class HeroUnitsConverter : JsonConverter<IDictionary<string, Unit>>
{
    /// <inheritdoc/>
    public override IDictionary<string, Unit>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException($"Expected StartObject, got {reader.TokenType}.");

        Dictionary<string, Unit> unitByIds = [];

        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException($"Expected PropertyName, got {reader.TokenType}.");

            string id = reader.GetString()!;

            reader.Read(); // -> value start

            Unit? unit = JsonSerializer.Deserialize<Unit>(ref reader, options);
            if (unit is not null)
            {
                unit.Id = id;
                unitByIds[id] = unit;
            }
        }

        return unitByIds;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, IDictionary<string, Unit> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<string, Unit> unitById in value)
        {
            writer.WritePropertyName(unitById.Key);
            JsonSerializer.Serialize(writer, unitById.Value, options);
        }

        writer.WriteEndObject();
    }
}