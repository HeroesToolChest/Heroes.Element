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
}
