namespace Heroes.Element.Models;

/// <summary>
/// Contains the information related to unit armor.
/// </summary>
public class UnitArmor
{
    /// <summary>
    /// Gets or sets the Physical (basic) armor.
    /// </summary>
    [JsonPropertyName("basic")]
    public double BasicArmor { get; set; }

    /// <summary>
    /// Gets or sets the Spell (ability) armor.
    /// </summary>
    [JsonPropertyName("ability")]
    public double AbilityArmor { get; set; }

    /// <summary>
    /// Gets or sets the Splash armor.
    /// </summary>
    [JsonPropertyName("splash")]
    public double SplashArmor { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"Basic: {BasicArmor}, Ability: {AbilityArmor}, Splash: {SplashArmor}";
    }
}
