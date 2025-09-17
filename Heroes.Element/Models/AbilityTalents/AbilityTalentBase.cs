namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Abtract class that contains properties related to both abilities and talents.
/// </summary>
public abstract class AbilityTalentBase
{
    /// <summary>
    /// Gets or sets the id of the button element. Usually the "face" value.
    /// </summary>
    [JsonPropertyName("buttonId")]
    public string ButtonElementId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the id of the ability element.
    /// </summary>
    [JsonPropertyName("abilityId")]
    public virtual string AbilityElementId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public GameStringText? Name { get; set; }

    /// <summary>
    /// Gets or sets the icon.
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the toggle cooldown.
    /// </summary>
    public double? ToggleCooldown { get; set; }

    /// <summary>
    /// Gets or sets the charges text.
    /// </summary>
    public TooltipCharges? Charges { get; set; }

    /// <summary>
    /// Gets or sets the energy text.
    /// </summary>
    public GameStringText? EnergyText { get; set; }

    /// <summary>
    /// Gets or sets the life text.
    /// </summary>
    public GameStringText? LifeText { get; set; }

    /// <summary>
    /// Gets or sets the cooldown text.
    /// </summary>
    public GameStringText? CooldownText { get; set; }

    /// <summary>
    /// Gets or sets the short text.
    /// </summary>
    public GameStringText? ShortText { get; set; }

    /// <summary>
    /// Gets or sets the full text.
    /// </summary>
    public GameStringText? FullText { get; set; }

    /// <summary>
    /// Gets or sets the abilityType.
    /// For Talents, this appears in the top-right corner of the talent tooltip.
    /// </summary>
    public AbilityType AbilityType { get; set; } = AbilityType.Unknown;

    /// <summary>
    /// Gets or sets a value indicating whether this is activable (through hotkey or auto casted).
    /// </summary>
    internal bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the parent abilities. This is the <see cref="Ability.AbilityElementId"/>.
    /// Use <see cref="ParentAbilityLinkIds"/> for a more specific reference to the parent ability.
    /// </summary>
    internal List<string> ParentAbilityElementIds { get; set; } = [];

    /// <summary>
    /// Gets or sets the parent talents. This is the <see cref="Talent.TalentElementId"/>.
    /// Use <see cref="ParentTalentLinkIds"/> for a more specific reference to the parent ability.
    /// </summary>
    internal List<string> ParentTalentElementIds { get; set; } = [];

    internal List<AbilityLinkId> ParentAbilityLinkIds { get; set; } = [];

    internal List<TalentLinkId> ParentTalentLinkIds { get; set; } = [];

    /// <summary>
    /// Gets a collection of summoned units.
    /// </summary>
    internal ISet<string> SummonedUnitIds { get; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets a collection of ids of talent elements that represent tooltip appenders.
    /// These appear ingame on the baseline ability tooltips after the corresponding talent has been selected.
    /// </summary>
    internal ISet<string> TooltipAppendersTalentElementIds { get; } = new HashSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets or sets the relative path of the icon that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? IconPath { get; set; }

    internal string? EnergyCost { get; set; }

    internal string? LifeCost { get; set; }

    internal bool IsValid { get; set; } = true;
}
