namespace Heroes.Element.JsonConverters;

/// <summary>
/// A converter to convert <see cref="AbilityLinkId"/> to and from JSON.
/// </summary>
public class AbilityLinkIdConverter : JsonConverter<AbilityLinkId>
{
    /// <inheritdoc/>
    public override AbilityLinkId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (string.IsNullOrWhiteSpace(value))
            return null;

        ReadOnlySpan<char> valueSpan = value.AsSpan();
        Span<Range> ranges = stackalloc Range[4];

        int count = valueSpan.Split(ranges, '|');

        if (count != 3)
            throw new JsonException("Not exactly three parts");

        if (!Enum.TryParse(valueSpan[ranges[2]], out AbilityType abilityType))
            throw new JsonException("Invalid ability type");

        return new AbilityLinkId(value[ranges[0]], value[ranges[1]], abilityType);
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
