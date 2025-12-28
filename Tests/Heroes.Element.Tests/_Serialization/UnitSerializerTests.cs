namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class UnitSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        SerializerSettings serializerSettings = SerializerSettings.Create();

        Unit unit = new("unit_id")
        {
            Name = new GameStringText("Unit Name"),
            Gender = Gender.Male,
            DamageType = ArmorSet.Structure,
            Radius = 2.0,
            InnerRadius = 1.0,
            Sight = 15.0,
            Speed = 5.5,
            KillXP = 150,
            Attributes = new SortedSet<string> { "Heroic", "Mechanical" },
            ScalingLinkIds = new SortedSet<string> { "scale1", "scale2" },
            Description = new GameStringText("Unit Description Text"),
            Life = new UnitLife
            {
                LifeMax = 3000,
                LifeMaxScaling = 0.05,
                LifeRegenerationRate = 10.0,
                LifeRegenerationRateScaling = 0.06,
                LifeType = new GameStringText("Life"),
            },
            Energy = new UnitEnergy
            {
                EnergyMax = 200,
                EnergyRegenerationRate = 5.0,
                EnergyType = new GameStringText("Energy"),
            },
            Shield = new UnitShield
            {
                ShieldMax = 500,
                ShieldRegenerationDelay = 3.0,
                ShieldRegenerationRate = 25.0,
                ShieldType = new GameStringText("Overshield"),
            },
            Armor = new SortedDictionary<ArmorSet, UnitArmor>
            {
                { ArmorSet.Structure, new UnitArmor { BasicArmor = 15.0, AbilityArmor = 5.0, SplashArmor = 50.0 } },
                { ArmorSet.Hero, new UnitArmor { BasicArmor = 10.0, AbilityArmor = 10.0, SplashArmor = 25.0 } },
            },
            HeroPlayStyles = new SortedSet<string> { "Ambusher", "Siege" },
            UnitPortraits = new UnitPortrait
            {
                TargetInfoPanel = "unit_target_info.dds",
                MiniMapIcon = "unit_minimap.dds",
            },
            SummonedUnitIds = new SortedSet<string> { "summon_unit1", "summon_unit2" },
            Weapons =
            [
                new UnitWeapon
                {
                    NameId = "weapon_main",
                    Damage = 150,
                    Period = 1.5,
                    Range = 6.0,
                    IsDisabled = false,
                    DamageScaling = 0.03,
                    MinimumRange = 0.5,
                    AttributeFactors = new Dictionary<string, double> { { "Power", 1.5 }, { "Attack", 0.75 } },
                    VitalCost = new Dictionary<VitalType, double> { { VitalType.Life, 10.0 }, { VitalType.Energy, 15.0 } },
                },
                new UnitWeapon
                {
                    NameId = "weapon_secondary",
                    Damage = 75,
                    Period = 0.75,
                    Range = 3.5,
                    IsDisabled = false,
                    DamageScaling = 0.015,
                    MinimumRange = 0.0,
                },
            ],
            Abilities = new SortedDictionary<AbilityTier, IList<Ability>>
            {
                {
                    AbilityTier.Basic, new List<Ability>
                    {
                        new()
                        {
                            AbilityElementId = "basic_ability1",
                            ButtonElementId = "basic_button1",
                            Name = new GameStringText("Basic Ability"),
                            Icon = "basic_ability_icon.dds",
                            ToggleCooldown = 1.0,
                            Charges = new TooltipCharges
                            {
                                CountMax = 3,
                                CountStart = 3,
                                CountUse = 1,
                                RecastCooldown = 4.0,
                                IsCountHidden = false,
                            },
                            EnergyText = new GameStringText("40 Energy"),
                            LifeText = new GameStringText("0"),
                            CooldownText = new GameStringText("6 seconds"),
                            ShortText = new GameStringText("Basic ability description"),
                            FullText = new GameStringText("Full description of the basic ability with all details"),
                            AbilityType = AbilityType.Q,
                            Tier = AbilityTier.Basic,
                        },
                    }
                },
                {
                    AbilityTier.Heroic, new List<Ability>
                    {
                        new()
                        {
                            AbilityElementId = "heroic_ability1",
                            ButtonElementId = "heroic_button1",
                            Name = new GameStringText("Heroic Ability"),
                            Icon = "heroic_ability_icon.dds",
                            ToggleCooldown = 0.0,
                            EnergyText = new GameStringText("80 Energy"),
                            CooldownText = new GameStringText("100 seconds"),
                            ShortText = new GameStringText("Ultimate ability"),
                            FullText = new GameStringText("Powerful heroic ability with major impact"),
                            AbilityType = AbilityType.Heroic,
                            Tier = AbilityTier.Heroic,
                        },
                    }
                },
            },
            SubAbilities = new SortedDictionary<LinkId, IDictionary<AbilityTier, IList<Ability>>>
            {
                {
                    new AbilityLinkId("basic_ability1", "basic_button1", AbilityType.Q),
                    new SortedDictionary<AbilityTier, IList<Ability>>
                    {
                        {
                            AbilityTier.Action, new List<Ability>
                            {
                                new()
                                {
                                    AbilityElementId = "sub_ability1",
                                    ButtonElementId = "sub_button1",
                                    Name = new GameStringText("Sub Ability"),
                                    Icon = "sub_ability_icon.dds",
                                    CooldownText = new GameStringText("2 seconds"),
                                    ShortText = new GameStringText("Sub ability text"),
                                    FullText = new GameStringText("Full sub ability description"),
                                    AbilityType = AbilityType.Active,
                                    Tier = AbilityTier.Action,
                                },
                            }
                        },
                    }
                },
            },
        };

        // act
        string json = JsonSerializer.Serialize(unit, serializerSettings.GetJsonSerializerDataOptions());

        // assert
        serializerSettings.ItemDictionary["ability"].Should().HaveCount(6);
        serializerSettings.ItemDictionary["unit"].Should().HaveCount(5);

        json.Should().Be(
            """
            {
              "name": "Unit Name",
              "gender": "Male",
              "damageType": "Structure",
              "radius": 2,
              "innerRadius": 1,
              "sight": 15,
              "speed": 5.5,
              "killXP": 150,
              "attributes": [
                "Heroic",
                "Mechanical"
              ],
              "scalingLinkIds": [
                "scale1",
                "scale2"
              ],
              "description": "Unit Description Text",
              "life": {
                "amount": 3000,
                "scale": 0.05,
                "regenRate": 10,
                "regenScale": 0.06,
                "type": "Life"
              },
              "energy": {
                "amount": 200,
                "regenRate": 5,
                "type": "Energy"
              },
              "shield": {
                "amount": 500,
                "scale": 0,
                "regenRate": 25,
                "regenScale": 0,
                "regenDelay": 3,
                "type": "Overshield"
              },
              "armor": {
                "Hero": {
                  "basic": 10,
                  "ability": 10,
                  "splash": 25
                },
                "Structure": {
                  "basic": 15,
                  "ability": 5,
                  "splash": 50
                }
              },
              "playstyles": [
                "Ambusher",
                "Siege"
              ],
              "portraits": {
                "minimap": "unit_minimap.dds",
                "targetInfo": "unit_target_info.dds"
              },
              "summonedUnitIds": [
                "summon_unit1",
                "summon_unit2"
              ],
              "weapons": [
                {
                  "nameId": "weapon_main",
                  "isDisabled": false,
                  "range": 6,
                  "minimumRange": 0.5,
                  "period": 1.5,
                  "damage": 150,
                  "damageScale": 0.03,
                  "damageFactors": {
                    "Power": 1.5,
                    "Attack": 0.75
                  },
                  "vitalCost": {
                    "Life": 10,
                    "Energy": 15
                  }
                },
                {
                  "nameId": "weapon_secondary",
                  "isDisabled": false,
                  "range": 3.5,
                  "minimumRange": 0,
                  "period": 0.75,
                  "damage": 75,
                  "damageScale": 0.015
                }
              ],
              "abilities": {
                "Basic": [
                  {
                    "linkId": "basic_ability1|basic_button1|Q",
                    "abilityId": "basic_ability1",
                    "buttonId": "basic_button1",
                    "name": "Basic Ability",
                    "icon": "basic_ability_icon.dds",
                    "toggleCooldown": 1,
                    "charges": {
                      "countMax": 3,
                      "countStart": 3,
                      "countUse": 1,
                      "recastCooldown": 4,
                      "isCountHidden": false
                    },
                    "energyText": "40 Energy",
                    "lifeText": "0",
                    "cooldownText": "6 seconds",
                    "shortText": "Basic ability description",
                    "fullText": "Full description of the basic ability with all details",
                    "abilityType": "Q"
                  }
                ],
                "Heroic": [
                  {
                    "linkId": "heroic_ability1|heroic_button1|Heroic",
                    "abilityId": "heroic_ability1",
                    "buttonId": "heroic_button1",
                    "name": "Heroic Ability",
                    "icon": "heroic_ability_icon.dds",
                    "toggleCooldown": 0,
                    "energyText": "80 Energy",
                    "cooldownText": "100 seconds",
                    "shortText": "Ultimate ability",
                    "fullText": "Powerful heroic ability with major impact",
                    "abilityType": "Heroic"
                  }
                ]
              },
              "subAbilities": {
                "basic_ability1|basic_button1|Q": {
                  "Action": [
                    {
                      "linkId": "sub_ability1|sub_button1|Active",
                      "abilityId": "sub_ability1",
                      "buttonId": "sub_button1",
                      "name": "Sub Ability",
                      "icon": "sub_ability_icon.dds",
                      "cooldownText": "2 seconds",
                      "shortText": "Sub ability text",
                      "fullText": "Full sub ability description",
                      "abilityType": "Active"
                    }
                  ]
                }
              }
            }
            """);
    }
}
