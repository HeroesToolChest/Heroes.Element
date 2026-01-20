namespace Heroes.Element.Tests;

[TestClass]
public class PortraitPackDataDocumentTests
{
    private readonly string _defaultArrangeJson =
"""
    {
      "meta": {},
      "items": {
        "PortraitPackStarcraftLegacy1": {
          "name": "StarCraft Legacy Pack 1",
          "hyperlinkId": "PortraitPackStarcraftLegacy1"
        },
        "PortraitPackStarcraftLegacy2": {
          "name": "StarCraft Legacy Pack 2",
          "hyperlinkId": "PortraitPackStarcraftLegacy2(hyperlink)"
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
        PortraitPackDataDocument portraitPackData = PortraitPackDataDocument.Load(jsonDocument);

        // act
        bool result = portraitPackData.TryGetElementById("other", out PortraitPack? portraitPack);

        // assert
        result.Should().BeFalse();
        portraitPack.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_PortraitPackStarcraftLegacy1_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {},
          "items": {
            "PortraitPackStarcraftLegacy1": {
              "name": "StarCraft Legacy Pack 1",
              "sortName": "Legacy StarCraft 1",
              "hyperlinkId": "PortraitPackStarcraftLegacy1",
              "franchise": "Starcraft",
              "rarity": "Epic",
              "releaseDate": "2016-06-14",
              "category": "portraitpack",
              "event": "no",
              "description": "A collection of StarCraft legacy portraits.",
              "searchText": "StarCraft Legacy Portrait Pack Terran Zerg Protoss",
              "rewardPortraitIds": [
                "SCLegacyPortrait001",
                "SCLegacyPortrait002",
                "SCLegacyPortrait003"
              ]
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        PortraitPackDataDocument portraitPackData = PortraitPackDataDocument.Load(jsonDocument);

        // act
        bool returnResult = portraitPackData.TryGetElementById("PortraitPackStarcraftLegacy1", out PortraitPack? portraitPack);

        // assert
        returnResult.Should().BeTrue();
        portraitPack.Should().NotBeNull();
        portraitPack.Id.Should().Be("PortraitPackStarcraftLegacy1");
        portraitPack.Name!.RawText.Should().Be("StarCraft Legacy Pack 1");
        portraitPack.SortName!.RawText.Should().Be("Legacy StarCraft 1");
        portraitPack.HyperlinkId.Should().Be("PortraitPackStarcraftLegacy1");
        portraitPack.Franchise.Should().Be(Franchise.Starcraft);
        portraitPack.Rarity.Should().Be(Rarity.Epic);
        portraitPack.ReleaseDate.Should().Be(new DateOnly(2016, 6, 14));
        portraitPack.Category.Should().Be("portraitpack");
        portraitPack.Event.Should().Be("no");
        portraitPack.Description!.RawText.Should().Be("A collection of StarCraft legacy portraits.");
        portraitPack.SearchText!.RawText.Should().Be("StarCraft Legacy Portrait Pack Terran Zerg Protoss");
        portraitPack.RewardPortraitIds.Should().HaveCount(3);
        portraitPack.RewardPortraitIds.Should().ContainInOrder("SCLegacyPortrait001", "SCLegacyPortrait002", "SCLegacyPortrait003");
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        PortraitPackDataDocument portraitPackData = PortraitPackDataDocument.Load(jsonDocument);

        // act
        bool result = portraitPackData.TryGetElementById("other", out PortraitPack? portraitPack);

        // assert
        result.Should().BeFalse();
        portraitPack.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        PortraitPackDataDocument portraitPackData = PortraitPackDataDocument.Load(jsonDocument);

        // act
        PortraitPack portraitPack = portraitPackData.GetElementById("PortraitPackStarcraftLegacy2");

        // assert
        Pack2BasicAssertions(portraitPack);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        PortraitPackDataDocument portraitPackData = PortraitPackDataDocument.Load(jsonDocument);

        // act
        Action act = () => portraitPackData.GetElementById("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        PortraitPackDataDocument portraitPackData = PortraitPackDataDocument.Load(jsonDocument);

        // act
        bool result = portraitPackData.TryGetElementByHyperlinkId("PortraitPackStarcraftLegacy2(hyperlink)", out PortraitPack? portraitPack);

        // assert
        result.Should().BeTrue();
        portraitPack.Should().NotBeNull();

        Pack2BasicAssertions(portraitPack);
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        PortraitPackDataDocument portraitPackData = PortraitPackDataDocument.Load(jsonDocument);

        // act
        bool result = portraitPackData.TryGetElementByHyperlinkId("other", out PortraitPack? portraitPack);

        // assert
        result.Should().BeFalse();
        portraitPack.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByHyperlinkId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        PortraitPackDataDocument portraitPackData = PortraitPackDataDocument.Load(jsonDocument);

        // act
        PortraitPack portraitPack = portraitPackData.GetElementByHyperlinkId("PortraitPackStarcraftLegacy2(hyperlink)");

        // assert
        Pack2BasicAssertions(portraitPack);
    }

    [TestMethod]
    public void GetElementByHyperlinkId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        PortraitPackDataDocument portraitPackData = PortraitPackDataDocument.Load(jsonDocument);

        // act
        Action act = () => portraitPackData.GetElementByHyperlinkId("other");

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
            "PortraitPackStarcraftLegacy2": {
              "name": "StarCraft Legacy Pack 2",
              "description": "A description",
              "searchText": "Search terms",
              "sortName": "Sort StarCraft"
            }
          }
        }
        """;

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
              "name": {
                "PortraitPackStarcraftLegacy2": "StarCraft Legacy Pack 2 Localized"
              },
              "description": {
                "PortraitPackStarcraftLegacy2": "Localized Description"
              },
              "searchText": {
                "PortraitPackStarcraftLegacy2": "Localized Search Terms"
              },
              "sortName": {
                "PortraitPackStarcraftLegacy2": "Localized Sort Name"
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        PortraitPackDataDocument portraitPackData = PortraitPackDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        PortraitPack portraitPack = portraitPackData.GetElementById("PortraitPackStarcraftLegacy2");

        // assert
        portraitPack.Should().NotBeNull();
        portraitPack.Name!.RawText.Should().Be("StarCraft Legacy Pack 2 Localized");
        portraitPack.Name.GameStringLocale.Should().Be(StormLocale.FRFR);
        portraitPack.Description!.RawText.Should().Be("Localized Description");
        portraitPack.Description.GameStringLocale.Should().Be(StormLocale.FRFR);
        portraitPack.SearchText!.RawText.Should().Be("Localized Search Terms");
        portraitPack.SearchText.GameStringLocale.Should().Be(StormLocale.FRFR);
        portraitPack.SortName!.RawText.Should().Be("Localized Sort Name");
        portraitPack.SortName.GameStringLocale.Should().Be(StormLocale.FRFR);
    }

    private static void Pack2BasicAssertions(PortraitPack portraitPack)
    {
        portraitPack.Id.Should().Be("PortraitPackStarcraftLegacy2");
        portraitPack.Name!.RawText.Should().Be("StarCraft Legacy Pack 2");
        portraitPack.HyperlinkId.Should().Be("PortraitPackStarcraftLegacy2(hyperlink)");
    }
}
