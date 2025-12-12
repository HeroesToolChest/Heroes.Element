namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="Hero"/> objects from a JSON document.
/// </summary>
public class HeroDataDocument : ElementDocument<Hero>, IStoreItemRetrieval<Hero>
{
    private HeroDataDocument(JsonDocument document, GameStringDocument? gameStringDocument = null)
        : base(document, gameStringDocument)
    {
        JsonSerializerOptions.Converters.Add(new LinkIdConverter());
        JsonSerializerOptions.Converters.Add(new AbilityLinkIdConverter());
    }

    /// <summary>
    /// Creates a new instance of <see cref="HeroDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="HeroDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    public static HeroDataDocument Load(JsonDocument dataDocument, GameStringDocument? gameStringDocument = null)
    {
        return new HeroDataDocument(dataDocument, gameStringDocument);
    }

    /// <summary>
    /// Attempts to retrieve a <see cref="Hero"/> based on the specified <paramref name="unitId"/>.
    /// </summary>
    /// <param name="unitId">The unit id of a hero.</param>
    /// <param name="value">When this method returns, contains the <see cref="Hero"/> associated with the specified <paramref name="unitId"/> if the operation succeeds; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if a <see cref="Hero"/> with the specified <paramref name="unitId"/> is found; otherwise, <see langword="false"/>.</returns>
    public bool TryGetHeroByUnitId(string unitId, [NotNullWhen(true)] out Hero? value)
        => PropertyLookup(JsonNamingPolicy.CamelCase.ConvertName(nameof(Hero.UnitId)), unitId, out value);

    /// <summary>
    /// Retrieves the <see cref="Hero"/> associated with the specified <paramref name="unitId"/>.
    /// </summary>
    /// <param name="unitId">The unit id of a hero.</param>
    /// <returns>The <see cref="Hero"/> associated with the specified <paramref name="unitId"/>.</returns>
    /// <exception cref="KeyNotFoundException"><paramref name="unitId"/> property value was not found.</exception>
    public Hero GetHeroByUnitId(string unitId)
    {
        if (TryGetHeroByUnitId(unitId, out Hero? hero))
            return hero;

        throw new KeyNotFoundException($"The given unitId '{unitId}' was not present in items.");
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(Hero element)
    {
        GameStringDocument?.UpdateGameStrings(element);
    }
}
