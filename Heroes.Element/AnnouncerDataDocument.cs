namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="Announcer"/> objects from a JSON document.
/// </summary>
public class AnnouncerDataDocument : ElementDocument<Announcer>, ILoadoutItemRetrieval<Announcer>
{
    private AnnouncerDataDocument(JsonDocument dataDocument, GameStringDocument? gameStringDocument = null)
        : base(dataDocument, gameStringDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="AnnouncerDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="HeroDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    public static AnnouncerDataDocument Load(JsonDocument dataDocument, GameStringDocument? gameStringDocument = null)
    {
        return new AnnouncerDataDocument(dataDocument, gameStringDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(Announcer element)
    {
        GameStringDocument?.UpdateGameStrings(element);
    }
}
