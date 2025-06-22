namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Represents the unique(ish) identifier for an ability or talent.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="LinkId"/> class.
/// </remarks>
/// <param name="elementId">The id of the element.</param>
/// <param name="buttonElementId">The id of the button element. Usually the "face" value.</param>
/// <param name="abilityType">The <see cref="AbilityType"/> (e.g. Q or Heroic).</param>
public class LinkId(string elementId, string buttonElementId, AbilityType abilityType) : IEquatable<LinkId>
{
    /// <summary>
    /// Gets the id of the element.
    /// </summary>
    public string ElementId { get; init; } = elementId;

    /// <summary>
    /// Gets the id of the button element. Usually the "face" value.
    /// </summary>
    public string ButtonElementId { get; init; } = buttonElementId;

    /// <summary>
    /// Gets the <see cref="AbilityType"/> (e.g. Q or Heroic).
    /// </summary>
    public AbilityType AbilityType { get; init; } = abilityType;

    /// <inheritdoc/>
    public bool Equals(LinkId? other)
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
        return Equals(obj as LinkId);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(ElementId, ButtonElementId, AbilityType);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{ElementId}|{ButtonElementId}|{AbilityType}";
    }
}
