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
    public void UpdateGameStrings_HeroPropertyNotFound_ReturnsUpdatedObject()
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
        hero.Description.Should().BeNull();
        hero.Name.Should().BeNull();
        hero.Difficulty.Should().BeNull();
        hero.ExpandedRole.Should().BeNull();
        hero.InfoText.Should().BeNull();
        hero.SearchText.Should().BeNull();
        hero.SortName.Should().BeNull();
        hero.Title.Should().BeNull();
        hero.Roles.Should().BeEmpty();
        hero.Energy.EnergyType.Should().BeNull();
        hero.Life.LifeType.Should().BeNull();
        hero.Shield.ShieldType.Should().BeNull();

        hero.Talents[TalentTier.Level1][0].CooldownText.Should().BeNull();
        hero.Talents[TalentTier.Level1][0].EnergyText.Should().BeNull();
        hero.Talents[TalentTier.Level1][0].LifeText.Should().BeNull();
        hero.Talents[TalentTier.Level1][0].Name.Should().BeNull();
        hero.Talents[TalentTier.Level1][0].ShortText.Should().BeNull();
        hero.Talents[TalentTier.Level1][0].FullText.Should().BeNull();

        hero.Talents[TalentTier.Level4][0].CooldownText.Should().BeNull();
        hero.Talents[TalentTier.Level4][0].EnergyText.Should().BeNull();
        hero.Talents[TalentTier.Level4][0].LifeText.Should().BeNull();
        hero.Talents[TalentTier.Level4][0].Name.Should().BeNull();
        hero.Talents[TalentTier.Level4][0].ShortText.Should().BeNull();
        hero.Talents[TalentTier.Level4][0].FullText.Should().BeNull();

        hero.Talents[TalentTier.Level4][1].CooldownText.Should().BeNull();
        hero.Talents[TalentTier.Level4][1].EnergyText.Should().BeNull();
        hero.Talents[TalentTier.Level4][1].LifeText.Should().BeNull();
        hero.Talents[TalentTier.Level4][1].Name.Should().BeNull();
        hero.Talents[TalentTier.Level4][1].ShortText.Should().BeNull();
        hero.Talents[TalentTier.Level4][1].FullText.Should().BeNull();
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
                "Abathur": [
                  "Support",
                  "Specialist"
                ]
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
        hero.Talents[TalentTier.Level4][1].Name.Should().BeNull();
        hero.Talents[TalentTier.Level4][1].ShortText.Should().BeNull();
        hero.Talents[TalentTier.Level4][1].FullText.Should().BeNull();
    }

    [TestMethod]
    public void UpdateGameStrings_UnitPropertyNotFound_ReturnsUpdatedObject()
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
        unit.Description.Should().BeNull();
        unit.Name.Should().BeNull();
        unit.Energy.EnergyType.Should().BeNull();
        unit.Life.LifeType.Should().BeNull();
        unit.Shield.ShieldType.Should().BeNull();
        unit.Abilities[AbilityTier.Basic][0].CooldownText.Should().BeNull();
        unit.Abilities[AbilityTier.Basic][0].EnergyText.Should().BeNull();
        unit.Abilities[AbilityTier.Basic][0].LifeText.Should().BeNull();
        unit.Abilities[AbilityTier.Basic][0].Name.Should().BeNull();
        unit.Abilities[AbilityTier.Basic][0].ShortText.Should().BeNull();
        unit.Abilities[AbilityTier.Basic][0].FullText.Should().BeNull();

        unit.Abilities[AbilityTier.Heroic][0].CooldownText.Should().BeNull();
        unit.Abilities[AbilityTier.Heroic][0].EnergyText.Should().BeNull();
        unit.Abilities[AbilityTier.Heroic][0].LifeText.Should().BeNull();
        unit.Abilities[AbilityTier.Heroic][0].Name.Should().BeNull();
        unit.Abilities[AbilityTier.Heroic][0].ShortText.Should().BeNull();
        unit.Abilities[AbilityTier.Heroic][0].FullText.Should().BeNull();

        unit.Abilities[AbilityTier.Heroic][1].CooldownText.Should().BeNull();
        unit.Abilities[AbilityTier.Heroic][1].EnergyText.Should().BeNull();
        unit.Abilities[AbilityTier.Heroic][1].LifeText.Should().BeNull();
        unit.Abilities[AbilityTier.Heroic][1].Name.Should().BeNull();
        unit.Abilities[AbilityTier.Heroic][1].ShortText.Should().BeNull();
        unit.Abilities[AbilityTier.Heroic][1].FullText.Should().BeNull();
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

    [TestMethod]
    public void UpdateGameStrings_AnnouncerPropertyNotFound_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "announcerdata"
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
          }
        }
        """;
        Announcer announcer = new("AbathurAnnouncer")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(announcer);

        // assert
        announcer.Description.Should().BeNull();
        announcer.Name.Should().BeNull();
        announcer.SortName.Should().BeNull();
        announcer.SearchText.Should().BeNull();
    }

    [TestMethod]
    public void UpdateGameStrings_Announcer_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "announcerdata"
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
            "announcer": {
              "description": {
                "AbathurAnnouncer": "The Evolution Master will guide you to victory."
              },
              "name": {
                "AbathurAnnouncer": "Abathur Announcer"
              },
              "sortName": {
                "AbathurAnnouncer": "Abathur"
              },
              "searchText": {
                "AbathurAnnouncer": "Abathur Evolution Master Announcer Pack"
              }
            }
          }
        }
        """;
        Announcer announcer = new("AbathurAnnouncer")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(announcer);

        // assert
        announcer.Description!.RawText.Should().Be("The Evolution Master will guide you to victory.");
        announcer.Name!.RawText.Should().Be("Abathur Announcer");
        announcer.SortName!.RawText.Should().Be("Abathur");
        announcer.SearchText!.RawText.Should().Be("Abathur Evolution Master Announcer Pack");
    }

    [TestMethod]
    public void UpdateGameStrings_BannerPropertyNotFound_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "bannerdata"
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
          }
        }
        """;
        Banner banner = new("BannerD3Imperius")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(banner);

        // assert
        banner.Description.Should().BeNull();
        banner.Name.Should().BeNull();
        banner.SortName.Should().BeNull();
        banner.SearchText.Should().BeNull();
    }

    [TestMethod]
    public void UpdateGameStrings_Banner_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "bannerdata"
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
            "banner": {
              "description": {
                "BannerD3Imperius": "Banner celebrating the Archangel of Valor."
              },
              "name": {
                "BannerD3Imperius": "Imperius Banner"
              },
              "sortName": {
                "BannerD3Imperius": "Imperius"
              },
              "searchText": {
                "BannerD3Imperius": "Imperius Banner Diablo Archangel Valor"
              }
            }
          }
        }
        """;
        Banner banner = new("BannerD3Imperius")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(banner);

        // assert
        banner.Description!.RawText.Should().Be("Banner celebrating the Archangel of Valor.");
        banner.Name!.RawText.Should().Be("Imperius Banner");
        banner.SortName!.RawText.Should().Be("Imperius");
        banner.SearchText!.RawText.Should().Be("Imperius Banner Diablo Archangel Valor");
    }

    [TestMethod]
    public void UpdateGameStrings_BoostPropertyNotFound_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "boostdata"
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
          }
        }
        """;
        Boost boost = new("BoostStimpak")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(boost);

        // assert
        boost.Description.Should().BeNull();
        boost.Name.Should().BeNull();
        boost.SortName.Should().BeNull();
        boost.SearchText.Should().BeNull();
    }

    [TestMethod]
    public void UpdateGameStrings_Boost_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "boostdata"
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
            "boost": {
              "description": {
                "BoostStimpak": "Increases experience gain by 50% for 3 days."
              },
              "name": {
                "BoostStimpak": "3-Day Stimpack"
              },
              "sortName": {
                "BoostStimpak": "Stimpack"
              },
              "searchText": {
                "BoostStimpak": "Stimpack Boost Experience XP"
              }
            }
          }
        }
        """;
        Boost boost = new("BoostStimpak")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(boost);

        // assert
        boost.Description!.RawText.Should().Be("Increases experience gain by 50% for 3 days.");
        boost.Name!.RawText.Should().Be("3-Day Stimpack");
        boost.SortName!.RawText.Should().Be("Stimpack");
        boost.SearchText!.RawText.Should().Be("Stimpack Boost Experience XP");
    }

    [TestMethod]
    public void UpdateGameStrings_BundlePropertyNotFound_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "bundledata"
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
          }
        }
        """;
        Bundle bundle = new("MegaBundleStarterPack")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(bundle);

        // assert
        bundle.Description.Should().BeNull();
        bundle.Name.Should().BeNull();
        bundle.SortName.Should().BeNull();
        bundle.SearchText.Should().BeNull();
    }

    [TestMethod]
    public void UpdateGameStrings_Bundle_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "bundledata"
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
            "bundle": {
              "description": {
                "MegaBundleStarterPack": "Get started with this amazing bundle pack!"
              },
              "name": {
                "MegaBundleStarterPack": "Mega Starter Bundle"
              },
              "sortName": {
                "MegaBundleStarterPack": "Starter Bundle"
              },
              "searchText": {
                "MegaBundleStarterPack": "Mega Starter Bundle Pack Heroes Skins"
              }
            }
          }
        }
        """;
        Bundle bundle = new("MegaBundleStarterPack")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(bundle);

        // assert
        bundle.Description!.RawText.Should().Be("Get started with this amazing bundle pack!");
        bundle.Name!.RawText.Should().Be("Mega Starter Bundle");
        bundle.SortName!.RawText.Should().Be("Starter Bundle");
        bundle.SearchText!.RawText.Should().Be("Mega Starter Bundle Pack Heroes Skins");
    }

    [TestMethod]
    public void UpdateGameStrings_LootChestPropertyNotFound_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "lootchestdata"
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
          }
        }
        """;
        LootChest lootChest = new("LootChestRare")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(lootChest);

        // assert
        lootChest.Description.Should().BeNull();
        lootChest.Name.Should().BeNull();
    }

    [TestMethod]
    public void UpdateGameStrings_LootChest_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "lootchestdata"
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
            "lootChest": {
              "description": {
                "LootChestRare": "Contains four random items. Guaranteed to contain at least one Rare item."
              },
              "name": {
                "LootChestRare": "Rare Loot Chest"
              }
            }
          }
        }
        """;
        LootChest lootChest = new("LootChestRare")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(lootChest);

        // assert
        lootChest.Description!.RawText.Should().Be("Contains four random items. Guaranteed to contain at least one Rare item.");
        lootChest.Name!.RawText.Should().Be("Rare Loot Chest");
    }

    [TestMethod]
    public void UpdateGameStrings_MapPropertyNotFound_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "mapdata"
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
          }
        }
        """;
        Map map = new("Battlefield of Eternity");
        map.MapObjectives.Add(new MapObjective { Title = new GameStringText("temp1"), Description = new GameStringText("temp1 desc") });
        map.MapObjectives.Add(new MapObjective { Title = new GameStringText("temp2"), Description = new GameStringText("temp2 desc") });

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(map);

        // assert
        map.Name.Should().BeNull();
        map.MapObjectives.Should().HaveCount(2);
        map.MapObjectives[0].Title.Should().BeNull();
        map.MapObjectives[0].Description.Should().BeNull();
        map.MapObjectives[1].Title.Should().BeNull();
        map.MapObjectives[1].Description.Should().BeNull();
    }

    [TestMethod]
    public void UpdateGameStrings_Map_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "mapdata"
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
            "map": {
              "name": {
                "Battlefield of Eternity": "Battlefield of Eternity"
              },
              "objectiveTitle": {
                "Battlefield of Eternity": [
                  "Immortals",
                  "Defeat the Enemy Immortal"
                ]
              },
              "objectiveDescription": {
                "Battlefield of Eternity": [
                  "Two Immortals fight at the center of the map. Help yours prevail, and it will march with your team.",
                  "Defeat the enemy's Immortal and yours will march down a lane to aid your team in battle."
                ]
              }
            }
          }
        }
        """;
        Map map = new("Battlefield of Eternity");
        map.MapObjectives.Add(new MapObjective { Title = new GameStringText("temp1"), Description = new GameStringText("temp1 desc") });
        map.MapObjectives.Add(new MapObjective { Title = new GameStringText("temp2"), Description = new GameStringText("temp2 desc") });

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(map);

        // assert
        map.Name!.RawText.Should().Be("Battlefield of Eternity");
        map.MapObjectives.Should().HaveCount(2);
        map.MapObjectives[0].Title!.RawText.Should().Be("Immortals");
        map.MapObjectives[0].Description!.RawText.Should().Be("Two Immortals fight at the center of the map. Help yours prevail, and it will march with your team.");
        map.MapObjectives[1].Title!.RawText.Should().Be("Defeat the Enemy Immortal");
        map.MapObjectives[1].Description!.RawText.Should().Be("Defeat the enemy's Immortal and yours will march down a lane to aid your team in battle.");
    }

    [TestMethod]
    public void UpdateGameStrings_SkinPropertyNotFound_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "skindata"
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
          }
        }
        """;
        Skin skin = new("AbathurMechaVar1")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        gameStringDocument.UpdateGameStrings(skin);

        // assert
        skin.Description.Should().BeNull();
        skin.Name.Should().BeNull();
        skin.SortName.Should().BeNull();
        skin.SearchText.Should().BeNull();
        skin.InfoText.Should().BeNull();
    }

    [TestMethod]
    public void UpdateGameStrings_Skin_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "skindata"
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
            "skin": {
              "description": {
                "AbathurMechaVar1": "An alternate skin for the Evolution Master."
              },
              "name": {
                "AbathurMechaVar1": "Mecha Abathur"
              },
              "sortName": {
                "AbathurMechaVar1": "Abathur Mecha"
              },
              "searchText": {
                "AbathurMechaVar1": "Mecha Abathur Robot Mechanical"
              },
              "infoText": {
                "AbathurMechaVar1": "Legendary Skin from the Mecha universe"
              }
            }
          }
        }
        """;
        Skin skin = new("AbathurMechaVar1")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        gameStringDocument.UpdateGameStrings(skin);

        // assert
        skin.Description!.RawText.Should().Be("An alternate skin for the Evolution Master.");
        skin.Name!.RawText.Should().Be("Mecha Abathur");
        skin.SortName!.RawText.Should().Be("Abathur Mecha");
        skin.SearchText!.RawText.Should().Be("Mecha Abathur Robot Mechanical");
        skin.InfoText!.RawText.Should().Be("Legendary Skin from the Mecha universe");
    }

    [TestMethod]
    public void UpdateGameStrings_MountPropertyNotFound_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "mountdata"
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
          }
        }
        """;
        Mount mount = new("CloudSerpentMount")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(mount);

        // assert
        mount.Description.Should().BeNull();
        mount.Name.Should().BeNull();
        mount.SortName.Should().BeNull();
        mount.SearchText.Should().BeNull();
        mount.InfoText.Should().BeNull();
    }

    [TestMethod]
    public void UpdateGameStrings_Mount_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "mountdata"
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
            "mount": {
              "description": {
                "CloudSerpentMount": "A mystical serpent from the clouds of Pandaria."
              },
              "name": {
                "CloudSerpentMount": "Cloud Serpent"
              },
              "sortName": {
                "CloudSerpentMount": "Serpent Cloud"
              },
              "searchText": {
                "CloudSerpentMount": "Cloud Serpent Mount Flying Dragon"
              },
              "infoText": {
                "CloudSerpentMount": "Epic Mount from the Pandaria universe"
              }
            }
          }
        }
        """;
        Mount mount = new("CloudSerpentMount")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(mount);

        // assert
        mount.Description!.RawText.Should().Be("A mystical serpent from the clouds of Pandaria.");
        mount.Name!.RawText.Should().Be("Cloud Serpent");
        mount.SortName!.RawText.Should().Be("Serpent Cloud");
        mount.SearchText!.RawText.Should().Be("Cloud Serpent Mount Flying Dragon");
        mount.InfoText!.RawText.Should().Be("Epic Mount from the Pandaria universe");
    }

    [TestMethod]
    public void UpdateGameStrings_MatchAwardPropertyNotFound_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "matchawarddata"
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
          }
        }
        """;
        MatchAward matchAward = new("EndOfMatchAwardMVPBoolean")
        {
            ScoreScreenName = new GameStringText("temporary score screen name"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(matchAward);

        // assert
        matchAward.ScoreScreenName.Should().BeNull();
        matchAward.ScoreScreenDescription.Should().BeNull();
        matchAward.EndOfMatchName.Should().BeNull();
        matchAward.EndOfMatchDescription.Should().BeNull();
        matchAward.EndOfMatchTooltipText.Should().BeNull();
    }

    [TestMethod]
    public void UpdateGameStrings_MatchAward_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "matchawarddata"
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
            "matchAward": {
              "scoreScreenName": {
                "EndOfMatchAwardMVPBoolean": "Most Valuable Player"
              },
              "scoreScreenDescription": {
                "EndOfMatchAwardMVPBoolean": "Awarded to the player with the highest overall performance."
              },
              "endOfMatchName": {
                "EndOfMatchAwardMVPBoolean": "MVP"
              },
              "endOfMatchDescription": {
                "EndOfMatchAwardMVPBoolean": "Most Valuable Player"
              },
              "endOfMatchTooltipText": {
                "EndOfMatchAwardMVPBoolean": "You were the most valuable player in this match!"
              }
            }
          }
        }
        """;
        MatchAward matchAward = new("EndOfMatchAwardMVPBoolean")
        {
            ScoreScreenName = new GameStringText("temporary score screen name"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(matchAward);

        // assert
        matchAward.ScoreScreenName!.RawText.Should().Be("Most Valuable Player");
        matchAward.ScoreScreenDescription!.RawText.Should().Be("Awarded to the player with the highest overall performance.");
        matchAward.EndOfMatchName!.RawText.Should().Be("MVP");
        matchAward.EndOfMatchDescription!.RawText.Should().Be("Most Valuable Player");
        matchAward.EndOfMatchTooltipText!.RawText.Should().Be("You were the most valuable player in this match!");
    }

    [TestMethod]
    public void UpdateGameStrings_VoiceLinePropertyNotFound_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "voicelinedata"
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
          }
        }
        """;
        VoiceLine voiceLine = new("AbathurBase_VoiceLine01")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(voiceLine);

        // assert
        voiceLine.Name.Should().BeNull();
        voiceLine.SortName.Should().BeNull();
        voiceLine.Description.Should().BeNull();
        voiceLine.SearchText.Should().BeNull();
        voiceLine.InfoText.Should().BeNull();
    }

    [TestMethod]
    public void UpdateGameStrings_VoiceLine_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "voicelinedata"
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
            "voiceLine": {
              "name": {
                "AbathurBase_VoiceLine01": "Acceptable"
              },
              "sortName": {
                "AbathurBase_VoiceLine01": "Acceptable Abathur"
              },
              "description": {
                "AbathurBase_VoiceLine01": "Voice line from the Evolution Master."
              },
              "searchText": {
                "AbathurBase_VoiceLine01": "Abathur Acceptable Voice Line Slug"
              },
              "infoText": {
                "AbathurBase_VoiceLine01": "Common Voice Line from the Evolution Master"
              }
            }
          }
        }
        """;
        VoiceLine voiceLine = new("AbathurBase_VoiceLine01")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(voiceLine);

        // assert
        voiceLine.Name!.RawText.Should().Be("Acceptable");
        voiceLine.SortName!.RawText.Should().Be("Acceptable Abathur");
        voiceLine.Description!.RawText.Should().Be("Voice line from the Evolution Master.");
        voiceLine.SearchText!.RawText.Should().Be("Abathur Acceptable Voice Line Slug");
        voiceLine.InfoText!.RawText.Should().Be("Common Voice Line from the Evolution Master");
    }

    [TestMethod]
    public void UpdateGameStrings_EmoticonPropertyNotFound_ReturnsWithClearedProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "emoticondata"
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
          }
        }
        """;
        Emoticon emoticon = new("AbathurEmoticon")
        {
            Description = new GameStringText("temporary description"),
        };
        emoticon.LocalizedAliases.Add(new GameStringText("temp1"));
        emoticon.LocalizedAliases.Add(new GameStringText("temp2"));
        emoticon.LocalizedAliases.Add(new GameStringText("temp3"));

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(emoticon);

        // assert
        emoticon.Description.Should().BeNull();
        emoticon.SearchText.Should().BeNull();
        emoticon.LocalizedAliases.Should().BeEmpty();
    }

    [TestMethod]
    public void UpdateGameStrings_Emoticon_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "emoticondata"
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
            "emoticon": {
              "description": {
                "AbathurEmoticon": "Emoticon featuring Abathur, the Evolution Master."
              },
              "searchText": {
                "AbathurEmoticon": "Abathur Slug Evolution Master Emoticon"
              },
              "localizedAliases": {
                "AbathurEmoticon": [
                  "abathur",
                  "slug",
                  "evolution"
                ]
              }
            }
          }
        }
        """;
        Emoticon emoticon = new("AbathurEmoticon")
        {
            Description = new GameStringText("temporary description"),
        };
        emoticon.LocalizedAliases.Add(new GameStringText("temp1"));
        emoticon.LocalizedAliases.Add(new GameStringText("temp2"));
        emoticon.LocalizedAliases.Add(new GameStringText("temp3"));

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(emoticon);

        // assert
        emoticon.Description!.RawText.Should().Be("Emoticon featuring Abathur, the Evolution Master.");
        emoticon.SearchText!.RawText.Should().Be("Abathur Slug Evolution Master Emoticon");
        emoticon.LocalizedAliases.Should().HaveCount(3);
        emoticon.LocalizedAliases.ElementAt(0).RawText.Should().Be("abathur");
        emoticon.LocalizedAliases.ElementAt(1).RawText.Should().Be("slug");
        emoticon.LocalizedAliases.ElementAt(2).RawText.Should().Be("evolution");
    }

    [TestMethod]
    public void UpdateGameStrings_SprayPropertyNotFound_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "spraydata"
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
          }
        }
        """;
        Spray spray = new("SprayAnimatedBWAhhhh")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(spray);

        // assert
        spray.Name.Should().BeNull();
        spray.SortName.Should().BeNull();
        spray.Description.Should().BeNull();
        spray.SearchText.Should().BeNull();
        spray.InfoText.Should().BeNull();
    }

    [TestMethod]
    public void UpdateGameStrings_Spray_ReturnsUpdatedObject()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "heroesVersion": "2.55.14.95623_ptr",
            "hdpVersion": "5.0.0",
            "dataTypes": [
              "spraydata"
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
            "spray": {
              "name": {
                "SprayAnimatedBWAhhhh": "Ahhhh!"
              },
              "sortName": {
                "SprayAnimatedBWAhhhh": "Ahhhh Blackheart"
              },
              "description": {
                "SprayAnimatedBWAhhhh": "An animated spray featuring Blackheart's Bay."
              },
              "searchText": {
                "SprayAnimatedBWAhhhh": "Ahhhh Spray Animated Blackheart"
              },
              "infoText": {
                "SprayAnimatedBWAhhhh": "Animated Spray from the Blackheart's Bay universe"
              }
            }
          }
        }
        """;
        Spray spray = new("SprayAnimatedBWAhhhh")
        {
            Description = new GameStringText("temporary description"),
        };

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        GameStringDocument document = GameStringDocument.Load(jsonDocument);

        // act
        document.UpdateGameStrings(spray);

        // assert
        spray.Name!.RawText.Should().Be("Ahhhh!");
        spray.SortName!.RawText.Should().Be("Ahhhh Blackheart");
        spray.Description!.RawText.Should().Be("An animated spray featuring Blackheart's Bay.");
        spray.SearchText!.RawText.Should().Be("Ahhhh Spray Animated Blackheart");
        spray.InfoText!.RawText.Should().Be("Animated Spray from the Blackheart's Bay universe");
    }
}

