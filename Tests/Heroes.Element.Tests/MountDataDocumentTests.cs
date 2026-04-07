namespace Heroes.Element.Tests;

[TestClass]
public class MountDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {
        "itemsType": "Data",
        "dataType": "MountData"
      },
      "items": {
        "CloudSerpentMount": {
          "name": "Cloud Serpent",
          "hyperlinkId": "CloudSerpentMount",
          "attributeId": "CSer"
        },
        "MechanicalSpiderMount": {
          "name": "Mechanospider",
          "hyperlinkId": "MechanicalSpiderMount(hyperlink)",
          "attributeId": "MSpi"
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
          "meta": {
            "itemsType": "Data",
            "dataType": "MountData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MountDataDocument mountData = MountDataDocument.Load(jsonDocument);

        // act
        bool result = mountData.TryGetElementById("other", out Mount? mount);

        // assert
        result.Should().BeFalse();
        mount.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_CloudSerpentMount_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "MountData"
          },
          "items": {
            "CloudSerpentMount": {
              "name": "Cloud Serpent",
              "sortName": "Serpent Cloud",
              "hyperlinkId": "CloudSerpentMount",
              "attributeId": "CSer",
              "franchise": "Warcraft",
              "rarity": "Epic",
              "releaseDate": "2014-03-13",
              "category": "mount",
              "event": "no",
              "type": "Flying",
              "description": "A mystical serpent from the clouds of Pandaria.",
              "searchText": "Cloud Serpent Mount Flying Dragon",
              "variationMountIds": [
                "CloudSerpentMountVar1",
                "CloudSerpentMountVar2"
              ]
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MountDataDocument mountData = MountDataDocument.Load(jsonDocument);

        // act
        bool returnResult = mountData.TryGetElementById("CloudSerpentMount", out Mount? mount);

        // assert
        returnResult.Should().BeTrue();
        mount.Should().NotBeNull();
        mount.Id.Should().Be("CloudSerpentMount");
        mount.Name!.RawText.Should().Be("Cloud Serpent");
        mount.SortName!.RawText.Should().Be("Serpent Cloud");
        mount.HyperlinkId.Should().Be("CloudSerpentMount");
        mount.AttributeId.Should().Be("CSer");
        mount.Franchise.Should().Be(Franchise.Warcraft);
        mount.Rarity.Should().Be(Rarity.Epic);
        mount.ReleaseDate.Should().Be(new DateOnly(2014, 3, 13));
        mount.Category.Should().Be("mount");
        mount.Event.Should().Be("no");
        mount.MountCategory.Should().Be("Flying");
        mount.Description!.RawText.Should().Be("A mystical serpent from the clouds of Pandaria.");
        mount.SearchText!.RawText.Should().Be("Cloud Serpent Mount Flying Dragon");
        mount.VariationMountIds.Should().HaveCount(2)
            .And.ContainInConsecutiveOrder("CloudSerpentMountVar1", "CloudSerpentMountVar2");
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MountDataDocument mountData = MountDataDocument.Load(jsonDocument);

        // act
        bool result = mountData.TryGetElementById("other", out Mount? mount);

        // assert
        result.Should().BeFalse();
        mount.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MountDataDocument mountData = MountDataDocument.Load(jsonDocument);

        // act
        Mount mount = mountData.GetElementById("MechanicalSpiderMount");

        // assert
        MechanicalSpiderBasicAssertions(mount);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MountDataDocument mountData = MountDataDocument.Load(jsonDocument);

        // act
        Action act = () => mountData.GetElementById("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MountDataDocument mountData = MountDataDocument.Load(jsonDocument);

        // act
        bool result = mountData.TryGetElementByHyperlinkId("MechanicalSpiderMount(hyperlink)", out Mount? mount);

        // assert
        result.Should().BeTrue();
        mount.Should().NotBeNull();

        MechanicalSpiderBasicAssertions(mount);
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MountDataDocument mountData = MountDataDocument.Load(jsonDocument);

        // act
        bool result = mountData.TryGetElementByHyperlinkId("other", out Mount? mount);

        // assert
        result.Should().BeFalse();
        mount.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByHyperlinkId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MountDataDocument mountData = MountDataDocument.Load(jsonDocument);

        // act
        Mount mount = mountData.GetElementByHyperlinkId("MechanicalSpiderMount(hyperlink)");

        // assert
        MechanicalSpiderBasicAssertions(mount);
    }

    [TestMethod]
    public void GetElementByHyperlinkId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MountDataDocument mountData = MountDataDocument.Load(jsonDocument);

        // act
        Action act = () => mountData.GetElementByHyperlinkId("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByAttributeId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MountDataDocument mountData = MountDataDocument.Load(jsonDocument);

        // act
        bool result = mountData.TryGetElementByAttributeId("MSpi", out Mount? mount);

        // assert
        result.Should().BeTrue();
        mount.Should().NotBeNull();

        MechanicalSpiderBasicAssertions(mount);
    }

    [TestMethod]
    public void TryGetElementByAttributeId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MountDataDocument mountData = MountDataDocument.Load(jsonDocument);

        // act
        bool result = mountData.TryGetElementByAttributeId("other", out Mount? mount);

        // assert
        result.Should().BeFalse();
        mount.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByAttributeId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MountDataDocument mountData = MountDataDocument.Load(jsonDocument);

        // act
        Mount mount = mountData.GetElementByAttributeId("MSpi");

        // assert
        MechanicalSpiderBasicAssertions(mount);
    }

    [TestMethod]
    public void GetElementByAttributeId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MountDataDocument mountData = MountDataDocument.Load(jsonDocument);

        // act
        Action act = () => mountData.GetElementByAttributeId("other");

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
            "hdpVersion": "5.0.0",
            "itemsType": "Data",
            "dataType": "MountData"
          },
          "items": {
            "MechanicalSpiderMount": {
              "name": "Mechanospider",
              "description": "A description",
              "searchText": "Search terms",
              "sortName": "Sort Spider"
            }
          }
        }
        """;

        string gameStringData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0",
            "itemsType": "GameStrings",
            "gameStringText": {
              "locale": "FRFR",
              "textType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "items": {
            "mount": {
              "name": {
                "MechanicalSpiderMount": "Mechanospider Localized"
              },
              "description": {
                "MechanicalSpiderMount": "Localized Description"
              },
              "searchText": {
                "MechanicalSpiderMount": "Localized Search Terms"
              },
              "sortName": {
                "MechanicalSpiderMount": "Localized Sort Name"
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        MountDataDocument mountData = MountDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        Mount mount = mountData.GetElementById("MechanicalSpiderMount");

        // assert
        mount.Should().NotBeNull();
        mount.Name!.RawText.Should().Be("Mechanospider Localized");
        mount.Name.GameStringLocale.Should().Be(StormLocale.FRFR);
        mount.Description!.RawText.Should().Be("Localized Description");
        mount.Description.GameStringLocale.Should().Be(StormLocale.FRFR);
        mount.SearchText!.RawText.Should().Be("Localized Search Terms");
        mount.SearchText.GameStringLocale.Should().Be(StormLocale.FRFR);
        mount.SortName!.RawText.Should().Be("Localized Sort Name");
        mount.SortName.GameStringLocale.Should().Be(StormLocale.FRFR);
    }

    [TestMethod]
    public void GetElements_WithItems_ReturnsAllElements()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MountDataDocument mountData = MountDataDocument.Load(jsonDocument);

        // act
        List<Mount> result = [.. mountData.GetElements()];

        // assert
        result.Should().HaveCount(2);
        result.Should().Contain(m => m.Id == "CloudSerpentMount");
        result.Should().Contain(m => m.Id == "MechanicalSpiderMount");
    }

    [TestMethod]
    public void GetElements_WithEmptyItems_ReturnsEmpty()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "MountData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MountDataDocument mountData = MountDataDocument.Load(jsonDocument);

        // act
        List<Mount> result = [.. mountData.GetElements()];

        // assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    [DataRow("Unknown")]
    [DataRow("HeroData")]
    [DataRow("UnitData")]
    public void Load_WithMismatchedDataType_ThrowsJsonException(string dataType)
    {
        // arrange
        string json = $$"""
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "{{dataType}}"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);

        // act
        Action act = () => MountDataDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<JsonException>().WithMessage("*does not match the expected data type*");
    }

    private static void MechanicalSpiderBasicAssertions(Mount mount)
    {
        mount.Id.Should().Be("MechanicalSpiderMount");
        mount.Name!.RawText.Should().Be("Mechanospider");
        mount.HyperlinkId.Should().Be("MechanicalSpiderMount(hyperlink)");
        mount.AttributeId.Should().Be("MSpi");
    }
}
