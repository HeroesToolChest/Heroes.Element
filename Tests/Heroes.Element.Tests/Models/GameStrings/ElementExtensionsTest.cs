namespace Heroes.Element.Models.GameStrings.Tests;

[TestClass]
public class ElementExtensionsTest
{
    [TestMethod]
    public void UpdateGameStringTexts_Hero_UpdatesGameStringTexts()
    {
        // arrange
        Hero hero = new("heroId1")
        {
            Description = new GameStringText("a description"),
        };

        string gameStringData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0",
            "descriptionText": {
              "locale": "FRFR",
              "gameStringTextType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "gamestrings": {
            "hero": {
              "description": {
                "heroId1": "updated description"
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        hero.UpdateGameStringTexts(gameStringDocument);

        // assert
        hero.Description.RawText.Should().Be("updated description");
    }

    [TestMethod]
    public void UpdateGameStringTexts_Unit_UpdatesGameStringTexts()
    {
        // arrange
        Unit unit = new("unitId1")
        {
            Description = new GameStringText("a description"),
        };

        string gameStringData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0",
            "descriptionText": {
              "locale": "FRFR",
              "gameStringTextType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "gamestrings": {
            "unit": {
              "description": {
                "unitId1": "updated description"
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        unit.UpdateGameStringTexts(gameStringDocument);

        // assert
        unit.Description.RawText.Should().Be("updated description");
    }

    [TestMethod]
    public void UpdateGameStringTexts_Announcer_UpdatesGameStringTexts()
    {
        // arrange
        Announcer announcer = new("announcerId1")
        {
            Description = new GameStringText("a description"),
        };

        string gameStringData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0",
            "descriptionText": {
              "locale": "FRFR",
              "gameStringTextType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "gamestrings": {
            "announcer": {
              "description": {
                "announcerId1": "updated description"
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        announcer.UpdateGameStringTexts(gameStringDocument);

        // assert
        announcer.Description.RawText.Should().Be("updated description");
    }

    [TestMethod]
    public void UpdateGameStringTexts_Banner_UpdatesGameStringTexts()
    {
        // arrange
        Banner banner = new("bannerId1")
        {
            Description = new GameStringText("a description"),
        };

        string gameStringData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0",
            "descriptionText": {
              "locale": "FRFR",
              "gameStringTextType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "gamestrings": {
            "banner": {
              "description": {
                "bannerId1": "updated description"
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        banner.UpdateGameStringTexts(gameStringDocument);

        // assert
        banner.Description.RawText.Should().Be("updated description");
    }

    [TestMethod]
    public void UpdateGameStringTexts_Boost_UpdatesGameStringTexts()
    {
        // arrange
        Boost boost = new("boostId1")
        {
            Description = new GameStringText("a description"),
        };

        string gameStringData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0",
            "descriptionText": {
              "locale": "FRFR",
              "gameStringTextType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "gamestrings": {
            "boost": {
              "description": {
                "boostId1": "updated description"
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        boost.UpdateGameStringTexts(gameStringDocument);

        // assert
        boost.Description.RawText.Should().Be("updated description");
    }

    [TestMethod]
    public void UpdateGameStringTexts_Bundle_UpdatesGameStringTexts()
    {
        // arrange
        Bundle bundle = new("bundleId1")
        {
            Description = new GameStringText("a description"),
        };

        string gameStringData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0",
            "descriptionText": {
              "locale": "FRFR",
              "gameStringTextType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "gamestrings": {
            "bundle": {
              "description": {
                "bundleId1": "updated description"
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        bundle.UpdateGameStringTexts(gameStringDocument);

        // assert
        bundle.Description.RawText.Should().Be("updated description");
    }

    [TestMethod]
    public void UpdateGameStringTexts_LootChest_UpdatesGameStringTexts()
    {
        // arrange
        LootChest lootChest = new("lootChestId1")
        {
            Description = new GameStringText("a description"),
        };

        string gameStringData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0",
            "descriptionText": {
              "locale": "FRFR",
              "gameStringTextType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "gamestrings": {
            "lootchest": {
              "description": {
                "lootChestId1": "updated description"
              },
              "name": {
                "lootChestId1": "updated name"
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        lootChest.UpdateGameStringTexts(gameStringDocument);

        // assert
        lootChest.Description.RawText.Should().Be("updated description");
        lootChest.Name!.RawText.Should().Be("updated name");
    }

    [TestMethod]
    public void UpdateGameStringTexts_Map_UpdatesGameStringTexts()
    {
        // arrange
        Map map = new("BattlefieldOfEternity")
        {
            Name = new GameStringText("old name"),
        };
        map.MapObjectives.Add(new MapObjective { Title = new GameStringText("old title 1"), Description = new GameStringText("old description 1") });
        map.MapObjectives.Add(new MapObjective { Title = new GameStringText("old title 2"), Description = new GameStringText("old description 2") });

        string gameStringData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0",
            "descriptionText": {
              "locale": "FRFR",
              "gameStringTextType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "gamestrings": {
            "map": {
              "name": {
                "BattlefieldOfEternity": "Battlefield of Eternity"
              },
              "objectiveTitle": {
                "BattlefieldOfEternity": [
                  "Immortals",
                  "Defeat the Enemy Immortal"
                ]
              },
              "objectiveDescription": {
                "BattlefieldOfEternity": [
                  "Two Immortals fight at the center of the map.",
                  "Defeat the enemy's Immortal and yours will march down a lane."
                ]
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        map.UpdateGameStringTexts(gameStringDocument);

        // assert
        map.Name!.RawText.Should().Be("Battlefield of Eternity");
        map.MapObjectives[0].Title!.RawText.Should().Be("Immortals");
        map.MapObjectives[0].Description!.RawText.Should().Be("Two Immortals fight at the center of the map.");
        map.MapObjectives[1].Title!.RawText.Should().Be("Defeat the Enemy Immortal");
        map.MapObjectives[1].Description!.RawText.Should().Be("Defeat the enemy's Immortal and yours will march down a lane.");
    }

    [TestMethod]
    public void UpdateGameStringTexts_Skin_UpdatesGameStringTexts()
    {
        // arrange
        Skin skin = new("skinId1")
        {
            Description = new GameStringText("a description"),
        };

        string gameStringData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0",
            "descriptionText": {
              "locale": "FRFR",
              "gameStringTextType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "gamestrings": {
            "skin": {
              "description": {
                "skinId1": "updated description"
              },
              "name": {
                "skinId1": "updated name"
              },
              "sortName": {
                "skinId1": "updated sort name"
              },
              "searchText": {
                "skinId1": "updated search text"
              },
              "infoText": {
                "skinId1": "updated info text"
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        skin.UpdateGameStringTexts(gameStringDocument);

        // assert
        skin.Description!.RawText.Should().Be("updated description");
        skin.Name!.RawText.Should().Be("updated name");
        skin.SortName!.RawText.Should().Be("updated sort name");
        skin.SearchText!.RawText.Should().Be("updated search text");
        skin.InfoText!.RawText.Should().Be("updated info text");
    }

    [TestMethod]
    public void UpdateGameStringTexts_VoiceLine_UpdatesGameStringTexts()
    {
        // arrange
        VoiceLine voiceLine = new("voiceLineId1")
        {
            Description = new GameStringText("a description"),
        };

        string gameStringData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0",
            "descriptionText": {
              "locale": "FRFR",
              "gameStringTextType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "gamestrings": {
            "voiceline": {
              "description": {
                "voiceLineId1": "updated description"
              },
              "name": {
                "voiceLineId1": "updated name"
              },
              "sortName": {
                "voiceLineId1": "updated sort name"
              },
              "searchText": {
                "voiceLineId1": "updated search text"
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        voiceLine.UpdateGameStringTexts(gameStringDocument);

        // assert
        voiceLine.Description!.RawText.Should().Be("updated description");
        voiceLine.Name!.RawText.Should().Be("updated name");
        voiceLine.SortName!.RawText.Should().Be("updated sort name");
        voiceLine.SearchText!.RawText.Should().Be("updated search text");
    }

    [TestMethod]
    public void UpdateGameStringTexts_Mount_UpdatesGameStringTexts()
    {
        // arrange
        Mount mount = new("mountId1")
        {
            Description = new GameStringText("a description"),
        };

        string gameStringData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0",
            "descriptionText": {
              "locale": "FRFR",
              "gameStringTextType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "gamestrings": {
            "mount": {
              "description": {
                "mountId1": "updated description"
              },
              "name": {
                "mountId1": "updated name"
              },
              "sortName": {
                "mountId1": "updated sort name"
              },
              "searchText": {
                "mountId1": "updated search text"
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        mount.UpdateGameStringTexts(gameStringDocument);

        // assert
        mount.Description!.RawText.Should().Be("updated description");
        mount.Name!.RawText.Should().Be("updated name");
        mount.SortName!.RawText.Should().Be("updated sort name");
        mount.SearchText!.RawText.Should().Be("updated search text");
    }
}
