namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="Unit"/> objects from a JSON document.
/// </summary>
public class UnitDataDocument : ElementDocument<Unit>
{
    private UnitDataDocument(JsonDocument document, GameStringsDocument? gameStringsDocument = null)
    : base(DataType.UnitData, document, gameStringsDocument)
    {
        JsonSerializerOptions.Converters.Add(new LinkIdConverter());
        JsonSerializerOptions.Converters.Add(new AbilityLinkIdConverter());
    }

    /// <summary>
    /// Creates a new instance of <see cref="UnitDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringsDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="UnitDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static UnitDataDocument Load(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    {
        return new UnitDataDocument(dataDocument, gameStringsDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(Unit element)
    {
        GameStringsDocument?.UpdateGameStringTexts(element);
    }
}
