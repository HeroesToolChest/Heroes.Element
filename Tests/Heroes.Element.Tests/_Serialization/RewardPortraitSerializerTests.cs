namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class RewardPortraitSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        SerializerSettings serializerSettings = SerializerSettings.Create();

        RewardPortrait rewardPortrait = new("id")
        {
            Name = new GameStringText("Test Reward Portrait"),
            Description = new GameStringText("Test Description"),
            SortName = new GameStringText("SortName"),
            HyperlinkId = "hyperlink_id",
            Franchise = Franchise.Starcraft,
            Rarity = Rarity.Legendary,
            ReleaseDate = new DateOnly(2024, 1, 1),
            Category = "rewardportrait",
            Event = "an event",
            SearchText = new GameStringText("item1 item2"),
            DescriptionUnearned = new GameStringText("Unearned Description"),
            PortraitPackId = "PortraitPackStarcraftLegacy1",
            HeroId = "Raynor",
            IconSlot = 5,
            Image = "test.png",
            TextureSheet = new TextureSheet
            {
                Image = "ui_heroes_portraits_sheet0.png",
                Columns = 7,
                Rows = 8,
            },
        };

        // act
        string json = JsonSerializer.Serialize(rewardPortrait, serializerSettings.GetJsonSerializerDataOptions());

        // assert
        serializerSettings.ItemDictionary["rewardPortrait"].Should().HaveCount(5, "it's the total number of gamestringtext properties");

        json.Should().Be(
            """
            {
              "name": "Test Reward Portrait",
              "sortName": "SortName",
              "hyperlinkId": "hyperlink_id",
              "franchise": "Starcraft",
              "rarity": "Legendary",
              "releaseDate": "2024-01-01",
              "category": "rewardportrait",
              "event": "an event",
              "portraitPackId": "PortraitPackStarcraftLegacy1",
              "heroId": "Raynor",
              "iconSlot": 5,
              "image": "test.png",
              "textureSheet": {
                "image": "ui_heroes_portraits_sheet0.png",
                "rows": 8,
                "columns": 7
              },
              "searchText": "item1 item2",
              "description": "Test Description",
              "descriptionUnearned": "Unearned Description"
            }
            """);
    }
}
