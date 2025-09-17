namespace Heroes.Element.Models;

/// <summary>
/// Contains the unit energy data.
/// </summary>
public class UnitEnergy
{
    /// <summary>
    /// Gets or sets the amount of Energy the unit has (mana, brew, fury...).
    /// </summary>
    [JsonPropertyName("amount")]
    public double EnergyMax { get; set; }

    /// <summary>
    /// Gets or sets the energy regeneration rate of the unit.
    /// </summary>
    [JsonPropertyName("regenRate")]
    public double EnergyRegenerationRate { get; set; }

    /// <summary>
    /// Gets or sets the energy type.
    /// </summary>
    [JsonPropertyName("type")]
    public GameStringText? EnergyType { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"Energy: {EnergyMax} - RegenRate: {EnergyRegenerationRate}";
    }
}
