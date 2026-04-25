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

        _jsonSerializerOptions.Converters.Add(new GameStringTextConverter(new GameStringTextConverterOptions()
        {
            StormLocale = MetaGameStringProperties.GameStringTextProperties?.Locale ?? StormLocale.ENUS,
        }));

        if (MetaGameStringProperties.ItemsType != ItemsType.GameStrings)
            throw new JsonException($"The JSON document items type '{MetaGameStringProperties.ItemsType}' does not match the expected items type '{ItemsType.GameStrings}'.");
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
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
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
        ClearStoreItemProperties(hero);

        hero.ExpandedRole = null;
        hero.Difficulty = null;
        hero.Title = null;
        hero.Energy.EnergyType = null;
        hero.Life.LifeType = null;
        hero.Shield.ShieldType = null;
        hero.Roles.Clear();

        (List<Ability> abilities, List<Ability> subAbilities) = GetAbilities(hero);

        ClearAbilities(abilities, subAbilities);

        IEnumerable<Talent> talents = hero.Talents.SelectMany(x => x.Value);
        foreach (Talent talent in talents)
        {
            ClearAbilityTalentBaseData(talent);
        }

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("hero", out JsonElement heroElement))
            return;

        if (TryGetJsonElement(heroElement, "difficulty", hero.Id, out JsonElement element))
            hero.Difficulty = GetGameStringText(element.GetString());
        if (TryGetJsonElement(heroElement, "expandedRole", hero.Id, out element))
            hero.ExpandedRole = GetGameStringText(element.GetString());
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
            foreach (JsonElement roleElement in element.EnumerateArray())
            {
                GameStringText? gameStringText = GetGameStringText(roleElement.GetString());
                if (gameStringText is not null)
                    hero.Roles.Add(gameStringText);
            }
        }

        SetStoreItemProperties(hero.Id, hero, heroElement);

        if (gameStringElement.TryGetProperty("talent", out JsonElement talentElement))
        {
            foreach (Talent talent in talents)
            {
                SetAbilityTalentBaseData(talentElement, talent, talent.LinkId.Id);
            }
        }

        SetAbilities(abilities, subAbilities, gameStringElement);

        foreach (Unit heroUnit in hero.HeroUnits.Values)
        {
            UpdateGameStrings(heroUnit);
        }
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="Unit"/>.
    /// </summary>
    /// <param name="unit">The <see cref="Unit"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(Unit unit)
    {
        unit.Name = null;
        unit.Description = null;
        unit.Energy.EnergyType = null;
        unit.Life.LifeType = null;
        unit.Shield.ShieldType = null;

        (List<Ability> abilities, List<Ability> subAbilities) = GetAbilities(unit);

        ClearAbilities(abilities, subAbilities);

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
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

        SetAbilities(abilities, subAbilities, gameStringElement);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="Announcer"/>.
    /// </summary>
    /// <param name="announcer">The <see cref="Announcer"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(Announcer announcer)
    {
        ClearStoreItemProperties(announcer);

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("announcer", out JsonElement announcerElement))
            return;

        SetStoreItemProperties(announcer.Id, announcer, announcerElement);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="Banner"/>.
    /// </summary>
    /// <param name="banner">The <see cref="Banner"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(Banner banner)
    {
        ClearStoreItemProperties(banner);

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("banner", out JsonElement bannerElement))
            return;

        SetStoreItemProperties(banner.Id, banner, bannerElement);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="Boost"/>.
    /// </summary>
    /// <param name="boost">The <see cref="Boost"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(Boost boost)
    {
        ClearStoreItemProperties(boost);

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("boost", out JsonElement boostElement))
            return;

        SetStoreItemProperties(boost.Id, boost, boostElement);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="Bundle"/>.
    /// </summary>
    /// <param name="bundle">The <see cref="Bundle"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(Bundle bundle)
    {
        ClearStoreItemProperties(bundle);

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("bundle", out JsonElement bundleElement))
            return;

        SetStoreItemProperties(bundle.Id, bundle, bundleElement);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="LootChest"/>.
    /// </summary>
    /// <param name="lootchest">The <see cref="LootChest"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(LootChest lootchest)
    {
        lootchest.Name = null;
        lootchest.Description = null;

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("lootChest", out JsonElement lootChestElement))
            return;

        SetNameProperty(lootchest.Id, lootchest, lootChestElement);
        SetDescriptionProperty(lootchest.Id, lootchest, lootChestElement);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="Map"/>.
    /// </summary>
    /// <param name="map">The <see cref="Map"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(Map map)
    {
        map.Name = null;

        foreach (MapObjective mapObjective in map.MapObjectives)
        {
            mapObjective.Title = null;
            mapObjective.Description = null;
        }

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("map", out JsonElement mapElement))
            return;

        if (TryGetJsonElement(mapElement, "objectiveTitle", map.Id, out JsonElement element))
        {
            int index = 0;

            foreach (JsonElement titleElement in element.EnumerateArray())
            {
                map.MapObjectives[index].Title = GetGameStringText(titleElement.GetString());

                index++;
            }
        }

        if (TryGetJsonElement(mapElement, "objectiveDescription", map.Id, out element))
        {
            int index = 0;

            foreach (JsonElement descriptionElement in element.EnumerateArray())
            {
                map.MapObjectives[index].Description = GetGameStringText(descriptionElement.GetString());

                index++;
            }
        }

        SetNameProperty(map.Id, map, mapElement);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="Skin"/>.
    /// </summary>
    /// <param name="skin">The <see cref="Skin"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(Skin skin)
    {
        ClearStoreItemProperties(skin);

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("skin", out JsonElement skinElement))
            return;

        SetStoreItemProperties(skin.Id, skin, skinElement);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="VoiceLine"/>.
    /// </summary>
    /// <param name="voiceLine">The <see cref="VoiceLine"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(VoiceLine voiceLine)
    {
        ClearStoreItemProperties(voiceLine);

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("voiceLine", out JsonElement voiceLineElement))
            return;

        SetStoreItemProperties(voiceLine.Id, voiceLine, voiceLineElement);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="Mount"/>.
    /// </summary>
    /// <param name="mount">The <see cref="Mount"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(Mount mount)
    {
        ClearStoreItemProperties(mount);

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("mount", out JsonElement mountElement))
            return;

        SetStoreItemProperties(mount.Id, mount, mountElement);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="MatchAward"/>.
    /// </summary>
    /// <param name="matchAward">The <see cref="MatchAward"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(MatchAward matchAward)
    {
        matchAward.ScoreScreenName = null;
        matchAward.ScoreScreenDescription = null;
        matchAward.EndOfMatchName = null;
        matchAward.EndOfMatchDescription = null;
        matchAward.EndOfMatchTooltipText = null;

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("matchAward", out JsonElement matchAwardElement))
            return;

        if (TryGetJsonElement(matchAwardElement, "endOfMatchDescription", matchAward.Id, out JsonElement element))
            matchAward.EndOfMatchDescription = GetGameStringText(element.GetString());
        if (TryGetJsonElement(matchAwardElement, "endOfMatchName", matchAward.Id, out element))
            matchAward.EndOfMatchName = GetGameStringText(element.GetString());
        if (TryGetJsonElement(matchAwardElement, "endOfMatchTooltipText", matchAward.Id, out element))
            matchAward.EndOfMatchTooltipText = GetGameStringText(element.GetString());
        if (TryGetJsonElement(matchAwardElement, "scoreScreenDescription", matchAward.Id, out element))
            matchAward.ScoreScreenDescription = GetGameStringText(element.GetString());
        if (TryGetJsonElement(matchAwardElement, "scoreScreenName", matchAward.Id, out element))
            matchAward.ScoreScreenName = GetGameStringText(element.GetString());
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="Spray"/>.
    /// </summary>
    /// <param name="spray">The <see cref="Spray"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(Spray spray)
    {
        ClearStoreItemProperties(spray);

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("spray", out JsonElement sprayElement))
            return;

        SetStoreItemProperties(spray.Id, spray, sprayElement);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="Emoticon"/>.
    /// </summary>
    /// <param name="emoticon">The <see cref="Emoticon"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(Emoticon emoticon)
    {
        emoticon.Description = null;
        emoticon.SearchText = null;
        emoticon.LocalizedAliases.Clear();

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("emoticon", out JsonElement emoticonElement))
            return;

        SetDescriptionProperty(emoticon.Id, emoticon, emoticonElement);

        if (TryGetJsonElement(emoticonElement, "searchText", emoticon.Id, out JsonElement element))
            emoticon.SearchText = GetGameStringText(element.GetString());

        if (TryGetJsonElement(emoticonElement, "localizedAliases", emoticon.Id, out element))
        {
            foreach (JsonElement roleElement in element.EnumerateArray())
            {
                GameStringText? gameStringText = GetGameStringText(roleElement.GetString());
                if (gameStringText is not null)
                    emoticon.LocalizedAliases.Add(gameStringText);
            }
        }
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="EmoticonPack"/>.
    /// </summary>
    /// <param name="emoticonPack">The <see cref="EmoticonPack"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(EmoticonPack emoticonPack)
    {
        ClearStoreItemProperties(emoticonPack);

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("emoticonPack", out JsonElement emoticonPackElement))
            return;

        SetStoreItemProperties(emoticonPack.Id, emoticonPack, emoticonPackElement);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="PortraitPack"/>.
    /// </summary>
    /// <param name="portraitPack">The <see cref="PortraitPack"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(PortraitPack portraitPack)
    {
        ClearStoreItemProperties(portraitPack);

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("portraitPack", out JsonElement portraitPackElement))
            return;

        SetStoreItemProperties(portraitPack.Id, portraitPack, portraitPackElement);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="RewardPortrait"/>.
    /// </summary>
    /// <param name="rewardPortrait">The <see cref="RewardPortrait"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(RewardPortrait rewardPortrait)
    {
        ClearStoreItemProperties(rewardPortrait);
        rewardPortrait.DescriptionUnearned = null;

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("rewardPortrait", out JsonElement rewardPortraitElement))
            return;

        SetStoreItemProperties(rewardPortrait.Id, rewardPortrait, rewardPortraitElement);

        if (TryGetJsonElement(rewardPortraitElement, "descriptionUnearned", rewardPortrait.Id, out JsonElement element))
            rewardPortrait.DescriptionUnearned = GetGameStringText(element.GetString());
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties for the <see cref="TypeDescription"/>.
    /// </summary>
    /// <param name="typeDescription">The <see cref="TypeDescription"/> whose <see cref="GameStringText"/>s to update.</param>
    public void UpdateGameStrings(TypeDescription typeDescription)
    {
        typeDescription.Name = null;

        if (!JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out JsonElement gameStringElement) ||
            !gameStringElement.TryGetProperty("typeDescription", out JsonElement typeDescriptionElement))
            return;

        SetNameProperty(typeDescription.Id, typeDescription, typeDescriptionElement);
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

    private static void ClearStoreItemProperties(IStoreItem heroesCollectionObject)
    {
        heroesCollectionObject.Name = null;
        heroesCollectionObject.SortName = null;
        heroesCollectionObject.SearchText = null;
        heroesCollectionObject.Description = null;
        heroesCollectionObject.InfoText = null;
    }

    private static void ClearAbilityTalentBaseData(AbilityTalentBase abilityTalentBase)
    {
        abilityTalentBase.CooldownText = null;
        abilityTalentBase.EnergyText = null;
        abilityTalentBase.FullText = null;
        abilityTalentBase.LifeText = null;
        abilityTalentBase.Name = null;
        abilityTalentBase.ShortText = null;
    }

    private static void ClearAbilities(List<Ability> abilities, List<Ability> subAbilities)
    {
        foreach (Ability ability in abilities)
        {
            ClearAbilityTalentBaseData(ability);
        }

        foreach (Ability subAbility in subAbilities)
        {
            ClearAbilityTalentBaseData(subAbility);
        }
    }

    private GameStringText? GetGameStringText(string? value)
    {
        if (value is null)
            return null;

        return new GameStringText(value, MetaGameStringProperties.GameStringTextProperties.Locale);
    }

    private MetaGameStringProperties GetMetaGameStringProperties()
    {
        if (JsonDocument.RootElement.TryGetProperty(Constants.RootMetaPropertyName, out JsonElement metaElement) && JsonDocument.RootElement.TryGetProperty(Constants.ItemsPropertyName, out _))
        {
            return metaElement.Deserialize<MetaGameStringProperties>(_metaJsonSerializerOptions) ?? throw new JsonException("Could not deserialize 'meta' object");
        }

        throw new JsonException("No 'meta' and/or 'items' property found");
    }

    private (List<Ability> Abilities, List<Ability> SubAbilities) GetAbilities(Unit unit)
    {
        List<Ability> abilities = [.. unit.Abilities.SelectMany(x => x.Value)];
        List<Ability> subAbilities = [.. unit.SubAbilities.SelectMany(x => x.Value).SelectMany(x => x.Value)];

        return (abilities, subAbilities);
    }

    private void SetAbilities(List<Ability> abilities, List<Ability> subAbilities, JsonElement gameStringElement)
    {
        if (gameStringElement.TryGetProperty("ability", out JsonElement abilityElement))
        {
            foreach (Ability ability in abilities)
            {
                SetAbilityTalentBaseData(abilityElement, ability, ability.LinkId.Id);
            }

            foreach (Ability subAbility in subAbilities)
            {
                SetAbilityTalentBaseData(abilityElement, subAbility, subAbility.LinkId.Id);
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

    private void SetStoreItemProperties(string id, IStoreItem heroesCollectionObject, JsonElement heroesCollectionElement)
    {
        SetNameProperty(id, heroesCollectionObject, heroesCollectionElement);
        SetDescriptionProperty(id, heroesCollectionObject, heroesCollectionElement);
        SetInfoTextProperty(id, heroesCollectionObject, heroesCollectionElement);

        if (TryGetJsonElement(heroesCollectionElement, "sortName", id, out JsonElement element))
            heroesCollectionObject.SortName = GetGameStringText(element.GetString());
        if (TryGetJsonElement(heroesCollectionElement, "searchText", id, out element))
            heroesCollectionObject.SearchText = GetGameStringText(element.GetString());
    }

    private void SetNameProperty(string id, IName name, JsonElement nameElement)
    {
        if (TryGetJsonElement(nameElement, "name", id, out JsonElement element))
            name.Name = GetGameStringText(element.GetString());
    }

    private void SetDescriptionProperty(string id, IDescription description, JsonElement descriptionElement)
    {
        if (TryGetJsonElement(descriptionElement, "description", id, out JsonElement element))
            description.Description = GetGameStringText(element.GetString());
    }

    private void SetInfoTextProperty(string id, IInfoText infoText, JsonElement infoTextElement)
    {
        if (TryGetJsonElement(infoTextElement, "infoText", id, out JsonElement element))
            infoText.InfoText = GetGameStringText(element.GetString());
    }
}
