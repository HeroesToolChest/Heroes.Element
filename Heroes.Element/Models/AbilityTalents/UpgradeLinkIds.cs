namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// 
/// </summary>
public class UpgradeLinkIds
{
    /// <summary>
    /// Gets a colleciton of ability link ids that are used to upgrade the talent.
    /// </summary>
    public ISet<AbilityLinkId> AbilityLinkIds { get; } = new SortedSet<AbilityLinkId>(new AbilityLinkIdComparer());

    /// <summary>
    /// Gets a collection of talent link ids that are used to upgrade the talent.
    /// </summary>
    public ISet<TalentLinkId> TalentLinkIds { get; } = new SortedSet<TalentLinkId>(new TalentLinkIdComparer());
}
