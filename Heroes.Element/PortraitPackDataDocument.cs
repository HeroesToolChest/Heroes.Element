namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="PortraitPack"/> objects from a JSON document.
/// </summary>
public class PortraitPackDataDocument : ElementDocument<PortraitPack>
{
    private PortraitPackDataDocument(JsonDocument dataDocument, GameStringDocument? gameStringDocument = null)
        : base(DataType.PortraitPackData, dataDocument, gameStringDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="PortraitPackDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="PortraitPackDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static PortraitPackDataDocument Load(JsonDocument dataDocument, GameStringDocument? gameStringDocument = null)
    {
        return new PortraitPackDataDocument(dataDocument, gameStringDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(PortraitPack element)
    {
        GameStringDocument?.UpdateGameStrings(element);
    }
}
