namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="Banner"/> objects from a JSON document.
/// </summary>
public class BoostDataDocument : ElementDocument<Boost>, IStoreItemRetrieval<Boost>
{
    private BoostDataDocument(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
        : base(DataType.BoostData, dataDocument, gameStringsDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="BoostDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringsDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="BoostDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static BoostDataDocument Load(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    {
        return new BoostDataDocument(dataDocument, gameStringsDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(Boost element)
    {
        GameStringsDocument?.UpdateGameStringTexts(element);
    }
}
