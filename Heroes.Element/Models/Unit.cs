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
    private readonly SortedDictionary<LinkId, SortedDictionary<AbilityTier, List<Ability>>> _subAbilities = new(new LinkIdComparer());

    // for unknown subabilities, this is for when we don't know the parent ability id yet, most likely for talent abilities or other subabilities
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

    internal int TooltipTalentElementIdCount => _abilitiesByTooltipTalentElementId.Count;

    internal SortedDictionary<AbilityTier, List<Ability>> UnknownSubAbilities => _unknownSubAbilities;

    /// <summary>
    /// Adds an ability.
    /// </summary>
    /// <param name="ability">The <see cref="Ability"/>.</param>
    public void AddAbility(Ability ability)
    {
        if (_abilities.TryGetValue(ability.Tier, out List<Ability>? abilities))
            abilities.Add(ability);
        else
            _abilities[ability.Tier] = [ability];
    }

    /// <summary>
    /// Adds the ability as a sub ability if the parent ability was found as an existing ability or subability, otherwise adds it to the unknown sub abilities.
    /// </summary>
    /// <param name="ability">The <see cref="Ability"/> to be added.</param>
    public void AddAsSubAbilityToAbility(Ability ability)
    {
        // no parent ability id, so no sub ability
        if (ability.ParentAbilityLinkId is null && string.IsNullOrEmpty(ability.ParentAbilityElementId))
            return;

        IEnumerable<Ability> matchingAbilities;

        // check both abilities and sub abilities, first ParentAbilityLinkId, then ParentAbilityElementId
        if (ability.ParentAbilityLinkId is not null)
        {
            matchingAbilities = _abilities
                .SelectMany(x => x.Value)
                .Where(x => x.LinkId.Equals(ability.ParentAbilityLinkId))
                .Concat(_subAbilities
                    .SelectMany(x => x.Value)
                    .SelectMany(y => y.Value)
                    .Where(x => x.LinkId.Equals(ability.ParentAbilityLinkId)));
        }
        else
        {
            matchingAbilities = _abilities
                .SelectMany(x => x.Value)
                .Where(x => x.AbilityElementId == ability.ParentAbilityElementId)
                .Concat(_subAbilities
                    .SelectMany(x => x.Value)
                    .SelectMany(y => y.Value)
                    .Where(x => x.AbilityElementId == ability.ParentAbilityElementId));
        }

        if (!matchingAbilities.Any())
        {
            AddAsUnknownSubAbility(ability);

            return;
        }

        List<Ability> matchingAbilitiesList = [.. matchingAbilities];

        foreach (Ability matchingAbility in matchingAbilitiesList)
        {
            AssignSubAbilityToLink(ability, matchingAbility.LinkId);
        }
    }

    /// <summary>
    /// Adds the ability as a subability if the parent ability was found as an existing unknown ability or (other) subability.
    /// </summary>
    /// <param name="ability">The <see cref="Ability"/> to be added.</param>
    public void AddAsSubAbilityToSubAbility(Ability ability)
    {
        bool MatchAbility(Ability x) => !x.Equals(ability) && (x.AbilityElementId == ability.ParentAbilityElementId || x.LinkId.Equals(ability.ParentAbilityLinkId));

        // check other unknown subabilities
        IEnumerable<Ability> matchingUnknownSubAbilities = UnknownSubAbilities
            .SelectMany(x => x.Value)
            .Where(MatchAbility);

        if (matchingUnknownSubAbilities.Any())
        {
            foreach (Ability matchedSubAbility in matchingUnknownSubAbilities)
            {
                AssignSubAbilityToLink(ability, matchedSubAbility.LinkId);
            }

            return;
        }

        List<Ability> matchingSubAbilities = [.. _subAbilities
            .SelectMany(x => x.Value)
            .SelectMany(y => y.Value)
            .Where(MatchAbility)];

        if (matchingSubAbilities.Count != 0)
        {
            foreach (Ability matchedSubAbility in matchingSubAbilities)
            {
                AssignSubAbilityToLink(ability, matchedSubAbility.LinkId);
            }

            return;
        }
    }

    /// <summary>
    /// Adds an ability from the layout buttons.
    /// </summary>
    /// <param name="ability">The <see cref="Ability"/>.</param>
    internal void AddLayoutAbility(Ability ability)
    {
        _layoutAbilityTypeByNameId.TryAdd(ability.AbilityElementId, ability.AbilityType);

        AddAbility(ability);
    }

    /// <summary>
    /// Adds the ability, that is from the layout buttons, as a sub ability if the parent ability was found as an existing ability or subability, otherwise adds it to the unknown sub abilities.
    /// </summary>
    /// <param name="ability">The <see cref="Ability"/> to be added.</param>
    internal void AddAsLayoutSubAbilityToAbility(Ability ability)
    {
        // no parent ability id, so no sub ability
        if (ability.ParentAbilityLinkId is null && string.IsNullOrEmpty(ability.ParentAbilityElementId))
            return;

        _layoutAbilityTypeByNameId.TryAdd(ability.AbilityElementId, ability.AbilityType);

        AddAsSubAbilityToAbility(ability);
    }

    internal void AssignLayoutSubAbilityToLink(Ability subAbility, LinkId linkId)
    {
        _layoutAbilityTypeByNameId.TryAdd(subAbility.AbilityElementId, subAbility.AbilityType);

        AssignSubAbilityToLink(subAbility, linkId);
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
    /// There may have been duplicates of the name id, but we will only save the first one.
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

    /// <summary>
    /// Adds the ability as an unknown subability.
    /// </summary>
    /// <param name="ability">The <see cref="Ability"/> to be added.</param>
    internal void AddAsUnknownSubAbility(Ability ability)
    {
        if (UnknownSubAbilities.TryGetValue(ability.Tier, out List<Ability>? unknownSubAbilities))
            unknownSubAbilities.Add(ability);
        else
            UnknownSubAbilities[ability.Tier] = [ability];
    }

    /// <summary>
    /// Adds the ability, that is from the layout buttons, as an unknown subability.
    /// </summary>
    /// <param name="ability">The <see cref="Ability"/> to be added.</param>
    internal void AddAsLayoutUnknownSubAbility(Ability ability)
    {
        _layoutAbilityTypeByNameId.TryAdd(ability.AbilityElementId, ability.AbilityType);

        AddAsUnknownSubAbility(ability);
    }
}
