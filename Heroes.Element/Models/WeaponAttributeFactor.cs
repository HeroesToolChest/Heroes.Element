namespace Heroes.Element.Models;

/// <summary>
/// Represents a type value pair for the attribute factor of a weapon.
/// </summary>
public class WeaponAttributeFactor : TypeValue
{
    /// <inheritdoc/>
    public override string ToString()
    {
        return $"Type: {Type}, Value: {Value}";
    }
}
