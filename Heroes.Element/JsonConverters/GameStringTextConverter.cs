namespace Heroes.Element.JsonConverters;

/// <summary>
/// A converter to convert strings to and from <see cref="GameStringText"/>.
/// </summary>
public class GameStringTextConverter : JsonConverter<GameStringText>
{
    private readonly GameStringTextConverterOptions _gameStringTextConverterOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameStringTextConverter"/> class.
    /// </summary>
    /// <param name="gameStringTextConverterOptions">The options for the converter. Defaults to a new instance of <see cref="GameStringTextConverterOptions"/>.</param>
    public GameStringTextConverter(GameStringTextConverterOptions? gameStringTextConverterOptions = null)
    {
        _gameStringTextConverterOptions = gameStringTextConverterOptions ?? new GameStringTextConverterOptions();
    }

    /// <inheritdoc/>
    public override GameStringText? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException("Expected string type");

        return new GameStringText(reader.GetString() ?? string.Empty, _gameStringTextConverterOptions.StormLocale);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, GameStringText value, JsonSerializerOptions options)
    {
        if (_gameStringTextConverterOptions.RemoveHltForConstantTags == GameStringTextHltRemoveMode.Remove)
            value.RemoveHltNameForConstantTags(false);
        else if (_gameStringTextConverterOptions.RemoveHltForConstantTags == GameStringTextHltRemoveMode.RemoveAndUndo)
            value.RemoveHltNameForConstantTags(true);

        if (_gameStringTextConverterOptions.RemoveHltForStyleTags == GameStringTextHltRemoveMode.Remove)
            value.RemoveHltNameForStyleTags(false);
        else if (_gameStringTextConverterOptions.RemoveHltForStyleTags == GameStringTextHltRemoveMode.RemoveAndUndo)
            value.RemoveHltNameForStyleTags(true);

        GameStringTextType gameStringTextType = _gameStringTextConverterOptions.GameStringTextType;

        if (gameStringTextType == GameStringTextType.RawText)
            writer.WriteStringValue(value.RawText);
        else if (gameStringTextType == GameStringTextType.PlainText)
            writer.WriteStringValue(value.PlainText);
        else if (gameStringTextType == GameStringTextType.PlainTextWithNewlines)
            writer.WriteStringValue(value.PlainTextWithNewlines);
        else if (gameStringTextType == GameStringTextType.PlainTextWithScaling)
            writer.WriteStringValue(value.PlainTextWithScaling);
        else if (gameStringTextType == GameStringTextType.PlainTextWithScalingWithNewlines)
            writer.WriteStringValue(value.PlainTextWithScalingWithNewlines);
        else if (gameStringTextType == GameStringTextType.ColoredText)
            writer.WriteStringValue(value.ColoredText);
        else if (gameStringTextType == GameStringTextType.ColoredTextWithScaling)
            writer.WriteStringValue(value.ColoredTextWithScaling);
        else
            writer.WriteStringValue(value.ToString());
    }
}
