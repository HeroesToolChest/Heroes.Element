namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Represents the unique(ish) identifier for an ability.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AbilityLinkId"/> class.
/// </remarks>
/// <param name="elementId">The id of the ability element.</param>
/// <param name="buttonElementId">The id of the button element. Usually the "face" value.</param>
/// <param name="abilityType">The <see cref="AbilityType"/> (e.g. Q or Heroic).</param>
public sealed class AbilityLinkId(string elementId, string buttonElementId, AbilityType abilityType)
    : LinkId(elementId, buttonElementId, abilityType), IEquatable<AbilityLinkId>
{
    /// <summary>
    /// Gets the id in the format of ElementId|ButtonElementId|AbilityType.
    /// </summary>
    public override string Id => $"{ElementId}|{ButtonElementId}|{AbilityType}";

    /// <inheritdoc/>
    public bool Equals(AbilityLinkId? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return ElementId.Equals(other.ElementId, StringComparison.Ordinal) &&
               ButtonElementId.Equals(other.ButtonElementId, StringComparison.Ordinal) &&
               AbilityType == other.AbilityType;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as AbilityLinkId);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(ElementId, ButtonElementId, AbilityType);
    }

    /// <inheritdoc/>
    public override string ToString() => Id;
}
