namespace Heroes.Element.JsonConverters;

/// <summary>
/// Converter to convert <see cref="AbilityLinkId"/> to and from JSON.
/// </summary>
public class AbilityLinkIdConverter : JsonConverter<AbilityLinkId>
{
    /// <inheritdoc/>
    public override AbilityLinkId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (string.IsNullOrWhiteSpace(value))
            return null;

        string[] parts = value.Split('|');

        if (parts.Length == 3)
        {
            if (!Enum.TryParse(parts[2], out AbilityType abilityType))
                throw new JsonException();

            return new AbilityLinkId(parts[0], parts[1], abilityType);
        }

        throw new JsonException();
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, AbilityLinkId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }

    /// <inheritdoc/>
    public override AbilityLinkId ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Read(ref reader, typeToConvert, options) ?? throw new JsonException();
    }

    /// <inheritdoc/>
    public override void WriteAsPropertyName(Utf8JsonWriter writer, [DisallowNull] AbilityLinkId value, JsonSerializerOptions options)
    {
        writer.WritePropertyName(value.ToString());
    }
}
