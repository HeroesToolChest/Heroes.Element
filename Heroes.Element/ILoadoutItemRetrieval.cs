namespace Heroes.Element;

/// <summary>
/// Represents an interface for retrieving elements by their loadout item type ids.
/// </summary>
/// <typeparam name="T">The type of the element, which must implement the <see cref="IStoreItem"/> and <see cref="IElementObject"/> interfaces.</typeparam>
public interface ILoadoutItemRetrieval<T> : IStoreItemRetrieval<T>
    where T : class, ILoadoutItem, IElementObject
{
    /// <summary>
    /// Attempts to retrieve an element of <typeparamref name="T"/> based on the specified <paramref name="attributeId"/>.
    /// </summary>
    /// <param name="attributeId">The attribute id of the element.</param>
    /// <param name="value">When this method returns, contains the element associated with the specified <paramref name="attributeId"/> if the operation succeeds; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if an element with the specified <paramref name="attributeId"/> is found; otherwise, <see langword="false"/>.</returns>
    bool TryGetElementByAttributeId(string attributeId, [NotNullWhen(true)] out T? value);

    /// <summary>
    /// Retrieves an element of <typeparamref name="T"/> associated with the specified <paramref name="attributeId"/>.
    /// </summary>
    /// <param name="attributeId">The attribute id of the element.</param>
    /// <returns>The element of <typeparamref name="T"/> associated with the specified <paramref name="attributeId"/>.</returns>
    /// <exception cref="KeyNotFoundException"><paramref name="attributeId"/> property value was not found.</exception>
    T GetElementByAttributeId(string attributeId);
}
