namespace Heroes.Element.Models.Meta;

/// <summary>
/// Contains information about the gamestring text.
/// </summary>
public class GameStringTextProperties : IEquatable<GameStringTextProperties>
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

    /// <inheritdoc/>
    public bool Equals(GameStringTextProperties? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Locale == other.Locale &&
               GameStringTextType == other.GameStringTextType &&
               ConstantVars.Replaced == other.ConstantVars.Replaced &&
               ConstantVars.Preserved == other.ConstantVars.Preserved &&
               StyleVars.Replaced == other.StyleVars.Replaced &&
               StyleVars.Preserved == other.StyleVars.Preserved;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as GameStringTextProperties);

    /// <inheritdoc/>
    public override int GetHashCode() =>
        HashCode.Combine(
            Locale,
            GameStringTextType,
            ConstantVars.Replaced,
            ConstantVars.Preserved,
            StyleVars.Replaced,
            StyleVars.Preserved);
}
