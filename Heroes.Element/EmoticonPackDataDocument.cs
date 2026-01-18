namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="EmoticonPack"/> objects from a JSON document.
/// </summary>
public class EmoticonPackDataDocument : ElementDocument<EmoticonPack>
{
    private EmoticonPackDataDocument(JsonDocument dataDocument, GameStringDocument? gameStringDocument = null)
        : base(dataDocument, gameStringDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="EmoticonPackDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="HeroDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    public static EmoticonPackDataDocument Load(JsonDocument dataDocument, GameStringDocument? gameStringDocument = null)
    {
        return new EmoticonPackDataDocument(dataDocument, gameStringDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(EmoticonPack element)
    {
        GameStringDocument?.UpdateGameStrings(element);
    }
}
