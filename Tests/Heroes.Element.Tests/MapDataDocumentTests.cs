namespace Heroes.Element.Tests;

[TestClass]
public class MapDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {},
      "items": {
        "BattlefieldOfEternity": {
          "name": "Battlefield of Eternity"
        },
        "CursedHollow": {
          "name": "Cursed Hollow"
        }
      }
    }
    """;

    [TestMethod]
    public void TryGetElementById_ItemsPropertyIsEmpty_ReturnsFalse()
    {
        // arrange
        string json =
        """
        {
          "meta": {},
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MapDataDocument mapData = MapDataDocument.Load(jsonDocument);

        // act
        bool result = mapData.TryGetElementById("other", out Map? map);

        // assert
        result.Should().BeFalse();
        map.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_BattlefieldOfEternity_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {},
          "items": {
            "BattlefieldOfEternity": {
              "name": "Battlefield of Eternity",
              "mapId": "1001",
              "mapLink": "BattlefieldOfEternityLink",
              "mapSize": {
                "x": 200,
                "y": 215
              },
              "replayPreviewImage": "storm_ui_homescreenbackground_battlefieldofeternity.dds",
              "loadingScreenImage": "storm_ui_ingame_loadingscreen_battlefieldofeternity.dds",
              "mapObjectives": [
                {
                  "title": "Immortals",
                  "description": "Two Immortals fight at the center of the map.",
                  "icons": [
                    {
                      "image": "ui_ingame_mapmechanic_loadscreen_eternity_icon1.png",
                      "height": 165,
                      "scaleWidth": true
                    },
                    {
                      "image": "other_icoin.png",
                      "scaleWidth": false
                    }
                  ]
                },
                {
                  "title": "Defeat Enemy",
                  "description": "Defeat the enemy's Immortal.",
                  "icons": [
                    {
                      "image": "ui_ingame_mapmechanic_loadscreen_eternity_icon2.png",
                      "height": 175,
                      "scaleWidth": false
                    }
                  ]
                }
              ]
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MapDataDocument mapData = MapDataDocument.Load(jsonDocument);

        // act
        bool returnResult = mapData.TryGetElementById("BattlefieldOfEternity", out Map? map);

        // assert
        returnResult.Should().BeTrue();
        map.Should().NotBeNull();
        map.Id.Should().Be("BattlefieldOfEternity");
        map.Name!.RawText.Should().Be("Battlefield of Eternity");
        map.MapId.Should().Be("1001");
        map.MapLink.Should().Be("BattlefieldOfEternityLink");
        map.MapSize.Should().NotBeNull();
        map.MapSize.Value.X.Should().Be(200);
        map.MapSize.Value.Y.Should().Be(215);
        map.ReplayPreviewImage.Should().Be("storm_ui_homescreenbackground_battlefieldofeternity.dds");
        map.LoadingScreenImage.Should().Be("storm_ui_ingame_loadingscreen_battlefieldofeternity.dds");
        map.MapObjectives.Should().HaveCount(2);
        map.MapObjectives[0].Title!.RawText.Should().Be("Immortals");
        map.MapObjectives[0].Description!.RawText.Should().Be("Two Immortals fight at the center of the map.");
        map.MapObjectives[0].Icons.Should().HaveCount(2);
        map.MapObjectives[0].Icons[0].Image.Should().Be("ui_ingame_mapmechanic_loadscreen_eternity_icon1.png");
        map.MapObjectives[0].Icons[0].Height.Should().Be(165);
        map.MapObjectives[0].Icons[0].ScaleWidth.Should().BeTrue();
        map.MapObjectives[0].Icons[1].Image.Should().Be("other_icoin.png");
        map.MapObjectives[0].Icons[1].Height.Should().BeNull();
        map.MapObjectives[0].Icons[1].ScaleWidth.Should().BeFalse();
        map.MapObjectives[1].Title!.RawText.Should().Be("Defeat Enemy");
        map.MapObjectives[1].Description!.RawText.Should().Be("Defeat the enemy's Immortal.");
        map.MapObjectives[1].Icons.Should().ContainSingle();
        map.MapObjectives[1].Icons[0].Image.Should().Be("ui_ingame_mapmechanic_loadscreen_eternity_icon2.png");
        map.MapObjectives[1].Icons[0].Height.Should().Be(175);
        map.MapObjectives[1].Icons[0].ScaleWidth.Should().BeFalse();
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MapDataDocument mapData = MapDataDocument.Load(jsonDocument);

        // act
        bool result = mapData.TryGetElementById("other", out Map? map);

        // assert
        result.Should().BeFalse();
        map.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MapDataDocument mapData = MapDataDocument.Load(jsonDocument);

        // act
        Map map = mapData.GetElementById("CursedHollow");

        // assert
        CursedHollowBasicAssertions(map);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MapDataDocument mapData = MapDataDocument.Load(jsonDocument);

        // act
        Action act = () => mapData.GetElementById("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MapDataDocument mapData = MapDataDocument.Load(jsonDocument);

        // act
        bool result = mapData.TryGetElementByHyperlinkId("other", out Map? map);

        // assert
        result.Should().BeFalse();
        map.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByHyperlinkId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MapDataDocument mapData = MapDataDocument.Load(jsonDocument);

        // act
        Action act = () => mapData.GetElementByHyperlinkId("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByAttributeId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MapDataDocument mapData = MapDataDocument.Load(jsonDocument);

        // act
        bool result = mapData.TryGetElementByAttributeId("other", out Map? map);

        // assert
        result.Should().BeFalse();
        map.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByAttributeId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MapDataDocument mapData = MapDataDocument.Load(jsonDocument);

        // act
        Action act = () => mapData.GetElementByAttributeId("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void GetElementById_WithGameStringDocument_UpdatesGameStrings()
    {
        // arrange
        string jsonData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0"
          },
          "items": {
            "BattlefieldOfEternity": {
              "name": "Battlefield of Eternity",
              "mapObjectives": [
                {
                  "title": "Immortals",
                  "description": "Two Immortals fight."
                },
                {
                  "title": "Defeat Enemy",
                  "description": "Defeat the enemy's Immortal."
                }
              ]
            }
          }
        }
        """;

        string gameStringData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0",
            "gameStringText": {
              "locale": "FRFR",
              "textType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "gamestrings": {
            "map": {
              "name": {
                "BattlefieldOfEternity": "Champ de bataille de l'éternité"
              },
              "objectiveTitle": {
                "BattlefieldOfEternity": [
                  "Immortels",
                  "Vaincre l'Immortel ennemi"
                ]
              },
              "objectiveDescription": {
                "BattlefieldOfEternity": [
                  "Deux Immortels se battent au centre de la carte.",
                  "Vaincre l'Immortel de l'ennemi."
                ]
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        MapDataDocument mapData = MapDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        Map map = mapData.GetElementById("BattlefieldOfEternity");

        // assert
        map.Should().NotBeNull();
        map.Name!.RawText.Should().Be("Champ de bataille de l'éternité");
        map.Name.GameStringLocale.Should().Be(StormLocale.FRFR);
        map.MapObjectives.Should().HaveCount(2);
        map.MapObjectives[0].Title!.RawText.Should().Be("Immortels");
        map.MapObjectives[0].Title!.GameStringLocale.Should().Be(StormLocale.FRFR);
        map.MapObjectives[0].Description!.RawText.Should().Be("Deux Immortels se battent au centre de la carte.");
        map.MapObjectives[0].Description!.GameStringLocale.Should().Be(StormLocale.FRFR);
        map.MapObjectives[1].Title!.RawText.Should().Be("Vaincre l'Immortel ennemi");
        map.MapObjectives[1].Title!.GameStringLocale.Should().Be(StormLocale.FRFR);
        map.MapObjectives[1].Description!.RawText.Should().Be("Vaincre l'Immortel de l'ennemi.");
        map.MapObjectives[1].Description!.GameStringLocale.Should().Be(StormLocale.FRFR);
    }

    [TestMethod]
    public void Load_WithoutMetaSection_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);

        // act
        Action act = () => MapDataDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<JsonException>().WithMessage("No 'meta' and/or 'items' property found");
    }

    [TestMethod]
    public void Load_WithoutItemsSection_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "meta": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);

        // act
        Action act = () => MapDataDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<JsonException>().WithMessage("No 'meta' and/or 'items' property found");
    }

    [TestMethod]
    public void GetAllElements_WithItems_ReturnsAllElements()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MapDataDocument mapData = MapDataDocument.Load(jsonDocument);

        // act
        List<Map> result = [.. mapData.GetAllElements()];

        // assert
        result.Should().HaveCount(2);
        result.Should().Contain(m => m.Id == "BattlefieldOfEternity");
        result.Should().Contain(m => m.Id == "CursedHollow");
    }

    [TestMethod]
    public void GetAllElements_WithEmptyItems_ReturnsEmpty()
    {
        // arrange
        string json =
        """
        {
          "meta": {},
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MapDataDocument mapData = MapDataDocument.Load(jsonDocument);

        // act
        List<Map> result = [.. mapData.GetAllElements()];

        // assert
        result.Should().BeEmpty();
    }

    private static void CursedHollowBasicAssertions(Map map)
    {
        map.Id.Should().Be("CursedHollow");
        map.Name!.RawText.Should().Be("Cursed Hollow");
    }
}
