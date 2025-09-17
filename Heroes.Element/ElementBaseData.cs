using Heroes.Element.Models.Meta;

namespace Heroes.Element;

/// <summary>
/// Represents the base class for processing and managing JSON-based data for elements of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the element data, which must implement the <see cref="IElementObject"/> interface.</typeparam>
public abstract class ElementBaseData<T> : IDisposable
    where T : class, IElementObject
{
    private readonly JsonSerializerOptions _metaJsonSerializerOptions;

    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="ElementBaseData{T}"/> class using the specified JSON document and serializer options.
    /// </summary>
    /// <param name="jsonDocument">The <see cref="JsonDocument"/> containing the JSON data to be processed.</param>
    protected ElementBaseData(JsonDocument jsonDocument)
    {
        JsonDocument = jsonDocument;

        // do not serialize with this options in this constructor
        JsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonStringEnumConverter(),
            },
        };

        // for meta object only
        _metaJsonSerializerOptions = new JsonSerializerOptions(JsonSerializerOptions);

        MetaProperties = GetMetaProperties();

        JsonSerializerOptions.Converters.Add(new GameStringTextConverter(MetaProperties.DescriptionText?.Locale));
    }

    /// <summary>
    /// Gets the JSON document associated with the current instance.
    /// </summary>
    public JsonDocument JsonDocument { get; }

    /// <summary>
    /// Gets the options used to configure JSON serialization and deserialization.
    /// </summary>
    public JsonSerializerOptions JsonSerializerOptions { get; }

    /// <summary>
    /// Gets the metadata properties associated with the JSON data.
    /// </summary>
    public MetaProperties MetaProperties { get; }

    /// <summary>
    /// Releases the <see cref="JsonDocument"/> from memory.
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

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
    /// <returns>An instance of type <typeparamref name="T"/> populated with data from the JSON element,  or <see langword="null"/> if the deserialization fails.</returns>
    protected virtual T? DeserializeElement(JsonElement jsonElement, string id)
    {
        T? element = jsonElement.Deserialize<T>(JsonSerializerOptions);

        element?.SetId(id);

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
    /// <remarks>This method is called by the public <c>Dispose</c> method and the finalizer.
    /// When <paramref name="disposing"/> is <see langword="true"/>, this method releases all resources held by managed objects that the object references. Override this method in a derived class to release additional resources.</remarks>
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

    protected bool Test()
    {
        if (MetaProperties.IsLegacy is false)
        {
            return false;
        }
        else
        {
            // legacy handling

            return true;
        }
    }

    private MetaProperties GetMetaProperties()
    {
        if (JsonDocument.RootElement.TryGetProperty("meta", out JsonElement metaElement) && JsonDocument.RootElement.TryGetProperty("items", out _))
        {
            return metaElement.Deserialize<MetaProperties>(_metaJsonSerializerOptions) ?? throw new JsonException("Could not deserialize 'meta' object");
        }
        else
        {
            return new MetaProperties()
            {
                IsLegacy = true,
            };
        }
    }
}
