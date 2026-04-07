namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="Spray"/> objects from a JSON document.
/// </summary>
public class SprayDataDocument : ElementDocument<Spray>, ILoadoutItemRetrieval<Spray>
{
    private SprayDataDocument(JsonDocument dataDocument, GameStringDocument? gameStringDocument = null)
        : base(DataType.SprayData, dataDocument, gameStringDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="SprayDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="SprayDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static SprayDataDocument Load(JsonDocument dataDocument, GameStringDocument? gameStringDocument = null)
    {
        return new SprayDataDocument(dataDocument, gameStringDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(Spray element)
    {
        GameStringDocument?.UpdateGameStrings(element);
    }
}
