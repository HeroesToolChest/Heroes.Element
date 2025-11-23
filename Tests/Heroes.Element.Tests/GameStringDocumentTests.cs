namespace Heroes.Element.Tests;

[TestClass]
public class GameStringDocumentTests
{
    [TestMethod]
    public void MetaGameStringProperties_WithMinimalMeta_ReturnsMinimalProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "hdpVersion": "5.0.0"
          },
          "gamestrings": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        MetaGameStringProperties meta = document.MetaGameStringProperties;

        // assert
        meta.Should().NotBeNull();
        meta.HdpVersion.Should().Be("5.0.0");
        meta.DataTypes.Should().NotBeNull();
        meta.DataTypes.Should().BeEmpty();
        meta.DescriptionText.Should().NotBeNull();
        meta.IsLegacy.Should().BeFalse();
    }

    [TestMethod]
    public void MetaGameStringProperties_WithoutMetaSection_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "gamestrings": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);

        // act
        Action act = () => GameStringDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void MetaGameStringProperties_WithoutGamestringsSection_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "hdpVersion": "5.0.0"
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);

        // act
        Action act = () => GameStringDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void MetaGameStringProperties_WithDifferentLocale_ReturnsCorrectLocale()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "hdpVersion": "5.0.0",
            "descriptionText": {
              "locale": "FRFR",
              "gameStringTextType": "ColoredText"
            }
          },
          "gamestrings": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        MetaGameStringProperties meta = document.MetaGameStringProperties;

        // assert
        meta.Should().NotBeNull();
        meta.DescriptionText.Should().NotBeNull();
        meta.DescriptionText!.Locale.Should().Be(StormLocale.FRFR);
        meta.DescriptionText.GameStringTextType.Should().Be(GameStringTextType.ColoredText);
    }

    [TestMethod]
    public void MetaGameStringProperties_MultipleDataTypes_ReturnsSortedCollection()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "unitdata",
              "abilitydata",
              "herodata"
            ]
          },
          "gamestrings": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        MetaGameStringProperties meta = document.MetaGameStringProperties;

        // assert
        meta.DataTypes.Should().HaveCount(3).And
            .ContainInOrder("abilitydata", "herodata", "unitdata");
    }

    [TestMethod]
    public void UpdateGameStrings_Hero_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "herodata"
            ],
            "descriptionText": {
              "locale": "ENUS",
              "gameStringTextType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "gamestrings": {
            "unit": {
            },
            "hero": {
              "description": {
                "Abathur": "A unique Hero who can manipulate the battle from anywhere on the map."
              },
              "name": {
                "Abathur": "Abathur"
              },
              "difficulty": {
                "Abathur": "Very Hard"
              },
              "expandedRole": {
                "Abathur": "Utility Support"
              },
              "infoText": {
                "Abathur": "Specialist Hero from the StarCraft universe"
              },
              "searchText": {
                "Abathur": "Abathur Evolution Master Slug"
              },
              "sortName": {
                "Abathur": "Abathur"
              },
              "title": {
                "Abathur": "The Evolution Master"
              },
              "roles": {
                "Abathur": "Support,Specialist"
              },
              "energyType": {
                "Abathur": "mana"
              },
              "lifeType": {
                "Abathur": "health"
              },
              "shieldType": {
                "Abathur": "shield"
              }
            },
            "talent": {
              "cooldownText": {
                "AbathurPressurizedGlands|AbathurPressurizedGlands|Heroic|Level1": "Cooldown: 5 seconds",
                "AbathurSurvivalInstincts|AbathurSurvivalInstincts|Active|Level4": "Cooldown: 10 seconds"
              },
              "energyText": {
                "AbathurPressurizedGlands|AbathurPressurizedGlands|Heroic|Level1": "Costs 50 Mana"
              },
              "lifeText": {
                "AbathurPressurizedGlands|AbathurPressurizedGlands|Heroic|Level1": "Costs 5 Health"
              },
              "name": {
                "AbathurPressurizedGlands|AbathurPressurizedGlands|Heroic|Level1": "Pressurized Glands",
                "AbathurSurvivalInstincts|AbathurSurvivalInstincts|Active|Level4": "Survival Instincts"
              },
              "shortText": {
                "AbathurPressurizedGlands|AbathurPressurizedGlands|Heroic|Level1": "Increases Spike Burst damage."
              },
              "fullText": {
                "AbathurPressurizedGlands|AbathurPressurizedGlands|Heroic|Level1": "Increases Spike Burst damage by 20%."
              }
            }
          }
        }
        """;
        Hero hero = new("Abathur");
        hero.Talents.Add(
            TalentTier.Level1,
            [
                new Talent() { TalentElementId = "AbathurPressurizedGlands", ButtonElementId = "AbathurPressurizedGlands", AbilityType = AbilityType.Heroic, Tier = TalentTier.Level1 }
            ]);
        hero.Talents.Add(
            TalentTier.Level4,
            [
                new Talent() { TalentElementId = "AbathurSurvivalInstincts", ButtonElementId = "AbathurSurvivalInstincts", AbilityType = AbilityType.Active, Tier = TalentTier.Level4 },
                new Talent() { TalentElementId = "AbathurRegenBioSteel", ButtonElementId = "AbathurRegenBioSteel", AbilityType = AbilityType.Passive, Tier = TalentTier.Level4, Name = new GameStringText("Bio") }
            ]);

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(hero);

        // assert
        hero.Description!.RawText.Should().Be("A unique Hero who can manipulate the battle from anywhere on the map.");
        hero.Name!.RawText.Should().Be("Abathur");
        hero.Difficulty!.RawText.Should().Be("Very Hard");
        hero.ExpandedRole!.RawText.Should().Be("Utility Support");
        hero.InfoText!.RawText.Should().Be("Specialist Hero from the StarCraft universe");
        hero.SearchText!.RawText.Should().Be("Abathur Evolution Master Slug");
        hero.SortName!.RawText.Should().Be("Abathur");
        hero.Title!.RawText.Should().Be("The Evolution Master");
        hero.Roles.Should().HaveCount(2);
        hero.Roles.Select(r => r.RawText).Should().Contain(new[] { "Support", "Specialist" });
        hero.Energy.EnergyType!.RawText.Should().Be("mana");
        hero.Life.LifeType!.RawText.Should().Be("health");
        hero.Shield.ShieldType!.RawText.Should().Be("shield");

        hero.Talents[TalentTier.Level1][0].CooldownText!.RawText.Should().Be("Cooldown: 5 seconds");
        hero.Talents[TalentTier.Level1][0].EnergyText!.RawText.Should().Be("Costs 50 Mana");
        hero.Talents[TalentTier.Level1][0].LifeText!.RawText.Should().Be("Costs 5 Health");
        hero.Talents[TalentTier.Level1][0].Name!.RawText.Should().Be("Pressurized Glands");
        hero.Talents[TalentTier.Level1][0].ShortText!.RawText.Should().Be("Increases Spike Burst damage.");
        hero.Talents[TalentTier.Level1][0].FullText!.RawText.Should().Be("Increases Spike Burst damage by 20%.");

        hero.Talents[TalentTier.Level4][0].CooldownText!.RawText.Should().Be("Cooldown: 10 seconds");
        hero.Talents[TalentTier.Level4][0].EnergyText.Should().BeNull();
        hero.Talents[TalentTier.Level4][0].LifeText.Should().BeNull();
        hero.Talents[TalentTier.Level4][0].Name!.RawText.Should().Be("Survival Instincts");
        hero.Talents[TalentTier.Level4][0].ShortText.Should().BeNull();
        hero.Talents[TalentTier.Level4][0].FullText.Should().BeNull();

        hero.Talents[TalentTier.Level4][1].CooldownText.Should().BeNull();
        hero.Talents[TalentTier.Level4][1].EnergyText.Should().BeNull();
        hero.Talents[TalentTier.Level4][1].LifeText.Should().BeNull();
        hero.Talents[TalentTier.Level4][1].Name!.RawText.Should().Be("Bio");
        hero.Talents[TalentTier.Level4][1].ShortText.Should().BeNull();
        hero.Talents[TalentTier.Level4][1].FullText.Should().BeNull();
    }

    [TestMethod]
    public void UpdateGameStrings_Unit_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "unitdata"
            ],
            "descriptionText": {
              "locale": "ENUS",
              "gameStringTextType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "gamestrings": {
            "unit": {
              "description": {
                "AbathurSymbiote": "Spawn and attach a Symbiote to a target ally or Structure. While active, Abathur controls the Symbiote, gaining access to new Abilities. The Symbiote is able to gain XP from nearby enemy deaths.",
                "MedicMedivacDropship": "Target a location for a Medivac transport. For up to <c val=\"bfd4fd\">10</c> seconds before takeoff, allies can right-click to enter the Medivac.",
                "RagnarosBigRag": "Lava Wave"
              },
              "name": {
                "AbathurSymbiote": "Symbiote",
                "FirebatBunkerDropBunkerUnit": "Bunker"
              },
              "energyType": {
                "AbathurSymbiote": "Mana"
              },
              "lifeType": {
                "AbathurSymbiote": "Health"
              },
              "shieldType": {
                "AbathurSymbiote": "Shields"
              }
            },
            "ability": {
              "cooldownText": {
                "AbathurDeepTunnel|AbathurDeepTunnel|Z": "Cooldown: 30 seconds",
                "AbathurEvolveMonstrosity|AbathurEvolveMonstrosityHotbar|Heroic": "Cooldown: 90 seconds",
                "AbathurSpawnLocusts|AbathurLocustStrain|Q": "Cooldown: 15 seconds"
              },
              "energyText": {
                "AbathurEvolveMonstrosity|AbathurEvolveMonstrosityHotbar|Heroic": "Costs <c val=\"bfd4fd\">100</c> Mana"
              },
              "lifeText": {
                "AbathurEvolveMonstrosity|AbathurEvolveMonstrosityHotbar|Heroic": "Costs 1 Health"
              },
              "name": {
                "AbathurDeepTunnel|AbathurDeepTunnel|Z": "Deep Tunnel"
              },
              "shortText": {
                "AbathurDeepTunnel|AbathurDeepTunnel|Z": "Burrow to a target location, emerging after a short delay."
              },
              "fullText": {
                "AbathurDeepTunnel|AbathurDeepTunnel|Z": "Burrow to a target location, emerging after a short delay. While burrowed, the Symbiote is invulnerable and cannot move or attack."
              }
            }
          }
        }
        """;
        Unit unit = new("AbathurSymbiote")
        {
            Description = new GameStringText("hello world"),
        };
        unit.Abilities.Add(AbilityTier.Basic, [new Ability() { AbilityElementId = "AbathurDeepTunnel", ButtonElementId = "AbathurDeepTunnel", AbilityType = AbilityType.Z, FullText = new GameStringText("temp") }]);
        unit.Abilities.Add(
            AbilityTier.Heroic,
            [
                new Ability() { AbilityElementId = "AbathurEvolveMonstrosity", ButtonElementId = "AbathurEvolveMonstrosityHotbar", AbilityType = AbilityType.Heroic },
                new Ability() { AbilityElementId = "AbathurSpawnLocusts", ButtonElementId = "AbathurLocustStrain", AbilityType = AbilityType.Q },
            ]);

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(unit);

        // assert
        unit.Description!.RawText.Should().Be("Spawn and attach a Symbiote to a target ally or Structure. While active, Abathur controls the Symbiote, gaining access to new Abilities. The Symbiote is able to gain XP from nearby enemy deaths.");
        unit.Name!.RawText.Should().Be("Symbiote");
        unit.Energy.EnergyType!.RawText.Should().Be("Mana");
        unit.Life.LifeType!.RawText.Should().Be("Health");
        unit.Shield.ShieldType!.RawText.Should().Be("Shields");
        unit.Abilities[AbilityTier.Basic][0].CooldownText!.RawText.Should().Be("Cooldown: 30 seconds");
        unit.Abilities[AbilityTier.Basic][0].EnergyText.Should().BeNull();
        unit.Abilities[AbilityTier.Basic][0].LifeText.Should().BeNull();
        unit.Abilities[AbilityTier.Basic][0].Name!.RawText.Should().Be("Deep Tunnel");
        unit.Abilities[AbilityTier.Basic][0].ShortText!.RawText.Should().Be("Burrow to a target location, emerging after a short delay.");
        unit.Abilities[AbilityTier.Basic][0].FullText!.RawText.Should().Be("Burrow to a target location, emerging after a short delay. While burrowed, the Symbiote is invulnerable and cannot move or attack.");

        unit.Abilities[AbilityTier.Heroic][0].CooldownText!.RawText.Should().Be("Cooldown: 90 seconds");
        unit.Abilities[AbilityTier.Heroic][0].EnergyText!.RawText.Should().Be("Costs <c val=\"bfd4fd\">100</c> Mana");
        unit.Abilities[AbilityTier.Heroic][0].LifeText!.RawText.Should().Be("Costs 1 Health");
        unit.Abilities[AbilityTier.Heroic][0].Name.Should().BeNull();
        unit.Abilities[AbilityTier.Heroic][0].ShortText.Should().BeNull();
        unit.Abilities[AbilityTier.Heroic][0].FullText.Should().BeNull();

        unit.Abilities[AbilityTier.Heroic][1].CooldownText!.RawText.Should().Be("Cooldown: 15 seconds");
        unit.Abilities[AbilityTier.Heroic][1].EnergyText.Should().BeNull();
        unit.Abilities[AbilityTier.Heroic][1].LifeText.Should().BeNull();
        unit.Abilities[AbilityTier.Heroic][1].Name.Should().BeNull();
        unit.Abilities[AbilityTier.Heroic][1].ShortText.Should().BeNull();
        unit.Abilities[AbilityTier.Heroic][1].FullText.Should().BeNull();
    }
}
