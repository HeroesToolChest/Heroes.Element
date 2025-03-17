using Heroes.Element.Comparers;

namespace Heroes.Element.Models;

/// <summary>
/// Contains the hero data.
/// </summary>
[DebuggerDisplay("{Id,nq}")]
public class Hero : Unit, IHeroesCollectionObject, IInfoText
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Hero"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Hero(string id)
        : base(id)
    {
    }

    /// <inheritdoc/>
    [JsonPropertyOrder(-100)]
    public TooltipDescription? SortName { get; set; }

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
    public TooltipDescription? Title { get; set; }

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
    public TooltipDescription? Difficulty { get; set; }

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
    /// Gets a unique collection roles of the hero, multiclass will be first if hero has multiple roles.
    /// </summary>
    [JsonPropertyOrder(-9)]
    public ISet<TooltipDescription> Roles { get; } = new HashSet<TooltipDescription>(new TooltipDescriptionEqualityComparer());

    /// <summary>
    /// Gets or sets the expanded role of the hero.
    /// </summary>
    [JsonPropertyOrder(-8)]
    public TooltipDescription? ExpandedRole { get; set; }

    /// <summary>
    /// Gets or sets the ratings of the hero.
    /// </summary>
    [JsonPropertyOrder(-7)]
    public HeroRatings Ratings { get; set; } = new HeroRatings();

    /// <summary>
    /// Gets or sets the hero portraits.
    /// </summary>
    [JsonPropertyOrder(-6)]
    public new HeroPortrait Portraits { get; set; } = new HeroPortrait();

    /// <summary>
    /// Gets a unique collection of <see cref="HeroSkin"/> ids that are associated with this hero.
    /// </summary>
    [JsonPropertyOrder(-5)]
    public ISet<string> SkinIds { get; } = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Gets a unique collection of <see cref="HeroSkin"/> ids that are associated with this hero.
    /// </summary>
    [JsonPropertyOrder(-4)]
    public ISet<string> VariationSkinIds { get; } = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Gets a unique colection of <see cref="VoiceLine"/> ids that are associated with this hero.
    /// </summary>
    [JsonPropertyOrder(-3)]
    public ISet<string> VoiceLineIds { get; } = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Gets a unique collection of <see cref="Mount.MountCategory"/> ids that this hero is allowed to use.
    /// </summary>
    [JsonPropertyOrder(-2)]
    public ISet<string> MountCategoryIds { get; } = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Gets or sets the info text of the unit.
    /// </summary>
    [JsonPropertyOrder(105)]
    public TooltipDescription? InfoText { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public TooltipDescription? SearchText { get; set; }

    /// <summary>
    /// Gets a collection of <see cref="Unit"/>s by their id which represents other hero type units that this hero has control over.
    /// </summary>
    public IDictionary<string, Unit> HeroUnits { get; } = new Dictionary<string, Unit>(StringComparer.OrdinalIgnoreCase);

    internal void SetUnitData(Unit unit)
    {
        Name = unit.Name;
        Description = unit.Description;
        InnerRadius = unit.InnerRadius;
        Radius = unit.Radius;
        Sight = unit.Sight;
        Speed = unit.Speed;
        KillXP = unit.KillXP;
        DamageType = unit.DamageType;
        ScalingLinkIds.UnionWith(unit.ScalingLinkIds);
        Life = unit.Life;
        Energy = unit.Energy;
        Shield = unit.Shield;
        unit.Armor.ToList().ForEach(x => Armor[x.Key] = x.Value);
        HeroPlayStyles.UnionWith(unit.HeroPlayStyles);
        unit.Weapons.ToList().ForEach(Weapons.Add);
        Attributes.UnionWith(unit.Attributes);
        UnitIds.UnionWith(unit.UnitIds);
        unit.Abilities.ToList().ForEach(x => Abilities[x.Key] = x.Value);
        ParentLink = unit.ParentLink;
        Gender = unit.Gender;

        Portraits.MiniMapIcon = unit.Portraits.MiniMapIcon;
        Portraits.TargetInfoPanel = unit.Portraits.TargetInfoPanel;
        Portraits.MiniMapIconPath = unit.Portraits.MiniMapIconPath;
        Portraits.TargetInfoPanelPath = unit.Portraits.TargetInfoPanelPath;
    }
}
