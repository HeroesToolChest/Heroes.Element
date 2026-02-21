namespace Heroes.Element.Tests;

[TestClass]
public class AnnouncerDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {},
      "items": {
        "AbathurAnnouncer": {
          "name": "Abathur Announcer",
          "hyperlinkId": "AbathurAnnouncer",
          "attributeId": "Abat"
        },
        "AlarakAnnouncer": {
          "name": "Alarak Announcer",
          "hyperlinkId": "AlarakAnnouncer(hyperlink)",
          "attributeId": "Alar"
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
        AnnouncerDataDocument announcerData = AnnouncerDataDocument.Load(jsonDocument);

        // act
        bool result = announcerData.TryGetElementById("other", out Announcer? announcer);

        // assert
        result.Should().BeFalse();
        announcer.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_AbathurAnnouncer_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {},
          "items": {
            "AbathurAnnouncer": {
              "name": "Abathur Announcer",
              "sortName": "Abathur",
              "hyperlinkId": "AbathurAnnouncer",
              "attributeId": "Abat",
              "franchise": "Starcraft",
              "rarity": "Legendary",
              "releaseDate": "2014-03-13",
              "category": "announcer",
              "event": "no",
              "gender": "Neutral",
              "heroId": "Abathur",
              "image": "storm_ui_announcer_abathur.png",
              "description": "The Evolution Master will guide you to victory.",
              "searchText": "Abathur Announcer Evolution Master Slug"
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        AnnouncerDataDocument announcerData = AnnouncerDataDocument.Load(jsonDocument);

        // act
        bool returnResult = announcerData.TryGetElementById("AbathurAnnouncer", out Announcer? announcer);

        // assert
        returnResult.Should().BeTrue();
        announcer.Should().NotBeNull();
        announcer.Id.Should().Be("AbathurAnnouncer");
        announcer.Name!.RawText.Should().Be("Abathur Announcer");
        announcer.SortName!.RawText.Should().Be("Abathur");
        announcer.HyperlinkId.Should().Be("AbathurAnnouncer");
        announcer.AttributeId.Should().Be("Abat");
        announcer.Franchise.Should().Be(Franchise.Starcraft);
        announcer.Rarity.Should().Be(Rarity.Legendary);
        announcer.ReleaseDate.Should().Be(new DateOnly(2014, 3, 13));
        announcer.Category.Should().Be("announcer");
        announcer.Event.Should().Be("no");
        announcer.Gender.Should().Be("Neutral");
        announcer.HeroId.Should().Be("Abathur");
        announcer.Image.Should().Be("storm_ui_announcer_abathur.png");
        announcer.Description!.RawText.Should().Be("The Evolution Master will guide you to victory.");
        announcer.SearchText!.RawText.Should().Be("Abathur Announcer Evolution Master Slug");
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        AnnouncerDataDocument announcerData = AnnouncerDataDocument.Load(jsonDocument);

        // act
        bool result = announcerData.TryGetElementById("other", out Announcer? announcer);

        // assert
        result.Should().BeFalse();
        announcer.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        AnnouncerDataDocument announcerData = AnnouncerDataDocument.Load(jsonDocument);

        // act
        Announcer announcer = announcerData.GetElementById("AlarakAnnouncer");

        // assert
        AlarakBasicAssertions(announcer);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        AnnouncerDataDocument announcerData = AnnouncerDataDocument.Load(jsonDocument);

        // act
        Action act = () => announcerData.GetElementById("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        AnnouncerDataDocument announcerData = AnnouncerDataDocument.Load(jsonDocument);

        // act
        bool result = announcerData.TryGetElementByHyperlinkId("AlarakAnnouncer(hyperlink)", out Announcer? announcer);

        // assert
        result.Should().BeTrue();
        announcer.Should().NotBeNull();

        AlarakBasicAssertions(announcer);
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        AnnouncerDataDocument announcerData = AnnouncerDataDocument.Load(jsonDocument);

        // act
        bool result = announcerData.TryGetElementByHyperlinkId("other", out Announcer? announcer);

        // assert
        result.Should().BeFalse();
        announcer.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByHyperlinkId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        AnnouncerDataDocument announcerData = AnnouncerDataDocument.Load(jsonDocument);

        // act
        Announcer announcer = announcerData.GetElementByHyperlinkId("AlarakAnnouncer(hyperlink)");

        // assert
        AlarakBasicAssertions(announcer);
    }

    [TestMethod]
    public void GetElementByHyperlinkId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        AnnouncerDataDocument announcerData = AnnouncerDataDocument.Load(jsonDocument);

        // act
        Action act = () => announcerData.GetElementByHyperlinkId("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByAttributeId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        AnnouncerDataDocument announcerData = AnnouncerDataDocument.Load(jsonDocument);

        // act
        bool result = announcerData.TryGetElementByAttributeId("Alar", out Announcer? announcer);

        // assert
        result.Should().BeTrue();
        announcer.Should().NotBeNull();

        AlarakBasicAssertions(announcer);
    }

    [TestMethod]
    public void TryGetElementByAttributeId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        AnnouncerDataDocument announcerData = AnnouncerDataDocument.Load(jsonDocument);

        // act
        bool result = announcerData.TryGetElementByAttributeId("other", out Announcer? announcer);

        // assert
        result.Should().BeFalse();
        announcer.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByAttributeId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        AnnouncerDataDocument announcerData = AnnouncerDataDocument.Load(jsonDocument);

        // act
        Announcer announcer = announcerData.GetElementByAttributeId("Alar");

        // assert
        AlarakBasicAssertions(announcer);
    }

    [TestMethod]
    public void GetElementByAttributeId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        AnnouncerDataDocument announcerData = AnnouncerDataDocument.Load(jsonDocument);

        // act
        Action act = () => announcerData.GetElementByAttributeId("other");

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
            "AlarakAnnouncer": {
              "name": "Alarak Announcer",
              "description": "A description",
              "searchText": "Search terms",
              "sortName": "Sort Alarak"
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
            "announcer": {
              "name": {
                "AlarakAnnouncer": "Alarak Announcer Localized"
              },
              "description": {
                "AlarakAnnouncer": "Localized Description"
              },
              "searchText": {
                "AlarakAnnouncer": "Localized Search Terms"
              },
              "sortName": {
                "AlarakAnnouncer": "Localized Sort Name"
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        AnnouncerDataDocument announcerData = AnnouncerDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        Announcer announcer = announcerData.GetElementById("AlarakAnnouncer");

        // assert
        announcer.Should().NotBeNull();
        announcer.Name!.RawText.Should().Be("Alarak Announcer Localized");
        announcer.Name.GameStringLocale.Should().Be(StormLocale.FRFR);
        announcer.Description!.RawText.Should().Be("Localized Description");
        announcer.Description.GameStringLocale.Should().Be(StormLocale.FRFR);
        announcer.SearchText!.RawText.Should().Be("Localized Search Terms");
        announcer.SearchText.GameStringLocale.Should().Be(StormLocale.FRFR);
        announcer.SortName!.RawText.Should().Be("Localized Sort Name");
        announcer.SortName.GameStringLocale.Should().Be(StormLocale.FRFR);
    }

    private static void AlarakBasicAssertions(Announcer announcer)
    {
        announcer.Id.Should().Be("AlarakAnnouncer");
        announcer.Name!.RawText.Should().Be("Alarak Announcer");
        announcer.HyperlinkId.Should().Be("AlarakAnnouncer(hyperlink)");
        announcer.AttributeId.Should().Be("Alar");
    }
}
