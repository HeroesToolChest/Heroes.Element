using System.Diagnostics;

namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Contains the data for ability talent ingame-tooltip data.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
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

    internal string? EnergyCost { get; set; }

    internal string? LifeCost { get; set; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay
    {
        get
        {
            if (ShortTooltip is not null)
                return ShortTooltip.PlainText;
            else if (FullTooltip is not null)
                return FullTooltip.PlainText;
            else
                return ToString()!;
        }
    }
}
