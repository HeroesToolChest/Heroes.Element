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
    public TooltipDescription? EnergyText { get; set; }

    /// <summary>
    /// Gets or sets the life tooltip.
    /// </summary>
    public TooltipDescription? LifeText { get; set; }

    /// <summary>
    /// Gets or sets the cooldown tooltip.
    /// </summary>
    public TooltipDescription? CooldownText { get; set; }

    /// <summary>
    /// Gets or sets the short tooltip.
    /// </summary>
    public TooltipDescription? ShortText { get; set; }

    /// <summary>
    /// Gets or sets the full tooltip.
    /// </summary>
    public TooltipDescription? FullText { get; set; }

    internal string? EnergyCost { get; set; }

    internal string? LifeCost { get; set; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay
    {
        get
        {
            if (ShortText is not null)
                return ShortText.PlainText;
            else if (FullText is not null)
                return FullText.PlainText;
            else
                return ToString()!;
        }
    }
}
