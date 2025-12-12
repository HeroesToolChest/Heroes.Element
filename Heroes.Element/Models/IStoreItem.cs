namespace Heroes.Element.Models;

/// <summary>
/// An interface for an in-game store collection item.
/// </summary>
public interface IStoreItem : IName, IHyperlinkId, IFranchise, IRarity, IEventName, IDescription
{
    /// <summary>
    /// Gets or sets the sort name.
    /// </summary>
    GameStringText? SortName { get; set; }

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
    GameStringText? SearchText { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this item should be shown in the store.
    /// </summary>
    bool IsShownInStore { get; set; }
}
