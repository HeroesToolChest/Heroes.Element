namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="AnnouncerPack"/> objects from a JSON document.
/// </summary>
public class AnnouncerDataDocument : ElementDocument<AnnouncerPack>, ILoadoutItemRetrieval<AnnouncerPack>
{
    private AnnouncerDataDocument(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
        : base(DataType.AnnouncerPackData, dataDocument, gameStringsDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="AnnouncerDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringsDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="AnnouncerDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static AnnouncerDataDocument Load(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    {
        return new AnnouncerDataDocument(dataDocument, gameStringsDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(AnnouncerPack element)
    {
        GameStringsDocument?.UpdateGameStringTexts(element);
    }
}
