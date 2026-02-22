namespace Heroes.Element.Models;

/// <summary>
/// Contains the meta and the items for a specific type of game data.
/// </summary>
/// <typeparam name="TElement">The type of game data.</typeparam>
public class RootDataElement<TElement>
    where TElement : IElementObject
{
    /// <summary>
    /// Gets or sets the meta properties.
    /// </summary>
    [JsonPropertyName(Constants.RootMetaPropertyName)]
    public MetaDataProperties Meta { get; set; } = new();

    /// <summary>
    /// Gets or sets the items, sorted by their unique identifier.
    /// </summary>
    [JsonPropertyName(Constants.ItemsPropertyName)]
    public SortedDictionary<string, TElement> Items { get; set; } = new(StringComparer.OrdinalIgnoreCase);
}
