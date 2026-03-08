namespace Heroes.Element.JsonConverters.Tests;

[TestClass]
public class UnitAbilitiesConverterTests
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public UnitAbilitiesConverterTests()
    {
        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Converters =
            {
                new AbilityLinkIdConverter(),
                new GameStringTextConverter(),
            },
        };
    }

    [TestMethod]
    public void Read_HasSingleTierWithAbility_ReturnsDictionaryWithTierSetOnAbility()
    {
        // arrange
        string json =
        """
        {
          "Abilities": {
            "Basic": [
              {
                "abilityId": "Ability1",
                "buttonId": "Button1",
                "AbilityType": 1
              }
            ]
          }
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Abilities.Should().ContainKey(AbilityTier.Basic);
        testClass.Abilities![AbilityTier.Basic].Should().ContainSingle();
        testClass.Abilities[AbilityTier.Basic][0].AbilityElementId.Should().Be("Ability1");
        testClass.Abilities[AbilityTier.Basic][0].ButtonElementId.Should().Be("Button1");
        testClass.Abilities[AbilityTier.Basic][0].AbilityType.Should().Be(AbilityType.Q);
        testClass.Abilities[AbilityTier.Basic][0].Tier.Should().Be(AbilityTier.Basic);
    }

    [TestMethod]
    public void Read_HasMultipleTiers_ReturnsDictionaryWithCorrectTierSetOnEachAbility()
    {
        // arrange
        string json =
        """
        {
          "Abilities": {
            "Basic": [
              {
                "abilityId": "Ability1",
                "buttonId": "Button1"
              }
            ],
            "Heroic": [
              {
                "abilityId": "Ability2",
                "buttonId": "Button2"
              }
            ]
          }
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Abilities.Should().HaveCount(2);
        testClass.Abilities![AbilityTier.Basic][0].Tier.Should().Be(AbilityTier.Basic);
        testClass.Abilities[AbilityTier.Heroic][0].Tier.Should().Be(AbilityTier.Heroic);
    }

    [TestMethod]
    public void Read_HasMultipleAbilitiesInTier_ReturnsTierSetOnAllAbilities()
    {
        // arrange
        string json =
        """
        {
          "Abilities": {
            "Basic": [
              {
                "abilityId": "AbilityA",
                "buttonId": "ButtonA"
              },
              {
                "abilityId": "AbilityB",
                "buttonId": "ButtonB"
              }
            ]
          }
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Abilities![AbilityTier.Basic].Should().HaveCount(2);
        testClass.Abilities[AbilityTier.Basic][0].Tier.Should().Be(AbilityTier.Basic);
        testClass.Abilities[AbilityTier.Basic][1].Tier.Should().Be(AbilityTier.Basic);
    }

    [TestMethod]
    public void Read_HasNullValue_ReturnsNull()
    {
        // arrange
        string json =
        """
        {
          "Abilities": null
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Abilities.Should().BeNull();
    }

    [TestMethod]
    public void Read_HasEmptyObject_ReturnsEmptyDictionary()
    {
        // arrange
        string json =
        """
        {
          "Abilities": {}
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Abilities.Should().BeEmpty();
    }

    [TestMethod]
    public void Write_HasSingleTierWithAbility_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            Abilities = new SortedDictionary<AbilityTier, IList<Ability>>
            {
                [AbilityTier.Basic] =
                [
                    new Ability
                    {
                        AbilityElementId = "Ability1",
                        ButtonElementId = "Button1",
                        AbilityType = AbilityType.Q,
                        Tier = AbilityTier.Basic,
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
              "Abilities": {
                "Basic": [
                  {
                    "LinkId": "Ability1|Button1|Q",
                    "abilityId": "Ability1",
                    "buttonId": "Button1",
                    "Name": null,
                    "Icon": null,
                    "ToggleCooldown": null,
                    "Charges": null,
                    "EnergyText": null,
                    "LifeText": null,
                    "CooldownText": null,
                    "ShortText": null,
                    "FullText": null,
                    "AbilityType": 1
                  }
                ]
              }
            }
            """);

    }

    [TestMethod]
    public void Write_HasMultipleAbilitiesInTier_ReturnsAbilitiesSortedByAbilityType()
    {
        // arrange
        TestClass testClass = new()
        {
            Abilities = new SortedDictionary<AbilityTier, IList<Ability>>
            {
                [AbilityTier.Basic] =
                [
                    new Ability
                    {
                        AbilityElementId = "AbilityE",
                        ButtonElementId = "ButtonE",
                        AbilityType = AbilityType.E,
                        Tier = AbilityTier.Basic,
                    },
                    new Ability
                    {
                        AbilityElementId = "AbilityQ",
                        ButtonElementId = "ButtonQ",
                        AbilityType = AbilityType.Q,
                        Tier = AbilityTier.Basic,
                    },
                    new Ability
                    {
                        AbilityElementId = "AbilityW",
                        ButtonElementId = "ButtonW",
                        AbilityType = AbilityType.W,
                        Tier = AbilityTier.Basic,
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
              "Abilities": {
                "Basic": [
                  {
                    "LinkId": "AbilityQ|ButtonQ|Q",
                    "abilityId": "AbilityQ",
                    "buttonId": "ButtonQ",
                    "Name": null,
                    "Icon": null,
                    "ToggleCooldown": null,
                    "Charges": null,
                    "EnergyText": null,
                    "LifeText": null,
                    "CooldownText": null,
                    "ShortText": null,
                    "FullText": null,
                    "AbilityType": 1
                  },
                  {
                    "LinkId": "AbilityW|ButtonW|W",
                    "abilityId": "AbilityW",
                    "buttonId": "ButtonW",
                    "Name": null,
                    "Icon": null,
                    "ToggleCooldown": null,
                    "Charges": null,
                    "EnergyText": null,
                    "LifeText": null,
                    "CooldownText": null,
                    "ShortText": null,
                    "FullText": null,
                    "AbilityType": 2
                  },
                  {
                    "LinkId": "AbilityE|ButtonE|E",
                    "abilityId": "AbilityE",
                    "buttonId": "ButtonE",
                    "Name": null,
                    "Icon": null,
                    "ToggleCooldown": null,
                    "Charges": null,
                    "EnergyText": null,
                    "LifeText": null,
                    "CooldownText": null,
                    "ShortText": null,
                    "FullText": null,
                    "AbilityType": 3
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
            Abilities = new SortedDictionary<AbilityTier, IList<Ability>>(),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "Abilities": {}
        }
        """);
    }

    public class TestClass
    {
        [JsonConverter(typeof(UnitAbilitiesConverter))]
        public IDictionary<AbilityTier, IList<Ability>>? Abilities { get; set; }
    }
}
