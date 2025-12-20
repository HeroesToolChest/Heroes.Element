namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class AnnouncerSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        Announcer announcer = new("id")
        {
            Name = new GameStringText("Test Announcer"),
            Description = new GameStringText("Test Description"),
            SortName = new GameStringText("SortName"),
            HyperlinkId = "hyperlink_id",
            Image = "test.png",
            Gender = "Male",
            Rarity = Rarity.Legendary,
            ReleaseDate = new DateOnly(2024, 1, 1),
            AttributeId = "ABDD",
            Category = "category",
            Event = "an event",
            Franchise = Franchise.Starcraft,
            HeroId = "AI",
            SearchText = new GameStringText("item1 item2"),
        };

        // act
        string json = JsonSerializer.Serialize(announcer, SerializerSettings.GetJsonSerializerDataOptions());

        // assert
        json.Should().Be(
            """
            {
              "name": "Test Announcer",
              "sortName": "SortName",
              "hyperlinkId": "hyperlink_id",
              "attributeId": "ABDD",
              "franchise": "Starcraft",
              "rarity": "Legendary",
              "releaseDate": "2024-01-01",
              "category": "category",
              "event": "an event",
              "gender": "Male",
              "heroId": "AI",
              "image": "test.png",
              "searchText": "item1 item2",
              "description": "Test Description"
            }
            """);
    }
}
