namespace Heroes.Element.Models;

/// <summary>
/// Contains the information related to unit armor.
/// </summary>
public class UnitArmor
{
    /// <summary>
    /// Gets or sets the type of armor (Hero, Merc, etc...)
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the physical (basic) armor.
    /// </summary>
    public int BasicArmor { get; set; }

    /// <summary>
    /// Gets or sets the Spell (ability) armor.
    /// </summary>
    public int AbilityArmor { get; set; }

    /// <summary>
    /// Gets or sets the Splash armor.
    /// </summary>
    public int SplashArmor { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{nameof(Type)}: {Type}, Basic: {BasicArmor}, Ability: {AbilityArmor}, Splash: {SplashArmor}";
    }
}
