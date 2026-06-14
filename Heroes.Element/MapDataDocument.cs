namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="Map"/> objects from a JSON document.
/// </summary>
public class MapDataDocument : ElementDocument<Map>
{
    private MapDataDocument(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    : base(DataType.MapData, dataDocument, gameStringsDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="MapDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringsDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="MapDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static MapDataDocument Load(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    {
        return new MapDataDocument(dataDocument, gameStringsDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(Map element)
    {
        GameStringsDocument?.UpdateGameStringTexts(element);
    }
}
