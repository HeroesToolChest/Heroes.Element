namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="Mount"/> objects from a JSON document.
/// </summary>
public class MountDataDocument : ElementDocument<Mount>, ILoadoutItemRetrieval<Mount>
{
    private MountDataDocument(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    : base(DataType.MountData, dataDocument, gameStringsDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="MountDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringsDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="MountDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static MountDataDocument Load(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    {
        return new MountDataDocument(dataDocument, gameStringsDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(Mount element)
    {
        GameStringsDocument?.UpdateGameStringTexts(element);
    }
}
