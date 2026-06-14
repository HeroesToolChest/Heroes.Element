namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="RewardPortrait"/> objects from a JSON document.
/// </summary>
public class RewardPortraitDataDocument : ElementDocument<RewardPortrait>
{
    private RewardPortraitDataDocument(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
        : base(DataType.RewardPortraitData, dataDocument, gameStringsDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="RewardPortraitDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringsDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="RewardPortraitDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static RewardPortraitDataDocument Load(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    {
        return new RewardPortraitDataDocument(dataDocument, gameStringsDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(RewardPortrait element)
    {
        GameStringsDocument?.UpdateGameStringTexts(element);
    }
}
