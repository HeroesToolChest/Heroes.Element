using System.Diagnostics.CodeAnalysis;

namespace Heroes.Element.Comparers;

/// <summary>
/// A custom comparer for <see cref="TooltipDescription"/>. Compares equality by comparing the raw descriptions.
/// </summary>
public class TooltipDescriptionEqualityComparer : IEqualityComparer<TooltipDescription>
{
    /// <inheritdoc/>
    public bool Equals(TooltipDescription? x, TooltipDescription? y)
    {
        if (x is null && y is null)
            return true;

        if (x is null || y is null)
            return false;

        return x.RawDescription.Equals(y.RawDescription, StringComparison.Ordinal);
    }

    /// <inheritdoc/>
    public int GetHashCode([DisallowNull] TooltipDescription obj)
    {
        return obj.RawDescription.GetHashCode();
    }
}
