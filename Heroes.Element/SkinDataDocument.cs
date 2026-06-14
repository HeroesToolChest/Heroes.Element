namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="Skin"/> objects from a JSON document.
/// </summary>
public class SkinDataDocument : ElementDocument<Skin>, ILoadoutItemRetrieval<Skin>
{
    private SkinDataDocument(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
        : base(DataType.SkinData, dataDocument, gameStringsDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="SkinDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringsDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="SkinDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static SkinDataDocument Load(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    {
        return new SkinDataDocument(dataDocument, gameStringsDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(Skin element)
    {
        GameStringsDocument?.UpdateGameStringTexts(element);
    }
}
