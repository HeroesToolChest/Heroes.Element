namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class PortraitPackSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        SerializerSettings serializerSettings = SerializerSettings.Create();

        PortraitPack portraitPack = new("id")
        {
            Name = new GameStringText("Test Portrait Pack"),
            Description = new GameStringText("Test Description"),
            SortName = new GameStringText("SortName"),
            HyperlinkId = "hyperlink_id",
            Franchise = Franchise.Starcraft,
            Rarity = Rarity.Epic,
            ReleaseDate = new DateOnly(2024, 1, 1),
            Category = "portraitpack",
            Event = "an event",
            SearchText = new GameStringText("item1 item2"),
        };
        portraitPack.RewardPortraitIds.Add("SCLegacyPortrait003");
        portraitPack.RewardPortraitIds.Add("SCLegacyPortrait001");
        portraitPack.RewardPortraitIds.Add("SCLegacyPortrait002");

        // act
        string json = JsonSerializer.Serialize(portraitPack, serializerSettings.GetJsonSerializerDataOptions());

        // assert
        serializerSettings.ItemDictionary["portraitPack"].Should().HaveCount(4, "it's the total number of gamestringtext properties");

        json.Should().Be(
            """
            {
              "name": "Test Portrait Pack",
              "sortName": "SortName",
              "hyperlinkId": "hyperlink_id",
              "franchise": "Starcraft",
              "rarity": "Epic",
              "releaseDate": "2024-01-01",
              "category": "portraitpack",
              "event": "an event",
              "rewardPortraitIds": [
                "SCLegacyPortrait001",
                "SCLegacyPortrait002",
                "SCLegacyPortrait003"
              ],
              "searchText": "item1 item2",
              "description": "Test Description"
            }
            """);
    }
}
