namespace Heroes.Element.Models;

/// <summary>
/// Contains the skin data.
/// </summary>
public class Skin : LoadoutItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Skin"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Skin(string id)
        : base(id)
    {
        Rarity = Types.Rarity.Common;
        Franchise = Types.Franchise.Starcraft;
    }

    /// <summary>
    /// Gets or sets a unique collection of features.
    /// </summary>
    public ISet<string> Features { get; set; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets or sets a unique collection of <see cref="Skin"/> ids. This are usually just texture variations of the same model.
    /// </summary>
    public ISet<string> VariationSkinIds { get; set; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets or sets a unique collection of <see cref="VoiceLine"/> ids that are associated with this skin.
    /// </summary>
    public ISet<string> VoiceLineIds { get; set; } = new SortedSet<string>(StringComparer.Ordinal);
}
