namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="EmoticonPack"/> objects from a JSON document.
/// </summary>
public class EmoticonPackDataDocument : ElementDocument<EmoticonPack>
{
    private EmoticonPackDataDocument(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
        : base(DataType.EmoticonPackData, dataDocument, gameStringsDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="EmoticonPackDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringsDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="EmoticonPackDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static EmoticonPackDataDocument Load(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    {
        return new EmoticonPackDataDocument(dataDocument, gameStringsDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(EmoticonPack element)
    {
        GameStringsDocument?.UpdateGameStringTexts(element);
    }
}
