namespace Heroes.Element.Models;

/// <summary>
/// An interface for an element object found in the in-game store collection.
/// </summary>
public interface IHeroesCollectionObject : IName, IHyperlinkId, IRarity, IEventName, IDescription
{
    /// <summary>
    /// Gets or sets the sort name.
    /// </summary>
    string? SortName { get; set; }

    /// <summary>
    /// Gets or sets the attribute id.
    /// </summary>
    string? AttributeId { get; set; }

    /// <summary>
    /// Gets or sets the release date.
    /// </summary>
    DateOnly? ReleaseDate { get; set; }

    /// <summary>
    /// Gets or sets the type of collection category this belongs to, if any.
    /// </summary>
    string? Category { get; set; }

    /// <summary>
    /// Gets or sets the search texts (should be space delimited).
    /// </summary>
    string? SearchText { get; set; }
}
