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
        {
            return null;
        }

        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Expected string token, but got {reader.TokenType}");
        }

        string? versionString = reader.GetString();

        if (!HeroesDataVersion.TryParse(versionString, out HeroesDataVersion? result))
        {
            throw new JsonException($"Invalid version format: {versionString}");
        }

        return result;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, HeroesDataVersion value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetAsVersionString());
    }
}
