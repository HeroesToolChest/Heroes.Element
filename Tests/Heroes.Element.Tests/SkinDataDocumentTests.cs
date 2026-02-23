namespace Heroes.Element.Tests;

[TestClass]
public class SkinDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {},
      "items": {
        "AbathurMechaVar1": {
          "name": "Mecha Abathur",
          "hyperlinkId": "AbathurMechaVar1",
          "attributeId": "MechaA"
        },
        "AbathurMechaVar2": {
          "name": "Mecha Abathur Blue",
          "hyperlinkId": "AbathurMechaVar2(hyperlink)",
          "attributeId": "MechaB"
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
        SkinDataDocument skinData = SkinDataDocument.Load(jsonDocument);

        // act
        bool result = skinData.TryGetElementById("other", out Skin? skin);

        // assert
        result.Should().BeFalse();
        skin.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_AbathurMechaVar1_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {},
          "items": {
            "AbathurMechaVar1": {
              "name": "Mecha Abathur",
              "sortName": "Abathur Mecha",
              "hyperlinkId": "AbathurMechaVar1",
              "attributeId": "MechaA",
              "franchise": "Nexus",
              "rarity": "Legendary",
              "releaseDate": "2018-05-22",
              "category": "skin",
              "event": "none",
              "description": "An alternate skin for the Evolution Master.",
              "searchText": "Mecha Abathur Robot Mechanical",
              "infoText": "Legendary Skin from the Mecha universe",
              "features": [
                "ThemedAbilities",
                "ThemedAnimations",
                "AlternateMountForm"
              ],
              "variationSkinIds": [
                "AbathurMechaVar2",
                "AbathurMechaVar3"
              ],
              "voiceLineIds": [
                "AbathurMecha_VoiceLine01",
                "AbathurMecha_VoiceLine02"
              ]
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SkinDataDocument skinData = SkinDataDocument.Load(jsonDocument);

        // act
        bool returnResult = skinData.TryGetElementById("AbathurMechaVar1", out Skin? skin);

        // assert
        returnResult.Should().BeTrue();
        skin.Should().NotBeNull();
        skin.Id.Should().Be("AbathurMechaVar1");
        skin.Name!.RawText.Should().Be("Mecha Abathur");
        skin.SortName!.RawText.Should().Be("Abathur Mecha");
        skin.HyperlinkId.Should().Be("AbathurMechaVar1");
        skin.AttributeId.Should().Be("MechaA");
        skin.Franchise.Should().Be(Franchise.Nexus);
        skin.Rarity.Should().Be(Rarity.Legendary);
        skin.ReleaseDate.Should().Be(new DateOnly(2018, 5, 22));
        skin.Category.Should().Be("skin");
        skin.Event.Should().Be("none");
        skin.Description!.RawText.Should().Be("An alternate skin for the Evolution Master.");
        skin.SearchText!.RawText.Should().Be("Mecha Abathur Robot Mechanical");
        skin.InfoText!.RawText.Should().Be("Legendary Skin from the Mecha universe");
        skin.Features.Should().HaveCount(3)
            .And.Contain("AlternateMountForm", "ThemedAbilities", "ThemedAnimations");
        skin.VariationSkinIds.Should().HaveCount(2)
            .And.Contain("AbathurMechaVar2", "AbathurMechaVar3");
        skin.VoiceLineIds.Should().HaveCount(2)
            .And.Contain("AbathurMecha_VoiceLine01", "AbathurMecha_VoiceLine02");
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SkinDataDocument skinData = SkinDataDocument.Load(jsonDocument);

        // act
        bool result = skinData.TryGetElementById("other", out Skin? skin);

        // assert
        result.Should().BeFalse();
        skin.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SkinDataDocument skinData = SkinDataDocument.Load(jsonDocument);

        // act
        Skin skin = skinData.GetElementById("AbathurMechaVar2");

        // assert
        MechaVar2BasicAssertions(skin);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SkinDataDocument skinData = SkinDataDocument.Load(jsonDocument);

        // act
        Action act = () => skinData.GetElementById("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SkinDataDocument skinData = SkinDataDocument.Load(jsonDocument);

        // act
        bool result = skinData.TryGetElementByHyperlinkId("AbathurMechaVar2(hyperlink)", out Skin? skin);

        // assert
        result.Should().BeTrue();
        skin.Should().NotBeNull();

        MechaVar2BasicAssertions(skin);
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SkinDataDocument skinData = SkinDataDocument.Load(jsonDocument);

        // act
        bool result = skinData.TryGetElementByHyperlinkId("other", out Skin? skin);

        // assert
        result.Should().BeFalse();
        skin.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByHyperlinkId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SkinDataDocument skinData = SkinDataDocument.Load(jsonDocument);

        // act
        Skin skin = skinData.GetElementByHyperlinkId("AbathurMechaVar2(hyperlink)");

        // assert
        MechaVar2BasicAssertions(skin);
    }

    [TestMethod]
    public void GetElementByHyperlinkId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SkinDataDocument skinData = SkinDataDocument.Load(jsonDocument);

        // act
        Action act = () => skinData.GetElementByHyperlinkId("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByAttributeId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SkinDataDocument skinData = SkinDataDocument.Load(jsonDocument);

        // act
        bool result = skinData.TryGetElementByAttributeId("MechaB", out Skin? skin);

        // assert
        result.Should().BeTrue();
        skin.Should().NotBeNull();

        MechaVar2BasicAssertions(skin);
    }

    [TestMethod]
    public void TryGetElementByAttributeId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SkinDataDocument skinData = SkinDataDocument.Load(jsonDocument);

        // act
        bool result = skinData.TryGetElementByAttributeId("other", out Skin? skin);

        // assert
        result.Should().BeFalse();
        skin.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByAttributeId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SkinDataDocument skinData = SkinDataDocument.Load(jsonDocument);

        // act
        Skin skin = skinData.GetElementByAttributeId("MechaB");

        // assert
        MechaVar2BasicAssertions(skin);
    }

    [TestMethod]
    public void GetElementByAttributeId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SkinDataDocument skinData = SkinDataDocument.Load(jsonDocument);

        // act
        Action act = () => skinData.GetElementByAttributeId("other");

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
            "AbathurMechaVar2": {
              "name": "Mecha Abathur Blue",
              "description": "A description",
              "searchText": "Search terms",
              "sortName": "Sort Mecha Blue",
              "infoText": "Info text"
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
            "skin": {
              "name": {
                "AbathurMechaVar2": "Mecha Abathur Blue Localized"
              },
              "description": {
                "AbathurMechaVar2": "Localized Description"
              },
              "searchText": {
                "AbathurMechaVar2": "Localized Search Terms"
              },
              "sortName": {
                "AbathurMechaVar2": "Localized Sort Name"
              },
              "infoText": {
                "AbathurMechaVar2": "Localized Info Text"
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        SkinDataDocument skinData = SkinDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        Skin skin = skinData.GetElementById("AbathurMechaVar2");

        // assert
        skin.Should().NotBeNull();
        skin.Name!.RawText.Should().Be("Mecha Abathur Blue Localized");
        skin.Name.GameStringLocale.Should().Be(StormLocale.FRFR);
        skin.Description!.RawText.Should().Be("Localized Description");
        skin.Description.GameStringLocale.Should().Be(StormLocale.FRFR);
        skin.SearchText!.RawText.Should().Be("Localized Search Terms");
        skin.SearchText.GameStringLocale.Should().Be(StormLocale.FRFR);
        skin.SortName!.RawText.Should().Be("Localized Sort Name");
        skin.SortName.GameStringLocale.Should().Be(StormLocale.FRFR);
        skin.InfoText!.RawText.Should().Be("Localized Info Text");
        skin.InfoText.GameStringLocale.Should().Be(StormLocale.FRFR);
    }

    [TestMethod]
    public void GetElements_WithItems_ReturnsAllElements()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        SkinDataDocument skinData = SkinDataDocument.Load(jsonDocument);

        // act
        List<Skin> result = [.. skinData.GetElements()];

        // assert
        result.Should().HaveCount(2);
        result.Should().Contain(s => s.Id == "AbathurMechaVar1");
        result.Should().Contain(s => s.Id == "AbathurMechaVar2");
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
        SkinDataDocument skinData = SkinDataDocument.Load(jsonDocument);

        // act
        List<Skin> result = [.. skinData.GetElements()];

        // assert
        result.Should().BeEmpty();
    }

    private static void MechaVar2BasicAssertions(Skin skin)
    {
        skin.Id.Should().Be("AbathurMechaVar2");
        skin.Name!.RawText.Should().Be("Mecha Abathur Blue");
        skin.HyperlinkId.Should().Be("AbathurMechaVar2(hyperlink)");
    }
}