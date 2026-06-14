namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="Banner"/> objects from a JSON document.
/// </summary>
public class BannerDataDocument : ElementDocument<Banner>, ILoadoutItemRetrieval<Banner>
{
    private BannerDataDocument(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
        : base(DataType.BannerData, dataDocument, gameStringsDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="BannerDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringsDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="BannerDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static BannerDataDocument Load(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    {
        return new BannerDataDocument(dataDocument, gameStringsDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(Banner element)
    {
        GameStringsDocument?.UpdateGameStringTexts(element);
    }
}
