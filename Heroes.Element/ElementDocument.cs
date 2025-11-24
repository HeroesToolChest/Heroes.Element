namespace Heroes.Element;

/// <summary>
/// Represents the base class for processing and managing JSON-based data for elements of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the element data, which must implement the <see cref="IElementObject"/> interface.</typeparam>
public abstract class ElementDocument<T> : IDisposable
    where T : class, IElementObject
{
    private readonly JsonSerializerOptions _metaJsonSerializerOptions;

    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="ElementDocument{T}"/> class using the specified JSON document and serializer options.
    /// </summary>
    /// <param name="jsonDocument">The <see cref="JsonDocument"/> containing the JSON data to be processed.</param>
    /// <param name="gameStringDocument">The <see cref="JsonDocument"/> containing the JSON gamestrings to be processed.</param>
    protected ElementDocument(JsonDocument jsonDocument, GameStringDocument? gameStringDocument = null)
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
        _metaJsonSerializerOptions = new JsonSerializerOptions(JsonSerializerOptions);

        MetaProperties = GetMetaProperties();

        JsonSerializerOptions.Converters.Add(new GameStringTextConverter(MetaProperties.DescriptionText?.Locale));
    }

    /// <summary>
    /// Gets the underlying JSON document. This is only the data document and not the optional gamestring document.
    /// </summary>
    public JsonDocument JsonDocument { get; }

    /// <summary>
    /// Gets the optional underlying JSON gamestring document.
    /// </summary>
    public GameStringDocument? GameStringDocument { get; }

    /// <summary>
    /// Gets the metadata properties associated with the JSON data. This includes properties overridden from the optional gamestring document.
    /// </summary>
    public MetaDataProperties MetaProperties { get; }

    /// <summary>
    /// Gets a value indicating whether the HeroesVersion in the JSON data does not match the version in the <see cref="GameStringDocument"/>.
    /// This returns <see langword="false"/> if there is no <see cref="GameStringDocument"/>.
    /// </summary>
    public bool MismatchedHeroesVersion => GameStringDocument is not null && MetaProperties.HeroesVersion != GameStringDocument.MetaGameStringProperties.HeroesVersion;

    /// <summary>
    /// Gets a value indicating whether the HDP version in the JSON data does not match the version in the <see cref="GameStringDocument"/>.
    /// </summary>
    public bool MismatchedHdpVersion => GameStringDocument is not null && !MetaProperties.HdpVersion.Equals(GameStringDocument.MetaGameStringProperties.HdpVersion, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Gets the options used to configure JSON serialization and deserialization.
    /// </summary>
    protected JsonSerializerOptions JsonSerializerOptions { get; }

    /// <summary>
    /// Attempts to retrieve an element of <typeparamref name="T"/> by it's <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The unique identifier of the element to retrieve.</param>
    /// <param name="value">When this method returns, contains the <typeparamref name="T"/> associated with the specified <paramref name="id"/> if the operation succeeds; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if an element with the specified <paramref name="id"/> is found; otherwise, <see langword="false"/>.</returns>
    public bool TryGetElementById(string id, [NotNullWhen(true)] out T? value)
    {
        value = null;

        if (!JsonDocument.RootElement.TryGetProperty("items", out JsonElement itemsElement))
            return false;

        if (itemsElement.TryGetProperty(id, out JsonElement element))
        {
            value = DeserializeElement(element, id);

            return value is not null;
        }

        return false;
    }

    /// <summary>
    /// Retrieves an element of <typeparamref name="T"/> by it's <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The unique identifier of the element to retrieve.</param>
    /// <returns>The <typeparamref name="T"/> associated with the specified <paramref name="id"/>.</returns>
    /// <exception cref="KeyNotFoundException"><paramref name="id"/> property value was not found.</exception>
    public T GetElementById(string id)
    {
        if (TryGetElementById(id, out T? element))
            return element;

        throw new KeyNotFoundException($"The given id '{id}' was not present in items.");
    }

    /// <summary>
    /// Releases the <see cref="JsonDocument"/> from memory.
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
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
            return null;

        element.SetId(id);
        UpdateGameStringTexts(element);

        return element;
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

        if (!JsonDocument.RootElement.TryGetProperty("items", out JsonElement itemsElement))
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

    private MetaDataProperties GetMetaProperties()
    {
        if (JsonDocument.RootElement.TryGetProperty("meta", out JsonElement metaElement) && JsonDocument.RootElement.TryGetProperty("items", out _))
        {
            MetaDataProperties metaDataProperties = metaElement.Deserialize<MetaDataProperties>(_metaJsonSerializerOptions) ?? throw new JsonException("Could not deserialize 'meta' object");

            if (GameStringDocument is not null)
                metaDataProperties.DescriptionText = GameStringDocument.MetaGameStringProperties.DescriptionText;

            return metaDataProperties;
        }

        throw new JsonException("No 'meta' and/or 'items' property found");
    }
}
