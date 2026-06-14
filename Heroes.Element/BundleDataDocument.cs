namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="Bundle"/> objects from a JSON document.
/// </summary>
public class BundleDataDocument : ElementDocument<Bundle>, IStoreItemRetrieval<Bundle>
{
    private BundleDataDocument(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    : base(DataType.BundleData, dataDocument, gameStringsDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="BundleDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringsDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="BundleDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static BundleDataDocument Load(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    {
        return new BundleDataDocument(dataDocument, gameStringsDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(Bundle element)
    {
        GameStringsDocument?.UpdateGameStringTexts(element);
    }
}
