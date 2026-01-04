namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class MatchAwardSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        SerializerSettings serializerSettings = SerializerSettings.Create();

        MatchAward matchAward = new("id")
        {
            GameLink = "EndOfMatchAwardMVPBoolean",
            Tag = "MVP",
            ScoreScreenName = new GameStringText("Most Valuable Player"),
            ScoreScreenDescription = new GameStringText("Awarded to the player with the highest overall performance."),
            EndOfMatchName = new GameStringText("MVP"),
            EndOfMatchDescription = new GameStringText("Most Valuable Player"),
            EndOfMatchTooltipText = new GameStringText("You were the most valuable player in this match!"),
            MVPScreenImage = "storm_ui_mvp_icons_mvp.png",
            ScoreScreenImage = "storm_ui_scorescreen_mvp_icon.png",
        };

        // act
        string json = JsonSerializer.Serialize(matchAward, serializerSettings.GetJsonSerializerDataOptions());

        // assert
        serializerSettings.ItemDictionary["matchAward"].Should().HaveCount(5, "it's the total number of gamestringtext properties");

        json.Should().Be(
            """
            {
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
            """);
    }
}
