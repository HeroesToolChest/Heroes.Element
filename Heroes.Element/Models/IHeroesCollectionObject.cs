namespace Heroes.Element.Models;

/// <summary>
/// An interface for a element object found in the in-game store collection.
/// </summary>
public interface IHeroesCollectionObject
{
    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    TooltipDescription? Name { get; set; }

    /// <summary>
    /// Gets or sets the sort name.
    /// </summary>
    TooltipDescription? SortName { get; set; }

    /// <summary>
    /// Gets or sets the attribute id.
    /// </summary>
    string? AttributeId { get; set; }

    /// <summary>
    /// Gets or sets the description text.
    /// </summary>
    TooltipDescription? Description { get; set; }

    /// <summary>
    /// Gets or sets the hyperlink id.
    /// </summary>
    string? HyperlinkId { get; set; }

    /// <summary>
    /// Gets or sets the release date.
    /// </summary>
    DateOnly? ReleaseDate { get; set; }

    /// <summary>
    /// Gets or sets the rarity.
    /// </summary>
    Rarity? Rarity { get; set; }

    /// <summary>
    /// Gets or sets the type of collection category this belongs to, if any.
    /// </summary>
    string? CollectionCategory { get; set; }

    /// <summary>
    /// Gets or sets the event name associated with this object.
    /// </summary>
    string? EventName { get; set; }

    /// <summary>
    /// Gets a unique collection of search texts.
    /// </summary>
    ISet<string> SearchTexts { get; }
}
