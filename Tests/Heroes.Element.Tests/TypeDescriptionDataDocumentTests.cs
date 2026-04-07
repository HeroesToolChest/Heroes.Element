namespace Heroes.Element.Tests;

[TestClass]
public class TypeDescriptionDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {
        "itemsType": "Data",
        "dataType": "TypeDescriptionData"
      },
      "items": {
        "TypeDescriptionHero": {
          "name": "Hero"
        },
        "TypeDescriptionSkin": {
          "name": "Skin"
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
            "dataType": "TypeDescriptionData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        TypeDescriptionDataDocument typeDescriptionData = TypeDescriptionDataDocument.Load(jsonDocument);

        // act
        bool result = typeDescriptionData.TryGetElementById("other", out TypeDescription? typeDescription);

        // assert
        result.Should().BeFalse();
        typeDescription.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_TypeDescriptionHero_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "TypeDescriptionData"
          },
          "items": {
            "TypeDescriptionHero": {
              "name": "Hero",
              "rewardIcon": "storm_ui_reward_icon_hero.png",
              "largeIcon": "storm_ui_large_icon_hero.png"
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        TypeDescriptionDataDocument typeDescriptionData = TypeDescriptionDataDocument.Load(jsonDocument);

        // act
        bool returnResult = typeDescriptionData.TryGetElementById("TypeDescriptionHero", out TypeDescription? typeDescription);

        // assert
        returnResult.Should().BeTrue();
        typeDescription.Should().NotBeNull();
        typeDescription.Id.Should().Be("TypeDescriptionHero");
        typeDescription.Name!.RawText.Should().Be("Hero");
        typeDescription.RewardIcon.Should().Be("storm_ui_reward_icon_hero.png");
        typeDescription.LargeIcon.Should().Be("storm_ui_large_icon_hero.png");
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        TypeDescriptionDataDocument typeDescriptionData = TypeDescriptionDataDocument.Load(jsonDocument);

        // act
        bool result = typeDescriptionData.TryGetElementById("other", out TypeDescription? typeDescription);

        // assert
        result.Should().BeFalse();
        typeDescription.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        TypeDescriptionDataDocument typeDescriptionData = TypeDescriptionDataDocument.Load(jsonDocument);

        // act
        TypeDescription typeDescription = typeDescriptionData.GetElementById("TypeDescriptionSkin");

        // assert
        SkinBasicAssertions(typeDescription);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        TypeDescriptionDataDocument typeDescriptionData = TypeDescriptionDataDocument.Load(jsonDocument);

        // act
        Action act = () => typeDescriptionData.GetElementById("other");

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
            "dataType": "TypeDescriptionData"
          },
          "items": {
            "TypeDescriptionSkin": {
              "name": "Skin"
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
            "typeDescription": {
              "name": {
                "TypeDescriptionSkin": "Skin Localized"
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TypeDescriptionDataDocument typeDescriptionData = TypeDescriptionDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        TypeDescription typeDescription = typeDescriptionData.GetElementById("TypeDescriptionSkin");

        // assert
        typeDescription.Should().NotBeNull();
        typeDescription.Name!.RawText.Should().Be("Skin Localized");
        typeDescription.Name.GameStringLocale.Should().Be(StormLocale.FRFR);
    }

    [TestMethod]
    public void GetElements_WithItems_ReturnsAllElements()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        TypeDescriptionDataDocument typeDescriptionData = TypeDescriptionDataDocument.Load(jsonDocument);

        // act
        List<TypeDescription> result = [.. typeDescriptionData.GetElements()];

        // assert
        result.Should().HaveCount(2);
        result.Should().Contain(t => t.Id == "TypeDescriptionHero");
        result.Should().Contain(t => t.Id == "TypeDescriptionSkin");
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
            "dataType": "TypeDescriptionData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        TypeDescriptionDataDocument typeDescriptionData = TypeDescriptionDataDocument.Load(jsonDocument);

        // act
        List<TypeDescription> result = [.. typeDescriptionData.GetElements()];

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
        Action act = () => TypeDescriptionDataDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<JsonException>().WithMessage("*does not match the expected data type*");
    }

    private static void SkinBasicAssertions(TypeDescription typeDescription)
    {
        typeDescription.Id.Should().Be("TypeDescriptionSkin");
        typeDescription.Name!.RawText.Should().Be("Skin");
    }
}
