namespace Heroes.Element.Models;

/// <summary>
/// Contains the unit data.
/// </summary>
public class Unit : HeroesCollectionObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Unit"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Unit(string id)
        : base(id)
    {
    }

    /// <summary>
    /// Gets or sets the size of the inner radius.
    /// </summary>
    public double InnerRadius { get; set; }

    /// <summary>
    /// Gets or sets the size of the radius.
    /// </summary>
    public double Radius { get; set; }

    /// <summary>
    /// Gets or sets the distance of the sight.
    /// </summary>
    public double Sight { get; set; }

    /// <summary>
    /// Gets or sets the value of the speed.
    /// </summary>
    public double Speed { get; set; }

    /// <summary>
    /// Gets or sets the kill xp.
    /// </summary>
    public int? KillXP { get; set; }

    /// <summary>
    /// Gets or sets the damage type of this unit. This value is the <see cref="ArmorSet"/>.
    /// </summary>
    public ArmorSet? DamageType { get; set; }

    /// <summary>
    /// Gets the scaling link ids. This is the <see cref="BehaviorVeterancy"/>.
    /// </summary>
    public ISet<string> ScalingLinkIds { get; } = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Gets or sets the Life properties.
    /// </summary>
    public UnitLife Life { get; set; } = new UnitLife();

    /// <summary>
    /// Gets or sets the Energy properties.
    /// </summary>
    public UnitEnergy Energy { get; set; } = new UnitEnergy();

    /// <summary>
    /// Gets or sets the Shield properties.
    /// </summary>
    public UnitShield Shield { get; set; } = new UnitShield();

    /// <summary>
    /// Gets the unit armor by the type of armor set.
    /// </summary>
    public IDictionary<ArmorSet, UnitArmor> Armor { get; } = new SortedDictionary<ArmorSet, UnitArmor>();

    /// <summary>
    /// Gets or sets the info text of the unit.
    /// </summary>
    public TooltipDescription? InfoText { get; set; }

    /// <summary>
    /// Gets or sets the unit portraits.
    /// </summary>
    [JsonPropertyName("portraits")]
    public UnitPortrait UnitPortrait { get; set; } = new UnitPortrait();

    /// <summary>
    /// Gets a collection of the hero play styles.
    /// </summary>
    [JsonPropertyName("playstyles")]
    public ISet<string> HeroPlayStyles { get; } = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Gets or sets a collection of basic attack weapons.
    /// </summary>
    public ICollection<UnitWeapon> Weapons { get; set; } = [];

    /// <summary>
    /// Gets a collection of attributes.
    /// </summary>
    public ISet<string> Attributes { get; } = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Gets a collection of additional units associated with this unit.
    /// </summary>
    public ISet<string> UnitIds { get; } = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

    //// TODO: add abilities

    /// <summary>
    /// Gets or sets the parent link of this unit.
    /// </summary>
    public string? ParentLink { get; set; }
}
