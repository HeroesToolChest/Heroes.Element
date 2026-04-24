namespace Heroes.Element.Tests;

[TestClass]
public class RewardPortraitDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {
        "itemsType": "Data",
        "dataType": "RewardPortraitData"
      },
      "items": {
        "RewardPortraitRaynor001": {
          "name": "Raynor Portrait 001",
          "hyperlinkId": "RewardPortraitRaynor001"
        },
        "RewardPortraitRaynor002": {
          "name": "Raynor Portrait 002",
          "hyperlinkId": "RewardPortraitRaynor002(hyperlink)"
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
            "dataType": "RewardPortraitData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardPortraitDataDocument rewardPortraitData = RewardPortraitDataDocument.Load(jsonDocument);

        // act
        bool result = rewardPortraitData.TryGetElementById("other", out RewardPortrait? rewardPortrait);

        // assert
        result.Should().BeFalse();
        rewardPortrait.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_RewardPortraitRaynor001_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "RewardPortraitData"
          },
          "items": {
            "RewardPortraitRaynor001": {
              "name": "Raynor Portrait 001",
              "sortName": "Portrait Raynor 001",
              "hyperlinkId": "RewardPortraitRaynor001",
              "franchise": "Starcraft",
              "rarity": "Epic",
              "releaseDate": "2015-03-13",
              "category": "rewardportrait",
              "event": "no",
              "description": "Portrait of Jim Raynor, Marshal of Mar Sara.",
              "searchText": "Raynor Portrait Marshal Terran",
              "descriptionUnearned": "Complete 10 games as Raynor to unlock this portrait.",
              "portraitPackId": "PortraitPackStarcraftLegacy1",
              "heroId": "Raynor",
              "iconSlot": 5,
              "image": "storm_ui_reward_portrait_raynor001.png",
              "textureSheet": {
                "image": "ui_heroes_portraits_sheet0.png",
                "columns": 8,
                "rows": 8
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardPortraitDataDocument rewardPortraitData = RewardPortraitDataDocument.Load(jsonDocument);

        // act
        bool returnResult = rewardPortraitData.TryGetElementById("RewardPortraitRaynor001", out RewardPortrait? rewardPortrait);

        // assert
        returnResult.Should().BeTrue();
        rewardPortrait.Should().NotBeNull();
        rewardPortrait.Id.Should().Be("RewardPortraitRaynor001");
        rewardPortrait.Name!.RawText.Should().Be("Raynor Portrait 001");
        rewardPortrait.SortName!.RawText.Should().Be("Portrait Raynor 001");
        rewardPortrait.HyperlinkId.Should().Be("RewardPortraitRaynor001");
        rewardPortrait.Franchise.Should().Be(Franchise.Starcraft);
        rewardPortrait.Rarity.Should().Be(Rarity.Epic);
        rewardPortrait.ReleaseDate.Should().Be(new DateOnly(2015, 3, 13));
        rewardPortrait.Category.Should().Be("rewardportrait");
        rewardPortrait.Event.Should().Be("no");
        rewardPortrait.Description!.RawText.Should().Be("Portrait of Jim Raynor, Marshal of Mar Sara.");
        rewardPortrait.SearchText!.RawText.Should().Be("Raynor Portrait Marshal Terran");
        rewardPortrait.DescriptionUnearned!.RawText.Should().Be("Complete 10 games as Raynor to unlock this portrait.");
        rewardPortrait.PortraitPackId.Should().Be("PortraitPackStarcraftLegacy1");
        rewardPortrait.HeroId.Should().Be("Raynor");
        rewardPortrait.IconSlot.Should().Be(5);
        rewardPortrait.Image.Should().Be("storm_ui_reward_portrait_raynor001.png");
        rewardPortrait.TextureSheet.Should().NotBeNull();
        rewardPortrait.TextureSheet.Image.Should().Be("ui_heroes_portraits_sheet0.png");
        rewardPortrait.TextureSheet.Columns.Should().Be(8);
        rewardPortrait.TextureSheet.Rows.Should().Be(8);
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardPortraitDataDocument rewardPortraitData = RewardPortraitDataDocument.Load(jsonDocument);

        // act
        bool result = rewardPortraitData.TryGetElementById("other", out RewardPortrait? rewardPortrait);

        // assert
        result.Should().BeFalse();
        rewardPortrait.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardPortraitDataDocument rewardPortraitData = RewardPortraitDataDocument.Load(jsonDocument);

        // act
        RewardPortrait rewardPortrait = rewardPortraitData.GetElementById("RewardPortraitRaynor002");

        // assert
        Portrait002BasicAssertions(rewardPortrait);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardPortraitDataDocument rewardPortraitData = RewardPortraitDataDocument.Load(jsonDocument);

        // act
        Action act = () => rewardPortraitData.GetElementById("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardPortraitDataDocument rewardPortraitData = RewardPortraitDataDocument.Load(jsonDocument);

        // act
        bool result = rewardPortraitData.TryGetElementByHyperlinkId("RewardPortraitRaynor002(hyperlink)", out RewardPortrait? rewardPortrait);

        // assert
        result.Should().BeTrue();
        rewardPortrait.Should().NotBeNull();

        Portrait002BasicAssertions(rewardPortrait);
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardPortraitDataDocument rewardPortraitData = RewardPortraitDataDocument.Load(jsonDocument);

        // act
        bool result = rewardPortraitData.TryGetElementByHyperlinkId("other", out RewardPortrait? rewardPortrait);

        // assert
        result.Should().BeFalse();
        rewardPortrait.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByHyperlinkId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardPortraitDataDocument rewardPortraitData = RewardPortraitDataDocument.Load(jsonDocument);

        // act
        RewardPortrait rewardPortrait = rewardPortraitData.GetElementByHyperlinkId("RewardPortraitRaynor002(hyperlink)");

        // assert
        Portrait002BasicAssertions(rewardPortrait);
    }

    [TestMethod]
    public void GetElementByHyperlinkId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardPortraitDataDocument rewardPortraitData = RewardPortraitDataDocument.Load(jsonDocument);

        // act
        Action act = () => rewardPortraitData.GetElementByHyperlinkId("other");

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
            "dataType": "RewardPortraitData"
          },
          "items": {
            "RewardPortraitRaynor002": {
              "name": "Raynor Portrait 002",
              "description": "A description",
              "searchText": "Search terms",
              "sortName": "Sort Raynor",
              "descriptionUnearned": "Unearned description"
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
              "preserveFontConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "items": {
            "rewardPortrait": {
              "name": {
                "RewardPortraitRaynor002": "Raynor Portrait 002 Localized"
              },
              "description": {
                "RewardPortraitRaynor002": "Localized Description"
              },
              "searchText": {
                "RewardPortraitRaynor002": "Localized Search Terms"
              },
              "sortName": {
                "RewardPortraitRaynor002": "Localized Sort Name"
              },
              "descriptionUnearned": {
                "RewardPortraitRaynor002": "Localized Unearned Description"
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        RewardPortraitDataDocument rewardPortraitData = RewardPortraitDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        RewardPortrait rewardPortrait = rewardPortraitData.GetElementById("RewardPortraitRaynor002");

        // assert
        rewardPortrait.Should().NotBeNull();
        rewardPortrait.Name!.RawText.Should().Be("Raynor Portrait 002 Localized");
        rewardPortrait.Name.GameStringLocale.Should().Be(StormLocale.FRFR);
        rewardPortrait.Description!.RawText.Should().Be("Localized Description");
        rewardPortrait.Description.GameStringLocale.Should().Be(StormLocale.FRFR);
        rewardPortrait.SearchText!.RawText.Should().Be("Localized Search Terms");
        rewardPortrait.SearchText.GameStringLocale.Should().Be(StormLocale.FRFR);
        rewardPortrait.SortName!.RawText.Should().Be("Localized Sort Name");
        rewardPortrait.SortName.GameStringLocale.Should().Be(StormLocale.FRFR);
        rewardPortrait.DescriptionUnearned!.RawText.Should().Be("Localized Unearned Description");
        rewardPortrait.DescriptionUnearned.GameStringLocale.Should().Be(StormLocale.FRFR);
    }

    [TestMethod]
    public void GetElements_WithItems_ReturnsAllElements()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardPortraitDataDocument rewardPortraitData = RewardPortraitDataDocument.Load(jsonDocument);

        // act
        List<RewardPortrait> result = [.. rewardPortraitData.GetElements()];

        // assert
        result.Should().HaveCount(2);
        result.Should().Contain(r => r.Id == "RewardPortraitRaynor001");
        result.Should().Contain(r => r.Id == "RewardPortraitRaynor002");
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
            "dataType": "RewardPortraitData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardPortraitDataDocument rewardPortraitData = RewardPortraitDataDocument.Load(jsonDocument);

        // act
        List<RewardPortrait> result = [.. rewardPortraitData.GetElements()];

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
        Action act = () => RewardPortraitDataDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<JsonException>().WithMessage("*does not match the expected data type*");
    }

    [TestMethod]
    public void GetElementObjects_WithItems_ReturnsAllElementsAsObjects()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardPortraitDataDocument rewardPortraitData = RewardPortraitDataDocument.Load(jsonDocument);

        // act
        List<object> result = [.. rewardPortraitData.GetElementObjects()];

        // assert
        result.Should().HaveCount(2);
        result.Should().AllBeOfType<RewardPortrait>();
        result.OfType<RewardPortrait>().Should().Contain(r => r.Id == "RewardPortraitRaynor001");
        result.OfType<RewardPortrait>().Should().Contain(r => r.Id == "RewardPortraitRaynor002");
    }

    [TestMethod]
    public void GetElementObjects_WithEmptyItems_ReturnsEmpty()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "RewardPortraitData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardPortraitDataDocument rewardPortraitData = RewardPortraitDataDocument.Load(jsonDocument);

        // act
        List<object> result = [.. rewardPortraitData.GetElementObjects()];

        // assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void GetElementObjects_WithItems_ReturnsObjectsWithCorrectProperties()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardPortraitDataDocument rewardPortraitData = RewardPortraitDataDocument.Load(jsonDocument);

        // act
        List<object> result = [.. rewardPortraitData.GetElementObjects()];

        // assert
        result.Should().HaveCount(2);

        RewardPortrait portrait002 = result.OfType<RewardPortrait>().First(r => r.Id == "RewardPortraitRaynor002");
        Portrait002BasicAssertions(portrait002);
    }

    private static void Portrait002BasicAssertions(RewardPortrait rewardPortrait)
    {
        rewardPortrait.Id.Should().Be("RewardPortraitRaynor002");
        rewardPortrait.Name!.RawText.Should().Be("Raynor Portrait 002");
        rewardPortrait.HyperlinkId.Should().Be("RewardPortraitRaynor002(hyperlink)");
    }
}
