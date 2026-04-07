namespace Heroes.Element.Tests;

[TestClass]
public class EmoticonDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {
        "itemsType": "Data",
        "dataType": "EmoticonData"
      },
      "items": {
        "abathur_pack_1": {
          "expression": ":abathur:"
        },
        "abathur_pack_2": {
          "expression": ":slug:"
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
            "dataType": "EmoticonData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonDataDocument emoticonData = EmoticonDataDocument.Load(jsonDocument);

        // act
        bool result = emoticonData.TryGetElementById("other", out Emoticon? emoticon);

        // assert
        result.Should().BeFalse();
        emoticon.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_AbathurPack1_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "EmoticonData"
          },
          "items": {
            "abathur_pack_1": {
              "expression": ":abathur:",
              "description": "Emoticon featuring Abathur, the Evolution Master.",
              "universalAliases": [
                ":aba:",
                ":evolutionmaster:"
              ],
              "localizedAliases": [
                "abathur",
                "slug"
              ],
              "searchText": "Abathur Evolution Master Slug Emoticon",
              "isCaseSensitive": false,
              "isHidden": false,
              "heroId": "Abathur",
              "skinId": "AbathurBase",
              "image": "storm_emoji_abathur.png",
              "animation": {
                "texture": "storm_emoji_abathur_animation.png",
                "frames": 16,
                "duration": 1500,
                "width": 52
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonDataDocument emoticonData = EmoticonDataDocument.Load(jsonDocument);

        // act
        bool returnResult = emoticonData.TryGetElementById("abathur_pack_1", out Emoticon? emoticon);

        // assert
        returnResult.Should().BeTrue();
        emoticon.Should().NotBeNull();
        emoticon.Id.Should().Be("abathur_pack_1");
        emoticon.Expression.Should().Be(":abathur:");
        emoticon.Description!.RawText.Should().Be("Emoticon featuring Abathur, the Evolution Master.");
        emoticon.UniversalAliases.Should().HaveCount(2);
        emoticon.UniversalAliases.Should().Contain(":aba:");
        emoticon.UniversalAliases.Should().Contain(":evolutionmaster:");
        emoticon.LocalizedAliases.Should().HaveCount(2);
        emoticon.SearchText!.RawText.Should().Be("Abathur Evolution Master Slug Emoticon");
        emoticon.IsCaseSensitive.Should().BeFalse();
        emoticon.IsHidden.Should().BeFalse();
        emoticon.HeroId.Should().Be("Abathur");
        emoticon.SkinId.Should().Be("AbathurBase");
        emoticon.Image.Should().Be("storm_emoji_abathur.png");
        emoticon.Animation.Should().NotBeNull();
        emoticon.Animation!.Texture.Should().Be("storm_emoji_abathur_animation.png");
        emoticon.Animation.Frames.Should().Be(16);
        emoticon.Animation.Duration.Should().Be(1500);
        emoticon.Animation.Width.Should().Be(52);
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonDataDocument emoticonData = EmoticonDataDocument.Load(jsonDocument);

        // act
        bool result = emoticonData.TryGetElementById("other", out Emoticon? emoticon);

        // assert
        result.Should().BeFalse();
        emoticon.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonDataDocument emoticonData = EmoticonDataDocument.Load(jsonDocument);

        // act
        Emoticon emoticon = emoticonData.GetElementById("abathur_pack_2");

        // assert
        SlugBasicAssertions(emoticon);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonDataDocument emoticonData = EmoticonDataDocument.Load(jsonDocument);

        // act
        Action act = () => emoticonData.GetElementById("other");

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
            "dataType": "EmoticonData"
          },
          "items": {
            "abathur_pack_2": {
              "expression": ":slug:",
              "description": "A description",
              "searchText": "Search terms",
              "localizedAliases": [
                "temp1",
                "temp2"
              ]
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
            "emoticon": {
              "description": {
                "abathur_pack_2": "Localized Description"
              },
              "searchText": {
                "abathur_pack_2": "Localized Search Terms"
              },
              "localizedAliases": {
                "abathur_pack_2": [
                  "localized alias 1",
                  "localized alias 2"
                ]
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        EmoticonDataDocument emoticonData = EmoticonDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        Emoticon emoticon = emoticonData.GetElementById("abathur_pack_2");

        // assert
        emoticon.Should().NotBeNull();
        emoticon.Description!.RawText.Should().Be("Localized Description");
        emoticon.Description.GameStringLocale.Should().Be(StormLocale.FRFR);
        emoticon.SearchText!.RawText.Should().Be("Localized Search Terms");
        emoticon.SearchText.GameStringLocale.Should().Be(StormLocale.FRFR);
        emoticon.LocalizedAliases.Should().HaveCount(2);
        emoticon.LocalizedAliases.ElementAt(0).RawText.Should().Be("localized alias 1");
        emoticon.LocalizedAliases.ElementAt(0).GameStringLocale.Should().Be(StormLocale.FRFR);
        emoticon.LocalizedAliases.ElementAt(1).RawText.Should().Be("localized alias 2");
        emoticon.LocalizedAliases.ElementAt(1).GameStringLocale.Should().Be(StormLocale.FRFR);
    }

    [TestMethod]
    public void GetElements_WithItems_ReturnsAllElements()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonDataDocument emoticonData = EmoticonDataDocument.Load(jsonDocument);

        // act
        List<Emoticon> result = [.. emoticonData.GetElements()];

        // assert
        result.Should().HaveCount(2);
        result.Should().Contain(e => e.Id == "abathur_pack_1");
        result.Should().Contain(e => e.Id == "abathur_pack_2");
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
            "dataType": "EmoticonData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        EmoticonDataDocument emoticonData = EmoticonDataDocument.Load(jsonDocument);

        // act
        List<Emoticon> result = [.. emoticonData.GetElements()];

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
        Action act = () => EmoticonDataDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<JsonException>().WithMessage("*does not match the expected data type*");
    }

    private static void SlugBasicAssertions(Emoticon emoticon)
    {
        emoticon.Id.Should().Be("abathur_pack_2");
        emoticon.Expression.Should().Be(":slug:");
    }
}
