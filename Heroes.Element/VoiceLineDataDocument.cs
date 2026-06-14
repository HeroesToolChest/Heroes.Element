namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="VoiceLine"/> objects from a JSON document.
/// </summary>
public class VoiceLineDataDocument : ElementDocument<VoiceLine>, ILoadoutItemRetrieval<VoiceLine>
{
    private VoiceLineDataDocument(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
        : base(DataType.VoiceLineData, dataDocument, gameStringsDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="VoiceLineDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringsDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="VoiceLineDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static VoiceLineDataDocument Load(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    {
        return new VoiceLineDataDocument(dataDocument, gameStringsDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(VoiceLine element)
    {
        GameStringsDocument?.UpdateGameStringTexts(element);
    }
}
