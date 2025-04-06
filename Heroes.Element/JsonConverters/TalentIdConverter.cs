namespace Heroes.Element.JsonConverters;

/// <summary>
/// Converter to convert <see cref="TalentId"/> to and from JSON.
/// </summary>
public class TalentIdConverter : JsonConverter<TalentId>
{
    /// <inheritdoc/>
    public override TalentId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (string.IsNullOrWhiteSpace(value))
            return null;

        string[] parts = value.Split('|');

        if (parts.Length == 2)
            return new TalentId(parts[0], parts[1]);
        else
            throw new JsonException();
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, TalentId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }

    /// <inheritdoc/>
    public override TalentId ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Read(ref reader, typeToConvert, options) ?? throw new JsonException();
    }

    /// <inheritdoc/>
    public override void WriteAsPropertyName(Utf8JsonWriter writer, [DisallowNull] TalentId value, JsonSerializerOptions options)
    {
        writer.WritePropertyName(value.ToString());
    }
}
