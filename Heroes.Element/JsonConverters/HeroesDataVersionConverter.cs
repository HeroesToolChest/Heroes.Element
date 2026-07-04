using System.Text;

namespace Heroes.Element.JsonConverters;

/// <summary>
/// A converter to convert <see cref="HeroesDataVersion"/> to and from JSON.
/// </summary>
public class HeroesDataVersionConverter : JsonConverter<HeroesDataVersion>
{
    /// <inheritdoc/>
    public override HeroesDataVersion? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException($"Expected string token, but got {reader.TokenType}.");

        if (reader.HasValueSequence || reader.ValueIsEscaped)
            return ReadFromString(reader.GetString());

        ReadOnlySpan<byte> utf8Value = reader.ValueSpan;

        Span<char> chars = stackalloc char[utf8Value.Length];
        int written = Encoding.UTF8.GetChars(utf8Value, chars);

        ReadOnlySpan<char> versionSpan = chars[..written];

        if (!HeroesDataVersion.TryParse(versionSpan, out HeroesDataVersion? result))
            throw new JsonException($"Invalid version format: {versionSpan}.");

        return result;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, HeroesDataVersion value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetAsVersionString());
    }

    private static HeroesDataVersion? ReadFromString(string? versionString)
    {
        if (!HeroesDataVersion.TryParse(versionString, out HeroesDataVersion? result))
            throw new JsonException($"Invalid version format: {versionString}.");

        return result;
    }
}
