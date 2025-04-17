namespace Heroes.Element.Comparers;

/// <summary>
/// A custom comparer for <see cref="TalentLinkId"/>. Orders by <see cref="AbilityType"/>.
/// </summary>
public class TalentLinkIdComparer : IComparer<TalentLinkId>
{
    /// <inheritdoc/>
    public int Compare(TalentLinkId? x, TalentLinkId? y)
    {
        if (x is null && y is null)
            return 0;

        if (x is null)
            return -1;

        if (y is null)
            return 1;

        return x.AbilityType.CompareTo(y.AbilityType);
    }
}
