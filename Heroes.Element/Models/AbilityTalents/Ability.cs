namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Contains the data for ability data.
/// </summary>
[DebuggerDisplay("{Id,nq}")]
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
    /// Gets a unique(ish) id. Is in the format of NameId|ButtonId|AbilityType|IsPassive.
    /// </summary>
    [JsonIgnore]
    public AbilityId Id => new(NameId, ButtonId, AbilityType, IsPassive);

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

        return other.Id.Equals(Id);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as Ability);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return Id.ToString();
    }
}
