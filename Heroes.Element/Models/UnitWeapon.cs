namespace Heroes.Element.Models;

/// <summary>
/// Contains the unit weapon data.
/// </summary>
public class UnitWeapon
{
    /// <summary>
    /// Gets or sets the unique id of the weapon.
    /// </summary>
    public string NameId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the distance of the attack.
    /// </summary>
    public double Range { get; set; }

    /// <summary>
    /// Gets or sets the time between attacks.
    /// </summary>
    public double Period { get; set; }

    /// <summary>
    /// Gets or sets the amount of damage the attack deals.
    /// </summary>
    public double Damage { get; set; }

    /// <summary>
    /// Gets or sets the damage scaling per level.
    /// </summary>
    [JsonPropertyName("damageScale")]
    public double DamageScaling { get; set; }

    /// <summary>
    /// Gets a collection of attribute factors. These are the damage value by the attribute.
    /// </summary>
    [JsonPropertyName("damageFactors")]
    public IDictionary<string, double> AttributeFactors { get; } = new SortedDictionary<string, double>(StringComparer.Ordinal);

    /// <summary>
    /// Gets or sets the unit that is associated with this weapon.
    /// </summary>
    public string? ParentLink { get; set; }

    /// <summary>
    /// Gets the attacks per second.
    /// </summary>
    /// <returns>A value indicating the number of attacks per second.</returns>
    [JsonIgnore]
    public double AttacksPerSecond
    {
        get
        {
            if (Period <= 0)
                return 0;

            return 1 / Period;
        }
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return NameId;
    }
}
