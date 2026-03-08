namespace Heroes.Element.JsonConverters.Tests;

[TestClass]
public class HeroTalentsConverterTests
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public HeroTalentsConverterTests()
    {
        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Converters =
            {
                new HeroTalentsConverter(),
                new TalentLinkIdConverter(),
                new AbilityLinkIdConverter(),
                new GameStringTextConverter(),
            },
        };
    }

    [TestMethod]
    public void Read_HasSingleTierWithTalent_ReturnsDictionaryWithTierSetOnTalent()
    {
        // arrange
        string json =
        """
        {
          "Talents": {
            "Level1": [
              {
                "linkId": "Talent1|Button1|W|Level1",
                "talentId": "Talent1",
                "buttonId": "Button1",
                "sort": 1
              }
            ]
          }
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Talents.Should().ContainKey(TalentTier.Level1);
        testClass.Talents![TalentTier.Level1].Should().ContainSingle();
        testClass.Talents[TalentTier.Level1][0].TalentElementId.Should().Be("Talent1");
        testClass.Talents[TalentTier.Level1][0].ButtonElementId.Should().Be("Button1");
        testClass.Talents[TalentTier.Level1][0].Column.Should().Be(1);
        testClass.Talents[TalentTier.Level1][0].Tier.Should().Be(TalentTier.Level1);
    }

    [TestMethod]
    public void Read_HasMultipleTiers_ReturnsDictionaryWithCorrectTierSetOnEachTalent()
    {
        // arrange
        string json =
        """
        {
          "Talents": {
            "Level1": [
              {
                "talentId": "Talent1",
                "buttonId": "Button1",
                "sort": 1
              }
            ],
            "Level4": [
              {
                "talentId": "Talent2",
                "buttonId": "Button2",
                "sort": 1
              }
            ]
          }
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Talents.Should().HaveCount(2);
        testClass.Talents![TalentTier.Level1][0].Tier.Should().Be(TalentTier.Level1);
        testClass.Talents[TalentTier.Level4][0].Tier.Should().Be(TalentTier.Level4);
    }

    [TestMethod]
    public void Read_HasMultipleTalentsInTier_ReturnsTierSetOnAllTalents()
    {
        // arrange
        string json =
        """
        {
          "Talents": {
            "Level10": [
              {
                "talentId": "TalentA",
                "buttonId": "ButtonA",
                "sort": 1
              },
              {
                "talentId": "TalentB",
                "buttonId": "ButtonB",
                "sort": 2
              }
            ]
          }
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Talents![TalentTier.Level10].Should().HaveCount(2);
        testClass.Talents[TalentTier.Level10][0].Tier.Should().Be(TalentTier.Level10);
        testClass.Talents[TalentTier.Level10][1].Tier.Should().Be(TalentTier.Level10);
    }

    [TestMethod]
    public void Read_HasNullValue_ReturnsNull()
    {
        // arrange
        string json =
        """
        {
          "Talents": null
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Talents.Should().BeNull();
    }

    [TestMethod]
    public void Read_NotObjectType_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "Talents": "invalid"
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Read_HasInvalidTalentTier_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "Talents": {
            "InvalidTier": [
              {
                "talentId": "Talent1",
                "buttonId": "Button1",
                "sort": 1
              }
            ]
          }
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<ArgumentException>();
    }

    [TestMethod]
    public void Read_HasEmptyObject_ReturnsEmptyDictionary()
    {
        // arrange
        string json =
        """
        {
          "Talents": {}
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Talents.Should().BeEmpty();
    }

    [TestMethod]
    public void Write_HasSingleTierWithTalent_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            Talents = new Dictionary<TalentTier, IList<Talent>>
            {
                [TalentTier.Level1] =
                [
                    new Talent
                    {
                        TalentElementId = "Talent1",
                        ButtonElementId = "Button1",
                        Column = 1,
                        Tier = TalentTier.Level1,
                    },
                ],
            },
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
            """
            {
              "Talents": {
                "Level1": [
                  {
                    "LinkId": "Talent1|Button1|Unknown|Level1",
                    "talentId": "Talent1",
                    "buttonId": "Button1",
                    "abilityId": "",
                    "Name": null,
                    "Icon": null,
                    "ToggleCooldown": null,
                    "Charges": null,
                    "EnergyText": null,
                    "LifeText": null,
                    "CooldownText": null,
                    "ShortText": null,
                    "FullText": null,
                    "AbilityType": 0,
                    "IsQuest": false,
                    "UpgradesAbilityType": false,
                    "sort": 1,
                    "AbilityTalentLinkIds": [],
                    "TooltipAbilityLinkIds": [],
                    "PrerequisiteTalentIds": []
                  }
                ]
              }
            }
            """);
    }

    [TestMethod]
    public void Write_HasMultipleTalentsInTier_ReturnsTalentsSortedByColumn()
    {
        // arrange
        TestClass testClass = new()
        {
            Talents = new Dictionary<TalentTier, IList<Talent>>
            {
                [TalentTier.Level1] =
                [
                    new Talent
                    {
                        TalentElementId = "TalentC",
                        ButtonElementId = "ButtonC",
                        Column = 3,
                        Tier = TalentTier.Level1,
                    },
                    new Talent
                    {
                        TalentElementId = "TalentA",
                        ButtonElementId = "ButtonA",
                        Column = 1,
                        Tier = TalentTier.Level1,
                    },
                    new Talent
                    {
                        TalentElementId = "TalentB",
                        ButtonElementId = "ButtonB",
                        Column = 2,
                        Tier = TalentTier.Level1,
                    },
                ],
            },
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
            """
            {
              "Talents": {
                "Level1": [
                  {
                    "LinkId": "TalentA|ButtonA|Unknown|Level1",
                    "talentId": "TalentA",
                    "buttonId": "ButtonA",
                    "abilityId": "",
                    "Name": null,
                    "Icon": null,
                    "ToggleCooldown": null,
                    "Charges": null,
                    "EnergyText": null,
                    "LifeText": null,
                    "CooldownText": null,
                    "ShortText": null,
                    "FullText": null,
                    "AbilityType": 0,
                    "IsQuest": false,
                    "UpgradesAbilityType": false,
                    "sort": 1,
                    "AbilityTalentLinkIds": [],
                    "TooltipAbilityLinkIds": [],
                    "PrerequisiteTalentIds": []
                  },
                  {
                    "LinkId": "TalentB|ButtonB|Unknown|Level1",
                    "talentId": "TalentB",
                    "buttonId": "ButtonB",
                    "abilityId": "",
                    "Name": null,
                    "Icon": null,
                    "ToggleCooldown": null,
                    "Charges": null,
                    "EnergyText": null,
                    "LifeText": null,
                    "CooldownText": null,
                    "ShortText": null,
                    "FullText": null,
                    "AbilityType": 0,
                    "IsQuest": false,
                    "UpgradesAbilityType": false,
                    "sort": 2,
                    "AbilityTalentLinkIds": [],
                    "TooltipAbilityLinkIds": [],
                    "PrerequisiteTalentIds": []
                  },
                  {
                    "LinkId": "TalentC|ButtonC|Unknown|Level1",
                    "talentId": "TalentC",
                    "buttonId": "ButtonC",
                    "abilityId": "",
                    "Name": null,
                    "Icon": null,
                    "ToggleCooldown": null,
                    "Charges": null,
                    "EnergyText": null,
                    "LifeText": null,
                    "CooldownText": null,
                    "ShortText": null,
                    "FullText": null,
                    "AbilityType": 0,
                    "IsQuest": false,
                    "UpgradesAbilityType": false,
                    "sort": 3,
                    "AbilityTalentLinkIds": [],
                    "TooltipAbilityLinkIds": [],
                    "PrerequisiteTalentIds": []
                  }
                ]
              }
            }
            """);
    }

    [TestMethod]
    public void Write_HasEmptyDictionary_ReturnsEmptyJson()
    {
        // arrange
        TestClass testClass = new()
        {
            Talents = new Dictionary<TalentTier, IList<Talent>>(),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "Talents": {}
        }
        """);
    }

    public class TestClass
    {
        public IDictionary<TalentTier, IList<Talent>>? Talents { get; set; }
    }
}
