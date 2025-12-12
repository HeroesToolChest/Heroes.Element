namespace Heroes.Element.Tests;

[TestClass]
public class BundleDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {},
      "items": {
        "MegaBundleStarterPack": {
          "name": "Mega Starter Bundle",
          "hyperlinkId": "MegaBundleStarterPack",
          "attributeId": "Mega"
        },
        "BundleHeroesOfTheStorm": {
          "name": "Heroes of the Storm Bundle",
          "hyperlinkId": "BundleHeroesOfTheStorm(hyperlink)",
          "attributeId": "Hots"
        }
      }
    }
    """;

    [TestMethod]
    public void TryGetElementById_ItemsPropertyIsEmpty_ReturnsFalse()
    {
        // arrange
        string json =
        """
        {
          "meta": {},
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BundleDataDocument bundleData = BundleDataDocument.Load(jsonDocument);

        // act
        bool result = bundleData.TryGetElementById("other", out Bundle? bundle);

        // assert
        result.Should().BeFalse();
        bundle.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_MegaBundleStarterPack_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {},
          "items": {
            "MegaBundleStarterPack": {
              "name": "Mega Starter Bundle",
              "sortName": "Starter Pack",
              "hyperlinkId": "MegaBundleStarterPack",
              "franchise": "Nexus",
              "rarity": "Epic",
              "releaseDate": "2016-04-12",
              "category": "bundle",
              "event": "no",
              "description": "Get started with this amazing bundle pack!",
              "searchText": "Mega Starter Bundle Pack Heroes Skins",
              "isDynamicContent": true,
              "heroIds": [
                "Arthas",
                "Jaina",
                "Thrall"
              ],
              "skinIds": {
                "Arthas": [
                  "ArthasCrownPrince"
                ],
                "Jaina": [
                  "JainaDreadlord",
                  "JainaTheramore"
                ]
              },
              "mountIds": [
                "CloudSerpentMount",
                "MechanicalSheepMount"
              ],
              "image": "storm_ui_bundle_mega_starter.png",
              "boostId": "BoostStimpak",
              "goldBonus": 1000,
              "gemsBonus": 500,
              "lootChestId": "LootChestRare"
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BundleDataDocument bundleData = BundleDataDocument.Load(jsonDocument);

        // act
        bool returnResult = bundleData.TryGetElementById("MegaBundleStarterPack", out Bundle? bundle);

        // assert
        returnResult.Should().BeTrue();
        bundle.Should().NotBeNull();
        bundle.Id.Should().Be("MegaBundleStarterPack");
        bundle.Name!.RawText.Should().Be("Mega Starter Bundle");
        bundle.SortName!.RawText.Should().Be("Starter Pack");
        bundle.HyperlinkId.Should().Be("MegaBundleStarterPack");
        bundle.Franchise.Should().Be(Franchise.Nexus);
        bundle.Rarity.Should().Be(Rarity.Epic);
        bundle.ReleaseDate.Should().Be(new DateOnly(2016, 4, 12));
        bundle.Category.Should().Be("bundle");
        bundle.Event.Should().Be("no");
        bundle.Description!.RawText.Should().Be("Get started with this amazing bundle pack!");
        bundle.SearchText!.RawText.Should().Be("Mega Starter Bundle Pack Heroes Skins");
        bundle.IsDynamicContent.Should().BeTrue();
        bundle.HeroIds.Should().HaveCount(3)
            .And.ContainInConsecutiveOrder("Arthas", "Jaina", "Thrall");
        bundle.HeroSkinIdsByHeroId.Should().HaveCount(2);
        bundle.HeroSkinIdsByHeroId["Arthas"].Should().ContainSingle()
            .And.Contain("ArthasCrownPrince");
        bundle.HeroSkinIdsByHeroId["Jaina"].Should().HaveCount(2)
            .And.ContainInConsecutiveOrder("JainaDreadlord", "JainaTheramore");
        bundle.MountIds.Should().HaveCount(2)
            .And.ContainInConsecutiveOrder("CloudSerpentMount", "MechanicalSheepMount");
        bundle.Image.Should().Be("storm_ui_bundle_mega_starter.png");
        bundle.BoostBonusId.Should().Be("BoostStimpak");
        bundle.GoldBonus.Should().Be(1000);
        bundle.GemsBonus.Should().Be(500);
        bundle.LootChestBonus.Should().Be("LootChestRare");
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BundleDataDocument bundleData = BundleDataDocument.Load(jsonDocument);

        // act
        bool result = bundleData.TryGetElementById("other", out Bundle? bundle);

        // assert
        result.Should().BeFalse();
        bundle.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BundleDataDocument bundleData = BundleDataDocument.Load(jsonDocument);

        // act
        Bundle bundle = bundleData.GetElementById("BundleHeroesOfTheStorm");

        // assert
        HeroesOfTheStormBasicAssertions(bundle);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BundleDataDocument bundleData = BundleDataDocument.Load(jsonDocument);

        // act
        Action act = () => bundleData.GetElementById("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BundleDataDocument bundleData = BundleDataDocument.Load(jsonDocument);

        // act
        bool result = bundleData.TryGetElementByHyperlinkId("BundleHeroesOfTheStorm(hyperlink)", out Bundle? bundle);

        // assert
        result.Should().BeTrue();
        bundle.Should().NotBeNull();

        HeroesOfTheStormBasicAssertions(bundle);
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BundleDataDocument bundleData = BundleDataDocument.Load(jsonDocument);

        // act
        bool result = bundleData.TryGetElementByHyperlinkId("other", out Bundle? bundle);

        // assert
        result.Should().BeFalse();
        bundle.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByHyperlinkId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BundleDataDocument bundleData = BundleDataDocument.Load(jsonDocument);

        // act
        Bundle bundle = bundleData.GetElementByHyperlinkId("BundleHeroesOfTheStorm(hyperlink)");

        // assert
        HeroesOfTheStormBasicAssertions(bundle);
    }

    [TestMethod]
    public void GetElementByHyperlinkId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BundleDataDocument bundleData = BundleDataDocument.Load(jsonDocument);

        // act
        Action act = () => bundleData.GetElementByHyperlinkId("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByAttributeId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BundleDataDocument bundleData = BundleDataDocument.Load(jsonDocument);

        // act
        bool result = bundleData.TryGetElementByAttributeId("Hots", out Bundle? bundle);

        // assert
        result.Should().BeTrue();
        bundle.Should().NotBeNull();

        HeroesOfTheStormBasicAssertions(bundle);
    }

    [TestMethod]
    public void TryGetElementByAttributeId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BundleDataDocument bundleData = BundleDataDocument.Load(jsonDocument);

        // act
        bool result = bundleData.TryGetElementByAttributeId("other", out Bundle? bundle);

        // assert
        result.Should().BeFalse();
        bundle.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByAttributeId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BundleDataDocument bundleData = BundleDataDocument.Load(jsonDocument);

        // act
        Bundle bundle = bundleData.GetElementByAttributeId("Hots");

        // assert
        HeroesOfTheStormBasicAssertions(bundle);
    }

    [TestMethod]
    public void GetElementByAttributeId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        BundleDataDocument bundleData = BundleDataDocument.Load(jsonDocument);

        // act
        Action act = () => bundleData.GetElementByAttributeId("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void GetElementById_WithGameStringDocument_UpdatesGameStrings()
    {
        // arrange
        string jsonData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0"
          },
          "items": {
            "BundleHeroesOfTheStorm": {
              "name": "Heroes of the Storm Bundle",
              "description": "A description",
              "searchText": "Search terms",
              "sortName": "Sort Heroes Bundle"
            }
          }
        }
        """;

        string gameStringData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0",
            "descriptionText": {
              "locale": "FRFR",
              "gameStringTextType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "gamestrings": {
            "bundle": {
              "name": {
                "BundleHeroesOfTheStorm": "Heroes of the Storm Bundle Localized"
              },
              "description": {
                "BundleHeroesOfTheStorm": "Localized Description"
              },
              "searchText": {
                "BundleHeroesOfTheStorm": "Localized Search Terms"
              },
              "sortName": {
                "BundleHeroesOfTheStorm": "Localized Sort Name"
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        BundleDataDocument bundleData = BundleDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        Bundle bundle = bundleData.GetElementById("BundleHeroesOfTheStorm");

        // assert
        bundle.Should().NotBeNull();
        bundle.Name!.RawText.Should().Be("Heroes of the Storm Bundle Localized");
        bundle.Name.GameStringLocale.Should().Be(StormLocale.FRFR);
        bundle.Description!.RawText.Should().Be("Localized Description");
        bundle.Description.GameStringLocale.Should().Be(StormLocale.FRFR);
        bundle.SearchText!.RawText.Should().Be("Localized Search Terms");
        bundle.SearchText.GameStringLocale.Should().Be(StormLocale.FRFR);
        bundle.SortName!.RawText.Should().Be("Localized Sort Name");
        bundle.SortName.GameStringLocale.Should().Be(StormLocale.FRFR);
    }

    private static void HeroesOfTheStormBasicAssertions(Bundle bundle)
    {
        bundle.Id.Should().Be("BundleHeroesOfTheStorm");
        bundle.Name!.RawText.Should().Be("Heroes of the Storm Bundle");
        bundle.HyperlinkId.Should().Be("BundleHeroesOfTheStorm(hyperlink)");
    }
}
