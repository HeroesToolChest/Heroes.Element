namespace Heroes.Element.Models;

/// <summary>
/// Contains the unit data.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class Unit : ElementObject, IName, IDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Unit"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Unit(string id)
        : base(id)
    {
    }

    /// <inheritdoc/>
    [JsonPropertyOrder(-110)]
    public GameStringText? Name { get; set; }

    /// <summary>
    /// Gets or sets the gender of the unit.
    /// </summary>
    [JsonPropertyOrder(-20)]
    public Gender? Gender { get; set; }

    /// <summary>
    /// Gets or sets the damage type of this unit. This value is the <see cref="ArmorSet"/>.
    /// </summary>
    [JsonPropertyOrder(-19)]
    public ArmorSet? DamageType { get; set; }

    /// <summary>
    /// Gets or sets the size of the radius.
    /// </summary>
    [JsonPropertyOrder(-18)]
    public double Radius { get; set; }

    /// <summary>
    /// Gets or sets the size of the inner radius.
    /// </summary>
    [JsonPropertyOrder(-17)]
    public double InnerRadius { get; set; }

    /// <summary>
    /// Gets or sets the distance of the sight.
    /// </summary>
    [JsonPropertyOrder(-16)]
    public double Sight { get; set; }

    /// <summary>
    /// Gets or sets the value of the speed.
    /// </summary>
    [JsonPropertyOrder(-15)]
    public double Speed { get; set; }

    /// <summary>
    /// Gets or sets the kill xp.
    /// </summary>
    [JsonPropertyOrder(-14)]
    public int? KillXP { get; set; }

    /// <summary>
    /// Gets or sets a collection of attributes.
    /// </summary>
    [JsonPropertyOrder(-13)]
    public ISet<string> Attributes { get; set; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets or sets a collection of scaling link ids. This is the <see cref="BehaviorVeterancy"/>.
    /// </summary>
    [JsonPropertyOrder(-12)]
    public ISet<string> ScalingLinkIds { get; set; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public GameStringText? Description { get; set; }

    /// <summary>
    /// Gets or sets the Life properties.
    /// </summary>
    [JsonPropertyOrder(112)]
    public UnitLife Life { get; set; } = new UnitLife();

    /// <summary>
    /// Gets or sets the Energy properties.
    /// </summary>
    [JsonPropertyOrder(113)]
    public UnitEnergy Energy { get; set; } = new UnitEnergy();

    /// <summary>
    /// Gets or sets the Shield properties.
    /// </summary>
    [JsonPropertyOrder(114)]
    public UnitShield Shield { get; set; } = new UnitShield();

    /// <summary>
    /// Gets or sets a collection of unit armor by the type of armor set.
    /// </summary>
    [JsonPropertyOrder(115)]
    public IDictionary<ArmorSet, UnitArmor> Armor { get; set; } = new SortedDictionary<ArmorSet, UnitArmor>();

    /// <summary>
    /// Gets or sets a collection of the hero play styles.
    /// </summary>
    [JsonPropertyOrder(116)]
    [JsonPropertyName("playstyles")]
    public ISet<string> HeroPlayStyles { get; set; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets or sets the unit portraits.
    /// </summary>
    [JsonPropertyOrder(117)]
    [JsonPropertyName("portraits")]
    public virtual UnitPortrait UnitPortraits { get; set; } = new UnitPortrait();

    /// <summary>
    /// Gets or sets a collection of summoned units associated with this unit.
    /// </summary>
    [JsonPropertyOrder(118)]
    public virtual ISet<string> SummonedUnitIds { get; set; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets or sets a collection of basic attack weapons.
    /// </summary>
    [JsonPropertyOrder(119)]
    public IList<UnitWeapon> Weapons { get; set; } = [];

    /// <summary>
    /// Gets or sets a collection of abilities by their <see cref="AbilityTier"/>.
    /// </summary>
    [JsonPropertyOrder(200)]
    [JsonConverter(typeof(UnitAbilitiesConverter))]
    public IDictionary<AbilityTier, IList<Ability>> Abilities { get; set; } = new SortedDictionary<AbilityTier, IList<Ability>>();

    /// <summary>
    /// Gets or sets a collection of subabilities by their parent's ability's <see cref="LinkId"/>.
    /// </summary>
    [JsonPropertyOrder(201)]
    [JsonConverter(typeof(UnitSubAbilitiesConverter))]
    public IDictionary<LinkId, IDictionary<AbilityTier, IList<Ability>>> SubAbilities { get; set; } = new SortedDictionary<LinkId, IDictionary<AbilityTier, IList<Ability>>>(new LinkIdComparer());

    /// <summary>
    /// Gets the layout ability type by name id, this is for keeping track of the ability types by the ability id.
    /// There may be duplicates of the ability id, but we will only track the first one.
    /// This has all abilities including sub abilities.
    /// </summary>
    internal Dictionary<string, AbilityType> LayoutAbilityTypeByNameId { get; } = [];

    /// <summary>
    /// Gets the unknown subabilities, this is for when we don't know the parent ability id yet, most likely for talent abilities or other subabilities.
    /// </summary>
    internal SortedDictionary<AbilityTier, List<Ability>> UnknownSubAbilities { get; } = [];

    /// <summary>
    /// Gets the ability tooltip appenders. For a talent element id, we will have a list of abilities (includes subabilities) that the talent affects.
    /// </summary>
    internal Dictionary<string, List<Ability>> AbilitiesByTooltipTalentElementId { get; } = new(StringComparer.Ordinal);
}
