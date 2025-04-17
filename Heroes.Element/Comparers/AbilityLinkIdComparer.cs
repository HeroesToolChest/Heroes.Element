namespace Heroes.Element.Comparers;

/// <summary>
/// A custom comparer for <see cref="AbilityLinkId"/>. Orders by <see cref="AbilityType"/>.
/// </summary>
public class AbilityLinkIdComparer : IComparer<AbilityLinkId>
{
    /// <inheritdoc/>
    public int Compare(AbilityLinkId? x, AbilityLinkId? y)
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
