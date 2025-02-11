namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Abtract class that contains properties related to both abilities and talents.
/// </summary>
public abstract class AbilityTalentBase
{
    /// <summary>
    /// Gets or sets the abilityTalent id.
    /// </summary>
    public AbilityTalentId? AbilityTalentId { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public TooltipDescription? Name { get; set; }
}
