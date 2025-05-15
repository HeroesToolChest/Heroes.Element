namespace Heroes.Element.Comparers;

/// <summary>
/// A custom comparer for <see cref="LinkId"/>. Orders by <see cref="AbilityType"/>.
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

        return x.AbilityType.CompareTo(y.AbilityType);
    }
}
