namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class BundleSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        Bundle bundle = new("bundle_id")
        {
            // Bundle properties
            IsDynamicContent = true,
            HeroIds = new SortedSet<string> { "hero1", "hero2", "hero3" },
            HeroSkinIdsByHeroId = new SortedDictionary<string, ISet<string>>(StringComparer.Ordinal)
            {
                { "hero1", new SortedSet<string> { "skin1", "skin2" } },
                { "hero2", new SortedSet<string> { "skin3", "skin4" } },
            },
            MountIds = new SortedSet<string> { "mount1", "mount2" },
            Image = "bundle_image.dds",
            BoostBonusId = "boost1",
            GoldBonus = 5000,
            GemsBonus = 1000,
            LootChestBonus = "lootchest1",

            // HeroesCollectionObject properties (inherited)
            Name = new GameStringText("Bundle Name"),
            SortName = new GameStringText("Sort Name"),
            HyperlinkId = "hyperlink_id",
            AttributeId = "BUND",
            Franchise = Franchise.Diablo,
            Rarity = Rarity.Legendary,
            ReleaseDate = new DateOnly(2024, 12, 1),
            Category = "Bundle Category",
            Event = "Holiday Event",
            IsShownInStore = true,
            SearchText = new GameStringText("bundle search keywords"),
            Description = new GameStringText("Bundle Description Text"),
        };

        // act
        string json = JsonSerializer.Serialize(bundle, SerializerSettings.SetJsonSerializerDataOptions());

        // assert
        json.Should().Be(
            """
            {
              "name": "Bundle Name",
              "sortName": "Sort Name",
              "hyperlinkId": "hyperlink_id",
              "attributeId": "BUND",
              "franchise": "Diablo",
              "rarity": "Legendary",
              "releaseDate": "2024-12-01",
              "category": "Bundle Category",
              "event": "Holiday Event",
              "isShownInStore": true,
              "isDynamicContent": true,
              "heroIds": [
                "hero1",
                "hero2",
                "hero3"
              ],
              "skinIds": {
                "hero1": [
                  "skin1",
                  "skin2"
                ],
                "hero2": [
                  "skin3",
                  "skin4"
                ]
              },
              "mountIds": [
                "mount1",
                "mount2"
              ],
              "image": "bundle_image.dds",
              "boostId": "boost1",
              "goldBonus": 5000,
              "gemsBonus": 1000,
              "lootChestId": "lootchest1",
              "searchText": "bundle search keywords",
              "description": "Bundle Description Text"
            }
            """);
    }
}
