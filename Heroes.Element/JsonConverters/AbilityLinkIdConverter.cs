using System.Text;

namespace Heroes.Element.JsonConverters;

/// <summary>
/// A converter to convert <see cref="AbilityLinkId"/> to and from JSON.
/// </summary>
public class AbilityLinkIdConverter : JsonConverter<AbilityLinkId>
{
    /// <inheritdoc/>
    public override AbilityLinkId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        if (reader.HasValueSequence || reader.ValueIsEscaped)
            return ReadFromString(reader.GetString());

        ReadOnlySpan<byte> utf8Value = reader.ValueSpan;

        if (utf8Value.IsEmpty)
            return null;

        Span<Range> ranges = stackalloc Range[4];
        int count = 0;

        foreach (Range range in utf8Value.Split((byte)'|'))
        {
            if (count == ranges.Length)
                break; // over 4 parts; falls through to the count != 3 check below

            ranges[count++] = range;
        }

        if (count != 3)
            throw new JsonException("Not exactly three parts.");

        if (!JsonConverterHelpers.TryParseEnumUtf8(utf8Value[ranges[2]], out AbilityType abilityType))
            throw new JsonException("Invalid ability type.");

        return new AbilityLinkId(
            Encoding.UTF8.GetString(utf8Value[ranges[0]]),
            Encoding.UTF8.GetString(utf8Value[ranges[1]]),
            abilityType);
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

    private static AbilityLinkId? ReadFromString(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        ReadOnlySpan<char> valueSpan = value.AsSpan();
        Span<Range> ranges = stackalloc Range[4];

        int count = valueSpan.Split(ranges, '|');

        if (count != 3)
            throw new JsonException("Not exactly three parts.");

        if (!Enum.TryParse(valueSpan[ranges[2]], out AbilityType abilityType))
            throw new JsonException("Invalid ability type.");

        return new AbilityLinkId(value[ranges[0]], value[ranges[1]], abilityType);
    }
}
