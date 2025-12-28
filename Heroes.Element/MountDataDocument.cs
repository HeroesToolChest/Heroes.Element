namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="Mount"/> objects from a JSON document.
/// </summary>
public class MountDataDocument : ElementDocument<Mount>, ILoadoutItemRetrieval<Mount>
{
    private MountDataDocument(JsonDocument dataDocument, GameStringDocument? gameStringDocument = null)
    : base(dataDocument, gameStringDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="MountDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="HeroDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    public static MountDataDocument Load(JsonDocument dataDocument, GameStringDocument? gameStringDocument = null)
    {
        return new MountDataDocument(dataDocument, gameStringDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(Mount element)
    {
        GameStringDocument?.UpdateGameStrings(element);
    }
}
