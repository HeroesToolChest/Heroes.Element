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

        ReadOnlySpan<char> valueSpan = value.AsSpan();
        Span<Range> ranges = stackalloc Range[5];

        int count = valueSpan.Split(ranges, '|');

        if (count != 4)
            throw new JsonException("Not exactly four parts");

        if (!Enum.TryParse(valueSpan[ranges[2]], out AbilityType abilityType))
            throw new JsonException("Invalid ability type");

        if (!Enum.TryParse(valueSpan[ranges[3]], out TalentTier talentTier))
            throw new JsonException("Invalid talent tier");

        return new TalentLinkId(value[ranges[0]], value[ranges[1]], abilityType, talentTier);
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
