namespace Heroes.Element.Tests;

[TestClass]
public class MatchAwardDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {},
      "items": {
        "EndOfMatchAwardMVPBoolean": {
          "gameLink": "EndOfMatchAwardMVPBoolean",
          "tag": "MVP"
        },
        "EndOfMatchAwardMostXPContributionValue": {
          "gameLink": "EndOfMatchAwardMostXPContributionValue",
          "tag": "MostXP"
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
        MatchAwardDataDocument matchAwardData = MatchAwardDataDocument.Load(jsonDocument);

        // act
        bool result = matchAwardData.TryGetElementById("other", out MatchAward? matchAward);

        // assert
        result.Should().BeFalse();
        matchAward.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_EndOfMatchAwardMVPBoolean_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {},
          "items": {
            "EndOfMatchAwardMVPBoolean": {
              "gameLink": "EndOfMatchAwardMVPBoolean",
              "tag": "MVP",
              "scoreScreenName": "Most Valuable Player",
              "scoreScreenDescription": "Awarded to the player with the highest overall performance.",
              "endOfMatchName": "MVP",
              "endOfMatchDescription": "Most Valuable Player",
              "endOfMatchTooltipText": "You were the most valuable player in this match!",
              "mvpScreenIcon": "storm_ui_mvp_icons_mvp.png",
              "scoreScreenIcon": "storm_ui_scorescreen_mvp_icon.png"
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MatchAwardDataDocument matchAwardData = MatchAwardDataDocument.Load(jsonDocument);

        // act
        bool returnResult = matchAwardData.TryGetElementById("EndOfMatchAwardMVPBoolean", out MatchAward? matchAward);

        // assert
        returnResult.Should().BeTrue();
        matchAward.Should().NotBeNull();
        matchAward.Id.Should().Be("EndOfMatchAwardMVPBoolean");
        matchAward.GameLink.Should().Be("EndOfMatchAwardMVPBoolean");
        matchAward.Tag.Should().Be("MVP");
        matchAward.ScoreScreenName!.RawText.Should().Be("Most Valuable Player");
        matchAward.ScoreScreenDescription!.RawText.Should().Be("Awarded to the player with the highest overall performance.");
        matchAward.EndOfMatchName!.RawText.Should().Be("MVP");
        matchAward.EndOfMatchDescription!.RawText.Should().Be("Most Valuable Player");
        matchAward.EndOfMatchTooltipText!.RawText.Should().Be("You were the most valuable player in this match!");
        matchAward.MVPScreenImage.Should().Be("storm_ui_mvp_icons_mvp.png");
        matchAward.ScoreScreenImage.Should().Be("storm_ui_scorescreen_mvp_icon.png");
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MatchAwardDataDocument matchAwardData = MatchAwardDataDocument.Load(jsonDocument);

        // act
        bool result = matchAwardData.TryGetElementById("other", out MatchAward? matchAward);

        // assert
        result.Should().BeFalse();
        matchAward.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MatchAwardDataDocument matchAwardData = MatchAwardDataDocument.Load(jsonDocument);

        // act
        MatchAward matchAward = matchAwardData.GetElementById("EndOfMatchAwardMostXPContributionValue");

        // assert
        MostXPBasicAssertions(matchAward);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MatchAwardDataDocument matchAwardData = MatchAwardDataDocument.Load(jsonDocument);

        // act
        Action act = () => matchAwardData.GetElementById("other");

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
            "EndOfMatchAwardMostXPContributionValue": {
              "gameLink": "EndOfMatchAwardMostXPContributionValue",
              "tag": "MostXP",
              "scoreScreenName": "Top XP Contributor",
              "scoreScreenDescription": "Awarded for contributing the most experience.",
              "endOfMatchName": "XP Leader",
              "endOfMatchDescription": "Top XP Contributor",
              "endOfMatchTooltipText": "You contributed the most experience!"
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
            "matchAward": {
              "scoreScreenName": {
                "EndOfMatchAwardMostXPContributionValue": "Top XP Contributor Localized"
              },
              "scoreScreenDescription": {
                "EndOfMatchAwardMostXPContributionValue": "Localized Score Screen Description"
              },
              "endOfMatchName": {
                "EndOfMatchAwardMostXPContributionValue": "XP Leader Localized"
              },
              "endOfMatchDescription": {
                "EndOfMatchAwardMostXPContributionValue": "Localized End Of Match Description"
              },
              "endOfMatchTooltipText": {
                "EndOfMatchAwardMostXPContributionValue": "Localized Tooltip Text"
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        MatchAwardDataDocument matchAwardData = MatchAwardDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        MatchAward matchAward = matchAwardData.GetElementById("EndOfMatchAwardMostXPContributionValue");

        // assert
        matchAward.Should().NotBeNull();
        matchAward.ScoreScreenName!.RawText.Should().Be("Top XP Contributor Localized");
        matchAward.ScoreScreenName.GameStringLocale.Should().Be(StormLocale.FRFR);
        matchAward.ScoreScreenDescription!.RawText.Should().Be("Localized Score Screen Description");
        matchAward.ScoreScreenDescription.GameStringLocale.Should().Be(StormLocale.FRFR);
        matchAward.EndOfMatchName!.RawText.Should().Be("XP Leader Localized");
        matchAward.EndOfMatchName.GameStringLocale.Should().Be(StormLocale.FRFR);
        matchAward.EndOfMatchDescription!.RawText.Should().Be("Localized End Of Match Description");
        matchAward.EndOfMatchDescription.GameStringLocale.Should().Be(StormLocale.FRFR);
        matchAward.EndOfMatchTooltipText!.RawText.Should().Be("Localized Tooltip Text");
        matchAward.EndOfMatchTooltipText.GameStringLocale.Should().Be(StormLocale.FRFR);
    }

    [TestMethod]
    public void GetElements_WithItems_ReturnsAllElements()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        MatchAwardDataDocument matchAwardData = MatchAwardDataDocument.Load(jsonDocument);

        // act
        List<MatchAward> result = [.. matchAwardData.GetElements()];

        // assert
        result.Should().HaveCount(2);
        result.Should().Contain(m => m.Id == "EndOfMatchAwardMVPBoolean");
        result.Should().Contain(m => m.Id == "EndOfMatchAwardMostXPContributionValue");
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
        MatchAwardDataDocument matchAwardData = MatchAwardDataDocument.Load(jsonDocument);

        // act
        List<MatchAward> result = [.. matchAwardData.GetElements()];

        // assert
        result.Should().BeEmpty();
    }

    private static void MostXPBasicAssertions(MatchAward matchAward)
    {
        matchAward.Id.Should().Be("EndOfMatchAwardMostXPContributionValue");
        matchAward.GameLink.Should().Be("EndOfMatchAwardMostXPContributionValue");
        matchAward.Tag.Should().Be("MostXP");
    }
}