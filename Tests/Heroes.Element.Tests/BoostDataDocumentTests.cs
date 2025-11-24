namespace Heroes.Element.Tests;

[TestClass]
public class BoostDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {},
      "items": {
        "BoostStimpak": {
          "name": "3-Day Stimpack",
          "hyperlinkId": "BoostStimpak",
          "attributeId": "Stim"
        },
        "BoostMegaStimpak": {
          "name": "7-Day Stimpack",
          "hyperlinkId": "BoostMegaStimpak(hyperlink)",
          "attributeId": "Mega"
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
        BoostDataDocument boostData = BoostDataDocument.Load(jsonDocument);

        // act
        bool result = boostData.TryGetElementById("other", out Boost? boost);

        // assert
        result.Should().BeFalse();
        boost.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_BoostStimpak_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {},
          "items": {
            "BoostStimpak": {
              "name": "3-Day Stimpack",
              "sortName": "Stimpack 3",
              "hyperlinkId": "BoostStimpak",
              "attributeId": "Stim",
              "franchise": "Starcraft",
              "rarity": "Common",
              "releaseDate": "2015-06-02",
              "category": "boost",
              "event": "no",
              "description": "Increases experience gain by 50% for 3 days.",
              "searchText": "Stimpack Boost Experience XP 3 Day"
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BoostDataDocument boostData = BoostDataDocument.Load(jsonDocument);

        // act
        bool returnResult = boostData.TryGetElementById("BoostStimpak", out Boost? boost);

        // assert
        returnResult.Should().BeTrue();
        boost.Should().NotBeNull();
        boost.Id.Should().Be("BoostStimpak");
        boost.Name!.RawText.Should().Be("3-Day Stimpack");
        boost.SortName!.RawText.Should().Be("Stimpack 3");
        boost.HyperlinkId.Should().Be("BoostStimpak");
        boost.AttributeId.Should().Be("Stim");
        boost.Franchise.Should().Be(Franchise.Starcraft);
        boost.Rarity.Should().Be(Rarity.Common);
        boost.ReleaseDate.Should().Be(new DateOnly(2015, 6, 2));
        boost.Category.Should().Be("boost");
        boost.Event.Should().Be("no");
        boost.Description!.RawText.Should().Be("Increases experience gain by 50% for 3 days.");
        boost.SearchText!.RawText.Should().Be("Stimpack Boost Experience XP 3 Day");
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BoostDataDocument boostData = BoostDataDocument.Load(jsonDocument);

        // act
        bool result = boostData.TryGetElementById("other", out Boost? boost);

        // assert
        result.Should().BeFalse();
        boost.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BoostDataDocument boostData = BoostDataDocument.Load(jsonDocument);

        // act
        Boost boost = boostData.GetElementById("BoostMegaStimpak");

        // assert
        MegaStimpakBasicAssertions(boost);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BoostDataDocument boostData = BoostDataDocument.Load(jsonDocument);

        // act
        Action act = () => boostData.GetElementById("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BoostDataDocument boostData = BoostDataDocument.Load(jsonDocument);

        // act
        bool result = boostData.TryGetElementByHyperlinkId("BoostMegaStimpak(hyperlink)", out Boost? boost);

        // assert
        result.Should().BeTrue();
        boost.Should().NotBeNull();

        MegaStimpakBasicAssertions(boost);
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BoostDataDocument boostData = BoostDataDocument.Load(jsonDocument);

        // act
        bool result = boostData.TryGetElementByHyperlinkId("other", out Boost? boost);

        // assert
        result.Should().BeFalse();
        boost.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByHyperlinkId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BoostDataDocument boostData = BoostDataDocument.Load(jsonDocument);

        // act
        Boost boost = boostData.GetElementByHyperlinkId("BoostMegaStimpak(hyperlink)");

        // assert
        MegaStimpakBasicAssertions(boost);
    }

    [TestMethod]
    public void GetElementByHyperlinkId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BoostDataDocument boostData = BoostDataDocument.Load(jsonDocument);

        // act
        Action act = () => boostData.GetElementByHyperlinkId("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByAttributeId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BoostDataDocument boostData = BoostDataDocument.Load(jsonDocument);

        // act
        bool result = boostData.TryGetElementByAttributeId("Mega", out Boost? boost);

        // assert
        result.Should().BeTrue();
        boost.Should().NotBeNull();

        MegaStimpakBasicAssertions(boost);
    }

    [TestMethod]
    public void TryGetElementByAttributeId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BoostDataDocument boostData = BoostDataDocument.Load(jsonDocument);

        // act
        bool result = boostData.TryGetElementByAttributeId("other", out Boost? boost);

        // assert
        result.Should().BeFalse();
        boost.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByAttributeId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BoostDataDocument boostData = BoostDataDocument.Load(jsonDocument);

        // act
        Boost boost = boostData.GetElementByAttributeId("Mega");

        // assert
        MegaStimpakBasicAssertions(boost);
    }

    [TestMethod]
    public void GetElementByAttributeId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BoostDataDocument boostData = BoostDataDocument.Load(jsonDocument);

        // act
        Action act = () => boostData.GetElementByAttributeId("other");

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
            "BoostMegaStimpak": {
              "name": "7-Day Stimpack",
              "description": "A description",
              "searchText": "Search terms",
              "sortName": "Sort Mega Stimpack"
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
            "boost": {
              "name": {
                "BoostMegaStimpak": "7-Day Stimpack Localized"
              },
              "description": {
                "BoostMegaStimpak": "Localized Description"
              },
              "searchText": {
                "BoostMegaStimpak": "Localized Search Terms"
              },
              "sortName": {
                "BoostMegaStimpak": "Localized Sort Name"
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        BoostDataDocument boostData = BoostDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        Boost boost = boostData.GetElementById("BoostMegaStimpak");

        // assert
        boost.Should().NotBeNull();
        boost.Name!.RawText.Should().Be("7-Day Stimpack Localized");
        boost.Name.GameStringLocale.Should().Be(StormLocale.FRFR);
        boost.Description!.RawText.Should().Be("Localized Description");
        boost.Description.GameStringLocale.Should().Be(StormLocale.FRFR);
        boost.SearchText!.RawText.Should().Be("Localized Search Terms");
        boost.SearchText.GameStringLocale.Should().Be(StormLocale.FRFR);
        boost.SortName!.RawText.Should().Be("Localized Sort Name");
        boost.SortName.GameStringLocale.Should().Be(StormLocale.FRFR);
    }

    private static void MegaStimpakBasicAssertions(Boost boost)
    {
        boost.Id.Should().Be("BoostMegaStimpak");
        boost.Name!.RawText.Should().Be("7-Day Stimpack");
        boost.HyperlinkId.Should().Be("BoostMegaStimpak(hyperlink)");
        boost.AttributeId.Should().Be("Mega");
    }
}
