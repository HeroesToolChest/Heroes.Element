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
    /// Gets or sets the unit id.
    /// </summary>
    [JsonPropertyOrder(-99)]
    public string? UnitId { get; set; }

    /// <summary>
    /// Gets or sets the gender of the unit.
    /// </summary>
    public Gender? Gender { get; set; }

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
    /// Gets or sets the damage type of this unit.
    /// </summary>
    public string? DamageType { get; set; }

    /// <summary>
    /// Gets or sets the kill xp.
    /// </summary>
    public int? KillXP { get; set; }

    /// <summary>
    /// Gets or sets the scaling link id.
    /// </summary>
    public string? ScalingLinkId { get; set; }

    /// <summary>
    /// Gets or sets the parent link of this unit.
    /// </summary>
    public string? ParentLink { get; set; }

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
    /// Gets a collection of unit armor.
    /// </summary>
    public ISet<UnitArmor> Armor { get; } = new HashSet<UnitArmor>();

    /// <summary>
    /// Gets or sets the info text of the unit.
    /// </summary>
    public TooltipDescription? InfoText { get; set; }

    /// <summary>
    /// Gets or sets the unit portraits.
    /// </summary>
    public UnitPortrait UnitPortrait { get; set; } = new UnitPortrait();

    /// <summary>
    /// Gets a collection of the hero play styles.
    /// </summary>
    public ISet<string> HeroDescriptors { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Gets a collection of basic attack weapons.
    /// </summary>
    public ISet<UnitWeapon> Weapons { get; } = new HashSet<UnitWeapon>();

    /// <summary>
    /// Gets a collection of attributes.
    /// </summary>
    public ISet<string> Attributes { get; } = new HashSet<string>();

    /// <summary>
    /// Gets a collection of additional units associated with this unit.
    /// </summary>
    public ISet<string> UnitIds { get; } = new HashSet<string>();

    // TODO: add abilities
}
