namespace Heroes.Element.JsonTypeInfoResolvers.Tests;

[TestClass]
public class JsonTypeInfoModifiersTests
{
    [TestMethod]
    public void SerializationModifiers_IEnumerableHasValue_PropertyShouldBeSerialized()
    {
        // arrange
        GameStringItemDictionary gameStringItemDictionary = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringItemDictionary, LocalizedTextOption.Extract);

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
        GameStringItemDictionary gameStringItemDictionary = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringItemDictionary, LocalizedTextOption.Extract);

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
        GameStringItemDictionary gameStringItemDictionary = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringItemDictionary, LocalizedTextOption.Extract);

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
        GameStringItemDictionary gameStringItemDictionary = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringItemDictionary, LocalizedTextOption.Extract);

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
        GameStringItemDictionary gameStringItemDictionary = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringItemDictionary, LocalizedTextOption.Extract);

        Hero hero = new("id")
        {
            Name = null,
        };

        string json = JsonSerializer.Serialize(hero, jsonSerializerOptions);

        // act
        JsonDocument jsonDocument = JsonDocument.Parse(json); // read the result json to verify the SerialiazationModifiers

        // assert
        jsonDocument.RootElement.TryGetProperty("name", out _).Should().BeFalse();
        gameStringItemDictionary.Should().BeEmpty();
    }

    [TestMethod]
    public void SerializationModifiers_ExtractGameStringTextPropertyHasValue_PropertyShouldBeExtracted()
    {
        // arrange
        GameStringItemDictionary gameStringItemDictionary = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringItemDictionary, LocalizedTextOption.Extract);

        Hero hero = new("id")
        {
            Name = new GameStringText("value"),
        };

        string json = JsonSerializer.Serialize(hero, jsonSerializerOptions);

        // act
        JsonDocument jsonDocument = JsonDocument.Parse(json); // read the result json to verify the SerialiazationModifiers

        // assert
        jsonDocument.RootElement.TryGetProperty("name", out _).Should().BeFalse();
        gameStringItemDictionary.Should().ContainSingle();
    }

    [TestMethod]
    public void SerializationModifiers_NoneGameStringTextPropertyIsNull_PropertyShouldNotBeCopied()
    {
        // arrange
        GameStringItemDictionary gameStringItemDictionary = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringItemDictionary, LocalizedTextOption.None);

        Hero hero = new("id")
        {
            Name = null,
        };

        string json = JsonSerializer.Serialize(hero, jsonSerializerOptions);

        // act
        JsonDocument jsonDocument = JsonDocument.Parse(json); // read the result json to verify the SerialiazationModifiers

        // assert
        jsonDocument.RootElement.TryGetProperty("name", out _).Should().BeFalse();
        gameStringItemDictionary.Should().BeEmpty();
    }

    [TestMethod]
    public void SerializationModifiers_CopyGameStringTextPropertyIsNull_PropertyShouldNotBeCopied()
    {
        // arrange
        GameStringItemDictionary gameStringItemDictionary = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringItemDictionary, LocalizedTextOption.Copy);

        Hero hero = new("id")
        {
            Name = null,
        };

        string json = JsonSerializer.Serialize(hero, jsonSerializerOptions);

        // act
        JsonDocument jsonDocument = JsonDocument.Parse(json); // read the result json to verify the SerialiazationModifiers

        // assert
        jsonDocument.RootElement.TryGetProperty("name", out _).Should().BeFalse();
        gameStringItemDictionary.Should().BeEmpty();
    }

    [TestMethod]
    public void SerializationModifiers_CopyGameStringTextPropertyHasValue_PropertyShouldBeCopied()
    {
        // arrange
        GameStringItemDictionary gameStringItemDictionary = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringItemDictionary, LocalizedTextOption.Copy);

        Hero hero = new("id")
        {
            Name = new GameStringText("value"),
        };

        string json = JsonSerializer.Serialize(hero, jsonSerializerOptions);

        // act
        JsonDocument jsonDocument = JsonDocument.Parse(json); // read the result json to verify the SerialiazationModifiers

        // assert
        jsonDocument.RootElement.TryGetProperty("name", out _).Should().BeTrue();
        gameStringItemDictionary.Should().ContainSingle();
    }

    [TestMethod]
    public void SerializationModifiers_NoneGameStringTextPropertyHasValue_PropertyShouldNotBeExtracted()
    {
        // arrange
        GameStringItemDictionary gameStringItemDictionary = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringItemDictionary, LocalizedTextOption.None);

        Hero hero = new("id")
        {
            Name = new GameStringText("value"),
        };

        string json = JsonSerializer.Serialize(hero, jsonSerializerOptions);

        // act
        JsonDocument jsonDocument = JsonDocument.Parse(json); // read the result json to verify the SerialiazationModifiers

        // assert
        jsonDocument.RootElement.TryGetProperty("name", out _).Should().BeTrue();
        gameStringItemDictionary.Should().BeEmpty();
    }

    [TestMethod]
    public void SerializationModifiers_PropertyIsIElementObject_GameStringItemDictionaryHasPropertyName()
    {
        // arrange
        GameStringItemDictionary gameStringItemDictionary = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringItemDictionary, LocalizedTextOption.Extract);

        Unit unit = new("unitId")
        {
            Name = new GameStringText("value"),
        };

        // act
        JsonSerializer.Serialize(unit, jsonSerializerOptions); // serialize to get the gameStringItemDictionary

        // assert
        gameStringItemDictionary["unit"]["name"]["unitId"].RawText.Should().Be("value");
    }

    [TestMethod]
    public void SerializationModifiers_PropertyIsHero_GameStringItemDictionaryHasPropertyName()
    {
        // arrange
        GameStringItemDictionary gameStringItemDictionary = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringItemDictionary, LocalizedTextOption.Extract);

        Hero hero = new("heroId")
        {
            Name = new GameStringText("heroName"),
        };

        // act
        JsonSerializer.Serialize(hero, jsonSerializerOptions); // serialize to get the gameStringItemDictionary

        // assert
        gameStringItemDictionary["hero"]["name"]["heroId"].RawText.Should().Be("heroName");
    }

    [TestMethod]
    public void SerializationModifiers_PropertyIsAbility_GameStringItemDictionaryHasPropertyName()
    {
        // arrange
        GameStringItemDictionary gameStringItemDictionary = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringItemDictionary, LocalizedTextOption.Extract);

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
        JsonSerializer.Serialize(unit, jsonSerializerOptions); // serialize to get the gameStringItemDictionary

        // assert
        gameStringItemDictionary["ability"]["name"]["abil1|button1|Q"].RawText.Should().Be("value");
    }

    [TestMethod]
    public void SerializationModifiers_PropertyIsTalent_GameStringItemDictionaryHasPropertyName()
    {
        // arrange
        GameStringItemDictionary gameStringItemDictionary = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringItemDictionary, LocalizedTextOption.Extract);

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
        JsonSerializer.Serialize(hero, jsonSerializerOptions); // serialize to get the gameStringItemDictionary

        // assert
        gameStringItemDictionary["talent"]["name"]["talent1|button1|Q|Level4"].RawText.Should().Be("talentName");
    }

    [TestMethod]
    public void SerializationModifiers_UnitInnerGameStringTextProperties_GameStringItemDictionaryHasPropertyName()
    {
        // arrange
        GameStringItemDictionary gameStringItemDictionary = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringItemDictionary, LocalizedTextOption.Extract);

        Hero hero = new("heroId")
        {
            Name = new GameStringText("heroName"),
            Life =
            {
                LifeMax = 110,
                LifeType = new GameStringText("Health"),
            },
            Energy =
            {
                EnergyMax = 120,
                EnergyType = new GameStringText("Energy"),
            },
            Shield =
            {
                ShieldMax = 130,
                ShieldType = new GameStringText("Shield"),
            },
        };

        // act
        JsonSerializer.Serialize(hero, jsonSerializerOptions); // serialize to get the gameStringItemDictionary

        // assert
        gameStringItemDictionary["unit"]["lifeType"]["heroId"].RawText.Should().Be("Health");
        gameStringItemDictionary["unit"]["energyType"]["heroId"].RawText.Should().Be("Energy");
        gameStringItemDictionary["unit"]["shieldType"]["heroId"].RawText.Should().Be("Shield");
    }

    [TestMethod]
    public void SerializationModifiers_IEnumerableGameStringText_GameStringItemDictionaryHasPropertyName()
    {
        // arrange
        GameStringItemDictionary gameStringItemDictionary = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringItemDictionary, LocalizedTextOption.Extract);

        Hero hero = new("heroId")
        {
            Name = new GameStringText("heroName"),
            Roles =
            {
                new GameStringText("Role1"),
                new GameStringText("Role2"),
            },
        };

        // act
        JsonSerializer.Serialize(hero, jsonSerializerOptions); // serialize to get the gameStringItemDictionary

        // assert
        gameStringItemDictionary["hero"]["roles"]["heroId"].RawText.Should().Be("Role1|Role2");
    }

    [TestMethod]
    public void SerializationModifiers_SerializingMultipleElements_SetsGameStringItemDictionary()
    {
        // arrange
        GameStringItemDictionary gameStringItemDictionary = [];
        JsonSerializerOptions jsonSerializerOptions = GetExtractSerializerOptions(gameStringItemDictionary, LocalizedTextOption.Extract);

        Unit unit1 = new("unitId1")
        {
            Name = new GameStringText("value1"),
            Abilities =
            {
                {
                    AbilityTier.Mount,
                    [
                        new Ability()
                        {
                            AbilityElementId = "Mount",
                            ButtonElementId = "SummonMount",
                            AbilityType = AbilityType.Z,
                            CooldownText = new GameStringText("Cooldown: 4 seconds"),
                        }
                    ]
                },
            },
        };
        Unit unit2 = new("unitId2")
        {
            Name = new GameStringText("value2"),
            Description = new GameStringText("desc2"),
            Abilities =
            {
                {
                    AbilityTier.Mount,
                    [
                        new Ability()
                        {
                            AbilityElementId = "Mount",
                            ButtonElementId = "SummonMount",
                            AbilityType = AbilityType.Z,
                            CooldownText = new GameStringText("Cooldown: 6 seconds"),
                        }
                    ]
                },
            },
        };

        // act
        JsonSerializer.Serialize(unit1, jsonSerializerOptions);
        JsonSerializer.Serialize(unit2, jsonSerializerOptions);

        // assert
        gameStringItemDictionary["unit"]["name"]["unitId1"].RawText.Should().Be("value1");
        gameStringItemDictionary["unit"]["name"]["unitId2"].RawText.Should().Be("value2");
        gameStringItemDictionary["unit"]["description"]["unitId2"].RawText.Should().Be("desc2");
        gameStringItemDictionary["ability"]["cooldownText"]["Mount|SummonMount|Z"].RawText.Should().Be("Cooldown: 6 seconds");
    }

    private static JsonSerializerOptions GetExtractSerializerOptions(GameStringItemDictionary gameStringItemDictionary, LocalizedTextOption localizedTextOption)
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
                    typeInfo => JsonTypeInfoModifiers.SerializationModifiers(typeInfo, localizedTextOption, gameStringItemDictionary),
                },
            },
        };
        return jsonSerializerOptions;
    }
}