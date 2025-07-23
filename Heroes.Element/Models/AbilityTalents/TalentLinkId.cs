namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Represents the unique(ish) identifier for a talent.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TalentLinkId"/> class.
/// </remarks>
/// <param name="elementId">The id of the talent element.</param>
/// <param name="buttonElementId">The id of the button element. Usually the "face" value.</param>
/// <param name="abilityType">The <see cref="AbilityType"/> (e.g. Q or Heroic).</param>
/// <param name="talentTier">The <see cref="Types.TalentTier"/>.</param>
public sealed class TalentLinkId(string elementId, string buttonElementId, AbilityType abilityType, TalentTier talentTier)
    : LinkId(elementId, buttonElementId, abilityType), IEquatable<TalentLinkId>
{
    /// <summary>
    /// Gets the <see cref="Types.TalentTier"/>.
    /// </summary>
    public TalentTier TalentTier { get; init; } = talentTier;

    /// <inheritdoc/>
    public bool Equals(TalentLinkId? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return ElementId.Equals(other.ElementId, StringComparison.Ordinal) &&
               ButtonElementId.Equals(other.ButtonElementId, StringComparison.Ordinal) &&
               AbilityType == other.AbilityType &&
               TalentTier == other.TalentTier;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as TalentLinkId);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(ElementId, ButtonElementId, AbilityType, TalentTier);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{ElementId}|{ButtonElementId}|{AbilityType}|{TalentTier}";
    }
}
