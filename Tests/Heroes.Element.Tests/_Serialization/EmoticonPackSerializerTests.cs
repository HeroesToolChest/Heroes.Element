namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class EmoticonPackSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        SerializerSettings serializerSettings = SerializerSettings.Create();

        EmoticonPack emoticonPack = new("id")
        {
            Name = new GameStringText("Test Emoticon Pack"),
            Description = new GameStringText("Test Description"),
            SortName = new GameStringText("SortName"),
            HyperlinkId = "hyperlink_id",
            Franchise = Franchise.Starcraft,
            Rarity = Rarity.Rare,
            ReleaseDate = new DateOnly(2024, 1, 1),
            Category = "emoticonpack",
            Event = "an event",
            SearchText = new GameStringText("item1 item2"),
            InfoText = new GameStringText("Some info text"),
        };
        emoticonPack.EmoticonIds.Add("abathur_pack_1");
        emoticonPack.EmoticonIds.Add("abathur_pack_2");
        emoticonPack.EmoticonIds.Add("abathur_pack_3");

        // act
        string json = JsonSerializer.Serialize(emoticonPack, serializerSettings.GetJsonSerializerDataOptions());

        // assert
        serializerSettings.ItemDictionary["emoticonPack"].Should().HaveCount(5, "it's the total number of gamestringtext properties");

        json.Should().Be(
            """
            {
              "name": "Test Emoticon Pack",
              "sortName": "SortName",
              "hyperlinkId": "hyperlink_id",
              "franchise": "Starcraft",
              "rarity": "Rare",
              "releaseDate": "2024-01-01",
              "category": "emoticonpack",
              "event": "an event",
              "emoticonIds": [
                "abathur_pack_1",
                "abathur_pack_2",
                "abathur_pack_3"
              ],
              "searchText": "item1 item2",
              "description": "Test Description",
              "infoText": "Some info text"
            }
            """);
    }
}
