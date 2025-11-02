namespace Heroes.Element.JsonTypeInfoResolvers.Tests;

[TestClass]
public class JsonTypeInfoModifiersTests
{
    [TestMethod]
    public void SerializationModifiers_IEnumerableHasValue_PropertyShouldBeSerialized()
    {
        // arrange
        GameStringElementName gameStringElements = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringElements, LocalizedTextOption.Extract);

        Hero hero = new("id");
        hero.Attributes.Add("attr1");
        hero.Attributes.Add("attr2");

        string json = JsonSerializer.Serialize(hero, jsonSerializerOptions);

        // act
        JsonDocument jsonDocument = JsonDocument.Parse(json);

        // assert
        jsonDocument.RootElement.TryGetProperty("attributes", out _).Should().BeTrue();
    }

    [TestMethod]
    public void SerializationModifiers_IEnumerableHasNoValue_PropertyShouldNotBeSerialized()
    {
        // arrange
        GameStringElementName gameStringElements = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringElements, LocalizedTextOption.Extract);

        Hero hero = new("id");
        hero.Attributes.Clear();

        string json = JsonSerializer.Serialize(hero, jsonSerializerOptions);

        // act
        JsonDocument jsonDocument = JsonDocument.Parse(json);

        // assert
        jsonDocument.RootElement.TryGetProperty("attributes", out _).Should().BeFalse();
    }

    [TestMethod]
    public void SerializationModifiers_HeroProtraitPartyFrames_PropertyShouldBeSerialized()
    {
        // arrange
        GameStringElementName gameStringElements = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringElements, LocalizedTextOption.Extract);

        Hero hero = new("id");
        hero.HeroPortraits.PartyFrames.Clear();

        string json = JsonSerializer.Serialize(hero, jsonSerializerOptions);

        // act
        JsonDocument jsonDocument = JsonDocument.Parse(json);

        // assert
        jsonDocument.RootElement.GetProperty("portraits").TryGetProperty("partyFrames", out _).Should().BeTrue();
    }

    [TestMethod]
    [DataRow(0.0, 0.0, 0.0, false)]
    [DataRow(1, 1, 1, true)]
    public void SerializationModifiers_LifeEnergyShieldModifiers_(double life, double energy, double shield, bool shouldSerialize)
    {
        // arrange
        GameStringElementName gameStringElements = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringElements, LocalizedTextOption.Extract);

        Hero hero = new("id");
        {
            hero.Life = new UnitLife
            {
                LifeMax = life,
            };
            hero.Energy = new UnitEnergy
            {
                EnergyMax = energy,
            };
            hero.Shield = new UnitShield
            {
                ShieldMax = shield,
            };
        }

        string json = JsonSerializer.Serialize(hero, jsonSerializerOptions);

        // act
        JsonDocument jsonDocument = JsonDocument.Parse(json);

        // assert
        jsonDocument.RootElement.TryGetProperty("life", out _).Should().Be(shouldSerialize);
        jsonDocument.RootElement.TryGetProperty("energy", out _).Should().Be(shouldSerialize);
        jsonDocument.RootElement.TryGetProperty("shield", out _).Should().Be(shouldSerialize);
    }

    [TestMethod]
    public void SerializationModifiers_ExtractGameStringTextPropertyIsNull_PropertyShouldNotBeExtracted()
    {
        // arrange
        GameStringElementName gameStringElements = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringElements, LocalizedTextOption.Extract);

        Hero hero = new("id")
        {
            Name = null,
        };

        string json = JsonSerializer.Serialize(hero, jsonSerializerOptions);

        // act
        JsonDocument jsonDocument = JsonDocument.Parse(json); // read the result json to verify the SerialiazationModifiers

        // assert
        jsonDocument.RootElement.TryGetProperty("name", out _).Should().BeFalse();
        gameStringElements.Should().BeEmpty();
    }

    [TestMethod]
    public void SerializationModifiers_ExtractGameStringTextPropertyHasValue_PropertyShouldBeExtracted()
    {
        // arrange
        GameStringElementName gameStringElements = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringElements, LocalizedTextOption.Extract);

        Hero hero = new("id")
        {
            Name = new GameStringText("value"),
        };

        string json = JsonSerializer.Serialize(hero, jsonSerializerOptions);

        // act
        JsonDocument jsonDocument = JsonDocument.Parse(json); // read the result json to verify the SerialiazationModifiers

        // assert
        jsonDocument.RootElement.TryGetProperty("name", out _).Should().BeFalse();
        gameStringElements.Should().ContainSingle();
    }

    [TestMethod]
    public void SerializationModifiers_NoneGameStringTextPropertyIsNull_PropertyShouldNotBeCopied()
    {
        // arrange
        GameStringElementName gameStringElements = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringElements, LocalizedTextOption.None);

        Hero hero = new("id")
        {
            Name = null,
        };

        string json = JsonSerializer.Serialize(hero, jsonSerializerOptions);

        // act
        JsonDocument jsonDocument = JsonDocument.Parse(json); // read the result json to verify the SerialiazationModifiers

        // assert
        jsonDocument.RootElement.TryGetProperty("name", out _).Should().BeFalse();
        gameStringElements.Should().BeEmpty();
    }

    [TestMethod]
    public void SerializationModifiers_CopyGameStringTextPropertyIsNull_PropertyShouldNotBeCopied()
    {
        // arrange
        GameStringElementName gameStringElements = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringElements, LocalizedTextOption.Copy);

        Hero hero = new("id")
        {
            Name = null,
        };

        string json = JsonSerializer.Serialize(hero, jsonSerializerOptions);

        // act
        JsonDocument jsonDocument = JsonDocument.Parse(json); // read the result json to verify the SerialiazationModifiers

        // assert
        jsonDocument.RootElement.TryGetProperty("name", out _).Should().BeFalse();
        gameStringElements.Should().BeEmpty();
    }

    [TestMethod]
    public void SerializationModifiers_CopyGameStringTextPropertyHasValue_PropertyShouldBeCopied()
    {
        // arrange
        GameStringElementName gameStringElements = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringElements, LocalizedTextOption.Copy);

        Hero hero = new("id")
        {
            Name = new GameStringText("value"),
        };

        string json = JsonSerializer.Serialize(hero, jsonSerializerOptions);

        // act
        JsonDocument jsonDocument = JsonDocument.Parse(json); // read the result json to verify the SerialiazationModifiers

        // assert
        jsonDocument.RootElement.TryGetProperty("name", out _).Should().BeTrue();
        gameStringElements.Should().ContainSingle();
    }

    [TestMethod]
    public void SerializationModifiers_NoneGameStringTextPropertyHasValue_PropertyShouldNotBeExtracted()
    {
        // arrange
        GameStringElementName gameStringElements = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringElements, LocalizedTextOption.None);

        Hero hero = new("id")
        {
            Name = new GameStringText("value"),
        };

        string json = JsonSerializer.Serialize(hero, jsonSerializerOptions);

        // act
        JsonDocument jsonDocument = JsonDocument.Parse(json); // read the result json to verify the SerialiazationModifiers

        // assert
        jsonDocument.RootElement.TryGetProperty("name", out _).Should().BeTrue();
        gameStringElements.Should().BeEmpty();
    }

