namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Contains the data for talent data.
/// </summary>
[DebuggerDisplay("{LinkId,nq}")]
public class Talent : AbilityTalentBase, IEquatable<Talent>
{
    /// <summary>
    /// Gets a unique(ish) id for this talent. Is in the format of TalentElementId|ButtonElementId|AbilityType.
    /// </summary>
    [JsonPropertyOrder(-11)]
    public TalentLinkId LinkId => new(TalentElementId, ButtonElementId, AbilityType);

    /// <summary>
    /// Gets or sets the id of the talent element.
    /// </summary>
    [JsonPropertyName("talentId")]
    [JsonPropertyOrder(-10)]
    public string TalentElementId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the tier of the talent.
    /// </summary>
    [JsonIgnore]
    public TalentTier Tier { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a quest. This indicates if the talent has a quest icon.
    /// Ingame the quest icon will be displayed before the <see cref="AbilityType"/>.
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
    /// <para>
    /// Gets a collection of ability and talent link ids that the talent affects or upgrades.
    /// </para>
    /// <para>
    /// This property is for legacy use with HDP version older than 5.0.0.
    /// </para>
    /// </summary>
    [JsonPropertyOrder(102)]
    public ISet<string> AbilityTalentLinkIds { get; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets the ability and talent link ids that this talent affects or upgrades.
    /// </summary>
    [JsonPropertyOrder(103)]
    public UpgradeLinkIds UpgradeLinkIds { get; } = new UpgradeLinkIds();

    /// <summary>
    /// Gets a collection of prerequisite talent ids.
    /// </summary>
    [JsonPropertyOrder(104)]
    public ISet<string> PrerequisiteTalentIds { get; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <inheritdoc/>
    public bool Equals(Talent? other)
    {
        if (other is null)
            return false;

        return other.LinkId.Equals(LinkId);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as Talent);
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
