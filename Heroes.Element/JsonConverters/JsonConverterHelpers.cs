using System.Text;

namespace Heroes.Element.JsonConverters;

internal static class JsonConverterHelpers
{
    public static bool TryParseEnumUtf8<TEnum>(ReadOnlySpan<byte> utf8Value, out TEnum result)
        where TEnum : struct, Enum
    {
        Span<char> chars = stackalloc char[utf8Value.Length];
        int written = Encoding.UTF8.GetChars(utf8Value, chars);

        return Enum.TryParse(chars[..written], out result);
    }

    public static TEnum ParseEnumProperty<TEnum>(ref Utf8JsonReader reader)
        where TEnum : struct, Enum
    {
        if (reader.HasValueSequence || reader.ValueIsEscaped)
            return Enum.Parse<TEnum>(reader.GetString()!);

        return ParseEnumUtf8<TEnum>(reader.ValueSpan);
    }

    public static TEnum ParseEnumUtf8<TEnum>(ReadOnlySpan<byte> utf8Value)
        where TEnum : struct, Enum
    {
        Span<char> chars = stackalloc char[utf8Value.Length];
        int written = Encoding.UTF8.GetChars(utf8Value, chars);

        return Enum.Parse<TEnum>(chars[..written]);
    }

    /// <summary>
    /// Parses a UTF-8 pipe-delimited LinkId (3 parts -&gt; <see cref="AbilityLinkId"/>,
    /// 4 parts -&gt; <see cref="TalentLinkId"/>) directly from the source bytes.
    /// </summary>
    /// <param name="utf8Value">The UTF-8 encoded bytes representing the LinkId.</param>
    /// <returns>The parsed <see cref="LinkId"/>.</returns>
    public static LinkId ParseLinkIdUtf8(ReadOnlySpan<byte> utf8Value)
    {
        Span<Range> ranges = stackalloc Range[5];
        int count = 0;

        foreach (Range range in utf8Value.Split((byte)'|'))
        {
            if (count == ranges.Length)
                break; // over 5 parts; falls through to the count check below

            ranges[count++] = range;
        }

        if (count is not (3 or 4))
            throw new JsonException("Not exactly three or four parts.");

        if (!TryParseEnumUtf8(utf8Value[ranges[2]], out AbilityType abilityType))
            throw new JsonException("Invalid ability type.");

        string elementId = Encoding.UTF8.GetString(utf8Value[ranges[0]]);
        string buttonElementId = Encoding.UTF8.GetString(utf8Value[ranges[1]]);

        if (count == 3)
            return new AbilityLinkId(elementId, buttonElementId, abilityType);

        if (!TryParseEnumUtf8(utf8Value[ranges[3]], out TalentTier talentTier))
            throw new JsonException("Invalid talent tier.");

        return new TalentLinkId(elementId, buttonElementId, abilityType, talentTier);
    }
}
