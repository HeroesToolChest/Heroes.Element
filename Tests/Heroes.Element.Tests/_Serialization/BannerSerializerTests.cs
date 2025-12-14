namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class BannerSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        Banner banner = new("banner_id")
        {
            // LoadoutItem properties (inherited)
            AttributeId = "BANN",

            // StoreItem properties (inherited)
            Name = new GameStringText("Banner Name"),
            SortName = new GameStringText("Sort Name"),
            HyperlinkId = "hyperlink_id",
            Franchise = Franchise.Overwatch,
            Rarity = Rarity.Rare,
            ReleaseDate = new DateOnly(2024, 11, 20),
            Category = "Banner Category",
            Event = "Seasonal Event",
            SearchText = new GameStringText("banner search keywords"),
            Description = new GameStringText("Banner Description Text"),
        };

        // act
        string json = JsonSerializer.Serialize(banner, SerializerSettings.SetJsonSerializerDataOptions());

        // assert
        json.Should().Be(
            """
            {
              "name": "Banner Name",
              "sortName": "Sort Name",
              "hyperlinkId": "hyperlink_id",
              "attributeId": "BANN",
              "franchise": "Overwatch",
              "rarity": "Rare",
              "releaseDate": "2024-11-20",
              "category": "Banner Category",
              "event": "Seasonal Event",
              "searchText": "banner search keywords",
              "description": "Banner Description Text"
            }
            """);
    }
}
