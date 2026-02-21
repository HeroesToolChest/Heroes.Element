namespace Heroes.Element.Tests;

[TestClass]
public class BannerDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {},
      "items": {
        "BannerD3Imperius": {
          "name": "Imperius Banner",
          "hyperlinkId": "BannerD3Imperius",
          "attributeId": "Impe"
        },
        "BannerD3Tyrael": {
          "name": "Tyrael Banner",
          "hyperlinkId": "BannerD3Tyrael(hyperlink)",
          "attributeId": "Tyra"
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
        BannerDataDocument bannerData = BannerDataDocument.Load(jsonDocument);

        // act
        bool result = bannerData.TryGetElementById("other", out Banner? banner);

        // assert
        result.Should().BeFalse();
        banner.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_BannerD3Imperius_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {},
          "items": {
            "BannerD3Imperius": {
              "name": "Imperius Banner",
              "sortName": "Imperius",
              "hyperlinkId": "BannerD3Imperius",
              "attributeId": "Impe",
              "franchise": "Diablo",
              "rarity": "Epic",
              "releaseDate": "2019-01-08",
              "category": "banner",
              "event": "no",
              "description": "Banner celebrating the Archangel of Valor.",
              "searchText": "Imperius Banner Diablo Archangel Valor"
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BannerDataDocument bannerData = BannerDataDocument.Load(jsonDocument);

        // act
        bool returnResult = bannerData.TryGetElementById("BannerD3Imperius", out Banner? banner);

        // assert
        returnResult.Should().BeTrue();
        banner.Should().NotBeNull();
        banner.Id.Should().Be("BannerD3Imperius");
        banner.Name!.RawText.Should().Be("Imperius Banner");
        banner.SortName!.RawText.Should().Be("Imperius");
        banner.HyperlinkId.Should().Be("BannerD3Imperius");
        banner.AttributeId.Should().Be("Impe");
        banner.Franchise.Should().Be(Franchise.Diablo);
        banner.Rarity.Should().Be(Rarity.Epic);
        banner.ReleaseDate.Should().Be(new DateOnly(2019, 1, 8));
        banner.Category.Should().Be("banner");
        banner.Event.Should().Be("no");
        banner.Description!.RawText.Should().Be("Banner celebrating the Archangel of Valor.");
        banner.SearchText!.RawText.Should().Be("Imperius Banner Diablo Archangel Valor");
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BannerDataDocument bannerData = BannerDataDocument.Load(jsonDocument);

        // act
        bool result = bannerData.TryGetElementById("other", out Banner? banner);

        // assert
        result.Should().BeFalse();
        banner.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BannerDataDocument bannerData = BannerDataDocument.Load(jsonDocument);

        // act
        Banner banner = bannerData.GetElementById("BannerD3Tyrael");

        // assert
        TyraelBasicAssertions(banner);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BannerDataDocument bannerData = BannerDataDocument.Load(jsonDocument);

        // act
        Action act = () => bannerData.GetElementById("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BannerDataDocument bannerData = BannerDataDocument.Load(jsonDocument);

        // act
        bool result = bannerData.TryGetElementByHyperlinkId("BannerD3Tyrael(hyperlink)", out Banner? banner);

        // assert
        result.Should().BeTrue();
        banner.Should().NotBeNull();

        TyraelBasicAssertions(banner);
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BannerDataDocument bannerData = BannerDataDocument.Load(jsonDocument);

        // act
        bool result = bannerData.TryGetElementByHyperlinkId("other", out Banner? banner);

        // assert
        result.Should().BeFalse();
        banner.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByHyperlinkId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BannerDataDocument bannerData = BannerDataDocument.Load(jsonDocument);

        // act
        Banner banner = bannerData.GetElementByHyperlinkId("BannerD3Tyrael(hyperlink)");

        // assert
        TyraelBasicAssertions(banner);
    }

    [TestMethod]
    public void GetElementByHyperlinkId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BannerDataDocument bannerData = BannerDataDocument.Load(jsonDocument);

        // act
        Action act = () => bannerData.GetElementByHyperlinkId("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByAttributeId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BannerDataDocument bannerData = BannerDataDocument.Load(jsonDocument);

        // act
        bool result = bannerData.TryGetElementByAttributeId("Tyra", out Banner? banner);

        // assert
        result.Should().BeTrue();
        banner.Should().NotBeNull();

        TyraelBasicAssertions(banner);
    }

    [TestMethod]
    public void TryGetElementByAttributeId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BannerDataDocument bannerData = BannerDataDocument.Load(jsonDocument);

        // act
        bool result = bannerData.TryGetElementByAttributeId("other", out Banner? banner);

        // assert
        result.Should().BeFalse();
        banner.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByAttributeId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BannerDataDocument bannerData = BannerDataDocument.Load(jsonDocument);

        // act
        Banner banner = bannerData.GetElementByAttributeId("Tyra");

        // assert
        TyraelBasicAssertions(banner);
    }

    [TestMethod]
    public void GetElementByAttributeId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BannerDataDocument bannerData = BannerDataDocument.Load(jsonDocument);

        // act
        Action act = () => bannerData.GetElementByAttributeId("other");

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
            "BannerD3Tyrael": {
              "name": "Tyrael Banner",
              "description": "A description",
              "searchText": "Search terms",
              "sortName": "Sort Tyrael"
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
            "banner": {
              "name": {
                "BannerD3Tyrael": "Tyrael Banner Localized"
              },
              "description": {
                "BannerD3Tyrael": "Localized Description"
              },
              "searchText": {
                "BannerD3Tyrael": "Localized Search Terms"
              },
              "sortName": {
                "BannerD3Tyrael": "Localized Sort Name"
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        BannerDataDocument bannerData = BannerDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        Banner banner = bannerData.GetElementById("BannerD3Tyrael");

        // assert
        banner.Should().NotBeNull();
        banner.Name!.RawText.Should().Be("Tyrael Banner Localized");
        banner.Name.GameStringLocale.Should().Be(StormLocale.FRFR);
        banner.Description!.RawText.Should().Be("Localized Description");
        banner.Description.GameStringLocale.Should().Be(StormLocale.FRFR);
        banner.SearchText!.RawText.Should().Be("Localized Search Terms");
        banner.SearchText.GameStringLocale.Should().Be(StormLocale.FRFR);
        banner.SortName!.RawText.Should().Be("Localized Sort Name");
        banner.SortName.GameStringLocale.Should().Be(StormLocale.FRFR);
    }

    private static void TyraelBasicAssertions(Banner banner)
    {
        banner.Id.Should().Be("BannerD3Tyrael");
        banner.Name!.RawText.Should().Be("Tyrael Banner");
        banner.HyperlinkId.Should().Be("BannerD3Tyrael(hyperlink)");
        banner.AttributeId.Should().Be("Tyra");
    }
}
