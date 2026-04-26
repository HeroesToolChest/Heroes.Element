namespace Heroes.Element;

/// <summary>
/// Represents the base class for processing and managing JSON-based data for elements of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the element data, which must implement the <see cref="IElementObject"/> interface.</typeparam>
public abstract class ElementDocument<T> : IElementIdRetrieval<T>, IElementDocument
    where T : IElementObject
{
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="ElementDocument{T}"/> class using the specified JSON document and serializer options.
    /// </summary>
    /// <param name="dataType">The expected data type of the JSON document.</param>
    /// <param name="jsonDocument">The <see cref="JsonDocument"/> containing the JSON data to be processed.</param>
    /// <param name="gameStringDocument">The <see cref="JsonDocument"/> containing the JSON gamestrings to be processed.</param>
    protected ElementDocument(DataType dataType, JsonDocument jsonDocument, GameStringDocument? gameStringDocument = null)
    {
        JsonDocument = jsonDocument;
        GameStringDocument = gameStringDocument;

        // do not serialize with this options in this constructor
        JsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonStringEnumConverter(),
                new HeroesDataVersionConverter(),
            },
        };

        // for meta object only
        MetaDataProperties = GetMetaProperties(new JsonSerializerOptions(JsonSerializerOptions));

        JsonSerializerOptions.Converters.Add(new GameStringTextConverter(new GameStringTextConverterOptions()
        {
            StormLocale = MetaDataProperties.GameStringTextProperties?.Locale ?? StormLocale.ENUS,
        }));

        ValidateTypes(dataType);
    }

    /// <inheritdoc/>
    public Type ElementType => typeof(T);

    /// <inheritdoc/>
    public JsonDocument JsonDocument { get; }

    /// <inheritdoc/>
    public GameStringDocument? GameStringDocument { get; }

    /// <inheritdoc/>
    public MetaDataProperties MetaDataProperties { get; }

    /// <inheritdoc/>
    public bool IsMatchedHeroesVersion => GameStringDocument is null || MetaDataProperties.HeroesVersion == GameStringDocument.MetaGameStringProperties.HeroesVersion;

    /// <inheritdoc/>
    public bool IsMatchedHdpVersion => GameStringDocument is null || MetaDataProperties.HdpVersion.Equals(GameStringDocument.MetaGameStringProperties.HdpVersion, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public bool IsMatchedDataType => GameStringDocument is null || GameStringDocument.MetaGameStringProperties.DataTypes.Contains(MetaDataProperties.DataType);

    /// <summary>
    /// Gets the options used to configure JSON serialization and deserialization.
    /// </summary>
    protected JsonSerializerOptions JsonSerializerOptions { get; }

    /// <inheritdoc/>
    public bool TryGetElementById(string id, [NotNullWhen(true)] out T? value)
    {
        value = default;

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement itemsElement))
            return false;

        if (itemsElement.TryGetProperty(id, out JsonElement element))
        {
            value = DeserializeElement(element, id);

            return value is not null;
        }

        return false;
    }

    /// <inheritdoc/>
    public T GetElementById(string id)
    {
        if (TryGetElementById(id, out T? element))
            return element;

        throw new KeyNotFoundException($"The given id '{id}' was not present in items.");
    }

    /// <summary>
    /// Gets all elements of type <typeparamref name="T"/> from the JSON document. If the "items" property is not found, an empty collection is returned.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all elements of type <typeparamref name="T"/>.</returns>
    public IEnumerable<T> GetElements()
    {
        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement itemsElement))
            yield break;

        foreach (JsonProperty property in itemsElement.EnumerateObject())
        {
            T? element = DeserializeElement(property.Value, property.Name);
            if (element is not null)
                yield return element;
        }
    }

    /// <inheritdoc/>
    public IEnumerable<IElementObject> GetElementObjects()
    {
        return GetElements().Cast<IElementObject>();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Attempts to retrieve a type of <typeparamref name="T"/> based on the specified <paramref name="hyperlinkId"/>.
    /// </summary>
    /// <param name="hyperlinkId">The hyperlink id of the element to retrieve.</param>
    /// <param name="value">When this method returns, contains the <typeparamref name="T"/> associated with the specified <paramref name="hyperlinkId"/> if the operation succeeds; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if an element with the specified <paramref name="hyperlinkId"/> is found; otherwise, <see langword="false"/>.</returns>
    public virtual bool TryGetElementByHyperlinkId(string hyperlinkId, [NotNullWhen(true)] out T? value)
        => PropertyLookup(JsonNamingPolicy.CamelCase.ConvertName(nameof(Hero.HyperlinkId)), hyperlinkId, out value);

    /// <summary>
    /// Retrieves an element <typeparamref name="T"/> associated with the specified <paramref name="hyperlinkId"/>.
    /// </summary>
    /// <param name="hyperlinkId">The hyperlink id of the element to retrieve.</param>
    /// <returns>The <typeparamref name="T"/> associated with the specified <paramref name="hyperlinkId"/>.</returns>
    /// <exception cref="KeyNotFoundException"><paramref name="hyperlinkId"/> property value was not found.</exception>
    public virtual T GetElementByHyperlinkId(string hyperlinkId)
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
    public virtual bool TryGetElementByAttributeId(string attributeId, [NotNullWhen(true)] out T? value)
        => PropertyLookup(JsonNamingPolicy.CamelCase.ConvertName(nameof(Hero.AttributeId)), attributeId, out value);

    /// <summary>
    /// Retrieves an element of <typeparamref name="T"/> associated with the specified <paramref name="attributeId"/>.
    /// </summary>
    /// <param name="attributeId">The attribute id of the element.</param>
    /// <returns>The element of <typeparamref name="T"/> associated with the specified <paramref name="attributeId"/>.</returns>
    /// <exception cref="KeyNotFoundException"><paramref name="attributeId"/> property value was not found.</exception>
    public virtual T GetElementByAttributeId(string attributeId)
    {
        if (TryGetElementByAttributeId(attributeId, out T? element))
            return element;

        throw new KeyNotFoundException($"The given attributeId '{attributeId}' was not present in items.");
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the specified element.
    /// </summary>
    /// <param name="element">The type of element.</param>
    protected abstract void UpdateGameStringTexts(T element);

    /// <summary>
    /// Retrieves the data for a specific element based on its identifier and JSON representation.
    /// </summary>
    /// <param name="id">The unique identifier of the element to retrieve.</param>
    /// <param name="jsonElement">The JSON representation of the element containing its data.</param>
    /// <returns>The data of the element as an instance of type <typeparamref name="T"/>.</returns>
    protected virtual T GetElementData(string id, JsonElement jsonElement)
    {
        T? element = DeserializeElement(jsonElement, id);

        return element is null ? throw new JsonException($"Could not deserialize {typeof(T)} with id '{id}'.") : element;
    }

    /// <summary>
    /// Deserializes a JSON element into an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="jsonElement">The JSON element to deserialize.</param>
    /// <param name="id">The identifier to assign to the deserialized object.</param>
    /// <returns>An instance of type <typeparamref name="T"/> populated with data from the JSON element, or <see langword="null"/> if the deserialization fails.</returns>
    protected virtual T? DeserializeElement(JsonElement jsonElement, string id)
    {
        T? element = jsonElement.Deserialize<T>(JsonSerializerOptions);

        if (element is null)
            return default;

        SetProperties(id, element);

        if (string.IsNullOrWhiteSpace(id))
            throw new JsonException("Element id property cannot be empty or whitespace.");

        UpdateGameStringTexts(element);

        return element;
    }

    /// <summary>
    /// Sets the properties for the specified element after it has been deserialized.
    /// The <c>id</c> property is required to be set.
    /// Gamestring properties should not be set in this method, but rather in the <see cref="UpdateGameStringTexts(T)"/> method.
    /// </summary>
    /// <param name="id">The identifier of the element.</param>
    /// <param name="element">The element to set properties for.</param>
    protected virtual void SetProperties(string id, T element)
    {
        (element as IElementObjectSetter)?.SetId(id);
    }

    /// <summary>
    /// Finds the value to a given <paramref name="propertyId"/>.
    /// </summary>
    /// <param name="propertyId">Json property name.</param>
    /// <param name="propertyValue">The value of the property to match.</param>
    /// <param name="value">An <see cref="IElementObject"/> object with the given <paramref name="propertyId"/>.</param>
    /// <returns><see langword="true"/> if the value was found; otherwise <see langword="false"/>.</returns>
    protected virtual bool PropertyLookup(string propertyId, string? propertyValue, [NotNullWhen(true)] out T? value)
    {
        value = default;

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement itemsElement))
            return false;

        foreach (JsonProperty property in itemsElement.EnumerateObject())
        {
            if (property.Value.TryGetProperty(propertyId, out JsonElement nameElement) && nameElement.ValueEquals(propertyValue))
            {
                value = GetElementData(property.Name, property.Value);

                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Releases the unmanaged resources used by the object and, optionally, releases the managed resources.
    /// </summary>
    /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                JsonDocument.Dispose();
            }

            _disposed = true;
        }
    }

    private MetaDataProperties GetMetaProperties(JsonSerializerOptions jsonSerializerOptions)
    {
        if (JsonDocument.RootElement.TryGetProperty(Constants.RootMetaPropertyName, out JsonElement metaElement) && JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out _))
        {
            return metaElement.Deserialize<MetaDataProperties>(jsonSerializerOptions) ?? throw new JsonException("Could not deserialize 'meta' object");
        }

        throw new JsonException("No 'meta' or 'items' property found");
    }

    private void ValidateTypes(DataType dataType)
    {
        if (MetaDataProperties.ItemsType != ItemsType.Data)
            throw new JsonException($"The JSON document items type '{MetaDataProperties.ItemsType}' does not match the expected items type '{ItemsType.Data}'.");

        if (MetaDataProperties.DataType != dataType)
            throw new JsonException($"The JSON document data type '{MetaDataProperties.DataType}' does not match the expected data type '{dataType}'.");
    }
}
