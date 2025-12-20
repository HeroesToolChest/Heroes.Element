namespace Heroes.Element.Models;

/// <summary>
/// Contains the properties for veterancy damage types.
/// </summary>
public class VeterancyDamageType
{
    /// <summary>
    /// Gets or sets the Physical (basic) damage.
    /// </summary>
    public double? Basic { get; set; }

    /// <summary>
    /// Gets or sets the Spell (ability) damage.
    /// </summary>
    public double? Ability { get; set; }

    /// <summary>
    /// Gets or sets the Splash damage.
    /// </summary>
    public double? Splash { get; set; }
}
