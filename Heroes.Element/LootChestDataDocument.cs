namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="LootChest"/> objects from a JSON document.
/// </summary>
public class LootChestDataDocument : ElementDocument<LootChest>, IHyperlinkIdRetrieval<LootChest>
{
    private LootChestDataDocument(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
        : base(DataType.LootChestData, dataDocument, gameStringsDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="LootChestDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringsDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="LootChestDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static LootChestDataDocument Load(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    {
        return new LootChestDataDocument(dataDocument, gameStringsDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(LootChest element)
    {
        GameStringsDocument?.UpdateGameStringTexts(element);
    }
}
