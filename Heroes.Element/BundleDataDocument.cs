namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="Bundle"/> objects from a JSON document.
/// </summary>
public class BundleDataDocument : ElementDocument<Bundle>, IStoreItemRetrieval<Bundle>
{
    private BundleDataDocument(JsonDocument dataDocument, GameStringDocument? gameStringDocument = null)
    : base(dataDocument, gameStringDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="BundleDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="HeroDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    public static BundleDataDocument Load(JsonDocument dataDocument, GameStringDocument? gameStringDocument = null)
    {
        return new BundleDataDocument(dataDocument, gameStringDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(Bundle element)
    {
        GameStringDocument?.UpdateGameStrings(element);
    }
}
