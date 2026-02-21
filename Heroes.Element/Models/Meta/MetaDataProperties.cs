using Heroes.Element.JsonTypeInfoResolvers;

namespace Heroes.Element.Models.Meta;

/// <summary>
/// Contains properties about the data json file.
/// </summary>
public class MetaDataProperties : MetaProperties
{
    /// <summary>
    /// Gets or sets the type of data contained in the file (e.g., "HeroData", "AnnouncerData").
    /// </summary>
    public string DataType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the map name associated with the data, if applicable (e.g., "Alterac Pass", "Battlefield of Eternity").
    /// </summary>
    public string? MapName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating the serialization of the <see cref="GameStringText"/> properties.
    /// </summary>
    public LocalizedTextOption LocalizedText { get; set; } = LocalizedTextOption.None;

    /// <summary>
    /// Gets or sets the description text for the gamestrings. Will be <see langword="null"/> if <see cref="LocalizedText"/> is set to <see cref="LocalizedTextOption.Extract"/>.
    /// </summary>
    [JsonPropertyName(Constants.DescriptionTextPropertyName)]
    public DescriptionText? DescriptionText { get; set; }

    /// <summary>
    /// Gets or sets the total number of items.
    /// </summary>
    public int TotalItems { get; set; }
}
