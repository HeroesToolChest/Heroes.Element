namespace Heroes.Element.JsonConverters;

/// <summary>
/// Converter to convert <see cref="LinkId"/> to and from JSON.
/// </summary>
public class TalentLinkIdConverter : JsonConverter<LinkId>
{
    /// <inheritdoc/>
    public override LinkId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (string.IsNullOrWhiteSpace(value))
            return null;

        string[] parts = value.Split('|');

        if (parts.Length == 3)
        {
            if (!Enum.TryParse(parts[2], out AbilityType abilityType))
                throw new JsonException();

            return new LinkId(parts[0], parts[1], abilityType);
        }

        throw new JsonException();
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, LinkId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }

    /// <inheritdoc/>
    public override LinkId ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Read(ref reader, typeToConvert, options) ?? throw new JsonException();
    }

    /// <inheritdoc/>
    public override void WriteAsPropertyName(Utf8JsonWriter writer, [DisallowNull] LinkId value, JsonSerializerOptions options)
    {
        writer.WritePropertyName(value.ToString());
    }
}
