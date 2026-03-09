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
    /// Gets or sets a value indicating whether to replace font constant variables in the gamestring text with the actual color text hex values.
    /// </summary>
    public bool ReplaceFontConstantVars { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to replace font styles variables in the gamestring text with the actual color text hex values.
    /// </summary>
    public bool ReplaceFontStylesVars { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to preserve font constant variables in the gamestring text with a new attribute "hlt-name".
    /// <see cref="ReplaceFontConstantVars"/> must be <see langword="true"/> for this to be enabled.
    /// </summary>
    public bool PreserveFontStyleConstantVars { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to preserve font style variables in the gamestring text with a new attribute "hlt-name".
    /// <see cref="ReplaceFontStylesVars"/> must be <see langword="true"/> for this to be enabled.
    /// </summary>
    public bool PreserveFontStyleVars { get; set; }
}
