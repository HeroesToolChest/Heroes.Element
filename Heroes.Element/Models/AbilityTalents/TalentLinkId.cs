namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Represents the unique(ish) identifier for a talent.
/// </summary>
/// <param name="TalentElementId">The id of the talent element.</param>
/// <param name="ButtonElementId">The button id. Usually the "face" value.</param>
/// <param name="AbilityType">The <see cref="AbilityType"/> (e.g. Q or Heroic).</param>
public record TalentLinkId(string TalentElementId, string ButtonElementId, AbilityType AbilityType)
{
    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{TalentElementId}|{ButtonElementId}|{AbilityType}";
    }
}
