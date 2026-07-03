namespace Heroes.Element.JsonConverters;

/// <summary>
/// Converter to convert <see cref="LinkId"/> to and from JSON.
/// </summary>
public class LinkIdConverter : JsonConverter<LinkId>
{
    /// <inheritdoc/>
    public override LinkId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (string.IsNullOrWhiteSpace(value))
            return null;

        ReadOnlySpan<char> valueSpan = value.AsSpan();

        Span<Range> ranges = stackalloc Range[5];
        int count = valueSpan.Split(ranges, '|');

        if (count is not (3 or 4))
            throw new JsonException("Not exactly three or four parts");

        if (!Enum.TryParse(valueSpan[ranges[2]], out AbilityType abilityType))
            throw new JsonException("Invalid ability type");

        if (count == 3)
            return new AbilityLinkId(value[ranges[0]], value[ranges[1]], abilityType);

        if (!Enum.TryParse(valueSpan[ranges[3]], out TalentTier talentTier))
            throw new JsonException("Invalid talent tier");

        return new TalentLinkId(value[ranges[0]], value[ranges[1]], abilityType, talentTier);
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
