namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Represents the unique(ish) identifier for a talent.
/// </summary>
/// <param name="NameId">The name id or the talent id.</param>
/// <param name="ButtonId">The button id. Usually the "face" value.</param>
public record TalentId(string NameId, string ButtonId)
{
    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{NameId}|{ButtonId}";
    }
}
