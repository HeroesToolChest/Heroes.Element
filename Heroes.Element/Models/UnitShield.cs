namespace Heroes.Element.Models;

/// <summary>
/// Contains the unit shield data.
/// </summary>
public class UnitShield
{
    /// <summary>
    /// Gets or sets the max number of shields the unit has.
    /// </summary>
    [JsonPropertyName("amount")]
    public double ShieldMax { get; set; }

    /// <summary>
    /// Gets or sets the shield scaling.
    /// </summary>
    [JsonPropertyName("scale")]
    public double ShieldMaxScaling { get; set; }

    /// <summary>
    /// Gets or sets the shiled regneration rate.
    /// </summary>
    [JsonPropertyName("regenRate")]
    public double ShieldRegenerationRate { get; set; }

    /// <summary>
    /// Gets or sets the shield regeneration rate scaling.
    /// </summary>
    [JsonPropertyName("regenScale")]
    public double ShieldRegenerationRateScaling { get; set; }

    /// <summary>
    /// Gets or sets the shield regeneration delay.
    /// </summary>
    [JsonPropertyName("regenDelay")]
    public double ShieldRegenerationDelay { get; set; }

    /// <summary>
    /// Gets or sets the type of shield.
    /// </summary>
    [JsonPropertyName("type")]
    public TooltipDescription? ShieldType { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"Shield: {ShieldMax} (+{ShieldMaxScaling * 100}% per level) - RegenRate: {ShieldRegenerationRate} (+{ShieldRegenerationRateScaling * 100}% per level)";
    }
}
