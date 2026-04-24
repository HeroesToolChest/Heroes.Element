namespace Heroes.Element.Tests;

[TestClass]
public class VoiceLineDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {
        "itemsType": "Data",
        "dataType": "VoiceLineData"
      },
      "items": {
        "AbathurBase_VoiceLine01": {
          "name": "Acceptable",
          "hyperlinkId": "AbathurBase_VoiceLine01",
          "attributeId": "Abat01"
        },
        "AbathurBase_VoiceLine02": {
          "name": "Evolution Complete",
          "hyperlinkId": "AbathurBase_VoiceLine02(hyperlink)",
          "attributeId": "Abat02"
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
            "dataType": "VoiceLineData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        bool result = voiceLineData.TryGetElementById("other", out VoiceLine? voiceLine);

        // assert
        result.Should().BeFalse();
        voiceLine.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_AbathurBase_VoiceLine01_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "VoiceLineData"
          },
          "items": {
            "AbathurBase_VoiceLine01": {
              "name": "Acceptable",
              "sortName": "Acceptable Abathur",
              "hyperlinkId": "AbathurBase_VoiceLine01",
              "attributeId": "Abat01",
              "franchise": "Starcraft",
              "rarity": "Common",
              "releaseDate": "2014-03-13",
              "category": "voiceline",
              "event": "no",
              "heroId": "Abathur",
              "image": "storm_ui_voice_abathur_acceptable.png",
              "description": "Voice line from the Evolution Master.",
              "searchText": "Abathur Acceptable Voice Line Slug"
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        bool returnResult = voiceLineData.TryGetElementById("AbathurBase_VoiceLine01", out VoiceLine? voiceLine);

        // assert
        returnResult.Should().BeTrue();
        voiceLine.Should().NotBeNull();
        voiceLine.Id.Should().Be("AbathurBase_VoiceLine01");
        voiceLine.Name!.RawText.Should().Be("Acceptable");
        voiceLine.SortName!.RawText.Should().Be("Acceptable Abathur");
        voiceLine.HyperlinkId.Should().Be("AbathurBase_VoiceLine01");
        voiceLine.AttributeId.Should().Be("Abat01");
        voiceLine.Franchise.Should().Be(Franchise.Starcraft);
        voiceLine.Rarity.Should().Be(Rarity.Common);
        voiceLine.ReleaseDate.Should().Be(new DateOnly(2014, 3, 13));
        voiceLine.Category.Should().Be("voiceline");
        voiceLine.Event.Should().Be("no");
        voiceLine.HeroId.Should().Be("Abathur");
        voiceLine.Image.Should().Be("storm_ui_voice_abathur_acceptable.png");
        voiceLine.Description!.RawText.Should().Be("Voice line from the Evolution Master.");
        voiceLine.SearchText!.RawText.Should().Be("Abathur Acceptable Voice Line Slug");
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        bool result = voiceLineData.TryGetElementById("other", out VoiceLine? voiceLine);

        // assert
        result.Should().BeFalse();
        voiceLine.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        VoiceLine voiceLine = voiceLineData.GetElementById("AbathurBase_VoiceLine02");

        // assert
        VoiceLine02BasicAssertions(voiceLine);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        Action act = () => voiceLineData.GetElementById("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        bool result = voiceLineData.TryGetElementByHyperlinkId("AbathurBase_VoiceLine02(hyperlink)", out VoiceLine? voiceLine);

        // assert
        result.Should().BeTrue();
        voiceLine.Should().NotBeNull();

        VoiceLine02BasicAssertions(voiceLine);
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        bool result = voiceLineData.TryGetElementByHyperlinkId("other", out VoiceLine? voiceLine);

        // assert
        result.Should().BeFalse();
        voiceLine.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByHyperlinkId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        VoiceLine voiceLine = voiceLineData.GetElementByHyperlinkId("AbathurBase_VoiceLine02(hyperlink)");

        // assert
        VoiceLine02BasicAssertions(voiceLine);
    }

    [TestMethod]
    public void GetElementByHyperlinkId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        Action act = () => voiceLineData.GetElementByHyperlinkId("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByAttributeId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        bool result = voiceLineData.TryGetElementByAttributeId("Abat02", out VoiceLine? voiceLine);

        // assert
        result.Should().BeTrue();
        voiceLine.Should().NotBeNull();

        VoiceLine02BasicAssertions(voiceLine);
    }

    [TestMethod]
    public void TryGetElementByAttributeId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        bool result = voiceLineData.TryGetElementByAttributeId("other", out VoiceLine? voiceLine);

        // assert
        result.Should().BeFalse();
        voiceLine.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByAttributeId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        VoiceLine voiceLine = voiceLineData.GetElementByAttributeId("Abat02");

        // assert
        VoiceLine02BasicAssertions(voiceLine);
    }

    [TestMethod]
    public void GetElementByAttributeId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        Action act = () => voiceLineData.GetElementByAttributeId("other");

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
            "dataType": "VoiceLineData"
          },
          "items": {
            "AbathurBase_VoiceLine02": {
              "name": "Evolution Complete",
              "description": "A description",
              "searchText": "Search terms",
              "sortName": "Sort Evolution"
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
            "voiceLine": {
              "name": {
                "AbathurBase_VoiceLine02": "Evolution Complete Localized"
              },
              "description": {
                "AbathurBase_VoiceLine02": "Localized Description"
              },
              "searchText": {
                "AbathurBase_VoiceLine02": "Localized Search Terms"
              },
              "sortName": {
                "AbathurBase_VoiceLine02": "Localized Sort Name"
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        VoiceLine voiceLine = voiceLineData.GetElementById("AbathurBase_VoiceLine02");

        // assert
        voiceLine.Should().NotBeNull();
        voiceLine.Name!.RawText.Should().Be("Evolution Complete Localized");
        voiceLine.Name.GameStringLocale.Should().Be(StormLocale.FRFR);
        voiceLine.Description!.RawText.Should().Be("Localized Description");
        voiceLine.Description.GameStringLocale.Should().Be(StormLocale.FRFR);
        voiceLine.SearchText!.RawText.Should().Be("Localized Search Terms");
        voiceLine.SearchText.GameStringLocale.Should().Be(StormLocale.FRFR);
        voiceLine.SortName!.RawText.Should().Be("Localized Sort Name");
        voiceLine.SortName.GameStringLocale.Should().Be(StormLocale.FRFR);
    }

    [TestMethod]
    public void GetElements_WithItems_ReturnsAllElements()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        List<VoiceLine> result = [.. voiceLineData.GetElements()];

        // assert
        result.Should().HaveCount(2);
        result.Should().Contain(v => v.Id == "AbathurBase_VoiceLine01");
        result.Should().Contain(v => v.Id == "AbathurBase_VoiceLine02");
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
            "dataType": "VoiceLineData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        List<VoiceLine> result = [.. voiceLineData.GetElements()];

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
        Action act = () => VoiceLineDataDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<JsonException>().WithMessage("*does not match the expected data type*");
    }

    [TestMethod]
    public void GetElementObjects_WithItems_ReturnsAllElementsAsObjects()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        List<object> result = [.. voiceLineData.GetElementObjects()];

        // assert
        result.Should().HaveCount(2);
        result.Should().AllBeOfType<VoiceLine>();
        result.OfType<VoiceLine>().Should().Contain(v => v.Id == "AbathurBase_VoiceLine01");
        result.OfType<VoiceLine>().Should().Contain(v => v.Id == "AbathurBase_VoiceLine02");
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
            "dataType": "VoiceLineData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        List<object> result = [.. voiceLineData.GetElementObjects()];

        // assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void GetElementObjects_WithItems_ReturnsObjectsWithCorrectProperties()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VoiceLineDataDocument voiceLineData = VoiceLineDataDocument.Load(jsonDocument);

        // act
        List<object> result = [.. voiceLineData.GetElementObjects()];

        // assert
        result.Should().HaveCount(2);

        VoiceLine voiceLine02 = result.OfType<VoiceLine>().First(v => v.Id == "AbathurBase_VoiceLine02");
        VoiceLine02BasicAssertions(voiceLine02);
    }

    private static void VoiceLine02BasicAssertions(VoiceLine voiceLine)
    {
        voiceLine.Id.Should().Be("AbathurBase_VoiceLine02");
        voiceLine.Name!.RawText.Should().Be("Evolution Complete");
        voiceLine.HyperlinkId.Should().Be("AbathurBase_VoiceLine02(hyperlink)");
        voiceLine.AttributeId.Should().Be("Abat02");
    }
}