namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class LootChestSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        SerializerSettings serializerSettings = SerializerSettings.Create();

        LootChest lootChest = new("lootchest_id")
        {
            // LootChest properties
            Name = new GameStringText("Loot Chest Name"),
            HyperlinkId = "hyperlink_id",
            Rarity = Rarity.Legendary,
            Event = "Winter Event",
            MaxRerolls = 3,
            TypeDescriptionId = "chest_type_description",
            Description = new GameStringText("Loot Chest Description Text"),
        };

        // act
        string json = JsonSerializer.Serialize(lootChest, serializerSettings.GetJsonSerializerDataOptions());

        // assert
        serializerSettings.ItemDictionary["lootChest"].Should().HaveCount(2, "it's the total number of gamestringtext properties");

        json.Should().Be(
            """
            {
              "name": "Loot Chest Name",
              "hyperlinkId": "hyperlink_id",
              "rarity": "Legendary",
              "event": "Winter Event",
              "maxRerolls": 3,
              "typeDescriptionId": "chest_type_description",
              "description": "Loot Chest Description Text"
            }
            """);
    }
}
