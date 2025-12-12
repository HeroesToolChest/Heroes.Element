namespace Heroes.Element.Models;

/// <summary>
/// An abstract class for an element object found in a hero loadout.
/// </summary>
public abstract class LoadoutItem : StoreItem, ILoadoutItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoadoutItem"/> class.
    /// </summary>
    /// <param name="id">A unique identifier.</param>
    public LoadoutItem(string id)
        : base(id)
    {
    }

    /// <inheritdoc/>
    [JsonPropertyOrder(-80)]
    public string? AttributeId { get; set; }
}
