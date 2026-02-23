namespace Heroes.Element.Tests;

[TestClass]
public class UnitDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {},
      "items": {
        "FootmanUnit": {
          "name": "Footman",
          "radius": 0.75,
          "innerRadius": 0.5,
          "sight": 10,
          "speed": 4.5
        },
        "ArcherUnit": {
          "name": "Archer",
          "radius": 0.625,
          "innerRadius": 0.4,
          "sight": 12,
          "speed": 4.0
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
        UnitDataDocument unitData = UnitDataDocument.Load(jsonDocument);

        // act
        bool result = unitData.TryGetElementById("other", out Unit? unit);

        // assert
        result.Should().BeFalse();
        unit.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_FootmanUnit_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {},
          "items": {
            "FootmanUnit": {
              "name": "Footman",
              "gender": "Male",
              "damageType": "Minion",
              "radius": 0.75,
              "innerRadius": 0.5,
              "sight": 10,
              "speed": 4.5,
              "killXP": 50,
              "attributes": [
                "Melee",
                "Soldier"
              ],
              "scalingLinkIds": [
                "UnitVeterancy"
              ],
              "description": "A basic footman unit.",
              "life": {
                "amount": 500,
                "scale": 0.04,
                "regenRate": 2.5,
                "regenScale": 0.04,
                "type": "Health"
              },
              "energy": {
                "amount": 100,
                "regenRate": 1.5,
                "type": "Mana"
              },
              "shield": {
                "amount": 200,
                "scale": 0.03,
                "regenRate": 10,
                "regenScale": 0.03,
                "regenDelay": 3,
                "type": "Shields"
              },
              "armor": {
                "Summon": {
                  "basic": 10,
                  "ability": 5,
                  "splash": 2
                }
              },
              "playstyles": [
                "Tank",
                "Defender"
              ],
              "portraits": {
                "minimap": "storm_ui_minimapicon_footman.png",
                "targetInfo": "ui_targetportrait_footman.png"
              },
              "summonedUnitIds": [
                "FootmanBanner",
                "FootmanSquire"
              ],
              "weapons": [
                {
                  "nameId": "FootmanWeapon",
                  "isDisabled": false,
                  "range": 1.5,
                  "minimumRange": 0,
                  "period": 1.0,
                  "damage": 50,
                  "damageScale": 0.04
                }
              ],
              "abilities": {
                "Basic": [
                  {
                    "linkId": "FootmanCharge|FootmanCharge|Q",
                    "abilityId": "FootmanCharge",
                    "buttonId": "FootmanCharge",
                    "name": "Charge",
                    "icon": "storm_ui_icon_footman_charge.png",
                    "cooldownText": "Cooldown: 8 seconds",
                    "shortText": "Charge forward",
                    "fullText": "Charge forward, dealing damage to enemies in the path.",
                    "abilityType": "Q"
                  }
                ]
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        UnitDataDocument unitData = UnitDataDocument.Load(jsonDocument);

        // act
        bool returnResult = unitData.TryGetElementById("FootmanUnit", out Unit? unit);

        // assert
        returnResult.Should().BeTrue();
        unit.Should().NotBeNull();
        unit!.Id.Should().Be("FootmanUnit");
        unit.Name!.RawText.Should().Be("Footman");
        unit.Gender.Should().Be(Gender.Male);
        unit.DamageType.Should().Be(ArmorSet.Minion);
        unit.Radius.Should().Be(0.75);
        unit.InnerRadius.Should().Be(0.5);
        unit.Sight.Should().Be(10);
        unit.Speed.Should().Be(4.5);
        unit.KillXP.Should().Be(50);
        unit.Attributes.Should().HaveCount(2)
            .And.ContainInConsecutiveOrder("Melee", "Soldier");
        unit.ScalingLinkIds.Should().ContainSingle()
            .And.ContainInConsecutiveOrder("UnitVeterancy");
        unit.Description!.RawText.Should().Be("A basic footman unit.");

        // armor
        unit.Armor[ArmorSet.Summon].BasicArmor.Should().Be(10);
        unit.Armor[ArmorSet.Summon].AbilityArmor.Should().Be(5);
        unit.Armor[ArmorSet.Summon].SplashArmor.Should().Be(2);

        // life
        unit.Life.LifeMax.Should().Be(500);
        unit.Life.LifeMaxScaling.Should().Be(0.04);
        unit.Life.LifeRegenerationRate.Should().Be(2.5);
        unit.Life.LifeRegenerationRateScaling.Should().Be(0.04);
        unit.Life.LifeType!.RawText.Should().Be("Health");

        // energy
        unit.Energy.EnergyMax.Should().Be(100);
        unit.Energy.EnergyRegenerationRate.Should().Be(1.5);
        unit.Energy.EnergyType!.RawText.Should().Be("Mana");

        // shield
        unit.Shield.ShieldMax.Should().Be(200);
        unit.Shield.ShieldMaxScaling.Should().Be(0.03);
        unit.Shield.ShieldRegenerationRate.Should().Be(10);
        unit.Shield.ShieldRegenerationRateScaling.Should().Be(0.03);
        unit.Shield.ShieldRegenerationDelay.Should().Be(3);

        // playstyles
        unit.HeroPlayStyles.Should().HaveCount(2)
            .And.ContainInConsecutiveOrder("Tank", "Defender");

        // portraits
        unit.UnitPortraits.MiniMapIcon.Should().Be("storm_ui_minimapicon_footman.png");
        unit.UnitPortraits.TargetInfoPanel.Should().Be("ui_targetportrait_footman.png");

        // summoned units
        unit.SummonedUnitIds.Should().HaveCount(2)
            .And.ContainInConsecutiveOrder("FootmanBanner", "FootmanSquire");

        // weapons
        unit.Weapons.Should().ContainSingle();
        unit.Weapons[0].NameId.Should().Be("FootmanWeapon");
        unit.Weapons[0].IsDisabled.Should().BeFalse();
        unit.Weapons[0].Range.Should().Be(1.5);
        unit.Weapons[0].MinimumRange.Should().Be(0);
        unit.Weapons[0].Period.Should().Be(1.0);
        unit.Weapons[0].Damage.Should().Be(50);
        unit.Weapons[0].DamageScaling.Should().Be(0.04);

        // abilities
        unit.Abilities[AbilityTier.Basic].Should().ContainSingle();
        Ability ability = unit.Abilities[AbilityTier.Basic][0];
        ability.LinkId.Id.Should().Be("FootmanCharge|FootmanCharge|Q");
        ability.AbilityElementId.Should().Be("FootmanCharge");
        ability.ButtonElementId.Should().Be("FootmanCharge");
        ability.Name!.RawText.Should().Be("Charge");
        ability.Icon.Should().Be("storm_ui_icon_footman_charge.png");
        ability.CooldownText!.RawText.Should().Be("Cooldown: 8 seconds");
        ability.ShortText!.RawText.Should().Be("Charge forward");
        ability.FullText!.RawText.Should().Be("Charge forward, dealing damage to enemies in the path.");
        ability.AbilityType.Should().Be(AbilityType.Q);
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        UnitDataDocument unitData = UnitDataDocument.Load(jsonDocument);

        // act
        bool result = unitData.TryGetElementById("other", out Unit? unit);

        // assert
        result.Should().BeFalse();
        unit.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        UnitDataDocument unitData = UnitDataDocument.Load(jsonDocument);

        // act
        Unit unit = unitData.GetElementById("ArcherUnit");

        // assert
        ArcherUnitBasicAssertions(unit);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        UnitDataDocument unitData = UnitDataDocument.Load(jsonDocument);

        // act
        Action act = () => unitData.GetElementById("other");

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
            "FootmanUnit": {
              "name": "Footman",
              "description": "A basic unit"
            }
          }
        }
        """;

        string gameStringData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0",
            "gameStringText": {
              "locale": "DEDE",
              "textType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "items": {
            "unit": {
              "name": {
                "FootmanUnit": "Fußsoldat"
              },
              "description": {
                "FootmanUnit": "Eine grundlegende Einheit"
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        UnitDataDocument unitData = UnitDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        Unit unit = unitData.GetElementById("FootmanUnit");

        // assert
        unit.Should().NotBeNull();
        unit.Name!.RawText.Should().Be("Fußsoldat");
        unit.Name.GameStringLocale.Should().Be(StormLocale.DEDE);
        unit.Description!.RawText.Should().Be("Eine grundlegende Einheit");
        unit.Description.GameStringLocale.Should().Be(StormLocale.DEDE);
    }

    [TestMethod]
    public void GetElements_WithItems_ReturnsAllElements()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        UnitDataDocument unitData = UnitDataDocument.Load(jsonDocument);

        // act
        List<Unit> result = [.. unitData.GetElements()];

        // assert
        result.Should().HaveCount(2);
        result.Should().Contain(u => u.Id == "FootmanUnit");
        result.Should().Contain(u => u.Id == "ArcherUnit");
    }

    [TestMethod]
    public void GetElements_WithEmptyItems_ReturnsEmpty()
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
        UnitDataDocument unitData = UnitDataDocument.Load(jsonDocument);

        // act
        List<Unit> result = [.. unitData.GetElements()];

        // assert
        result.Should().BeEmpty();
    }

    private static void ArcherUnitBasicAssertions(Unit unit)
    {
        unit.Id.Should().Be("ArcherUnit");
        unit.Name!.RawText.Should().Be("Archer");
        unit.Radius.Should().Be(0.625);
        unit.InnerRadius.Should().Be(0.4);
        unit.Sight.Should().Be(12);
        unit.Speed.Should().Be(4.0);
    }
}
