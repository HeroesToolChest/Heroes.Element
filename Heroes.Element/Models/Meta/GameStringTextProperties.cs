namespace Heroes.Element.Models.Meta;

/// <summary>
/// Contains information about the gamestring text.
/// </summary>
public class GameStringTextProperties
{
    /// <summary>
    /// Gets or sets the locale of the data (e.g., "enUS", "frFR").
    /// </summary>
    public StormLocale Locale { get; set; } = StormLocale.ENUS;

    /// <summary>
    /// Gets or sets the type of gamestrings used in the data (e.g., "RawText", "ColoredText").
    /// </summary>
    [JsonPropertyName("textType")]
    public GameStringTextType? GameStringTextType { get; set; }

    /// <summary>
    /// Gets or sets the properties for the constant variables used in the gamestrings.
    /// </summary>
    public ConstantVars ConstantVars { get; set; } = new();

    /// <summary>
    /// Gets or sets the properties for the style variables used in the gamestrings.
    /// </summary>
    public StyleVars StyleVars { get; set; } = new();
}
