namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class VoiceLineSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        SerializerSettings serializerSettings = SerializerSettings.Create();

        VoiceLine voiceLine = new("id")
        {
            Name = new GameStringText("Test Voice Line"),
            Description = new GameStringText("Test Description"),
            SortName = new GameStringText("SortName"),
            HyperlinkId = "hyperlink_id",
            AttributeId = "VLID",
            Franchise = Franchise.Starcraft,
            Rarity = Rarity.Epic,
            ReleaseDate = new DateOnly(2024, 1, 1),
            Category = "voiceline",
            Event = "an event",
            SearchText = new GameStringText("item1 item2"),
            HeroId = "Abathur",
            Image = "test.png",
            InfoText = new GameStringText("Info text"),
        };

        // act
        string json = JsonSerializer.Serialize(voiceLine, serializerSettings.GetJsonSerializerDataOptions());

        // assert
        serializerSettings.ItemDictionary["voiceLine"].Should().HaveCount(5, "it's the total number of gamestringtext properties");

        json.Should().Be(
            """
            {
              "name": "Test Voice Line",
              "sortName": "SortName",
              "hyperlinkId": "hyperlink_id",
              "attributeId": "VLID",
              "franchise": "Starcraft",
              "rarity": "Epic",
              "releaseDate": "2024-01-01",
              "category": "voiceline",
              "event": "an event",
              "heroId": "Abathur",
              "image": "test.png",
              "searchText": "item1 item2",
              "description": "Test Description",
              "infoText": "Info text"
            }
            """);
    }
}
