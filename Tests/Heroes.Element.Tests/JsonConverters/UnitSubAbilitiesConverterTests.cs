using Heroes.Element.Comparers;

namespace Heroes.Element.JsonConverter.Tests;

[TestClass]
public class UnitSubAbilitiesConverterTests
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public UnitSubAbilitiesConverterTests()
    {
        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Converters =
            {
                new LinkIdConverter(),
                new AbilityLinkIdConverter(),
                new GameStringTextConverter(),
            },
        };
    }

    [TestMethod]
    public void Read_HasSingleLinkIdWithTierAndAbility_ReturnsDictionaryWithTierSetOnAbility()
    {
        // arrange
        string json =
        """
        {
          "SubAbilities": {
            "ParentAbility|ParentButton|Q": {
              "Action": [
                {
                  "abilityId": "SubAbility1",
                  "buttonId": "SubButton1",
                  "AbilityType": 7
                }
              ]
            }
          }
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        AbilityLinkId parentKey = new("ParentAbility", "ParentButton", AbilityType.Q);
        testClass.SubAbilities.Should().ContainKey(parentKey);
        testClass.SubAbilities![parentKey].Should().ContainKey(AbilityTier.Action);
        testClass.SubAbilities[parentKey][AbilityTier.Action].Should().ContainSingle();
        testClass.SubAbilities[parentKey][AbilityTier.Action][0].AbilityElementId.Should().Be("SubAbility1");
        testClass.SubAbilities[parentKey][AbilityTier.Action][0].ButtonElementId.Should().Be("SubButton1");
        testClass.SubAbilities[parentKey][AbilityTier.Action][0].AbilityType.Should().Be(AbilityType.Active);
        testClass.SubAbilities[parentKey][AbilityTier.Action][0].Tier.Should().Be(AbilityTier.Action);
    }

    [TestMethod]
    public void Read_HasMultipleLinkIds_ReturnsDictionaryWithCorrectTierSetOnEach()
    {
        // arrange
        string json =
        """
        {
          "SubAbilities": {
            "AbilityA|ButtonA|Q": {
              "Action": [
                {
                  "abilityId": "Sub1",
                  "buttonId": "SubBtn1"
                }
              ]
            },
            "AbilityB|ButtonB|W": {
              "Activable": [
                {
                  "abilityId": "Sub2",
                  "buttonId": "SubBtn2"
                }
              ]
            }
          }
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.SubAbilities.Should().HaveCount(2);

        AbilityLinkId keyA = new("AbilityA", "ButtonA", AbilityType.Q);
        testClass.SubAbilities![keyA][AbilityTier.Action][0].Tier.Should().Be(AbilityTier.Action);

        AbilityLinkId keyB = new("AbilityB", "ButtonB", AbilityType.W);
        testClass.SubAbilities[keyB][AbilityTier.Activable][0].Tier.Should().Be(AbilityTier.Activable);
    }

    [TestMethod]
    public void Read_HasMultipleTiersUnderOneLinkId_ReturnsTierSetOnAllAbilities()
    {
        // arrange
        string json =
        """
        {
          "SubAbilities": {
            "ParentAbility|ParentButton|Q": {
              "Action": [
                {
                  "abilityId": "Sub1",
                  "buttonId": "SubBtn1"
                }
              ],
              "Hidden": [
                {
                  "abilityId": "Sub2",
                  "buttonId": "SubBtn2"
                }
              ]
            }
          }
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        AbilityLinkId parentKey = new("ParentAbility", "ParentButton", AbilityType.Q);
        testClass.SubAbilities![parentKey].Should().HaveCount(2);
        testClass.SubAbilities[parentKey][AbilityTier.Action][0].Tier.Should().Be(AbilityTier.Action);
        testClass.SubAbilities[parentKey][AbilityTier.Hidden][0].Tier.Should().Be(AbilityTier.Hidden);
    }

    [TestMethod]
    public void Read_HasMultipleAbilitiesInTier_ReturnsTierSetOnAllAbilities()
    {
        // arrange
        string json =
        """
        {
          "SubAbilities": {
            "ParentAbility|ParentButton|Q": {
              "Action": [
                {
                  "abilityId": "SubA",
                  "buttonId": "SubBtnA"
                },
                {
                  "abilityId": "SubB",
                  "buttonId": "SubBtnB"
                }
              ]
            }
          }
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        AbilityLinkId parentKey = new("ParentAbility", "ParentButton", AbilityType.Q);
        testClass.SubAbilities![parentKey][AbilityTier.Action].Should().HaveCount(2);
        testClass.SubAbilities[parentKey][AbilityTier.Action][0].Tier.Should().Be(AbilityTier.Action);
        testClass.SubAbilities[parentKey][AbilityTier.Action][1].Tier.Should().Be(AbilityTier.Action);
    }

    [TestMethod]
    public void Read_HasNullValue_ReturnsNull()
    {
        // arrange
        string json =
        """
        {
          "SubAbilities": null
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.SubAbilities.Should().BeNull();
    }

    [TestMethod]
    public void Read_HasEmptyObject_ReturnsEmptyDictionary()
    {
        // arrange
        string json =
        """
        {
          "SubAbilities": {}
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.SubAbilities.Should().BeEmpty();
    }

    [TestMethod]
    public void Write_HasSingleLinkIdWithTierAndAbility_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            SubAbilities = new SortedDictionary<LinkId, IDictionary<AbilityTier, IList<Ability>>>(new LinkIdComparer())
            {
                [new AbilityLinkId("ParentAbility", "ParentButton", AbilityType.Q)] = new SortedDictionary<AbilityTier, IList<Ability>>
                {
                    [AbilityTier.Action] =
                    [
                        new Ability
                        {
                            AbilityElementId = "SubAbility1",
                            ButtonElementId = "SubButton1",
                            AbilityType = AbilityType.Active,
                            Tier = AbilityTier.Action,
                        },
                    ],
                },
            },
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
            """
            {
              "SubAbilities": {
                "ParentAbility|ParentButton|Q": {
                  "Action": [
                    {
                      "LinkId": "SubAbility1|SubButton1|Active",
                      "abilityId": "SubAbility1",
                      "buttonId": "SubButton1",
                      "Name": null,
                      "Icon": null,
                      "ToggleCooldown": null,
                      "Charges": null,
                      "EnergyText": null,
                      "LifeText": null,
                      "CooldownText": null,
                      "ShortText": null,
                      "FullText": null,
                      "AbilityType": 7
                    }
                  ]
                }
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
            SubAbilities = new SortedDictionary<LinkId, IDictionary<AbilityTier, IList<Ability>>>(new LinkIdComparer())
            {
                [new AbilityLinkId("ParentAbility", "ParentButton", AbilityType.Q)] = new SortedDictionary<AbilityTier, IList<Ability>>
                {
                    [AbilityTier.Action] =
                    [
                        new Ability
                        {
                            AbilityElementId = "AbilityPassive",
                            ButtonElementId = "ButtonPassive",
                            AbilityType = AbilityType.Passive,
                            Tier = AbilityTier.Action,
                        },
                        new Ability
                        {
                            AbilityElementId = "AbilityActive",
                            ButtonElementId = "ButtonActive",
                            AbilityType = AbilityType.Active,
                            Tier = AbilityTier.Action,
                        },
                    ],
                },
            },
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
            """
            {
              "SubAbilities": {
                "ParentAbility|ParentButton|Q": {
                  "Action": [
                    {
                      "LinkId": "AbilityActive|ButtonActive|Active",
                      "abilityId": "AbilityActive",
                      "buttonId": "ButtonActive",
                      "Name": null,
                      "Icon": null,
                      "ToggleCooldown": null,
                      "Charges": null,
                      "EnergyText": null,
                      "LifeText": null,
                      "CooldownText": null,
                      "ShortText": null,
                      "FullText": null,
                      "AbilityType": 7
                    },
                    {
                      "LinkId": "AbilityPassive|ButtonPassive|Passive",
                      "abilityId": "AbilityPassive",
                      "buttonId": "ButtonPassive",
                      "Name": null,
                      "Icon": null,
                      "ToggleCooldown": null,
                      "Charges": null,
                      "EnergyText": null,
                      "LifeText": null,
                      "CooldownText": null,
                      "ShortText": null,
                      "FullText": null,
                      "AbilityType": 8
                    }
                  ]
                }
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
            SubAbilities = new SortedDictionary<LinkId, IDictionary<AbilityTier, IList<Ability>>>(new LinkIdComparer()),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "SubAbilities": {}
        }
        """);
    }

    public class TestClass
    {
        [JsonConverter(typeof(UnitSubAbilitiesConverter))]
        public IDictionary<LinkId, IDictionary<AbilityTier, IList<Ability>>>? SubAbilities { get; set; }
    }
}
