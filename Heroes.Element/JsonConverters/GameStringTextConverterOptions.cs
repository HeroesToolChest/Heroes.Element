namespace Heroes.Element.JsonConverters;

/// <summary>
/// The options for the <see cref="GameStringTextConverter"/> to determine how to convert <see cref="GameStringText"/> to and from JSON.
/// </summary>
public class GameStringTextConverterOptions
{
    /// <summary>
    /// Gets or sets the localization of the text (only for reading). Defaults to <see cref="StormLocale.ENUS"/>.
    /// </summary>
    public StormLocale StormLocale { get; set; } = StormLocale.ENUS;

    /// <summary>
    /// Gets or sets the type of text to convert to (only for writing). Defaults to <see cref="GameStringTextType.RawText"/>.
    /// </summary>
    public GameStringTextType GameStringTextType { get; set; } = GameStringTextType.RawText;

    /// <summary>
    /// Gets or sets the mode for removing <c>hlt-name</c> attributes for constant tags (only for writing). Defaults to <see cref="GameStringTextHltRemoveMode.None"/>.
    /// </summary>
    public GameStringTextHltRemoveMode RemoveHltForConstantTags { get; set; } = GameStringTextHltRemoveMode.None;

    /// <summary>
    /// Gets or sets the mode for removing <c>hlt-name</c> attributes for style tags (only for writing). Defaults to <see cref="GameStringTextHltRemoveMode.None"/>.
    /// </summary>
    public GameStringTextHltRemoveMode RemoveHltForStyleTags { get; set; } = GameStringTextHltRemoveMode.None;
}