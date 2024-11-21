namespace Heroes.Element.Models;

/// <summary>
/// Contains the bundle data.
/// </summary>
public class Bundle : HeroesCollectionObject, IFranchise
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Bundle"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Bundle(string id)
        : base(id)
    {
    }

    /// <inheritdoc/>
    public Franchise? Franchise { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is dynamic content.
    /// </summary>
    public bool IsDynamicContent { get; set; }

    /// <summary>
    /// Gets a unique collection of hero ids that are in this bundle.
    /// </summary>
    [JsonPropertyName("heroes")]
    public ISet<string> HeroIds { get; } = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Gets a collection of hero skins id by their hero id.
    /// </summary>
    [JsonPropertyName("skins")]
    [JsonConverter(typeof(DictionaryStringHashSetStringConverter))]
    public IDictionary<string, SortedSet<string>> HeroSkinsByHeroId { get; } = new SortedDictionary<string, SortedSet<string>>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Gets a unique collection of mount ids that are in this bundle.
    /// </summary>
    [JsonPropertyName("mounts")]
    public ISet<string> MountIds { get; } = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Gets or sets the bundle image.
    /// </summary>
    public string? Image { get; set; }

    internal string? ImagePath { get; set; }

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
    public string? LootChestBonus { get; set; }
}
