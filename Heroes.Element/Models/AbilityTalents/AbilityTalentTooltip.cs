namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Contains the data for ability talent ingame-tooltip data.
/// </summary>
public class AbilityTalentTooltip
{
    /// <summary>
    /// Gets or sets the charges tooltip.
    /// </summary>
    public TooltipCharges? Charges { get; set; }

    /// <summary>
    /// Gets or sets the energy tooltip.
    /// </summary>
    public TooltipDescription? EnergyTooltip { get; set; }

    /// <summary>
    /// Gets or sets the life tooltip.
    /// </summary>
    public TooltipDescription? LifeTooltip { get; set; }

    /// <summary>
    /// Gets or sets the cooldown tooltip.
    /// </summary>
    public TooltipDescription? CooldownTooltip { get; set; }

    /// <summary>
    /// Gets or sets the short tooltip.
    /// </summary>
    public TooltipDescription? ShortTooltip { get; set; }

    /// <summary>
    /// Gets or sets the full tooltip.
    /// </summary>
    public TooltipDescription? FullTooltip { get; set; }

    internal double? EnergyValue { get; set; }

    internal double? LifeValue { get; set; }
}
