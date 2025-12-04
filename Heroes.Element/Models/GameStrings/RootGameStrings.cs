namespace Heroes.Element.Models.GameStrings;

/// <summary>
/// Contains the meta and the gamestrings.
/// </summary>
public class RootGameStrings
{
    /// <summary>
    /// Gets or sets the meta properties.
    /// </summary>
    [JsonPropertyName(Constants.RootMetaPropertyName)]
    public MetaGameStringProperties Meta { get; set; } = new();
}
