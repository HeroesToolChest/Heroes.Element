namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="TypeDescription"/> objects from a JSON document.
/// </summary>
public class TypeDescriptionDataDocument : ElementDocument<TypeDescription>
{
    private TypeDescriptionDataDocument(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
        : base(DataType.TypeDescriptionData, dataDocument, gameStringsDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="TypeDescriptionDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringsDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="TypeDescriptionDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static TypeDescriptionDataDocument Load(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    {
        return new TypeDescriptionDataDocument(dataDocument, gameStringsDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(TypeDescription element)
    {
        GameStringsDocument?.UpdateGameStringTexts(element);
    }
}
