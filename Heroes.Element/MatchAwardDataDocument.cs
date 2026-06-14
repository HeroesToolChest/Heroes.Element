namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="MatchAward"/> objects from a JSON document.
/// </summary>
public class MatchAwardDataDocument : ElementDocument<MatchAward>
{
    private MatchAwardDataDocument(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    : base(DataType.MatchAwardData, dataDocument, gameStringsDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="MatchAwardDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringsDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="MatchAwardDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static MatchAwardDataDocument Load(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    {
        return new MatchAwardDataDocument(dataDocument, gameStringsDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(MatchAward element)
    {
        GameStringsDocument?.UpdateGameStringTexts(element);
    }
}
