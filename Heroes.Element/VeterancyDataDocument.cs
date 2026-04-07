namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="Veterancy"/> objects from a JSON document.
/// </summary>
public class VeterancyDataDocument : ElementDocument<Veterancy>
{
    private VeterancyDataDocument(JsonDocument dataDocument)
    : base(dataDocument, null)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="VeterancyDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <returns>A <see cref="VeterancyDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static VeterancyDataDocument Load(JsonDocument dataDocument)
    {
        return new VeterancyDataDocument(dataDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(Veterancy element)
    {
        return;
    }
}
