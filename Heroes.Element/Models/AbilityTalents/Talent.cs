namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Contains the data for talent data.
/// </summary>
public class Talent : AbilityTalentBase, IEquatable<Talent>
{
    /// <summary>
    /// Gets the talent id.
    /// </summary>
    [JsonIgnore]
    public TalentId Id => new(NameId, ButtonId);

    /// <summary>
    /// Gets or sets the tier of the talent.
    /// </summary>
    [JsonIgnore]
    public TalentTier Tier { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a quest.
    /// If <see langword="true"/>, then ingame the quest icon will be displayed before the <see cref="AbilityType"/>.
    /// </summary>
    [JsonPropertyOrder(100)]
    public bool IsQuest { get; set; }

    /// <summary>
    /// Gets or sets the column number, also known as the sort index number.
    /// </summary>
    [JsonPropertyName("sort")]
    [JsonPropertyOrder(101)]
    public int Column { get; set; }

    /// <summary>
    /// Gets a collection of prerequisite talent ids.
    /// </summary>
    public ISet<string> PrerequisiteTalentIds { get; } = new HashSet<string>();

    /// <inheritdoc/>
    public bool Equals(Talent? other)
    {
        if (other is null)
            return false;

        return other.Id.Equals(Id);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as Talent);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
