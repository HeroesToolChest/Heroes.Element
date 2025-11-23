namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Represents the unique(ish) identifier for an ability or talent.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="LinkId"/> class.
/// </remarks>
/// <param name="elementId">The id of the element.</param>
/// <param name="buttonElementId">The id of the button element. Usually the "face" value.</param>
/// <param name="abilityType">The <see cref="Types.AbilityType"/> (e.g. Q or Heroic).</param>
public abstract class LinkId(string elementId, string buttonElementId, AbilityType abilityType)
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
    /// Gets the <see cref="Types.AbilityType"/> (e.g. Q or Heroic).
    /// </summary>
    public AbilityType AbilityType { get; init; } = abilityType;

    /// <summary>
    /// Gets the id.
    /// </summary>
    public abstract string Id { get; }

    /// <inheritdoc/>
    public abstract override bool Equals(object? obj);

    /// <inheritdoc/>
    public abstract override int GetHashCode();

    /// <inheritdoc/>
    public abstract override string ToString();
}
