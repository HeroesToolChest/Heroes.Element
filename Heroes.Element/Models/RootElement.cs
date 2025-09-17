using Heroes.Element.Models.Meta;

namespace Heroes.Element.Models;

/// <summary>
/// Contains the meta and the items for a specific type of game data.
/// </summary>
/// <typeparam name="TElement">The type of game data.</typeparam>
public class RootElement<TElement>
    where TElement : IElementObject
{
    /// <summary>
    /// Gets or sets the meta properties.
    /// </summary>
    public MetaProperties Meta { get; set; } = new MetaProperties();

    /// <summary>
    /// Gets or sets the items, sorted by their unique identifier.
    /// </summary>
    public SortedDictionary<string, TElement> Items { get; set; } = new SortedDictionary<string, TElement>(StringComparer.Ordinal);
}
