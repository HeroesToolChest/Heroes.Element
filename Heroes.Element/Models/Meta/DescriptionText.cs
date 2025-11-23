namespace Heroes.Element.Models.Meta;

/// <summary>
/// Contains information about the gamestring text.
/// </summary>
public class DescriptionText
{
    /// <summary>
    /// Gets or sets the locale of the data (e.g., "enUS", "frFR").
    /// </summary>
    public StormLocale Locale { get; set; } = StormLocale.ENUS;

    /// <summary>
    /// Gets or sets the type of gamestrings used in the data (e.g., "RawText", "ColoredText").
    /// </summary>
    public GameStringTextType? GameStringTextType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to replace font styles variables in the gamestring text with the actual hex values.
    /// </summary>
    public bool ReplaceFontStyles { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to preserve font style constant variables in the gamestring text with a new attribute "hlt-name".
    /// <see cref="ReplaceFontStyles"/> must be <see langword="true"/> for this to be enabled.
    /// </summary>
    public bool PreserveFontStyleConstantVars { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to preserve font style variables in the gamestring text with a new attribute "hlt-name".
    /// <see cref="ReplaceFontStyles"/> must be <see langword="true"/> for this to be enabled.
    /// </summary>
    public bool PreserveFontStyleVars { get; set; }
}
