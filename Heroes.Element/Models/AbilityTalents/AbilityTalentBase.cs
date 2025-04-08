namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Abtract class that contains properties related to both abilities and talents.
/// </summary>
[DebuggerDisplay("{Id,nq}")]
public abstract class AbilityTalentBase
{
    /// <summary>
    /// Gets or sets the name id (also an ability or talent id).
    /// </summary>
    public string NameId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the button id. Usually the "face" value.
    /// </summary>
    public string ButtonId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public TooltipDescription? Name { get; set; }

    /// <summary>
    /// Gets or sets the icon.
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the toggle cooldown.
    /// </summary>
    public double? ToggleCooldown { get; set; }

    /// <summary>
    /// Gets or sets the tooltip data.
    /// </summary>
    public AbilityTalentTooltip Tooltip { get; set; } = new AbilityTalentTooltip();

    /// <summary>
    /// Gets or sets the abilityType.
    /// For Talents, this appears in the top-right corner of the talent tooltip.
    /// </summary>
    public AbilityType AbilityType { get; set; } = AbilityType.Unknown;

    /// <summary>
    /// Gets or sets a value indicating whether this is activable through a hotkey.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a passive ability. Either requires no hotkey to be pressed or has an ability that activates automatically (auto cast).
    /// </summary>
    public bool IsPassive { get; set; }

    /// <summary>
    /// Gets or sets the parent ability this ability. This is the id of the ability element (the <see cref="NameId"/>).
    /// </summary>
    [JsonIgnore]
    public string? ParentAbililtyId { get; set; }

    /// <summary>
    /// Gets a collection of created units.
    /// </summary>
    [JsonIgnore]
    public ISet<string> CreateUnits { get; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets a collection of talent ids that represent tooltip appenders.
    /// These appear ingame on the baseline ability tooltips after the corresponding talent has been selected.
    /// </summary>
    [JsonIgnore]
    public ISet<TalentId> TooltipAppenderTalentIds { get; } = new HashSet<TalentId>();

    /// <summary>
    /// Gets or sets the relative path of the icon that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? IconPath { get; set; }
}
