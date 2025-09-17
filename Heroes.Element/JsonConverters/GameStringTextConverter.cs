namespace Heroes.Element.JsonConverters;

/// <summary>
/// A converter to convert strings to and from <see cref="GameStringText"/>.
/// </summary>
public class GameStringTextConverter : JsonConverter<GameStringText>
{
    private readonly StormLocale _stormLocale = StormLocale.ENUS;
    private readonly GameStringTextType _gameStringTextType = GameStringTextType.RawText;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameStringTextConverter"/> class.
    /// </summary>
    /// <param name="stormLocale">The localization of the text (only for reading). Defaults to <see cref="StormLocale.ENUS"/>.</param>
    /// <param name="gameStringTextType">The type of text to convert to (only for writing). Defaults to <see cref="GameStringTextType.RawText"/>.</param>
    public GameStringTextConverter(StormLocale? stormLocale = null, GameStringTextType? gameStringTextType = null)
    {
        if (stormLocale is not null)
            _stormLocale = stormLocale.Value;

        if (gameStringTextType is not null)
            _gameStringTextType = gameStringTextType.Value;
    }

    /// <inheritdoc/>
    public override GameStringText? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            return new GameStringText(reader.GetString() ?? string.Empty, _stormLocale);
        }

        throw new JsonException("Expected string type");
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, GameStringText value, JsonSerializerOptions options)
    {
        if (_gameStringTextType == GameStringTextType.RawText)
            writer.WriteStringValue(value.RawText);
        else if (_gameStringTextType == GameStringTextType.PlainText)
            writer.WriteStringValue(value.PlainText);
        else if (_gameStringTextType == GameStringTextType.PlainTextWithNewlines)
            writer.WriteStringValue(value.PlainTextWithNewlines);
        else if (_gameStringTextType == GameStringTextType.PlainTextWithScaling)
            writer.WriteStringValue(value.PlainTextWithScaling);
        else if (_gameStringTextType == GameStringTextType.PlainTextWithScalingWithNewlines)
            writer.WriteStringValue(value.PlainTextWithScalingWithNewlines);
        else if (_gameStringTextType == GameStringTextType.ColoredText)
            writer.WriteStringValue(value.ColoredText);
        else if (_gameStringTextType == GameStringTextType.ColoredTextWithScaling)
            writer.WriteStringValue(value.ColoredTextWithScaling);
        else
            writer.WriteStringValue(value.ToString());
    }
}