#if NET9_0_OR_GREATER
    [TestMethod]
    public void SerializationModifiers_PropertyIsIElementObject_GameStringElementHasPropertyName()
    {
        // arrange
        GameStringElementName gameStringElements = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringElements, LocalizedTextOption.Extract);

        Unit unit = new("unitId")
        {
            Name = new GameStringText("value"),
        };

        // act
        JsonSerializer.Serialize(unit, jsonSerializerOptions); // serialize to get the gameStringElements

        // assert
        gameStringElements["Unit"]["name"]["unitId"].RawText.Should().Be("value");
    }
#endif

    [TestMethod]
    public void SerializationModifiers_PropertyIsHero_GameStringElementHasPropertyName()
    {
        // arrange
        GameStringElementName gameStringElements = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringElements, LocalizedTextOption.Extract);

        Hero hero = new("heroId")
        {
            Name = new GameStringText("heroName"),
        };

        // act
        JsonSerializer.Serialize(hero, jsonSerializerOptions); // serialize to get the gameStringElements

        // assert
        gameStringElements["Hero"]["name"]["heroId"].RawText.Should().Be("heroName");
    }

    [TestMethod]
    public void SerializationModifiers_PropertyIsAbility_GameStringElementHasPropertyName()
    {
        // arrange
        GameStringElementName gameStringElements = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringElements, LocalizedTextOption.Extract);

        Unit unit = new("unitId")
        {
            Abilities =
            {
                {
                    AbilityTier.Heroic,
                    [
                        new Ability()
                        {
                            AbilityElementId = "abil1",
                            ButtonElementId = "button1",
                            AbilityType = AbilityType.Q,
                            Name = new GameStringText("value"),
                        }
                    ]
                },
            },
        };

        // act
        JsonSerializer.Serialize(unit, jsonSerializerOptions); // serialize to get the gameStringElements

        // assert
        gameStringElements["AbilTalent"]["name"]["abil1|button1|Q"].RawText.Should().Be("value");
    }

    [TestMethod]
    public void SerializationModifiers_PropertyIsTalent_GameStringElementHasPropertyName()
    {
        // arrange
        GameStringElementName gameStringElements = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringElements, LocalizedTextOption.Extract);

        Hero hero = new("heroId")
        {
            Name = new GameStringText("heroName"),
            Talents =
            {
                {
                    TalentTier.Level4,
                    [
                        new Talent()
                        {
                            TalentElementId = "talent1",
                            ButtonElementId = "button1",
                            AbilityType = AbilityType.Q,
                            Tier = TalentTier.Level4,
                            Name = new GameStringText("talentName"),
                        }
                    ]
                },
            },
        };

        // act
        JsonSerializer.Serialize(hero, jsonSerializerOptions); // serialize to get the gameStringElements

        // assert
        gameStringElements["AbilTalent"]["name"]["talent1|button1|Q|Level4"].RawText.Should().Be("talentName");
    }

