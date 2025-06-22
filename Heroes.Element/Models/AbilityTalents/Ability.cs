namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Contains the data for ability data.
/// </summary>
[DebuggerDisplay("{LinkId,nq}")]
public class Ability : AbilityTalentBase, IEquatable<Ability>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Ability"/> class.
    /// </summary>
    public Ability()
    {
        IsActive = true;
    }

    /// <summary>
    /// Gets a unique(ish) id for this ability. Is in the format of AbilityElementId|ButtonElementId|AbilityType.
    /// </summary>
    [JsonPropertyOrder(-10)]
    public LinkId LinkId => new(AbilityElementId, ButtonElementId, AbilityType);

    /// <summary>
    /// Gets or sets the id of the ability element.
    /// </summary>
    [JsonPropertyOrder(-9)]
    [JsonPropertyName("abilityId")]
    public override string AbilityElementId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the tier of the ability (e.g Basic, Heroic).
    /// </summary>
    [JsonIgnore]
    public AbilityTier Tier { get; set; } = AbilityTier.Unknown;

    /// <inheritdoc/>
    public bool Equals(Ability? other)
    {
        if (other is null)
            return false;

        return other.LinkId.Equals(LinkId);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as Ability);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return LinkId.GetHashCode();
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return LinkId.ToString();
    }
}
