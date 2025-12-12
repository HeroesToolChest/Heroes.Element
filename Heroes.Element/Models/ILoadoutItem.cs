namespace Heroes.Element.Models;

/// <summary>
/// An interface for a a loadout item.
/// </summary>
public interface ILoadoutItem : IStoreItem
{
    /// <summary>
    /// Gets or sets the attribute id.
    /// </summary>
    string? AttributeId { get; set; }
}
