namespace Heroes.Element.Models.Meta;

/// <summary>
/// Contains properties about the data itself.
/// </summary>
public class MetaProperties
{
    /// <summary>
    /// Gets or sets the type of data contained in the file (e.g., "HeroData", "AnnouncerData").
    /// </summary>
    public string DataType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the version of the Heroes Data.
    /// </summary>
    public HeroesDataVersion Version { get; set; } = new(-1, -1, -1, -1, true);

    /// <summary>
    /// Gets or sets a value indicating whether the game strings have been removed (and are in a separate file).
    /// </summary>
    public bool IsLocalizedText { get; set; }

    /// <summary>
    /// Gets or sets the description text information for the data, if applicable. Will be <see langword="null"/> if <see cref="IsLocalizedText"/> is true.
    /// </summary>
    public DescriptionText? DescriptionText { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is data is created from Heroes Data Parser version 4.x or earlier.
    /// </summary>
    [JsonIgnore]
    public bool IsLegacy { get; set; }
}
