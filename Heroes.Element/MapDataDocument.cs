namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="Map"/> objects from a JSON document.
/// </summary>
public class MapDataDocument : ElementDocument<Map>
{
    private MapDataDocument(JsonDocument dataDocument, GameStringDocument? gameStringDocument = null)
    : base(DataType.MapData, dataDocument, gameStringDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="MapDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="MapDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static MapDataDocument Load(JsonDocument dataDocument, GameStringDocument? gameStringDocument = null)
    {
        return new MapDataDocument(dataDocument, gameStringDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(Map element)
    {
        GameStringDocument?.UpdateGameStrings(element);
    }
}
