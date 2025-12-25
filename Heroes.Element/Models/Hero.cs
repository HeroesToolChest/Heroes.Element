namespace Heroes.Element.Models;

/// <summary>
/// Contains the hero data.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class Hero : Unit, ILoadoutItem, IInfoText
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Hero"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Hero(string id)
        : base(id)
    {
        Gender = Types.Gender.Male;
        Franchise = Types.Franchise.Starcraft;
    }

    /// <inheritdoc/>
    [JsonPropertyOrder(-100)]
    public GameStringText? SortName { get; set; }

    /// <summary>
    /// Gets or sets the unit id.
    /// </summary>
    [JsonPropertyOrder(-99)]
    public string? UnitId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-90)]
    public string? HyperlinkId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-80)]
    public string? AttributeId { get; set; }

    /// <summary>
    /// Gets or sets the hero title.
    /// </summary>
    [JsonPropertyOrder(-79)]
    public GameStringText? Title { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-70)]
    public Franchise? Franchise { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-60)]
    public Rarity? Rarity { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-50)]
    public DateOnly? ReleaseDate { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-40)]
    public string? Category { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-30)]
    public string? Event { get; set; }

    /// <summary>
    /// Gets or sets the difficulty of the hero.
    /// </summary>
    [JsonPropertyOrder(-29)]
    public GameStringText? Difficulty { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this hero is melee.
    /// </summary>
    [JsonPropertyOrder(-28)]
    public bool IsMelee { get; set; } = false;

    /// <summary>
    /// Gets or sets the default mount id of this hero.
    /// </summary>
    [JsonPropertyOrder(-10)]
    public string? DefaultMountId { get; set; }

    /// <summary>
    /// Gets or sets a unique collection roles of the hero.
    /// </summary>
    [JsonPropertyOrder(-9)]
    public ISet<GameStringText> Roles { get; set; } = new HashSet<GameStringText>(new GameStringTextEqualityComparer());

    /// <summary>
    /// Gets or sets the expanded role of the hero.
    /// </summary>
    [JsonPropertyOrder(-8)]
    public GameStringText? ExpandedRole { get; set; }

    /// <summary>
    /// Gets or sets the ratings of the hero.
    /// </summary>
    [JsonPropertyOrder(-7)]
    public HeroRatings Ratings { get; set; } = new HeroRatings();

    /// <summary>
    /// Gets or sets the hero portraits.
    /// </summary>
    [JsonPropertyOrder(-6)]
    [JsonPropertyName("portraits")]
    public HeroPortrait HeroPortraits { get; set; } = new HeroPortrait();

    /// <inheritdoc/>
    [JsonIgnore]
    public override UnitPortrait UnitPortraits { get => base.UnitPortraits; set => base.UnitPortraits = value; }

    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public GameStringText? SearchText { get; set; }

    /// <summary>
    /// Gets or sets the info text of the unit.
    /// </summary>
    [JsonPropertyOrder(105)]
    public GameStringText? InfoText { get; set; }

    /// <summary>
    /// Gets or sets a unique collection of <see cref="Skin"/> ids that are associated with this hero.
    /// </summary>
    [JsonPropertyOrder(190)]
    public ISet<string> SkinIds { get; set; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets or sets a unique collection of <see cref="Skin"/> ids that are associated with this hero.
    /// </summary>
    [JsonPropertyOrder(191)]
    public ISet<string> VariationSkinIds { get; set; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets or sets a unique colection of <see cref="VoiceLine"/> ids that are associated with this hero.
    /// </summary>
    [JsonPropertyOrder(192)]
    public ISet<string> VoiceLineIds { get; set; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets or sets a unique collection of <see cref="Mount.MountCategory"/> ids that this hero is allowed to use.
    /// </summary>
    [JsonPropertyOrder(193)]
    public ISet<string> MountCategoryIds { get; set; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets or sets a collection of talents.
    /// </summary>
    [JsonPropertyOrder(220)]
    [JsonConverter(typeof(HeroTalentsConverter))]
    public IDictionary<TalentTier, IList<Talent>> Talents { get; set; } = new SortedDictionary<TalentTier, IList<Talent>>();

    /// <summary>
    /// Gets or sets a collection of <see cref="Unit"/>s by their id which represents other hero type units that this hero has control over.
    /// </summary>
    [JsonPropertyOrder(230)]
    [JsonConverter(typeof(HeroUnitsConverter))]
    public IDictionary<string, Unit> HeroUnits { get; set; } = new Dictionary<string, Unit>(StringComparer.Ordinal);
}
