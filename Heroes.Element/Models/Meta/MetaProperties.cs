namespace Heroes.Element.Models.Meta;

/// <summary>
/// Contains properties about a json file.
/// </summary>
public class MetaProperties
{
    /// <summary>
    /// Gets or sets the Heroes of the Storm version.
    /// </summary>
    [JsonPropertyOrder(-999)]
    public string HeroesVersion { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the version of the HDP (Heroes Data Parser) used to generate the data.
    /// </summary>
    [JsonPropertyOrder(-998)]
    public string HdpVersion { get; set; } = string.Empty;
}
