namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class MountSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        Mount mount = new("id")
        {
            Name = new GameStringText("Cloud Serpent"),
            Description = new GameStringText("A mystical serpent from the clouds of Pandaria."),
            SortName = new GameStringText("Serpent Cloud"),
            HyperlinkId = "hyperlink_id",
            AttributeId = "CSer",
            Franchise = Franchise.Warcraft,
            Rarity = Rarity.Epic,
            ReleaseDate = new DateOnly(2014, 3, 13),
            Category = "mount",
            Event = "an event",
            SearchText = new GameStringText("Cloud Serpent Mount Flying Dragon"),
            MountCategory = "Flying",
        };
        mount.VariationMountIds.Add("CloudSerpentMountVar1");
        mount.VariationMountIds.Add("CloudSerpentMountVar2");

        // act
        string json = JsonSerializer.Serialize(mount, SerializerSettings.GetJsonSerializerDataOptions());

        // assert
        json.Should().Be(
            """
            {
              "name": "Cloud Serpent",
              "sortName": "Serpent Cloud",
              "hyperlinkId": "hyperlink_id",
              "attributeId": "CSer",
              "franchise": "Warcraft",
              "rarity": "Epic",
              "releaseDate": "2014-03-13",
              "category": "mount",
              "event": "an event",
              "type": "Flying",
              "variationMountIds": [
                "CloudSerpentMountVar1",
                "CloudSerpentMountVar2"
              ],
              "searchText": "Cloud Serpent Mount Flying Dragon",
              "description": "A mystical serpent from the clouds of Pandaria."
            }
            """);
    }
}
