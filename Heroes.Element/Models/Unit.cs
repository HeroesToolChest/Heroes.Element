namespace Heroes.Element.Models;

/// <summary>
/// Contains the unit data.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class Unit : ElementObject, IName, IDescription
{
    // for keeping track of the ability types by the ability id
    // there may be duplicates of the ability id, but we will only track the first one
    // this has all abilities including sub abilities
    private readonly Dictionary<string, AbilityType> _layoutAbilityTypeByNameId = [];

    private readonly SortedDictionary<AbilityTier, List<Ability>> _abilities = [];
    private readonly Dictionary<LinkId, SortedDictionary<AbilityTier, List<Ability>>> _subAbilities = [];

    // for unknown sub abilities, this is for when we don't know the parent ability id yet, most likely for talent abilities
    private readonly SortedDictionary<AbilityTier, List<Ability>> _unknownSubAbilities = [];

    // for the ability tooltip appenders. For a talent element id, we will have a list of abilities (includes subabilities) that the talent affects
    private readonly Dictionary<string, List<Ability>> _abilitiesByTooltipTalentElementId = new(StringComparer.Ordinal);

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
    public TooltipDescription? Name { get; set; }

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
    /// Gets a collection of attributes.
    /// </summary>
    [JsonPropertyOrder(-13)]
    public ISet<string> Attributes { get; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets the scaling link ids. This is the <see cref="BehaviorVeterancy"/>.
    /// </summary>
    [JsonPropertyOrder(-12)]
    public ISet<string> ScalingLinkIds { get; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public TooltipDescription? Description { get; set; }

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
    /// Gets the unit armor by the type of armor set.
    /// </summary>
    [JsonPropertyOrder(115)]
    public IDictionary<ArmorSet, UnitArmor> Armor { get; } = new SortedDictionary<ArmorSet, UnitArmor>();

    /// <summary>
    /// Gets a collection of the hero play styles.
    /// </summary>
    [JsonPropertyOrder(116)]
    [JsonPropertyName("playstyles")]
    public ISet<string> HeroPlayStyles { get; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets or sets the unit portraits.
    /// </summary>
    [JsonPropertyOrder(117)]
    [JsonPropertyName("portraits")]
    public virtual UnitPortrait UnitPortraits { get; set; } = new UnitPortrait();

    /// <summary>
    /// Gets a collection of summoned units associated with this unit.
    /// </summary>
    [JsonPropertyOrder(118)]
    public virtual ISet<string> SummonedUnitIds => _abilities
        .SelectMany(x => x.Value)
            .SelectMany(x => x.SummonedUnitIds)
        .Concat(_subAbilities
            .SelectMany(x => x.Value)
                .SelectMany(y => y.Value)
                    .SelectMany(x => x.SummonedUnitIds))
        .Order()
        .ToHashSet(StringComparer.Ordinal);

    /// <summary>
    /// Gets a collection of basic attack weapons.
    /// </summary>
    [JsonPropertyOrder(119)]
    public ICollection<UnitWeapon> Weapons { get; } = [];

    /// <summary>
    /// Gets a collection of abilities by their <see cref="AbilityTier"/>.
    /// </summary>
    [JsonPropertyOrder(200)]
    public IReadOnlyDictionary<AbilityTier, IReadOnlyList<Ability>> Abilities => _abilities.ToDictionary(
        x => x.Key,
        x => (IReadOnlyList<Ability>)[.. x.Value.OrderBy(x => x.AbilityType)]);

    /// <summary>
    /// Gets a collection of subabilities by their parent's ability's <see cref="LinkId"/>.
    /// A subability becomes available after the parent's ability is used.
    /// </summary>
    [JsonPropertyOrder(201)]
    public IReadOnlyDictionary<LinkId, IReadOnlyDictionary<AbilityTier, IReadOnlyList<Ability>>> SubAbilities => _subAbilities.ToDictionary(
        outerKvp => outerKvp.Key,
        outerKvp => (IReadOnlyDictionary<AbilityTier, IReadOnlyList<Ability>>)outerKvp.Value.ToDictionary(
            innerKvp => innerKvp.Key,
            innerKvp => (IReadOnlyList<Ability>)[.. innerKvp.Value.OrderBy(x => x.AbilityType)]));

    /// <summary>
    /// Gets or sets the parent link of this unit.
    /// </summary>
    [JsonIgnore]
    public string? ParentLink { get; set; }

    internal int TooltipTalentElementIdCount => _abilitiesByTooltipTalentElementId.Count;

    internal SortedDictionary<AbilityTier, List<Ability>> UnknownSubAbilities => _unknownSubAbilities;

    /// <summary>
    /// Adds an ability.
    /// </summary>
    /// <param name="ability">The <see cref="Ability"/>.</param>
    /// <returns><see langword="true"/> if the ability was added, otherwise <see langword="false"/>.</returns>
    public bool AddAbility(Ability ability)
    {
        _layoutAbilityTypeByNameId.TryAdd(ability.AbilityElementId, ability.AbilityType);

        if (_abilities.TryGetValue(ability.Tier, out List<Ability>? abilities))
        {
            abilities.Add(ability);

            return true;
        }
        else
        {
            _abilities[ability.Tier] = [ability];

            return true;
        }
    }

    /// <summary>
    /// Adds a sub ability if the parent ability was found as an existing ability or subability.
    /// </summary>
    /// <param name="subAbility">The <see cref="Ability"/>.</param>
    /// <returns><see langword="true"/> if the subability was added, otherwise <see langword="false"/>.</returns>
    public bool AddSubAbility(Ability subAbility)
    {
        // no parent ability id, so no sub ability
        if (string.IsNullOrEmpty(subAbility.ParentAbilityElementId))
            return false;

        _layoutAbilityTypeByNameId.TryAdd(subAbility.AbilityElementId, subAbility.AbilityType);

        // check both abilites and sub abilities
        IEnumerable<Ability> matchingAbilities = _abilities
            .SelectMany(x => x.Value)
            .Where(x => x.AbilityElementId == subAbility.ParentAbilityElementId)
            .Concat(_subAbilities
                .SelectMany(x => x.Value)
                .SelectMany(y => y.Value)
                .Where(x => x.AbilityElementId == subAbility.ParentAbilityElementId));

        if (!matchingAbilities.Any())
        {
            if (UnknownSubAbilities.TryGetValue(subAbility.Tier, out List<Ability>? unknownSubAbilities))
                unknownSubAbilities.Add(subAbility);
            else
                UnknownSubAbilities[subAbility.Tier] = [subAbility];

            return false;
        }

        foreach (Ability ability in matchingAbilities)
        {
            AssignSubAbilityToLink(subAbility, ability.LinkId);
        }

        return true;
    }

    internal void AssignSubAbilityToLink(Ability subAbility, LinkId linkId)
    {
        if (_subAbilities.TryGetValue(linkId, out SortedDictionary<AbilityTier, List<Ability>>? subAbilities))
        {
            if (subAbilities.TryGetValue(subAbility.Tier, out List<Ability>? abilities))
                abilities.Add(subAbility);
            else
                subAbilities[subAbility.Tier] = [subAbility];
        }
        else
        {
            _subAbilities[linkId] = new SortedDictionary<AbilityTier, List<Ability>>()
            {
                [subAbility.Tier] = [subAbility],
            };
        }
    }

    /// <summary>
    /// Returns a value indicating whether the ability type was found by the name id. Based on the layout buttons.
    /// There may be duplicates of the name id, but we will only get the first one.
    /// </summary>
    /// <param name="nameId">The name id (or ability id).</param>
    /// <param name="abilityType">The <see cref="AbilityType"/>.</param>
    /// <returns><see langword="true"/> if found, otherwise <see langword="false"/>.</returns>
    internal bool GetAbilityTypeByNameId(string nameId, out AbilityType abilityType)
    {
        return _layoutAbilityTypeByNameId.TryGetValue(nameId, out abilityType);
    }

    /// <summary>
    /// Adds an ability by their tooltip talent element id.
    /// </summary>
    /// <param name="talentElementId">The talent element id.</param>
    /// <param name="ability">The <see cref="Ability"/> affected by the talent.</param>
    internal void AddAbilityByTooltipTalentElementId(string talentElementId, Ability ability)
    {
        if (_abilitiesByTooltipTalentElementId.TryGetValue(talentElementId, out List<Ability>? abilities))
        {
            abilities.Add(ability);
        }
        else
        {
            _abilitiesByTooltipTalentElementId[talentElementId] = [ability];
        }
    }

    /// <summary>
    /// Gets a collection of <see cref="AbilityLinkId"/>s associated with the talent element id.
    /// Will only return abilities that are in either abilities or subabilities.
    /// </summary>
    /// <param name="talentElementId">The talent element id.</param>
    /// <returns>A collection of <see cref="LinkId"/>s.</returns>
    internal List<AbilityLinkId> GetTooltipAbilityLinkIdsByTalentElementId(string talentElementId)
    {
        if (_abilitiesByTooltipTalentElementId.TryGetValue(talentElementId, out List<Ability>? abilities))
        {
            return [.. abilities
                .Where(ability =>
                    _abilities.Values.Any(x => x.Contains(ability)) ||
                    _subAbilities.Values.Any(x => x.Values.Any(x => x.Contains(ability))))
                .Select(x => x.LinkId)];
        }
        else
        {
            return [];
        }
    }
}
