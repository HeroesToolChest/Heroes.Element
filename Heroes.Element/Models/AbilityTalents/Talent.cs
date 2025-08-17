namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Contains the data for talent data.
/// </summary>
[DebuggerDisplay("{LinkId,nq}")]
public class Talent : AbilityTalentBase, IEquatable<Talent>
{
    /// <summary>
    /// Gets a unique(ish) id for this talent. Is in the format of TalentElementId|ButtonElementId|AbilityType|TalentTier.
    /// </summary>
    [JsonPropertyOrder(-11)]
    public TalentLinkId LinkId => new(TalentElementId, ButtonElementId, AbilityType, Tier);

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
    /// Ingame the quest icon will be displayed before the <see cref="AbilityType"/> on the talent tooltip.
    /// </summary>
    [JsonPropertyOrder(100)]
    public bool IsQuest { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the abilityType is upgraded.
    /// Ingame this is indicated by a upward arrow before the <see cref="AbilityType"/> on the talent tooltip.
    /// </summary>
    [JsonPropertyOrder(101)]
    public bool UpgradesAbilityType { get; set; }

    /// <summary>
    /// Gets or sets the column number, also known as the sort index number.
    /// </summary>
    [JsonPropertyName("sort")]
    [JsonPropertyOrder(102)]
    public int Column { get; set; }

    /// <summary>
    /// <para>
    /// Gets a collection of ability and talent link ids that the talent affects or upgrades.
    /// </para>
    /// <para>
    /// This property is for legacy use with HDP version older than 5.0.0.
    /// </para>
    /// </summary>
    [JsonPropertyOrder(103)]
    public ISet<string> AbilityTalentLinkIds { get; } = new SortedSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets the ability ids that this talent adds to the tooltips.
    /// </summary>
    [JsonPropertyOrder(104)]
    public ISet<AbilityLinkId> TooltipAbilityLinkIds { get; } = new SortedSet<AbilityLinkId>(new LinkIdComparer());

    /// <summary>
    /// Gets a collection of prerequisite talent ids.
    /// </summary>
    [JsonPropertyOrder(105)]
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