#if NET9_0_OR_GREATER
    [TestMethod]
    public void SerializationModifiers_SerializingMultipleElements_SetsGameStringElements()
    {
        // arrange
        GameStringElementName gameStringElements = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringElements, LocalizedTextOption.Extract);

        Unit unit1 = new("unitId1")
        {
            Name = new GameStringText("value1"),
        };
        Unit unit2 = new("unitId2")
        {
            Name = new GameStringText("value2"),
            Description = new GameStringText("desc2"),
        };

        // act
        JsonSerializer.Serialize(unit1, jsonSerializerOptions);
        JsonSerializer.Serialize(unit2, jsonSerializerOptions);

        // assert
        gameStringElements["Unit"]["name"]["unitId1"].RawText.Should().Be("value1");
        gameStringElements["Unit"]["name"]["unitId2"].RawText.Should().Be("value2");
        gameStringElements["Unit"]["description"]["unitId2"].RawText.Should().Be("desc2");
    }
#endif

    private static JsonSerializerOptions GetExtractSerializerOptions(GameStringElementName gameStringElements, LocalizedTextOption localizedTextOption)
    {
        JsonSerializerOptions jsonSerializerOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters =
            {
                new JsonStringEnumConverter(),
                new GameStringTextConverter(gameStringTextType: GameStringTextType.RawText),
            },
            TypeInfoResolver = new HeroesElementResolver()
            {
                Modifiers =
                {
                    typeInfo => JsonTypeInfoModifiers.SerializationModifiers(typeInfo, localizedTextOption, gameStringElements),
                },
            },
        };
        return jsonSerializerOptions;
    }
}