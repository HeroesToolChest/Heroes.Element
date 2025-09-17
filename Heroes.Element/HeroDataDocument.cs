namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="Hero"/> objects from a JSON document.
/// </summary>
public class HeroDataDocument : ElementBaseData<Hero>
{
    private HeroDataDocument(JsonDocument document)
        : base(document)
    {
        JsonSerializerOptions.Converters.Add(new LinkIdConverter());
        JsonSerializerOptions.Converters.Add(new AbilityLinkIdConverter());
    }

    /// <summary>
    /// Creates a new instance of <see cref="HeroDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="jsonDocument">The JSON document containing the data to initialize the <see cref="HeroDataDocument"/> instance.</param>
    /// <returns>A <see cref="HeroDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    public static HeroDataDocument Load(JsonDocument jsonDocument)
    {
        return new HeroDataDocument(jsonDocument);
    }

    /// <summary>
    /// Attempts to retrieve a hero by its <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The unique identifier of the hero to retrieve.</param>
    /// <param name="value">When this method returns, contains the <see cref="Hero"/> associated with the specified <paramref name="id"/> if the operation succeeds; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if a hero with the specified <paramref name="id"/> is found; otherwise, <see langword="false"/>.</returns>
    public bool TryGetHeroById(string id, [NotNullWhen(true)] out Hero? value)
    {
        value = null;

        if (!JsonDocument.RootElement.TryGetProperty("items", out JsonElement itemsElement))
            return false;

        if (itemsElement.TryGetProperty(id, out JsonElement element))
        {
            value = DeserializeElement(element, id);

            return value is not null;
        }

        return false;
    }

    /// <summary>
    /// Retrieves a hero by its <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The unique identifier of the hero to retrieve.</param>
    /// <returns>The <see cref="Hero"/> associated with the specified <paramref name="id"/>.</returns>
    /// <exception cref="KeyNotFoundException"><paramref name="id"/> property value was not found.</exception>
    public Hero GetHeroById(string id)
    {
        if (TryGetHeroById(id, out Hero? hero))
            return hero;

        throw new KeyNotFoundException($"The given id '{id}' was not present in items.");
    }

    /// <summary>
    /// Attempts to retrieve a <see cref="Hero"/> based on the specified <paramref name="unitId"/>.
    /// </summary>
    /// <param name="unitId">A hero unitId property value.</param>
    /// <param name="value">When this method returns, contains the <see cref="Hero"/> associated with the specified <paramref name="unitId"/> if the operation succeeds; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if a <see cref="Hero"/> with the specified <paramref name="unitId"/> is found; otherwise, <see langword="false"/>.</returns>
    public bool TryGetHeroByUnitId(string? unitId, [NotNullWhen(true)] out Hero? value)
        => PropertyLookup(JsonNamingPolicy.CamelCase.ConvertName(nameof(Hero.UnitId)), unitId, out value);

    /// <summary>
    /// Retrieves the <see cref="Hero"/> associated with the specified <paramref name="unitId"/>.
    /// </summary>
    /// <param name="unitId">A hero unitId property value..</param>
    /// <returns>The <see cref="Hero"/> associated with the specified <paramref name="unitId"/>.</returns>
    /// <exception cref="KeyNotFoundException"><paramref name="unitId"/> property value was not found.</exception>
    public Hero GetHeroByUnitId(string? unitId)
    {
        if (TryGetHeroByUnitId(unitId, out Hero? hero))
            return hero;

        throw new KeyNotFoundException($"The given unitId '{unitId}' was not present in items.");
    }
}
