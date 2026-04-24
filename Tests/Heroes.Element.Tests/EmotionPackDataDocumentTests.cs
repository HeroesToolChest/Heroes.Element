namespace Heroes.Element.Tests;

[TestClass]
public class EmotionPackDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {
        "itemsType": "Data",
        "dataType": "EmoticonPackData"
      },
      "items": {
        "AbathurEmoticonPack1": {
          "name": "Abathur Pack 1",
          "hyperlinkId": "AbathurEmoticonPack1"
        },
        "AbathurEmoticonPack2": {
          "name": "Abathur Pack 2",
          "hyperlinkId": "AbathurEmoticonPack2(hyperlink)"
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
            "dataType": "EmoticonPackData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonPackDataDocument emoticonPackData = EmoticonPackDataDocument.Load(jsonDocument);

        // act
        bool result = emoticonPackData.TryGetElementById("other", out EmoticonPack? emoticonPack);

        // assert
        result.Should().BeFalse();
        emoticonPack.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_AbathurEmoticonPack1_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "EmoticonPackData"
          },
          "items": {
            "AbathurEmoticonPack1": {
              "name": "Abathur Pack 1",
              "sortName": "Abathur 1",
              "hyperlinkId": "AbathurEmoticonPack1",
              "franchise": "Starcraft",
              "rarity": "Rare",
              "releaseDate": "2017-04-25",
              "category": "emoticonpack",
              "event": "no",
              "description": "A pack of Abathur-themed emoticons.",
              "searchText": "Abathur Emoticon Pack Evolution Master Slug",
              "emoticonIds": [
                "abathur_pack_1",
                "abathur_pack_2",
                "abathur_pack_3"
              ]
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonPackDataDocument emoticonPackData = EmoticonPackDataDocument.Load(jsonDocument);

        // act
        bool returnResult = emoticonPackData.TryGetElementById("AbathurEmoticonPack1", out EmoticonPack? emoticonPack);

        // assert
        returnResult.Should().BeTrue();
        emoticonPack.Should().NotBeNull();
        emoticonPack.Id.Should().Be("AbathurEmoticonPack1");
        emoticonPack.Name!.RawText.Should().Be("Abathur Pack 1");
        emoticonPack.SortName!.RawText.Should().Be("Abathur 1");
        emoticonPack.HyperlinkId.Should().Be("AbathurEmoticonPack1");
        emoticonPack.Franchise.Should().Be(Franchise.Starcraft);
        emoticonPack.Rarity.Should().Be(Rarity.Rare);
        emoticonPack.ReleaseDate.Should().Be(new DateOnly(2017, 4, 25));
        emoticonPack.Category.Should().Be("emoticonpack");
        emoticonPack.Event.Should().Be("no");
        emoticonPack.Description!.RawText.Should().Be("A pack of Abathur-themed emoticons.");
        emoticonPack.SearchText!.RawText.Should().Be("Abathur Emoticon Pack Evolution Master Slug");
        emoticonPack.EmoticonIds.Should().HaveCount(3);
        emoticonPack.EmoticonIds.Should().Contain("abathur_pack_1");
        emoticonPack.EmoticonIds.Should().Contain("abathur_pack_2");
        emoticonPack.EmoticonIds.Should().Contain("abathur_pack_3");
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonPackDataDocument emoticonPackData = EmoticonPackDataDocument.Load(jsonDocument);

        // act
        bool result = emoticonPackData.TryGetElementById("other", out EmoticonPack? emoticonPack);

        // assert
        result.Should().BeFalse();
        emoticonPack.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonPackDataDocument emoticonPackData = EmoticonPackDataDocument.Load(jsonDocument);

        // act
        EmoticonPack emoticonPack = emoticonPackData.GetElementById("AbathurEmoticonPack2");

        // assert
        Pack2BasicAssertions(emoticonPack);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonPackDataDocument emoticonPackData = EmoticonPackDataDocument.Load(jsonDocument);

        // act
        Action act = () => emoticonPackData.GetElementById("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonPackDataDocument emoticonPackData = EmoticonPackDataDocument.Load(jsonDocument);

        // act
        bool result = emoticonPackData.TryGetElementByHyperlinkId("AbathurEmoticonPack2(hyperlink)", out EmoticonPack? emoticonPack);

        // assert
        result.Should().BeTrue();
        emoticonPack.Should().NotBeNull();

        Pack2BasicAssertions(emoticonPack);
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonPackDataDocument emoticonPackData = EmoticonPackDataDocument.Load(jsonDocument);

        // act
        bool result = emoticonPackData.TryGetElementByHyperlinkId("other", out EmoticonPack? emoticonPack);

        // assert
        result.Should().BeFalse();
        emoticonPack.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByHyperlinkId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonPackDataDocument emoticonPackData = EmoticonPackDataDocument.Load(jsonDocument);

        // act
        EmoticonPack emoticonPack = emoticonPackData.GetElementByHyperlinkId("AbathurEmoticonPack2(hyperlink)");

        // assert
        Pack2BasicAssertions(emoticonPack);
    }

    [TestMethod]
    public void GetElementByHyperlinkId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonPackDataDocument emoticonPackData = EmoticonPackDataDocument.Load(jsonDocument);

        // act
        Action act = () => emoticonPackData.GetElementByHyperlinkId("other");

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
            "dataType": "EmoticonPackData"
          },
          "items": {
            "AbathurEmoticonPack2": {
              "name": "Abathur Pack 2",
              "description": "A description",
              "searchText": "Search terms",
              "sortName": "Sort Abathur"
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
            "emoticonPack": {
              "name": {
                "AbathurEmoticonPack2": "Abathur Pack 2 Localized"
              },
              "description": {
                "AbathurEmoticonPack2": "Localized Description"
              },
              "searchText": {
                "AbathurEmoticonPack2": "Localized Search Terms"
              },
              "sortName": {
                "AbathurEmoticonPack2": "Localized Sort Name"
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        EmoticonPackDataDocument emoticonPackData = EmoticonPackDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        EmoticonPack emoticonPack = emoticonPackData.GetElementById("AbathurEmoticonPack2");

        // assert
        emoticonPack.Should().NotBeNull();
        emoticonPack.Name!.RawText.Should().Be("Abathur Pack 2 Localized");
        emoticonPack.Name.GameStringLocale.Should().Be(StormLocale.FRFR);
        emoticonPack.Description!.RawText.Should().Be("Localized Description");
        emoticonPack.Description.GameStringLocale.Should().Be(StormLocale.FRFR);
        emoticonPack.SearchText!.RawText.Should().Be("Localized Search Terms");
        emoticonPack.SearchText.GameStringLocale.Should().Be(StormLocale.FRFR);
        emoticonPack.SortName!.RawText.Should().Be("Localized Sort Name");
        emoticonPack.SortName.GameStringLocale.Should().Be(StormLocale.FRFR);
    }

    [TestMethod]
    public void GetElements_WithItems_ReturnsAllElements()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonPackDataDocument emoticonPackData = EmoticonPackDataDocument.Load(jsonDocument);

        // act
        List<EmoticonPack> result = [.. emoticonPackData.GetElements()];

        // assert
        result.Should().HaveCount(2);
        result.Should().Contain(e => e.Id == "AbathurEmoticonPack1");
        result.Should().Contain(e => e.Id == "AbathurEmoticonPack2");
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
            "dataType": "EmoticonPackData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonPackDataDocument emoticonPackData = EmoticonPackDataDocument.Load(jsonDocument);

        // act
        List<EmoticonPack> result = [.. emoticonPackData.GetElements()];

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
        Action act = () => EmoticonPackDataDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<JsonException>().WithMessage("*does not match the expected data type*");
    }

    [TestMethod]
    public void GetElementObjects_WithItems_ReturnsAllElementsAsObjects()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonPackDataDocument emoticonPackData = EmoticonPackDataDocument.Load(jsonDocument);

        // act
        List<object> result = [.. emoticonPackData.GetElementObjects()];

        // assert
        result.Should().HaveCount(2);
        result.Should().AllBeOfType<EmoticonPack>();
        result.OfType<EmoticonPack>().Should().Contain(e => e.Id == "AbathurEmoticonPack1");
        result.OfType<EmoticonPack>().Should().Contain(e => e.Id == "AbathurEmoticonPack2");
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
            "dataType": "EmoticonPackData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonPackDataDocument emoticonPackData = EmoticonPackDataDocument.Load(jsonDocument);

        // act
        List<object> result = [.. emoticonPackData.GetElementObjects()];

        // assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void GetElementObjects_WithItems_ReturnsObjectsWithCorrectProperties()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonPackDataDocument emoticonPackData = EmoticonPackDataDocument.Load(jsonDocument);

        // act
        List<object> result = [.. emoticonPackData.GetElementObjects()];

        // assert
        result.Should().HaveCount(2);

        EmoticonPack pack2 = result.OfType<EmoticonPack>().First(e => e.Id == "AbathurEmoticonPack2");
        Pack2BasicAssertions(pack2);
    }

    private static void Pack2BasicAssertions(EmoticonPack emoticonPack)
    {
        emoticonPack.Id.Should().Be("AbathurEmoticonPack2");
        emoticonPack.Name!.RawText.Should().Be("Abathur Pack 2");
        emoticonPack.HyperlinkId.Should().Be("AbathurEmoticonPack2(hyperlink)");
    }
}
