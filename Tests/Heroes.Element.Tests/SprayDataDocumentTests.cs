namespace Heroes.Element.Tests;

[TestClass]
public class SprayDataDocumentTests
{
    private readonly string _defaultArrangeJson =
"""
    {
      "meta": {},
      "items": {
        "SprayAnimatedBWAhhhh": {
          "name": "Ahhhh!",
          "hyperlinkId": "SprayAnimatedBWAhhhh",
          "attributeId": "SAhh"
        },
        "SprayStaticPackHappy": {
          "name": "Happy Face",
          "hyperlinkId": "SprayStaticPackHappy(hyperlink)",
          "attributeId": "SHap"
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
        SprayDataDocument sprayData = SprayDataDocument.Load(jsonDocument);

        // act
        bool result = sprayData.TryGetElementById("other", out Spray? spray);

        // assert
        result.Should().BeFalse();
        spray.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_SprayAnimatedBWAhhhh_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {},
          "items": {
            "SprayAnimatedBWAhhhh": {
              "name": "Ahhhh!",
              "sortName": "Ahhhh Blackheart",
              "hyperlinkId": "SprayAnimatedBWAhhhh",
              "attributeId": "SAhh",
              "franchise": "Nexus",
              "rarity": "Rare",
              "releaseDate": "2017-04-25",
              "category": "spray",
              "event": "no",
              "image": "storm_ui_spray_bwahhh.png",
              "description": "An animated spray featuring Blackheart's Bay.",
              "searchText": "Ahhhh Spray Animated Blackheart",
              "animation": {
                "texture": "storm_ui_spray_bwahhh_animation.png",
                "frames": 30,
                "duration": 1000
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SprayDataDocument sprayData = SprayDataDocument.Load(jsonDocument);

        // act
        bool returnResult = sprayData.TryGetElementById("SprayAnimatedBWAhhhh", out Spray? spray);

        // assert
        returnResult.Should().BeTrue();
        spray.Should().NotBeNull();
        spray.Id.Should().Be("SprayAnimatedBWAhhhh");
        spray.Name!.RawText.Should().Be("Ahhhh!");
        spray.SortName!.RawText.Should().Be("Ahhhh Blackheart");
        spray.HyperlinkId.Should().Be("SprayAnimatedBWAhhhh");
        spray.AttributeId.Should().Be("SAhh");
        spray.Franchise.Should().Be(Franchise.Nexus);
        spray.Rarity.Should().Be(Rarity.Rare);
        spray.ReleaseDate.Should().Be(new DateOnly(2017, 4, 25));
        spray.Category.Should().Be("spray");
        spray.Event.Should().Be("no");
        spray.Image.Should().Be("storm_ui_spray_bwahhh.png");
        spray.Description!.RawText.Should().Be("An animated spray featuring Blackheart's Bay.");
        spray.SearchText!.RawText.Should().Be("Ahhhh Spray Animated Blackheart");
        spray.Animation.Should().NotBeNull();
        spray.Animation!.Texture.Should().Be("storm_ui_spray_bwahhh_animation.png");
        spray.Animation.Frames.Should().Be(30);
        spray.Animation.Duration.Should().Be(1000);
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SprayDataDocument sprayData = SprayDataDocument.Load(jsonDocument);

        // act
        bool result = sprayData.TryGetElementById("other", out Spray? spray);

        // assert
        result.Should().BeFalse();
        spray.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SprayDataDocument sprayData = SprayDataDocument.Load(jsonDocument);

        // act
        Spray spray = sprayData.GetElementById("SprayStaticPackHappy");

        // assert
        HappyFaceBasicAssertions(spray);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SprayDataDocument sprayData = SprayDataDocument.Load(jsonDocument);

        // act
        Action act = () => sprayData.GetElementById("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SprayDataDocument sprayData = SprayDataDocument.Load(jsonDocument);

        // act
        bool result = sprayData.TryGetElementByHyperlinkId("SprayStaticPackHappy(hyperlink)", out Spray? spray);

        // assert
        result.Should().BeTrue();
        spray.Should().NotBeNull();

        HappyFaceBasicAssertions(spray);
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SprayDataDocument sprayData = SprayDataDocument.Load(jsonDocument);

        // act
        bool result = sprayData.TryGetElementByHyperlinkId("other", out Spray? spray);

        // assert
        result.Should().BeFalse();
        spray.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByHyperlinkId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SprayDataDocument sprayData = SprayDataDocument.Load(jsonDocument);

        // act
        Spray spray = sprayData.GetElementByHyperlinkId("SprayStaticPackHappy(hyperlink)");

        // assert
        HappyFaceBasicAssertions(spray);
    }

    [TestMethod]
    public void GetElementByHyperlinkId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SprayDataDocument sprayData = SprayDataDocument.Load(jsonDocument);

        // act
        Action act = () => sprayData.GetElementByHyperlinkId("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByAttributeId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SprayDataDocument sprayData = SprayDataDocument.Load(jsonDocument);

        // act
        bool result = sprayData.TryGetElementByAttributeId("SHap", out Spray? spray);

        // assert
        result.Should().BeTrue();
        spray.Should().NotBeNull();

        HappyFaceBasicAssertions(spray);
    }

    [TestMethod]
    public void TryGetElementByAttributeId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SprayDataDocument sprayData = SprayDataDocument.Load(jsonDocument);

        // act
        bool result = sprayData.TryGetElementByAttributeId("other", out Spray? spray);

        // assert
        result.Should().BeFalse();
        spray.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByAttributeId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SprayDataDocument sprayData = SprayDataDocument.Load(jsonDocument);

        // act
        Spray spray = sprayData.GetElementByAttributeId("SHap");

        // assert
        HappyFaceBasicAssertions(spray);
    }

    [TestMethod]
    public void GetElementByAttributeId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SprayDataDocument sprayData = SprayDataDocument.Load(jsonDocument);

        // act
        Action act = () => sprayData.GetElementByAttributeId("other");

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
            "SprayStaticPackHappy": {
              "name": "Happy Face",
              "description": "A description",
              "searchText": "Search terms",
              "sortName": "Sort Happy"
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
          "items": {
            "spray": {
              "name": {
                "SprayStaticPackHappy": "Happy Face Localized"
              },
              "description": {
                "SprayStaticPackHappy": "Localized Description"
              },
              "searchText": {
                "SprayStaticPackHappy": "Localized Search Terms"
              },
              "sortName": {
                "SprayStaticPackHappy": "Localized Sort Name"
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        SprayDataDocument sprayData = SprayDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        Spray spray = sprayData.GetElementById("SprayStaticPackHappy");

        // assert
        spray.Should().NotBeNull();
        spray.Name!.RawText.Should().Be("Happy Face Localized");
        spray.Name.GameStringLocale.Should().Be(StormLocale.FRFR);
        spray.Description!.RawText.Should().Be("Localized Description");
        spray.Description.GameStringLocale.Should().Be(StormLocale.FRFR);
        spray.SearchText!.RawText.Should().Be("Localized Search Terms");
        spray.SearchText.GameStringLocale.Should().Be(StormLocale.FRFR);
        spray.SortName!.RawText.Should().Be("Localized Sort Name");
        spray.SortName.GameStringLocale.Should().Be(StormLocale.FRFR);
    }

    [TestMethod]
    public void GetElements_WithItems_ReturnsAllElements()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SprayDataDocument sprayData = SprayDataDocument.Load(jsonDocument);

        // act
        List<Spray> result = [.. sprayData.GetElements()];

        // assert
        result.Should().HaveCount(2);
        result.Should().Contain(s => s.Id == "SprayAnimatedBWAhhhh");
        result.Should().Contain(s => s.Id == "SprayStaticPackHappy");
    }

    [TestMethod]
    public void GetElements_WithEmptyItems_ReturnsEmpty()
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
        SprayDataDocument sprayData = SprayDataDocument.Load(jsonDocument);

        // act
        List<Spray> result = [.. sprayData.GetElements()];

        // assert
        result.Should().BeEmpty();
    }

    private static void HappyFaceBasicAssertions(Spray spray)
    {
        spray.Id.Should().Be("SprayStaticPackHappy");
        spray.Name!.RawText.Should().Be("Happy Face");
        spray.HyperlinkId.Should().Be("SprayStaticPackHappy(hyperlink)");
        spray.AttributeId.Should().Be("SHap");
    }
}
