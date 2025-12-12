namespace Heroes.Element.Models;

/// <summary>
/// Contains the bundle data.
/// </summary>
public class Bundle : StoreItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Bundle"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Bundle(string id)
        : base(id)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether this is dynamic content.
    /// </summary>
    public bool IsDynamicContent { get; set; }

    /// <summary>
    /// Gets or sets a unique collection of hero ids that are in this bundle.
    /// </summary>
    public ISet<string> HeroIds { get; set; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets or sets a collection of hero skins id by their hero id.
    /// </summary>
    [JsonPropertyName("skinIds")]
    public IDictionary<string, ISet<string>> HeroSkinIdsByHeroId { get; set; } = new SortedDictionary<string, ISet<string>>(StringComparer.Ordinal);

    /// <summary>
    /// Gets or sets a unique collection of mount ids that are in this bundle.
    /// </summary>
    public ISet<string> MountIds { get; set; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets or sets the bundle image.
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// Gets or sets the boost id that is in this bundle.
    /// </summary>
    [JsonPropertyName("boostId")]
    public string? BoostBonusId { get; set; }

    /// <summary>
    /// Gets or sets the amount of gold in this bundle.
    /// </summary>
    public int? GoldBonus { get; set; }

    /// <summary>
    /// Gets or sets the amount of gems in this bundle.
    /// </summary>
    public int? GemsBonus { get; set; }

    /// <summary>
    /// Gets or sets the loot chest id that is in this bundle.
    /// </summary>
    [JsonPropertyName("lootChestId")]
    public string? LootChestBonus { get; set; }

    /// <summary>
    /// Gets or sets the relative path of the image that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? ImagePath { get; set; }
}
