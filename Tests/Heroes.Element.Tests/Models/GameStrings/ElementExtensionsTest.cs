namespace Heroes.Element.Models.GameStrings.Tests;

[TestClass]
public class ElementExtensionsTest
{
    [TestMethod]
    public void UpdateGameStringTexts_Hero_UpdatesGameStringTexts()
    {
        // arrange
        Hero hero = new("heroId1")
        {
            Description = new GameStringText("a description"),
        };

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
            "hero": {
              "description": {
                "heroId1": "updated description"
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        hero.UpdateGameStringTexts(gameStringDocument);

        // assert
        hero.Description.RawText.Should().Be("updated description");
    }

    [TestMethod]
    public void UpdateGameStringTexts_Unit_UpdatesGameStringTexts()
    {
        // arrange
        Unit unit = new("unitId1")
        {
            Description = new GameStringText("a description"),
        };

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
            "unit": {
              "description": {
                "unitId1": "updated description"
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        unit.UpdateGameStringTexts(gameStringDocument);

        // assert
        unit.Description.RawText.Should().Be("updated description");
    }

    [TestMethod]
    public void UpdateGameStringTexts_Announcer_UpdatesGameStringTexts()
    {
        // arrange
        Announcer announcer = new("announcerId1")
        {
            Description = new GameStringText("a description"),
        };

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
            "announcer": {
              "description": {
                "announcerId1": "updated description"
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(jsonDocument);

        // act
        announcer.UpdateGameStringTexts(gameStringDocument);

        // assert
        announcer.Description.RawText.Should().Be("updated description");
    }
}
