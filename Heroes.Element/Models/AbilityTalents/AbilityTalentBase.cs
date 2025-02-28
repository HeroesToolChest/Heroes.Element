namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Abtract class that contains properties related to both abilities and talents.
/// </summary>
public abstract class AbilityTalentBase
{
    ///// <summary>
    ///// Initializes a new instance of the <see cref="AbilityTalentBase"/> class.
    ///// </summary>
    ///// <param name="abilityTalentId">Used for a unique id.</param>
    //public AbilityTalentBase(AbilityTalentId abilityTalentId)
    //{
    //    AbilityTalentId = abilityTalentId;
    //}

    ///// <summary>
    ///// Gets the ability talent id that is used for a unique id.
    ///// </summary>
    //[JsonIgnore]
    //public AbilityTalentId AbilityTalentId { get; }

    /// <summary>
    /// Gets or sets the name id (also known as the reference id).
    /// </summary>
    public string NameId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the button id.
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
    /// </summary>
    public AbilityType AbilityType { get; set; } = AbilityType.Unknown;

    /// <summary>
    /// Gets or sets a value indicating whether this is activable through a hotkey.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a passive ability.
    /// </summary>
    public bool IsPassive { get; set; }

    /// <summary>
    /// Gets or sets the relative path of the icon that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? IconPath { get; set; }
}
