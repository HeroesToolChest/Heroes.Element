namespace Heroes.Element.Models;

/// <summary>
/// Contains the portrait pack data.
/// </summary>
public class PortraitPack : StoreItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PortraitPack"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public PortraitPack(string id)
        : base(id)
    {
        Rarity = Types.Rarity.Common;
    }

    /// <summary>
    /// Gets or sets a unique collection of <see cref="RewardPortrait"/> ids in the portrait pack.
    /// </summary>
    public ISet<string> RewardPortraitIds { get; set; } = new SortedSet<string>(StringComparer.Ordinal);
}
