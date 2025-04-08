using System.Collections.Immutable;

namespace Heroes.Element.Models;

/// <summary>
/// Contains the unit data.
/// </summary>
[DebuggerDisplay("{Id,nq}")]
public class Unit : ElementObject, IName, IDescription
{
    // for keeping track of the ability types by the ability id
    // there may be duplicates of the ability id, but we will only track the first one
    // has all abilites including sub abilities
    private readonly Dictionary<string, AbilityType> _layoutAbilityTypeByNameId = [];

    private readonly SortedDictionary<AbilityTier, List<Ability>> _abilities = [];
    private readonly Dictionary<AbilityId, SortedDictionary<AbilityTier, List<Ability>>> _subAbilities = [];

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
    public UnitPortrait Portraits { get; set; } = new UnitPortrait();

    /// <summary>
    /// Gets a collection of additional units associated with this unit.
    /// </summary>
    [JsonPropertyOrder(118)]
    public ISet<string> UnitIds { get; } = new SortedSet<string>(StringComparer.Ordinal);

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
        x => (IReadOnlyList<Ability>)[.. x.Value]);

    /// <summary>
    /// Gets a collection of subabilities by their parent's ability's <see cref="AbilityId"/>.
    /// A subability becomes available after the parent's ability is used.
    /// </summary>
    [JsonPropertyOrder(201)]
    public IReadOnlyDictionary<AbilityId, IReadOnlyDictionary<AbilityTier, IReadOnlyList<Ability>>> SubAbilities => _subAbilities.ToDictionary(
        outerKvp => outerKvp.Key,
        outerKvp => (IReadOnlyDictionary<AbilityTier, IReadOnlyList<Ability>>)outerKvp.Value.ToDictionary(
            innerKvp => innerKvp.Key,
            innerKvp => (IReadOnlyList<Ability>)[.. innerKvp.Value]));

    /// <summary>
    /// Gets or sets the parent link of this unit.
    /// </summary>
    [JsonIgnore]
    public string? ParentLink { get; set; }

    /// <summary>
    /// Adds an ability.
    /// </summary>
    /// <param name="ability">The <see cref="Ability"/>.</param>
    /// <returns><see langword="true"/> if the ability was added, otherwise <see langword="false"/>.</returns>
    public bool AddAbility(Ability ability)
    {
        _layoutAbilityTypeByNameId.TryAdd(ability.NameId, ability.AbilityType);

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
    /// Adds a sub ability.
    /// </summary>
    /// <param name="subAbility">The <see cref="Ability"/>.</param>
    /// <returns><see langword="true"/> if the subability was added, otherwise <see langword="false"/>.</returns>
    public bool AddSubAbility(Ability subAbility)
    {
        // no parent ability id, so no sub ability
        if (string.IsNullOrEmpty(subAbility.ParentAbililtyId))
            return false;

        _layoutAbilityTypeByNameId.TryAdd(subAbility.NameId, subAbility.AbilityType);

        IEnumerable<Ability> matchingAbilities = _abilities
            .SelectMany(x => x.Value)
            .Where(x => x.NameId == subAbility.ParentAbililtyId);

        if (!matchingAbilities.Any())
            return false;

        foreach (Ability ability in matchingAbilities)
        {
            if (_subAbilities.TryGetValue(ability.Id, out SortedDictionary<AbilityTier, List<Ability>>? subAbilities))
            {
                if (subAbilities.TryGetValue(subAbility.Tier, out List<Ability>? abilities))
                    abilities.Add(subAbility);
                else
                    subAbilities[subAbility.Tier] = [subAbility];
            }
            else
            {
                _subAbilities[ability.Id] = new SortedDictionary<AbilityTier, List<Ability>>()
                {
                    [subAbility.Tier] = [subAbility],
                };
            }
        }

        return true;
    }

    /// <summary>
    /// Returns a value indicating whether the ability type was found by the name id. Based on the layout buttons.
    /// There may be duplicates of the name id, but we will only get the first one.
    /// </summary>
    /// <param name="nameId">The name id (or ability id).</param>
    /// <param name="abilityType">The <see cref="AbilityType"/>.</param>
    /// <returns><see langword="true"/> if found, otherwise <see langword="false"/>.</returns>
    public bool GetAbilityTypeByNameId(string nameId, out AbilityType abilityType)
    {
        return _layoutAbilityTypeByNameId.TryGetValue(nameId, out abilityType);
    }
}
