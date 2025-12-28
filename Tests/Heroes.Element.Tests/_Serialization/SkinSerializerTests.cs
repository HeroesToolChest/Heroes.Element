namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class SkinSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        SerializerSettings serializerSettings = SerializerSettings.Create();

        Skin skin = new("skin_id")
        {
            // StoreItem properties
            Name = new GameStringText("Mecha Abathur"),
            SortName = new GameStringText("Abathur Mecha"),
            HyperlinkId = "hyperlink_id",
            Franchise = Franchise.Nexus,
            Rarity = Rarity.Legendary,
            ReleaseDate = new DateOnly(2018, 5, 22),
            Category = "skin",
            Event = "Mecha Event",
            SearchText = new GameStringText("Mecha Abathur Robot Mechanical"),
            Description = new GameStringText("An alternate skin for the Evolution Master."),
            InfoText = new GameStringText("Legendary Skin from the Mecha universe"),

            // LoadoutItem properties
            AttributeId = "MechaA",
        };

        skin.Features.Add("ThemedAbilities");
        skin.Features.Add("ThemedAnimations");
        skin.Features.Add("AlternateMountForm");
        skin.VariationSkinIds.Add("AbathurMechaVar2");
        skin.VariationSkinIds.Add("AbathurMechaVar3");
        skin.VoiceLineIds.Add("AbathurMecha_VoiceLine01");
        skin.VoiceLineIds.Add("AbathurMecha_VoiceLine02");

        // act
        string json = JsonSerializer.Serialize(skin, serializerSettings.GetJsonSerializerDataOptions());

        // assert
        serializerSettings.ItemDictionary["skin"].Should().HaveCount(5, "it's the total number of gamestringtext properties");

        json.Should().Be(
            """
            {
              "name": "Mecha Abathur",
              "sortName": "Abathur Mecha",
              "hyperlinkId": "hyperlink_id",
              "attributeId": "MechaA",
              "franchise": "Nexus",
              "rarity": "Legendary",
              "releaseDate": "2018-05-22",
              "category": "skin",
              "event": "Mecha Event",
              "features": [
                "AlternateMountForm",
                "ThemedAbilities",
                "ThemedAnimations"
              ],
              "variationSkinIds": [
                "AbathurMechaVar2",
                "AbathurMechaVar3"
              ],
              "voiceLineIds": [
                "AbathurMecha_VoiceLine01",
                "AbathurMecha_VoiceLine02"
              ],
              "searchText": "Mecha Abathur Robot Mechanical",
              "description": "An alternate skin for the Evolution Master.",
              "infoText": "Legendary Skin from the Mecha universe"
            }
            """);
    }
}