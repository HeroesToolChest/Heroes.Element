namespace Heroes.Element.Comparers;

/// <summary>
/// A custom equality comparer for <see cref="GameStringText"/>. Compares equality by comparing the raw descriptions.
/// </summary>
public class GameStringTextEqualityComparer : IEqualityComparer<GameStringText>
{
    /// <inheritdoc/>
    public bool Equals(GameStringText? x, GameStringText? y)
    {
        if (x is null && y is null)
            return true;

        if (x is null || y is null)
            return false;

        return x.RawText.Equals(y.RawText, StringComparison.Ordinal);
    }

    /// <inheritdoc/>
    public int GetHashCode([DisallowNull] GameStringText obj)
    {
        return obj.RawText.GetHashCode();
    }
}
