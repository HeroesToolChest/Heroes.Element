namespace Heroes.Element.Models.Meta;

/// <summary>
/// Contains properties about the gamestring json file.
/// </summary>
public class MetaGameStringProperties : MetaProperties
{
    /// <summary>
    /// Gets or sets the type(s) of data contained in the file (e.g., "HeroData", "AnnouncerData").
    /// </summary>
    public SortedSet<string> DataTypes { get; set; } = new(StringComparer.Ordinal);

    /// <summary>
    /// Gets or sets the description text for the gamestrings.
    /// </summary>
    public DescriptionText DescriptionText { get; set; } = new();
}
