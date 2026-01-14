namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class EmoticonSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        SerializerSettings serializerSettings = SerializerSettings.Create();

        Emoticon emoticon = new("id")
        {
            Expression = ":abathur:",
            Description = new GameStringText("Test Description"),
            SearchText = new GameStringText("item1 item2"),
            IsCaseSensitive = true,
            IsHidden = false,
            HeroId = "Abathur",
            SkinId = "AbathurMechaVar1",
            Image = "test.png",
            Animation = new EmoticonAnimation
            {
                Texture = "test_animation.png",
                Frames = 16,
                Duration = 1500,
                Width = 52,
            },
        };
        emoticon.UniversalAliases.Add(":aba:");
        emoticon.UniversalAliases.Add(":slug:");
        emoticon.LocalizedAliases.Add(new GameStringText("abathur"));
        emoticon.LocalizedAliases.Add(new GameStringText("evolution"));

        // act
        string json = JsonSerializer.Serialize(emoticon, serializerSettings.GetJsonSerializerDataOptions());

        // assert
        serializerSettings.ItemDictionary["emoticon"].Should().HaveCount(3, "it's the total number of gamestringtext properties");

        json.Should().Be(
            """
            {
              "expression": ":abathur:",
              "description": "Test Description",
              "universalAliases": [
                ":aba:",
                ":slug:"
              ],
              "localizedAliases": [
                "abathur",
                "evolution"
              ],
              "searchText": "item1 item2",
              "isCaseSensitive": true,
              "isHidden": false,
              "heroId": "Abathur",
              "skinId": "AbathurMechaVar1",
              "image": "test.png",
              "animation": {
                "width": 52,
                "texture": "test_animation.png",
                "frames": 16,
                "duration": 1500
              }
            }
            """);
    }
}
