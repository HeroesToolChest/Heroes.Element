namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Represents the unique(ish) identifier for an ability or talent.
/// </summary>
/// <param name="ElementId">The id of the element.</param>
/// <param name="ButtonElementId">The id of the button element. Usually the "face" value.</param>
/// <param name="AbilityType">The <see cref="AbilityType"/> (e.g. Q or Heroic).</param>
public record LinkId(string ElementId, string ButtonElementId, AbilityType AbilityType)
{
    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{ElementId}|{ButtonElementId}|{AbilityType}";
    }
}
