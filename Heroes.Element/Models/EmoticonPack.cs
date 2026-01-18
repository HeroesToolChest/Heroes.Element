namespace Heroes.Element.Models;

/// <summary>
/// Contains the emoticon pack data.
/// </summary>
public class EmoticonPack : StoreItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmoticonPack"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public EmoticonPack(string id)
        : base(id)
    {
        Rarity = Types.Rarity.Common;
    }

    /// <summary>
    /// Gets or sets a unique collection of <see cref="Emoticon"/> ids that are in this emoticon pack.
    /// </summary>
    public ISet<string> EmoticonIds { get; set; } = new HashSet<string>();
}
