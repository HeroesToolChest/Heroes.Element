namespace Heroes.Element.Comparers;

/// <summary>
/// A custom comparer for <see cref="LinkId"/>.
/// </summary>
public class LinkIdComparer : IComparer<LinkId>
{
    /// <inheritdoc/>
    public int Compare(LinkId? x, LinkId? y)
    {
        if (x is null && y is null)
            return 0;

        if (x is null)
            return -1;

        if (y is null)
            return 1;

        if (x is TalentLinkId && y is AbilityLinkId)
            return 1;
        else if (x is AbilityLinkId && y is TalentLinkId)
            return -1;

        if (x is TalentLinkId talentLinkIdX && y is TalentLinkId talentLinkIdY)
        {
            int tierComparison = talentLinkIdX.TalentTier.CompareTo(talentLinkIdY.TalentTier);
            if (tierComparison != 0)
                return tierComparison;
        }

        int abilityTypeComparison = x.AbilityType.CompareTo(y.AbilityType);
        if (abilityTypeComparison != 0)
            return abilityTypeComparison;

        int elementIdComparison = x.ElementId.CompareTo(y.ElementId);
        if (elementIdComparison != 0)
            return elementIdComparison;

        return x.ButtonElementId.CompareTo(y.ButtonElementId);
    }
}
