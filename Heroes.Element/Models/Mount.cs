namespace Heroes.Element.Models;

/// <summary>
/// Contains the mount data.
/// </summary>
public class Mount : LoadoutItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Mount"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Mount(string id)
        : base(id)
    {
        Rarity = Types.Rarity.Common;
    }

    /// <summary>
    /// Gets or sets the type of mount category.
    /// </summary>
    [JsonPropertyName("type")]
    public string? MountCategory { get; set; }

    /// <summary>
    /// Gets or sets a unique collection of <see cref="Mount"/> ids. This are usually just texture variations of the same model.
    /// </summary>
    public ISet<string> VariationMountIds { get; set; } = new SortedSet<string>(StringComparer.Ordinal);
}
