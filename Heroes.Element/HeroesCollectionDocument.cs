namespace Heroes.Element;

/// <summary>
/// Represents the base class for processing and managing JSON-based data for elements of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the element data, which must implement the <see cref="IHeroesCollectionObject"/> and <see cref="IElementObject"/> interface.</typeparam>
public abstract class HeroesCollectionDocument<T> : ElementDocument<T>, IDisposable
    where T : class, IHeroesCollectionObject, IElementObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HeroesCollectionDocument{T}"/> class using the specified JSON document and serializer options.
    /// </summary>
    /// <param name="jsonDocument">The <see cref="JsonDocument"/> containing the JSON data to be processed.</param>
    /// <param name="gameStringDocument">The <see cref="JsonDocument"/> containing the JSON gamestrings to be processed.</param>
    protected HeroesCollectionDocument(JsonDocument jsonDocument, GameStringDocument? gameStringDocument = null)
        : base(jsonDocument, gameStringDocument)
    {
    }

    /// <summary>
    /// Attempts to retrieve a type of <typeparamref name="T"/> based on the specified <paramref name="hyperlinkId"/>.
    /// </summary>
    /// <param name="hyperlinkId">The hyperlink id of the element to retrieve.</param>
    /// <param name="value">When this method returns, contains the <typeparamref name="T"/> associated with the specified <paramref name="hyperlinkId"/> if the operation succeeds; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if an element with the specified <paramref name="hyperlinkId"/> is found; otherwise, <see langword="false"/>.</returns>
    public bool TryGetElementByHyperlinkId(string hyperlinkId, [NotNullWhen(true)] out T? value)
        => PropertyLookup(JsonNamingPolicy.CamelCase.ConvertName(nameof(Hero.HyperlinkId)), hyperlinkId, out value);

    /// <summary>
    /// Retrieves an element <typeparamref name="T"/> associated with the specified <paramref name="hyperlinkId"/>.
    /// </summary>
    /// <param name="hyperlinkId">The hyperlink id of the element to retrieve.</param>
    /// <returns>The <typeparamref name="T"/> associated with the specified <paramref name="hyperlinkId"/>.</returns>
    /// <exception cref="KeyNotFoundException"><paramref name="hyperlinkId"/> property value was not found.</exception>
    public T GetElementByHyperlinkId(string hyperlinkId)
    {
        if (TryGetElementByHyperlinkId(hyperlinkId, out T? element))
            return element;

        throw new KeyNotFoundException($"The given hyperlinkId '{hyperlinkId}' was not present in items.");
    }

    /// <summary>
    /// Attempts to retrieve an element of <typeparamref name="T"/> based on the specified <paramref name="attributeId"/>.
    /// </summary>
    /// <param name="attributeId">The attribute id of the element.</param>
    /// <param name="value">When this method returns, contains the element associated with the specified <paramref name="attributeId"/> if the operation succeeds; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if an element with the specified <paramref name="attributeId"/> is found; otherwise, <see langword="false"/>.</returns>
    public bool TryGetElementByAttributeId(string attributeId, [NotNullWhen(true)] out T? value)
        => PropertyLookup(JsonNamingPolicy.CamelCase.ConvertName(nameof(Hero.AttributeId)), attributeId, out value);

    /// <summary>
    /// Retrieves an element of <typeparamref name="T"/> associated with the specified <paramref name="attributeId"/>.
    /// </summary>
    /// <param name="attributeId">The attribute id of the element.</param>
    /// <returns>The element of <typeparamref name="T"/> associated with the specified <paramref name="attributeId"/>.</returns>
    /// <exception cref="KeyNotFoundException"><paramref name="attributeId"/> property value was not found.</exception>
    public T GetElementByAttributeId(string attributeId)
    {
        if (TryGetElementByAttributeId(attributeId, out T? element))
            return element;

        throw new KeyNotFoundException($"The given attributeId '{attributeId}' was not present in items.");
    }
}
