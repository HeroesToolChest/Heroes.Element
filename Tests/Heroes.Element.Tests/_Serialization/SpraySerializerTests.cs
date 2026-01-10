namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class SpraySerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        SerializerSettings serializerSettings = SerializerSettings.Create();

        Spray spray = new("id")
        {
            Name = new GameStringText("Test Spray"),
            Description = new GameStringText("Test Description"),
            SortName = new GameStringText("SortName"),
            HyperlinkId = "hyperlink_id",
            AttributeId = "SPRY",
            Franchise = Franchise.Overwatch,
            Rarity = Rarity.Rare,
            ReleaseDate = new DateOnly(2024, 1, 1),
            Category = "spray",
            Event = "an event",
            SearchText = new GameStringText("item1 item2"),
            Image = "test.png",
            Animation = new SprayAnimation
            {
                Texture = "test_animation.png",
                Frames = 30,
                Duration = 1000,
            },
        };

        // act
        string json = JsonSerializer.Serialize(spray, serializerSettings.GetJsonSerializerDataOptions());

        // assert
        serializerSettings.ItemDictionary["spray"].Should().HaveCount(4, "it's the total number of gamestringtext properties");

        json.Should().Be(
            """
            {
              "name": "Test Spray",
              "sortName": "SortName",
              "hyperlinkId": "hyperlink_id",
              "attributeId": "SPRY",
              "franchise": "Overwatch",
              "rarity": "Rare",
              "releaseDate": "2024-01-01",
              "category": "spray",
              "event": "an event",
              "image": "test.png",
              "animation": {
                "texture": "test_animation.png",
                "frames": 30,
                "duration": 1000
              },
              "searchText": "item1 item2",
              "description": "Test Description"
            }
            """);
    }
}
