namespace Heroes.Element.JsonConverters;

/// <summary>
/// Converter to convert <see cref="TalentLinkId"/> to and from JSON.
/// </summary>
public class TalentLinkIdConverter : JsonConverter<TalentLinkId>
{
    /// <inheritdoc/>
    public override TalentLinkId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (string.IsNullOrWhiteSpace(value))
            return null;

        string[] parts = value.Split('|');

        if (parts.Length == 4)
        {
            if (!Enum.TryParse(parts[2], out AbilityType abilityType))
                throw new JsonException();

            if (!Enum.TryParse(parts[3], out TalentTier talentTier))
                throw new JsonException();

            return new TalentLinkId(parts[0], parts[1], abilityType, talentTier);
        }

        throw new JsonException();
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, TalentLinkId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }

    /// <inheritdoc/>
    public override TalentLinkId ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Read(ref reader, typeToConvert, options) ?? throw new JsonException();
    }

    /// <inheritdoc/>
    public override void WriteAsPropertyName(Utf8JsonWriter writer, [DisallowNull] TalentLinkId value, JsonSerializerOptions options)
    {
        writer.WritePropertyName(value.ToString());
    }
}
