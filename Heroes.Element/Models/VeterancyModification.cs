namespace Heroes.Element.Models;

/// <summary>
/// Contains the properties for veterancy modifications.
/// </summary>
public class VeterancyModification
{
    /// <summary>
    /// Gets or sets the kill xp bonus.
    /// </summary>
    [JsonPropertyName("killXPBonus")]
    public double? KillXpBonus { get; set; }

    /// <summary>
    /// Gets or sets the vital max modifications by the vital type.
    /// </summary>
    [JsonPropertyName("vitalMax")]
    public VeterancyVitalType? VitalMaxValue { get; set; }

    /// <summary>
    /// Gets or sets the vital max fraction modifications by the vital type.
    /// </summary>
    [JsonPropertyName("vitalMaxFraction")]
    public VeterancyVitalType? VitalMaxFraction { get; set; }

    /// <summary>
    /// Gets or sets the vital regeneration modifications by the vital type.
    /// </summary>
    [JsonPropertyName("vitalRegen")]
    public VeterancyVitalType? VitalRegeneration { get; set; }

    /// <summary>
    /// Gets or sets the vital regeneration fraction modifications by vital type.
    /// </summary>
    [JsonPropertyName("vitalRegenFraction")]
    public VeterancyVitalType? VitalRegenerationFraction { get; set; }

    /// <summary>
    /// Gets or sets the damage dealt scaled modifications by damage type.
    /// </summary>
    [JsonPropertyName("damageDealtScaled")]
    public VeterancyDamageType? DamageDealtScaled { get; set; }

    /// <summary>
    /// Gets or sets the damage dealt fraction modifications by damage type.
    /// </summary>
    [JsonPropertyName("damageDealtFraction")]
    public VeterancyDamageType? DamageDealtFraction { get; set; }
}
