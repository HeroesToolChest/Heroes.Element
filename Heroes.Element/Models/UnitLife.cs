namespace Heroes.Element.Models;

/// <summary>
/// Contains the unit life data.
/// </summary>
public class UnitLife
{
    /// <summary>
    /// Gets or sets the amount of life the unit has.
    /// </summary>
    [JsonPropertyName("amount")]
    public double LifeMax { get; set; }

    /// <summary>
    /// Gets or sets the life scaling.
    /// </summary>
    [JsonPropertyName("scale")]
    public double LifeMaxScaling { get; set; }

    /// <summary>
    /// Gets or sets the life regeneration rate of the unit.
    /// </summary>
    [JsonPropertyName("regenRate")]
    public double LifeRegenerationRate { get; set; }

    /// <summary>
    /// Gets or sets the life regeneration rate scaling.
    /// </summary>
    [JsonPropertyName("regenScale")]
    public double LifeRegenerationRateScaling { get; set; }

    /// <summary>
    /// Gets or sets the life type.
    /// </summary>
    [JsonPropertyName("type")]
    public TooltipDescription? LifeType { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"Life: {LifeMax} (+{LifeMaxScaling * 100}% per level) - RegenRate: {LifeRegenerationRate} (+{LifeRegenerationRateScaling * 100}% per level)";
    }
}
