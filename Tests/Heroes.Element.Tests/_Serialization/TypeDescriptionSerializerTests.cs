namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class TypeDescriptionSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        SerializerSettings serializerSettings = SerializerSettings.Create();

        TypeDescription typeDescription = new("id")
        {
            Name = new GameStringText("Test Type Description"),
            RewardIcon = "storm_ui_reward_icon.png",
            LargeIcon = "storm_ui_large_icon.png",
        };

        // act
        string json = JsonSerializer.Serialize(typeDescription, serializerSettings.GetJsonSerializerDataOptions());

        // assert
        serializerSettings.ItemDictionary["typeDescription"].Should().ContainSingle("it's the total number of gamestringtext properties");

        json.Should().Be(
            """
            {
              "name": "Test Type Description",
              "rewardIcon": "storm_ui_reward_icon.png",
              "largeIcon": "storm_ui_large_icon.png"
            }
            """);
    }
}
