namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class BoostSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        Boost boost = new("boost_id")
        {
            // StoreItem properties (inherited)
            Name = new GameStringText("Boost Name"),
            SortName = new GameStringText("Sort Name"),
            HyperlinkId = "hyperlink_id",
            Franchise = Franchise.Nexus,
            Rarity = Rarity.Epic,
            ReleaseDate = new DateOnly(2024, 10, 15),
            Category = "Boost Category",
            Event = "Anniversary Event",
            SearchText = new GameStringText("boost search keywords"),
            Description = new GameStringText("Boost Description Text"),
        };

        // act
        string json = JsonSerializer.Serialize(boost, SerializerSettings.SetJsonSerializerDataOptions());

        // assert
        json.Should().Be(
            """
            {
              "name": "Boost Name",
              "sortName": "Sort Name",
              "hyperlinkId": "hyperlink_id",
              "franchise": "Nexus",
              "rarity": "Epic",
              "releaseDate": "2024-10-15",
              "category": "Boost Category",
              "event": "Anniversary Event",
              "searchText": "boost search keywords",
              "description": "Boost Description Text"
            }
            """);
    }
}
