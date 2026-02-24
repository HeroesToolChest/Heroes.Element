namespace Heroes.Element.Models.Meta;

/// <summary>
/// Contains properties about the gamestring json file.
/// </summary>
public class MetaGameStringProperties : MetaProperties
{
    /// <summary>
    /// Gets or sets the type(s) of data contained in the file (e.g., "HeroData", "AnnouncerData").
    /// </summary>
    [JsonPropertyName(Constants.MetaDataTypesPropertyName)]
    public SortedSet<DataType> DataTypes { get; set; } = [];

    /// <summary>
    /// Gets or sets the map name associated with the gamestrings, if applicable (e.g., "Alterac Pass", "Battlefield of Eternity").
    /// </summary>
    public string? MapName { get; set; }

    /// <summary>
    /// Gets or sets the description text for the gamestrings.
    /// </summary>
    [JsonPropertyName(Constants.DescriptionTextPropertyName)]
    public DescriptionText DescriptionText { get; set; } = new();
}
