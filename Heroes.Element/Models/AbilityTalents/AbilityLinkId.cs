namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Represents the unique(ish) identifier for an ability.
/// </summary>
/// <param name="AbilityElementId">The id of the ability element.</param>
/// <param name="ButtonElementId">The id of the button element. Usually the "face" value.</param>
/// <param name="AbilityType">The <see cref="AbilityType"/> (e.g. Q or Heroic).</param>
public record AbilityLinkId(string AbilityElementId, string ButtonElementId, AbilityType AbilityType)
{
    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{AbilityElementId}|{ButtonElementId}|{AbilityType}";
    }
}
