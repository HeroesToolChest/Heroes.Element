namespace Heroes.Element;

/// <summary>
/// Represents an interface for retrieving elements by their hyperlinkIds.
/// </summary>
/// <typeparam name="T">The type of the element, which must implement the <see cref="IHyperlinkId"/> and <see cref="IElementObject"/> interface.</typeparam>
public interface IHyperlinkIdRetrieval<T>
        where T : IHyperlinkId, IElementObject
{
    /// <summary>
    /// Attempts to retrieve a type of <typeparamref name="T"/> based on the specified <paramref name="hyperlinkId"/>.
    /// </summary>
    /// <param name="hyperlinkId">The hyperlink id of the element to retrieve.</param>
    /// <param name="value">When this method returns, contains the <typeparamref name="T"/> associated with the specified <paramref name="hyperlinkId"/> if the operation succeeds; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if an element with the specified <paramref name="hyperlinkId"/> is found; otherwise, <see langword="false"/>.</returns>
    bool TryGetElementByHyperlinkId(string hyperlinkId, [NotNullWhen(true)] out T? value);

    /// <summary>
    /// Retrieves an element <typeparamref name="T"/> associated with the specified <paramref name="hyperlinkId"/>.
    /// </summary>
    /// <param name="hyperlinkId">The hyperlink id of the element to retrieve.</param>
    /// <returns>The <typeparamref name="T"/> associated with the specified <paramref name="hyperlinkId"/>.</returns>
    /// <exception cref="KeyNotFoundException"><paramref name="hyperlinkId"/> property value was not found.</exception>
    T GetElementByHyperlinkId(string hyperlinkId);
}
