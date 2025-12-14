namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class LootChestSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
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
        string json = JsonSerializer.Serialize(lootChest, SerializerSettings.SetJsonSerializerDataOptions());

        // assert
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
