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
            "lootChest": {
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
            "voiceLine": {
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

    [TestMethod]
    public void UpdateGameStringTexts_MatchAward_UpdatesGameStringTexts()
    {
        // arrange
        MatchAward matchAward = new("matchAwardId1")
        {
            ScoreScreenName = new GameStringText("a score screen name"),
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
            "matchAward": {
              "scoreScreenName": {
                "matchAwardId1": "updated score screen name"
              },
              "scoreScreenDescription": {
                "matchAwardId1": "updated score screen description"
              },
              "endOfMatchName": {
                "matchAwardId1": "updated end of match name"
              },
              "endOfMatchDescription": {
                "matchAwardId1": "updated end of match description"
              },
              "endOfMatchTooltipText": {
                "matchAwardId1": "updated end of match tooltip text"
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        matchAward.UpdateGameStringTexts(gameStringDocument);

        // assert
        matchAward.ScoreScreenName!.RawText.Should().Be("updated score screen name");
        matchAward.ScoreScreenDescription!.RawText.Should().Be("updated score screen description");
        matchAward.EndOfMatchName!.RawText.Should().Be("updated end of match name");
        matchAward.EndOfMatchDescription!.RawText.Should().Be("updated end of match description");
        matchAward.EndOfMatchTooltipText!.RawText.Should().Be("updated end of match tooltip text");
    }

    [TestMethod]
    public void UpdateGameStringTexts_Spray_UpdatesGameStringTexts()
    {
        // arrange
        Spray spray = new("sprayId1")
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
            "spray": {
              "description": {
                "sprayId1": "updated description"
              },
              "name": {
                "sprayId1": "updated name"
              },
              "sortName": {
                "sprayId1": "updated sort name"
              },
              "searchText": {
                "sprayId1": "updated search text"
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        spray.UpdateGameStringTexts(gameStringDocument);

        // assert
        spray.Description!.RawText.Should().Be("updated description");
        spray.Name!.RawText.Should().Be("updated name");
        spray.SortName!.RawText.Should().Be("updated sort name");
        spray.SearchText!.RawText.Should().Be("updated search text");
    }

    [TestMethod]
    public void UpdateGameStringTexts_Emoticon_UpdatesGameStringTexts()
    {
        // arrange
        Emoticon emoticon = new("emoticonId1")
        {
            Description = new GameStringText("old description"),
            SearchText = new GameStringText("old search text"),
        };
        emoticon.LocalizedAliases.Add(new GameStringText("old alias 1"));
        emoticon.LocalizedAliases.Add(new GameStringText("old alias 2"));

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
            "emoticon": {
              "description": {
                "emoticonId1": "updated description"
              },
              "searchText": {
                "emoticonId1": "updated search text"
              },
              "localizedAliases": {
                "emoticonId1": [
                  "updated alias 1",
                  "updated alias 2"
                ]
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        emoticon.UpdateGameStringTexts(gameStringDocument);

        // assert
        emoticon.Description!.RawText.Should().Be("updated description");
        emoticon.SearchText!.RawText.Should().Be("updated search text");
        emoticon.LocalizedAliases.Should().HaveCount(2);
        emoticon.LocalizedAliases.ElementAt(0).RawText.Should().Be("updated alias 1");
        emoticon.LocalizedAliases.ElementAt(1).RawText.Should().Be("updated alias 2");
    }

    [TestMethod]
    public void UpdateGameStringTexts_EmoticonPack_UpdatesGameStringTexts()
    {
        // arrange
        EmoticonPack emoticonPack = new("emoticonPackId1")
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
            "emoticonPack": {
              "description": {
                "emoticonPackId1": "updated description"
              },
              "name": {
                "emoticonPackId1": "updated name"
              },
              "sortName": {
                "emoticonPackId1": "updated sort name"
              },
              "searchText": {
                "emoticonPackId1": "updated search text"
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        emoticonPack.UpdateGameStringTexts(gameStringDocument);

        // assert
        emoticonPack.Description!.RawText.Should().Be("updated description");
        emoticonPack.Name!.RawText.Should().Be("updated name");
        emoticonPack.SortName!.RawText.Should().Be("updated sort name");
        emoticonPack.SearchText!.RawText.Should().Be("updated search text");
    }

    [TestMethod]
    public void UpdateGameStringTexts_PortraitPack_UpdatesGameStringTexts()
    {
        // arrange
        PortraitPack portraitPack = new("portraitPackId1")
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
            "portraitPack": {
              "description": {
                "portraitPackId1": "updated description"
              },
              "name": {
                "portraitPackId1": "updated name"
              },
              "sortName": {
                "portraitPackId1": "updated sort name"
              },
              "searchText": {
                "portraitPackId1": "updated search text"
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        portraitPack.UpdateGameStringTexts(gameStringDocument);

        // assert
        portraitPack.Description!.RawText.Should().Be("updated description");
        portraitPack.Name!.RawText.Should().Be("updated name");
        portraitPack.SortName!.RawText.Should().Be("updated sort name");
        portraitPack.SearchText!.RawText.Should().Be("updated search text");
    }
}
