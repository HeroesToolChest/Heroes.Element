namespace Heroes.Element;

/// <summary>
/// Represents an interface for retrieving elements by their ids.
/// </summary>
/// <typeparam name="T">The type of the element, which must implement the <see cref="IElementObject"/> interface.</typeparam>
public interface IElementIdRetrieval<T>
    where T : IElementObject
{
    /// <summary>
    /// Attempts to retrieve an element of <typeparamref name="T"/> by it's <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The unique identifier of the element to retrieve.</param>
    /// <param name="value">When this method returns, contains the <typeparamref name="T"/> associated with the specified <paramref name="id"/> if the operation succeeds; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if an element with the specified <paramref name="id"/> is found; otherwise, <see langword="false"/>.</returns>
    bool TryGetElementById(string id, [NotNullWhen(true)] out T? value);

    /// <summary>
    /// Retrieves an element of <typeparamref name="T"/> by it's <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The unique identifier of the element to retrieve.</param>
    /// <returns>The <typeparamref name="T"/> associated with the specified <paramref name="id"/>.</returns>
    /// <exception cref="KeyNotFoundException"><paramref name="id"/> property value was not found.</exception>
    T GetElementById(string id);
}
