using System.Text.Json;

namespace Heroes.Element.JsonConverters;

/// <summary>
/// Converter to convert strings to <see cref="TooltipDescription"/>.
/// </summary>
public class TooltipDescriptionReadConverter : JsonConverter<TooltipDescription>
{
    private readonly StormLocale _stormLocale;

    /// <summary>
    /// Initializes a new instance of the <see cref="TooltipDescriptionReadConverter"/> class.
    /// </summary>
    /// <param name="stormLocale">The current localization.</param>
    public TooltipDescriptionReadConverter(StormLocale stormLocale)
    {
        _stormLocale = stormLocale;
    }

    /// <inheritdoc/>
    public override TooltipDescription? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            return new TooltipDescription(reader.GetString() ?? string.Empty, _stormLocale);
        }

        throw new JsonException("Expected string type");
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, TooltipDescription value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
