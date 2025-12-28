using Heroes.Element.Comparers;

namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class HeroSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        SerializerSettings serializerSettings = SerializerSettings.Create();

        Hero hero = new("hero_id")
        {
            // Hero properties
            SortName = new GameStringText("Sort Name"),
            UnitId = "unit_id",
            HyperlinkId = "hyperlink_id",
            AttributeId = "ATTR",
            Title = new GameStringText("Hero Title"),
            Franchise = Franchise.Warcraft,
            Rarity = Rarity.Epic,
            ReleaseDate = new DateOnly(2024, 6, 15),
            Category = "Hero Category",
            Event = "Special Event",
            Difficulty = new GameStringText("Hard"),
            IsMelee = true,
            DefaultMountId = "default_mount",
            Roles = new HashSet<GameStringText> { new("Tank"), new("Bruiser") },
            ExpandedRole = new GameStringText("Main Tank"),
            Ratings = new HeroRatings
            {
                Complexity = 5,
                Damage = 7,
                Survivability = 8,
                Utility = 6,
            },
            HeroPortraits = new HeroPortrait
            {
                DraftScreen = "draft_screen.dds",
                HeroSelectPortrait = "hero_select.dds",
                LeaderboardPortrait = "leaderboard.dds",
                LoadingScreenPortrait = "loading.dds",
                PartyPanelPortrait = "party.dds",
                TargetPortrait = "target.dds",
                MiniMapIcon = "minimap_icon.dds",
                TargetInfoPanel = "target_info.dds",
                PartyFrames = ["party_frame1.dds", "party_frame2.dds"],
            },
            SearchText = new GameStringText("search keywords"),
            InfoText = new GameStringText("Info about hero"),
            SkinIds = new SortedSet<string> { "skin1", "skin2" },
            VariationSkinIds = new SortedSet<string> { "var1", "var2" },
            VoiceLineIds = new SortedSet<string> { "voice1", "voice2" },
            MountCategoryIds = new SortedSet<string> { "mount_cat1", "mount_cat2" },
            HeroUnits = new Dictionary<string, Unit> { { "unit1", new Unit("summon_unit") } },
            Talents = new SortedDictionary<TalentTier, IList<Talent>>
            {
                {
                    TalentTier.Level1, new List<Talent>
                    {
                        new()
                        {
                            TalentElementId = "talent1",
                            ButtonElementId = "button1",
                            AbilityElementId = "ability1",
                            Name = new GameStringText("Talent Name"),
                            Icon = "talent_icon.dds",
                            ToggleCooldown = 1.5,
                            Charges = new TooltipCharges
                            {
                                CountMax = 3,
                                CountStart = 2,
                                CountUse = 1,
                                RecastCooldown = 5.0,
                                IsCountHidden = false,
                            },
                            EnergyText = new GameStringText("50 Mana"),
                            LifeText = new GameStringText("10% Health"),
                            CooldownText = new GameStringText("8 seconds"),
                            ShortText = new GameStringText("Short description"),
                            FullText = new GameStringText("Full description of the talent"),
                            AbilityType = AbilityType.Q,
                            Tier = TalentTier.Level1,
                            IsQuest = true,
                            UpgradesAbilityType = true,
                            Column = 1,
                            AbilityTalentLinkIds = new SortedSet<string> { "ability1|button1|Q" },
                            TooltipAbilityLinkIds = new SortedSet<AbilityLinkId>(new LinkIdComparer())
                            {
                                new("ability1", "button1", AbilityType.Q),
                            },
                            PrerequisiteTalentIds = new SortedSet<string> { "prereq_talent1" },
                        },
                    }
                },
            },

            // Unit properties (inherited)
            Name = new GameStringText("Hero Name"),
            Gender = Gender.Female,
            DamageType = ArmorSet.Hero,
            Radius = 1.5,
            InnerRadius = 0.5,
            Sight = 12.0,
            Speed = 4.5,
            KillXP = 100,
            Attributes = new SortedSet<string> { "Attribute1", "Attribute2" },
            ScalingLinkIds = new SortedSet<string> { "scaling1", "scaling2" },
            Description = new GameStringText("Hero Description"),
            Life = new UnitLife
            {
                LifeMax = 2500,
                LifeMaxScaling = 0.04,
                LifeRegenerationRate = 5.5,
                LifeRegenerationRateScaling = 0.05,
                LifeType = new GameStringText("Health"),
            },
            Energy = new UnitEnergy
            {
                EnergyMax = 500,
                EnergyRegenerationRate = 10.0,
                EnergyType = new GameStringText("Mana"),
            },
            Shield = new UnitShield
            {
                ShieldMax = 1000,
                ShieldRegenerationDelay = 2.0,
                ShieldRegenerationRate = 50.0,
                ShieldType = new GameStringText("Shield"),
            },
            Armor = new SortedDictionary<ArmorSet, UnitArmor>
            {
                { ArmorSet.Hero, new UnitArmor { BasicArmor = 20.5, AbilityArmor = 10.1, SplashArmor = 100 } },
            },
            HeroPlayStyles = new SortedSet<string> { "Sustained Damage", "Tank" },
            SummonedUnitIds = new SortedSet<string> { "summon1", "summon2" },
            Weapons =
            [
                new UnitWeapon
                {
                    NameId = "weapon1",
                    Damage = 100,
                    Period = 1.0,
                    Range = 5.5,
                    IsDisabled = true,
                    DamageScaling = 0.02,
                    MinimumRange = 1.0,
                    AttributeFactors = new Dictionary<string, double> { { "Strength", 1.0 }, { "Agility", 0.5 } },
                    VitalCost = new Dictionary<VitalType, double> { { VitalType.Energy, 20.0 } },
                },
            ],
            Abilities = new SortedDictionary<AbilityTier, IList<Ability>>
            {
                {
                    AbilityTier.Basic, new List<Ability>
                    {
                        new()
                        {
                            AbilityElementId = "ability1",
                            ButtonElementId = "button1",
                            Name = new GameStringText("Ability Name"),
                            Icon = "ability_icon.dds",
                            ToggleCooldown = 2.0,
                            Charges = new TooltipCharges
                            {
                                CountMax = 2,
                                CountStart = 1,
                                CountUse = 1,
                                RecastCooldown = 3.0,
                                IsCountHidden = false,
                            },
                            EnergyText = new GameStringText("30 Mana"),
                            LifeText = new GameStringText("5% Health"),
                            CooldownText = new GameStringText("5 seconds"),
                            ShortText = new GameStringText("Ability short text"),
                            FullText = new GameStringText("Ability full description"),
                            AbilityType = AbilityType.Q,
                            Tier = AbilityTier.Basic,
                        },
                    }
                },
            },
            SubAbilities = new SortedDictionary<LinkId, IDictionary<AbilityTier, IList<Ability>>>(),
        };

        // act
        string json = JsonSerializer.Serialize(hero, serializerSettings.GetJsonSerializerDataOptions());

        // assert
        serializerSettings.ItemDictionary["ability"].Should().HaveCount(6);
        serializerSettings.ItemDictionary["hero"].Should().HaveCount(12);
        serializerSettings.ItemDictionary["talent"].Should().HaveCount(6);

        json.Should().Be(
            """
            {
              "name": "Hero Name",
              "sortName": "Sort Name",
              "unitId": "unit_id",
              "hyperlinkId": "hyperlink_id",
              "attributeId": "ATTR",
              "title": "Hero Title",
              "franchise": "Warcraft",
              "rarity": "Epic",
              "releaseDate": "2024-06-15",
              "category": "Hero Category",
              "event": "Special Event",
              "difficulty": "Hard",
              "isMelee": true,
              "gender": "Female",
              "damageType": "Hero",
              "radius": 1.5,
              "innerRadius": 0.5,
              "sight": 12,
              "speed": 4.5,
              "killXP": 100,
              "attributes": [
                "Attribute1",
                "Attribute2"
              ],
              "scalingLinkIds": [
                "scaling1",
                "scaling2"
              ],
              "defaultMountId": "default_mount",
              "roles": [
                "Tank",
                "Bruiser"
              ],
              "expandedRole": "Main Tank",
              "ratings": {
                "complexity": 5,
                "damage": 7,
                "survivability": 8,
                "utility": 6
              },
              "portraits": {
                "heroSelect": "hero_select.dds",
                "leaderboard": "leaderboard.dds",
                "loading": "loading.dds",
                "partyPanel": "party.dds",
                "target": "target.dds",
                "draftScreen": "draft_screen.dds",
                "partyFrames": [
                  "party_frame1.dds",
                  "party_frame2.dds"
                ],
                "minimap": "minimap_icon.dds",
                "targetInfo": "target_info.dds"
              },
              "searchText": "search keywords",
              "description": "Hero Description",
              "infoText": "Info about hero",
              "life": {
                "amount": 2500,
                "scale": 0.04,
                "regenRate": 5.5,
                "regenScale": 0.05,
                "type": "Health"
              },
              "energy": {
                "amount": 500,
                "regenRate": 10,
                "type": "Mana"
              },
              "shield": {
                "amount": 1000,
                "scale": 0,
                "regenRate": 50,
                "regenScale": 0,
                "regenDelay": 2,
                "type": "Shield"
              },
              "armor": {
                "Hero": {
                  "basic": 20.5,
                  "ability": 10.1,
                  "splash": 100
                }
              },
              "playstyles": [
                "Sustained Damage",
                "Tank"
              ],
              "summonedUnitIds": [
                "summon1",
                "summon2"
              ],
              "weapons": [
                {
                  "nameId": "weapon1",
                  "isDisabled": true,
                  "range": 5.5,
                  "minimumRange": 1,
                  "period": 1,
                  "damage": 100,
                  "damageScale": 0.02,
                  "damageFactors": {
                    "Strength": 1,
                    "Agility": 0.5
                  },
                  "vitalCost": {
                    "Energy": 20
                  }
                }
              ],
              "skinIds": [
                "skin1",
                "skin2"
              ],
              "variationSkinIds": [
                "var1",
                "var2"
              ],
              "voiceLineIds": [
                "voice1",
                "voice2"
              ],
              "mountCategoryIds": [
                "mount_cat1",
                "mount_cat2"
              ],
              "abilities": {
                "Basic": [
                  {
                    "linkId": "ability1|button1|Q",
                    "abilityId": "ability1",
                    "buttonId": "button1",
                    "name": "Ability Name",
                    "icon": "ability_icon.dds",
                    "toggleCooldown": 2,
                    "charges": {
                      "countMax": 2,
                      "countStart": 1,
                      "countUse": 1,
                      "recastCooldown": 3,
                      "isCountHidden": false
                    },
                    "energyText": "30 Mana",
                    "lifeText": "5% Health",
                    "cooldownText": "5 seconds",
                    "shortText": "Ability short text",
                    "fullText": "Ability full description",
                    "abilityType": "Q"
                  }
                ]
              },
              "talents": {
                "Level1": [
                  {
                    "linkId": "talent1|button1|Q|Level1",
                    "talentId": "talent1",
                    "buttonId": "button1",
                    "abilityId": "ability1",
                    "name": "Talent Name",
                    "icon": "talent_icon.dds",
                    "toggleCooldown": 1.5,
                    "charges": {
                      "countMax": 3,
                      "countStart": 2,
                      "countUse": 1,
                      "recastCooldown": 5,
                      "isCountHidden": false
                    },
                    "energyText": "50 Mana",
                    "lifeText": "10% Health",
                    "cooldownText": "8 seconds",
                    "shortText": "Short description",
                    "fullText": "Full description of the talent",
                    "abilityType": "Q",
                    "isQuest": true,
                    "upgradesAbilityType": true,
                    "sort": 1,
                    "abilityTalentLinkIds": [
                      "ability1|button1|Q"
                    ],
                    "tooltipAbilityLinkIds": [
                      "ability1|button1|Q"
                    ],
                    "prerequisiteTalentIds": [
                      "prereq_talent1"
                    ]
                  }
                ]
              },
              "heroUnits": {
                "unit1": {
                  "radius": 0,
                  "innerRadius": 0,
                  "sight": 0,
                  "speed": 0,
                  "portraits": {}
                }
              }
            }
            """);
    }
}
