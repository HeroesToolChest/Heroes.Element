namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Represents the unique(ish) identifier for an ability.
/// </summary>
/// <param name="NameId">The name id or the ability id.</param>
/// <param name="ButtonId">The button id. Usually the "face" value.</param>
/// <param name="AbilityType">The <see cref="AbilityType"/> (e.g. Q or Heroic).</param>
/// <param name="IsPassive">Indicates whether this is a passive ability.</param>
public record AbilityId(string NameId, string ButtonId, AbilityType AbilityType, bool IsPassive)
{
    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{NameId}|{ButtonId}|{AbilityType}|{IsPassive}";
    }
}
