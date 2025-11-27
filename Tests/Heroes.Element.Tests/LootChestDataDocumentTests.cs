namespace Heroes.Element.Tests;

[TestClass]
public class LootChestDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {},
      "items": {
        "LootChestRare": {
          "name": "Rare Loot Chest",
          "hyperlinkId": "LootChestRare"
        },
        "LootChestEpic": {
          "name": "Epic Loot Chest",
          "hyperlinkId": "LootChestEpic(hyperlink)"
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
        LootChestDataDocument lootChestData = LootChestDataDocument.Load(jsonDocument);

        // act
        bool result = lootChestData.TryGetElementById("other", out LootChest? lootChest);

        // assert
        result.Should().BeFalse();
        lootChest.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_LootChestRare_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {},
          "items": {
            "LootChestRare": {
              "name": "Rare Loot Chest",
              "hyperlinkId": "LootChestRare",
              "rarity": "Rare",
              "event": "no",
              "maxRerolls": 3,
              "typeDescriptionId": "rare_chest",
              "description": "Contains four random items. Guaranteed to contain at least one Rare item."
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        LootChestDataDocument lootChestData = LootChestDataDocument.Load(jsonDocument);

        // act
        bool returnResult = lootChestData.TryGetElementById("LootChestRare", out LootChest? lootChest);

        // assert
        returnResult.Should().BeTrue();
        lootChest.Should().NotBeNull();
        lootChest.Id.Should().Be("LootChestRare");
        lootChest.Name!.RawText.Should().Be("Rare Loot Chest");
        lootChest.HyperlinkId.Should().Be("LootChestRare");
        lootChest.Rarity.Should().Be(Rarity.Rare);
        lootChest.Event.Should().Be("no");
        lootChest.MaxRerolls.Should().Be(3);
        lootChest.TypeDescriptionId.Should().Be("rare_chest");
        lootChest.Description!.RawText.Should().Be("Contains four random items. Guaranteed to contain at least one Rare item.");
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        LootChestDataDocument lootChestData = LootChestDataDocument.Load(jsonDocument);

        // act
        bool result = lootChestData.TryGetElementById("other", out LootChest? lootChest);

        // assert
        result.Should().BeFalse();
        lootChest.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        LootChestDataDocument lootChestData = LootChestDataDocument.Load(jsonDocument);

        // act
        LootChest lootChest = lootChestData.GetElementById("LootChestEpic");

        // assert
        EpicBasicAssertions(lootChest);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        LootChestDataDocument lootChestData = LootChestDataDocument.Load(jsonDocument);

        // act
        Action act = () => lootChestData.GetElementById("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        LootChestDataDocument lootChestData = LootChestDataDocument.Load(jsonDocument);

        // act
        bool result = lootChestData.TryGetElementByHyperlinkId("LootChestEpic(hyperlink)", out LootChest? lootChest);

        // assert
        result.Should().BeTrue();
        lootChest.Should().NotBeNull();

        EpicBasicAssertions(lootChest);
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        LootChestDataDocument lootChestData = LootChestDataDocument.Load(jsonDocument);

        // act
        bool result = lootChestData.TryGetElementByHyperlinkId("other", out LootChest? lootChest);

        // assert
        result.Should().BeFalse();
        lootChest.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByHyperlinkId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        LootChestDataDocument lootChestData = LootChestDataDocument.Load(jsonDocument);

        // act
        LootChest lootChest = lootChestData.GetElementByHyperlinkId("LootChestEpic(hyperlink)");

        // assert
        EpicBasicAssertions(lootChest);
    }

    [TestMethod]
    public void GetElementByHyperlinkId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        LootChestDataDocument lootChestData = LootChestDataDocument.Load(jsonDocument);

        // act
        Action act = () => lootChestData.GetElementByHyperlinkId("other");

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
            "LootChestEpic": {
              "name": "Epic Loot Chest",
              "description": "A description"
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
            "lootchest": {
              "name": {
                "LootChestEpic": "Epic Loot Chest Localized"
              },
              "description": {
                "LootChestEpic": "Localized Description"
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        LootChestDataDocument lootChestData = LootChestDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        LootChest lootChest = lootChestData.GetElementById("LootChestEpic");

        // assert
        lootChest.Should().NotBeNull();
        lootChest.Name!.RawText.Should().Be("Epic Loot Chest Localized");
        lootChest.Name.GameStringLocale.Should().Be(StormLocale.FRFR);
        lootChest.Description!.RawText.Should().Be("Localized Description");
        lootChest.Description.GameStringLocale.Should().Be(StormLocale.FRFR);
    }

    private static void EpicBasicAssertions(LootChest lootChest)
    {
        lootChest.Id.Should().Be("LootChestEpic");
        lootChest.Name!.RawText.Should().Be("Epic Loot Chest");
        lootChest.HyperlinkId.Should().Be("LootChestEpic(hyperlink)");
    }
}
