namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="GameStringText"/> objects from a JSON document.
/// </summary>
public class GameStringDocument : IDisposable
{
    private readonly JsonSerializerOptions _metaJsonSerializerOptions;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    private bool _disposed;

    private GameStringDocument(JsonDocument jsonDocument)
    {
        JsonDocument = jsonDocument;

        // do not serialize with this options in this constructor
        _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonStringEnumConverter(),
                new HeroesDataVersionConverter(),
            },
        };

        // for meta object only
        _metaJsonSerializerOptions = new JsonSerializerOptions(_jsonSerializerOptions);

        MetaGameStringProperties = GetMetaGameStringProperties();

        _jsonSerializerOptions.Converters.Add(new GameStringTextConverter(MetaGameStringProperties.DescriptionText?.Locale));
    }

    /// <summary>
    /// Gets the underyling JSON document.
    /// </summary>
    public JsonDocument JsonDocument { get; }

    /// <summary>
    /// Gets the metadata properties associated with the JSON gamestring data.
    /// </summary>
    public MetaGameStringProperties MetaGameStringProperties { get; }

    /// <summary>
    /// Creates a new instance of <see cref="GameStringDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="jsonDocument">The JSON document containing the data to initialize the <see cref="GameStringDocument"/> instance.</param>
    /// <returns>A <see cref="GameStringDocument"/> object initialized with the data from the provided JSON document.</returns>
    public static GameStringDocument Load(JsonDocument jsonDocument)
    {
        return new GameStringDocument(jsonDocument);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="Hero"/>.
    /// </summary>
    /// <param name="hero">The <see cref="Hero"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(Hero hero)
    {
        if (!JsonDocument.RootElement.TryGetProperty("gamestrings", out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("hero", out JsonElement heroElement))
            return;

        if (TryGetJsonElement(heroElement, "difficulty", hero.Id, out JsonElement element))
            hero.Difficulty = GetGameStringText(element.GetString());
        if (TryGetJsonElement(heroElement, "expandedRole", hero.Id, out element))
            hero.ExpandedRole = GetGameStringText(element.GetString());
        if (TryGetJsonElement(heroElement, "infoText", hero.Id, out element))
            hero.InfoText = GetGameStringText(element.GetString());
        if (TryGetJsonElement(heroElement, "title", hero.Id, out element))
            hero.Title = GetGameStringText(element.GetString());
        if (TryGetJsonElement(heroElement, "energyType", hero.Id, out element))
            hero.Energy.EnergyType = GetGameStringText(element.GetString());
        if (TryGetJsonElement(heroElement, "lifeType", hero.Id, out element))
            hero.Life.LifeType = GetGameStringText(element.GetString());
        if (TryGetJsonElement(heroElement, "shieldType", hero.Id, out element))
            hero.Shield.ShieldType = GetGameStringText(element.GetString());

        if (TryGetJsonElement(heroElement, "roles", hero.Id, out element))
        {
            string? roleElementValue = element.GetString();
            if (!string.IsNullOrWhiteSpace(roleElementValue))
            {
                foreach (string roleValue in roleElementValue.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    GameStringText? gameStringText = GetGameStringText(roleValue);
                    if (gameStringText is not null)
                        hero.Roles.Add(gameStringText);
                }
            }
        }

        SetHeroesCollectionObjectProperties(hero.Id, hero, heroElement);

        IEnumerable<Talent> talents = hero.Talents.SelectMany(x => x.Value);

        if (gameStringElement.TryGetProperty("talent", out JsonElement talentElement))
        {
            foreach (Talent talent in talents)
            {
                SetAbilityTalentBaseData(talentElement, talent, talent.LinkId.Id);
            }
        }

        SetAbilities(hero, gameStringElement);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="Unit"/>.
    /// </summary>
    /// <param name="unit">The <see cref="Unit"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(Unit unit)
    {
        if (!JsonDocument.RootElement.TryGetProperty("gamestrings", out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("unit", out JsonElement unitElement))
            return;

        if (TryGetJsonElement(unitElement, "description", unit.Id, out JsonElement element))
            unit.Description = GetGameStringText(element.GetString());
        if (TryGetJsonElement(unitElement, "energyType", unit.Id, out element))
            unit.Energy.EnergyType = GetGameStringText(element.GetString());
        if (TryGetJsonElement(unitElement, "lifeType", unit.Id, out element))
            unit.Life.LifeType = GetGameStringText(element.GetString());
        if (TryGetJsonElement(unitElement, "name", unit.Id, out element))
            unit.Name = GetGameStringText(element.GetString());
        if (TryGetJsonElement(unitElement, "shieldType", unit.Id, out element))
            unit.Shield.ShieldType = GetGameStringText(element.GetString());

        SetAbilities(unit, gameStringElement);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="Announcer"/>.
    /// </summary>
    /// <param name="announcer">The <see cref="Announcer"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(Announcer announcer)
    {
        if (!JsonDocument.RootElement.TryGetProperty("gamestrings", out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("announcer", out JsonElement announcerElement))
            return;

        SetHeroesCollectionObjectProperties(announcer.Id, announcer, announcerElement);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="Banner"/>.
    /// </summary>
    /// <param name="banner">The <see cref="Banner"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(Banner banner)
    {
        if (!JsonDocument.RootElement.TryGetProperty("gamestrings", out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("banner", out JsonElement bannerElement))
            return;

        SetHeroesCollectionObjectProperties(banner.Id, banner, bannerElement);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="Boost"/>.
    /// </summary>
    /// <param name="boost">The <see cref="Boost"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(Boost boost)
    {
        if (!JsonDocument.RootElement.TryGetProperty("gamestrings", out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("boost", out JsonElement boostElement))
            return;

        SetHeroesCollectionObjectProperties(boost.Id, boost, boostElement);
    }

    /// <summary>
    /// Releases the <see cref="JsonDocument"/> from memory.
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the object and, optionally, releases the managed resources.
    /// </summary>
    /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                JsonDocument.Dispose();
            }

            _disposed = true;
        }
    }

    private static bool TryGetJsonElement(JsonElement currentElement, string jsonPropertyName, string id, out JsonElement element)
    {
        element = default;

        return currentElement.TryGetProperty(jsonPropertyName, out JsonElement innerElement) && innerElement.TryGetProperty(id, out element);
    }

    private GameStringText? GetGameStringText(string? value)
    {
        if (value is null)
            return null;

        return new GameStringText(value, MetaGameStringProperties.DescriptionText.Locale);
    }

    private MetaGameStringProperties GetMetaGameStringProperties()
    {
        if (JsonDocument.RootElement.TryGetProperty("meta", out JsonElement metaElement) && JsonDocument.RootElement.TryGetProperty("gamestrings", out _))
        {
            return metaElement.Deserialize<MetaGameStringProperties>(_metaJsonSerializerOptions) ?? throw new JsonException("Could not deserialize 'meta' object");
        }

        throw new JsonException("No 'meta' and/or 'gamestrings' property found");
    }

    private void SetAbilities(Unit unit, JsonElement gameStringElement)
    {
        IEnumerable<Ability> abilities = unit.Abilities.SelectMany(x => x.Value);

        if (gameStringElement.TryGetProperty("ability", out JsonElement abilityElement))
        {
            foreach (Ability ability in abilities)
            {
                SetAbilityTalentBaseData(abilityElement, ability, ability.LinkId.Id);
            }
        }
    }

    private void SetAbilityTalentBaseData(JsonElement abilityElement, AbilityTalentBase abilityTalentBase, string id)
    {
        if (TryGetJsonElement(abilityElement, "cooldownText", id, out JsonElement value))
            abilityTalentBase.CooldownText = GetGameStringText(value.GetString());
        if (TryGetJsonElement(abilityElement, "energyText", id, out value))
            abilityTalentBase.EnergyText = GetGameStringText(value.GetString());
        if (TryGetJsonElement(abilityElement, "fullText", id, out value))
            abilityTalentBase.FullText = GetGameStringText(value.GetString());
        if (TryGetJsonElement(abilityElement, "lifeText", id, out value))
            abilityTalentBase.LifeText = GetGameStringText(value.GetString());
        if (TryGetJsonElement(abilityElement, "name", id, out value))
            abilityTalentBase.Name = GetGameStringText(value.GetString());
        if (TryGetJsonElement(abilityElement, "shortText", id, out value))
            abilityTalentBase.ShortText = GetGameStringText(value.GetString());
    }

    private void SetHeroesCollectionObjectProperties(string id, IHeroesCollectionObject heroesCollectionObject, JsonElement heroesCollectionElement)
    {
        if (TryGetJsonElement(heroesCollectionElement, "name", id, out JsonElement element))
            heroesCollectionObject.Name = GetGameStringText(element.GetString());
        if (TryGetJsonElement(heroesCollectionElement, "sortName", id, out element))
            heroesCollectionObject.SortName = GetGameStringText(element.GetString());
        if (TryGetJsonElement(heroesCollectionElement, "searchText", id, out element))
            heroesCollectionObject.SearchText = GetGameStringText(element.GetString());
        if (TryGetJsonElement(heroesCollectionElement, "description", id, out element))
            heroesCollectionObject.Description = GetGameStringText(element.GetString());
    }
}
