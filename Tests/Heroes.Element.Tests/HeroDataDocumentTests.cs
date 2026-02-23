namespace Heroes.Element.Tests;

[TestClass]
public class HeroDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {},
      "items": {
        "Abathur": {
          "name": "Abathur",
          "unitId": "HeroAbathur",
          "hyperlinkId": "Abathur",
          "attributeId": "Abat"
        },
        "Alarak": {
          "name": "Alarak",
          "unitId": "HeroAlarak",
          "hyperlinkId": "HeroAlarak(hyperlink)",
          "attributeId": "Alar"
        }
      }
    }
    """;

    [TestMethod]
    public void TryGetHeroByUnitId_ItemsPropertyIsEmpty_ReturnsFalse()
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
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        bool result = heroData.TryGetHeroByUnitId("other", out Hero? hero);

        // assert
        result.Should().BeFalse();
        hero.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_Abathur_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {},
          "items": {
            "Abathur": {
              "name": "Abathur",
              "sortName": "some sortname",
              "unitId": "HeroAbathur",
              "hyperlinkId": "Abathur",
              "attributeId": "Abat",
              "title": "Evolution Master",
              "franchise": "Starcraft",
              "rarity": "Legendary",
              "releaseDate": "2014-03-13",
              "difficulty": "Very Hard",
              "isMelee": true,
              "gender": "Neutral",
              "radius": 0.75,
              "innerRadius": 0.75,
              "sight": 12,
              "speed": 4.8398,
              "defaultMountId": "slime",
              "category": "none",
              "event": "no",
              "attributes": [
                "Heroic"
              ],
              "scalingLinkIds": [
                "HeroDummyVeterancy"
              ],
              "roles": [
                "Specialist"
              ],
              "expandedRole": "Support",
              "ratings": {
                "complexity": 9,
                "damage": 3,
                "survivability": 1,
                "utility": 7
              },
              "portraits": {
                "heroSelect": "storm_ui_ingame_heroselect_btn_infestor.png",
                "leaderboard": "storm_ui_ingame_hero_leaderboard_abathur.png",
                "loading": "storm_ui_ingame_hero_loadingscreen_abathur.png",
                "partyPanel": "storm_ui_ingame_partypanel_btn_abathur.png",
                "target": "ui_targetportrait_hero_abathur.png",
                "draftScreen": "storm_ui_glues_draft_portrait_abathur.png",
                "partyFrames": [
                  "storm_ui_ingame_partyframe_abathur.png"
                ],
                "minimap": "storm_ui_minimapicon_heros_infestor.png",
                "targetInfo": "storm_ui_ingame_partyframe_abathur.png"
              },
              "searchText": "Abathur Zerg Swarm HotS Heart of the Swarm StarCraft II 2 SC2 Star2 Starcraft2 SC slug Double Soak",
              "description": "A unique Hero that can manipulate the battle from anywhere on the map.",
              "infoText": "Abathur, the Evolution Master of Kerrigan's Swarm, works ceaselessly to improve the zerg from the genetic level up. His hate for chaos and imperfection almost rivals his hatred of pronouns.",
              "life": {
                "amount": 685,
                "scale": 0.04,
                "regenRate": 1.4257,
                "regenScale": 0.04,
                "type": "Health"
              },
              "energy": {
                "amount": 500,
                "regenRate": 3,
                "type": "Mana"
              },
              "shield": {
                "amount": 760,
                "scale": 0.04,
                "regenRate": 76,
                "regenScale": 0.04,
                "regenDelay": 5,
                "type": "Shields"
              },
              "armor": {
                "Hero": {
                  "basic": 10,
                  "ability": 5,
                  "splash": 2
                }
              },
              "playstyles": [
                "Ganker",
                "Helper"
              ],
              "summonedUnitIds": [
                "AbathurEvolvedMonstrosity",
                "AbathurLocustAssaultStrain"
              ],
              "weapons": [
                {
                  "nameId": "HeroAbathur",
                  "isDisabled": false,
                  "range": 1,
                  "minimumRange": 0,
                  "period": 0.7,
                  "damage": 26,
                  "damageScale": 0.04
                },
                {
                  "nameId": "testweapon",
                  "isDisabled": true,
                  "range": 1,
                  "minimumRange": 1,
                  "period": 2,
                  "damage": 26,
                  "damageScale": 0.04,
                  "damageFactors": {
                    "MapCreature": 1,
                    "Merc": 1,
                    "Minion": 1,
                    "Structure": 2
                  }
                }
              ],
              "skinIds": [
                "AbathurMecha",
                "AbathurPajamathur"
              ],
              "variationSkinIds": [
                "AbathurBaseVar3",
                "AbathurBone"
              ],
              "voiceLineIds": [
                "AbathurBase_VoiceLine01",
                "AbathurBase_VoiceLine02"
              ],
              "mountCategoryIds": [
                "AlarakTaldarimMarch",
                "Ridesurf"
              ],
              "abilities": {
                "Basic": [
                  {
                    "linkId": "AbathurSymbiote|AbathurSymbiote|Q",
                    "abilityId": "AbathurSymbiote",
                    "buttonId": "AbathurSymbiote",
                    "name": "Symbiote",
                    "icon": "storm_ui_icon_abathur_symbiote.png",
                    "cooldownText": "Cooldown: 4 seconds",
                    "shortText": "Assist an ally and gain new abilities",
                    "fullText": "Spawn and attach a Symbiote to a target ally or Structure. While active, Abathur controls the Symbiote, gaining access to new Abilities. The Symbiote is able to gain XP from nearby enemy deaths.",
                    "abilityType": "Q"
                  },
                  {
                    "linkId": "AbathurToxicNest|AbathurToxicNest|W",
                    "abilityId": "AbathurToxicNest",
                    "buttonId": "AbathurToxicNest",
                    "name": "Toxic Nest",
                    "icon": "storm_ui_icon_abathur_toxicnest.png",
                    "charges": {
                      "countMax": 3,
                      "countStart": 3,
                      "countUse": 1,
                      "recastCooldown": 0.0625,
                      "isCountHidden": false
                    },
                    "lifeText": "<s val=\"StandardTooltipDetails\">Health: </s><s val=\"StandardTooltipDetails\">15%</s>",
                    "energyText": "<s val=\"StandardTooltipDetails\">Mana: 55</s>",
                    "cooldownText": "Charge Cooldown: 10 seconds",
                    "shortText": "Spawn a mine",
                    "fullText": "Spawn a mine that becomes active after a short time. Deals <c val=\"#TooltipNumbers\">153~~0.04~~</c> damage and reveals the enemy for <c val=\"#TooltipNumbers\">4</c> seconds. Lasts <c val=\"#TooltipNumbers\">90</c> seconds.<n/><n/>Stores up to <c val=\"#TooltipNumbers\">3</c> charges.",
                    "abilityType": "W"
                  }
                ],
                "Heroic": [
                  {
                    "linkId": "AbathurEvolveMonstrosity|AbathurEvolveMonstrosityHotbar|Heroic",
                    "abilityId": "AbathurEvolveMonstrosity",
                    "buttonId": "AbathurEvolveMonstrosityHotbar",
                    "name": "Evolve Monstrosity",
                    "icon": "storm_ui_icon_abathur_evolvemonstrosity.png",
                    "cooldownText": "Cooldown: 90 seconds",
                    "shortText": "Minion or Locust becomes a powerful Monstrosity",
                    "fullText": "Turn an allied Minion or Locust into a Monstrosity. When enemy Minions near the Monstrosity die, it gains <c val=\"#TooltipNumbers\">2%</c> Health and <c val=\"#TooltipNumbers\">2%</c> Basic Attack damage, stacking up to <c val=\"#TooltipNumbers\">40</c> times. The Monstrosity can be healed by Carapace and has the ability to Burrow to a visible location every <c val=\"#TooltipNumbers\">80</c> seconds.<n/><n/>Using Symbiote on the Monstrosity allows Abathur to control it, in addition to Symbiote's normal benefits. This Ability can be reactivated to automatically cast Symbiote on his Monstrosity.",
                    "abilityType": "Heroic"
                  },
                  {
                    "linkId": "AbathurUltimateEvolution|AbathurUltimateEvolution|Heroic",
                    "abilityId": "AbathurUltimateEvolution",
                    "buttonId": "AbathurUltimateEvolution",
                    "name": "Ultimate Evolution",
                    "icon": "storm_ui_icon_abathur_ultimateevolution.png",
                    "cooldownText": "Cooldown: 70 seconds",
                    "shortText": "Clone target allied Hero and control it",
                    "fullText": "Clone target allied Hero and control it for <c val=\"#TooltipNumbers\">20</c> seconds. Abathur has perfected the clone, granting it <c val=\"#TooltipNumbers\">20%</c> Spell Power, <c val=\"#TooltipNumbers\">20%</c> bonus Attack Damage, and <c val=\"#TooltipNumbers\">10%</c> bonus Movement Speed. Cannot use their Heroic Ability.",
                    "abilityType": "Heroic"
                  }
                ],
                "Trait": [
                  {
                    "linkId": "AbathurSpawnLocusts|AbathurLocustStrain|Trait",
                    "abilityId": "AbathurSpawnLocusts",
                    "buttonId": "AbathurLocustStrain",
                    "name": "Locust Strain",
                    "icon": "storm_ui_icon_abathur_spawnlocust.png",
                    "cooldownText": "Cooldown: 15 seconds",
                    "shortText": "Spawn locusts that attack down the nearest lane",
                    "fullText": "Spawns a Locust to attack down the nearest lane every <c val=\"#TooltipNumbers\">15</c> seconds. Locusts last for <c val=\"#TooltipNumbers\">16</c> seconds, have <c val=\"#TooltipNumbers\">350~~0.04~~</c> health and deal <c val=\"#TooltipNumbers\">46~~0.04~~</c> damage with each Basic Attack, dealing <c val=\"#TooltipNumbers\">25%</c> bonus damage to enemy Structures.",
                    "abilityType": "Trait"
                  }
                ],
                "Mount": [
                  {
                    "linkId": "AbathurDeepTunnel|AbathurDeepTunnel|Z",
                    "abilityId": "AbathurDeepTunnel",
                    "buttonId": "AbathurDeepTunnel",
                    "name": "Deep Tunnel",
                    "icon": "storm_ui_icon_abathur_mount.png",
                    "cooldownText": "Cooldown: 30 seconds",
                    "shortText": "Tunnel to a location.",
                    "fullText": "Quickly tunnel to a visible location.",
                    "abilityType": "Z"
                  }
                ],
                "Hearth": [
                  {
                    "linkId": "Hearthstone|HearthstoneNoMana|B",
                    "abilityId": "Hearthstone",
                    "buttonId": "HearthstoneNoMana",
                    "name": "Hearthstone",
                    "icon": "storm_ui_icon_miscrune_1.png",
                    "fullText": "After Channeling for <c val=\"#TooltipNumbers\">6</c> seconds, teleport back to the Hall of Storms, instantly restoring <c val=\"#TooltipNumbers\">1125</c> Health.",
                    "abilityType": "B"
                  }
                ]
              },
              "subAbilities": {
                "AbathurEvolveMonstrosity|AbathurEvolveMonstrosityHotbar|Heroic": {
                  "Heroic": [
                    {
                      "linkId": "AbathurEvolveMonstrosityActiveSymbiote|EvolveMonstrosityActiveHotbar|Heroic",
                      "abilityId": "AbathurEvolveMonstrosityActiveSymbiote",
                      "buttonId": "EvolveMonstrosityActiveHotbar",
                      "name": "Evolve Monstrosity Active",
                      "icon": "storm_ui_icon_abathur_evolvemonstrosity.png",
                      "cooldownText": "Cooldown: 4 seconds",
                      "fullText": "Activate to cast Symbiote on Abathur's Monstrosity.",
                      "abilityType": "Heroic"
                    }
                  ]
                },
                "GenericTalentCalldownMULE|GenericCalldownMule|Active|Level7": {
                  "Activable": [
                    {
                      "linkId": "TalentBucketCalldownMule|GenericCalldownMule|Active",
                      "abilityId": "TalentBucketCalldownMule",
                      "buttonId": "GenericCalldownMule",
                      "name": "Calldown: MULE",
                      "icon": "storm_ui_icon_talent_mule.png",
                      "cooldownText": "Cooldown: 60 seconds",
                      "shortText": "Activate to heal Structures",
                      "fullText": "Activate to calldown a Mule that repairs Structures, one at a time, near target point for <c val=\"#TooltipNumbers\">40</c> seconds, healing for <c val=\"#TooltipNumbers\">90</c> Health every <c val=\"#TooltipNumbers\">1</c> second. ",
                      "abilityType": "Active"
                    }
                  ]
                }
              },
              "talents": {
                "Level1": [
                  {
                    "linkId": "AbathurMasteryPressurizedGlands|AbathurSymbiotePressurizedGlandsTalent|W|Level1",
                    "talentId": "AbathurMasteryPressurizedGlands",
                    "buttonId": "AbathurSymbiotePressurizedGlandsTalent",
                    "abilityId": "AbathurSymbioteSpikeBurst",
                    "name": "Pressurized Glands",
                    "icon": "storm_ui_icon_abathur_spikeburst.png",
                    "shortText": "Increases Spike Burst range and decreases cooldown",
                    "fullText": "Increases the range of Symbiote's Spike Burst by <c val=\"#TooltipNumbers\">25%</c> and decreases the cooldown by <c val=\"#TooltipNumbers\">1</c> second.",
                    "abilityType": "W",
                    "isQuest": false,
                    "upgradesAbilityType": true,
                    "sort": 1,
                    "tooltipAbilityLinkIds": [
                      "AbathurSymbiote|AbathurSymbiote|Q",
                      "AbathurSymbioteSpikeBurst|AbathurSymbioteSpikeBurst|W"
                    ]
                  },
                  {
                    "linkId": "AbathurMasteryEnvenomedNestsToxicNest|AbathurToxicNestEnvenomedNestTalent|W|Level1",
                    "talentId": "AbathurMasteryEnvenomedNestsToxicNest",
                    "buttonId": "AbathurToxicNestEnvenomedNestTalent",
                    "abilityId": "AbathurToxicNest",
                    "name": "Envenomed Nest",
                    "icon": "storm_ui_icon_abathur_toxicnest.png",
                    "shortText": "Toxic Nests deal more damage, reduce Armor",
                    "fullText": "Toxic Nests deal <c val=\"#TooltipNumbers\">75%</c> more damage over <c val=\"#TooltipNumbers\">3</c> seconds and reduce the Armor of enemy Heroes hit by <c val=\"#TooltipNumbers\">10</c> for <c val=\"#TooltipNumbers\">4</c> seconds.",
                    "abilityType": "W",
                    "isQuest": false,
                    "upgradesAbilityType": true,
                    "sort": 2,
                    "tooltipAbilityLinkIds": [
                      "AbathurToxicNest|AbathurToxicNest|W"
                    ]
                  },
                  {
                    "linkId": "AbathurReinforcedCarapace|AbathurReinforcedCarapaceTalent|E|Level1",
                    "talentId": "AbathurReinforcedCarapace",
                    "buttonId": "AbathurReinforcedCarapaceTalent",
                    "abilityId": "AbathurSymbioteCarapace",
                    "name": "Reinforced Carapace",
                    "icon": "storm_ui_icon_abathur_carapace.png",
                    "shortText": "Increase Carapace Shield",
                    "fullText": "Increase the Shield amount of Carapace by <c val=\"#TooltipNumbers\">40%</c>.",
                    "abilityType": "E",
                    "isQuest": false,
                    "upgradesAbilityType": true,
                    "sort": 3,
                    "tooltipAbilityLinkIds": [
                      "AbathurSymbiote|AbathurSymbiote|Q",
                      "AbathurSymbioteCarapace|AbathurSymbioteCarapace|E"
                    ]
                  },
                  {
                    "linkId": "AbathurCombatStyleSurvivalInstincts|AbathurLocustStrainSurvivalInstinctsTalent|Trait|Level1",
                    "talentId": "AbathurCombatStyleSurvivalInstincts",
                    "buttonId": "AbathurLocustStrainSurvivalInstinctsTalent",
                    "abilityId": ":PASSIVE:",
                    "name": "Survival Instincts",
                    "icon": "storm_ui_icon_abathur_spawnlocust.png",
                    "shortText": "Increases Locust Health and damage",
                    "fullText": "Increases Locust's Health by <c val=\"#TooltipNumbers\">100%</c> and damage by <c val=\"#TooltipNumbers\">60%</c>.",
                    "abilityType": "Trait",
                    "isQuest": false,
                    "upgradesAbilityType": true,
                    "sort": 4,
                    "tooltipAbilityLinkIds": [
                      "AbathurSpawnLocusts|AbathurLocustStrain|Trait"
                    ]
                  }
                ],
                "Level4": [
                  {
                    "linkId": "AbathurSymbioteAdrenalOverload|AbathurSymbioteAdrenalOverloadTalent|Q|Level4",
                    "talentId": "AbathurSymbioteAdrenalOverload",
                    "buttonId": "AbathurSymbioteAdrenalOverloadTalent",
                    "abilityId": "AbathurSymbiote",
                    "name": "Adrenal Overload",
                    "icon": "storm_ui_icon_abathur_symbiote.png",
                    "shortText": "Symbiote host gains Attack Speed",
                    "fullText": "Heroic Symbiote hosts gain <c val=\"#TooltipNumbers\">25%</c> increased Attack Speed.",
                    "abilityType": "Q",
                    "isQuest": false,
                    "upgradesAbilityType": true,
                    "sort": 1,
                    "tooltipAbilityLinkIds": [
                      "AbathurSymbiote|AbathurSymbiote|Q"
                    ]
                  },
                  {
                    "linkId": "AbathurMasteryNeedlespine|AbathurSymbioteNeedlespineTalent|Q|Level4",
                    "talentId": "AbathurMasteryNeedlespine",
                    "buttonId": "AbathurSymbioteNeedlespineTalent",
                    "abilityId": "AbathurSymbioteStab",
                    "name": "Needlespine",
                    "icon": "storm_ui_icon_abathur_stab.png",
                    "shortText": "Increases Stab damage and range",
                    "fullText": "Increases the damage and range of Symbiote's Stab by <c val=\"#TooltipNumbers\">20%</c>.",
                    "abilityType": "Q",
                    "isQuest": false,
                    "upgradesAbilityType": true,
                    "sort": 2,
                    "tooltipAbilityLinkIds": [
                      "AbathurSymbiote|AbathurSymbiote|Q",
                      "AbathurSymbioteStab|AbathurSymbioteStab|Q"
                    ]
                  },
                  {
                    "linkId": "AbathurMasteryProlificDispersal|AbathurToxicNestProlificDispersalTalent|W|Level4",
                    "talentId": "AbathurMasteryProlificDispersal",
                    "buttonId": "AbathurToxicNestProlificDispersalTalent",
                    "abilityId": "AbathurToxicNest",
                    "name": "Prolific Dispersal",
                    "icon": "storm_ui_icon_abathur_toxicnest.png",
                    "shortText": "Increases Toxic Nest charges, duration",
                    "fullText": "Increase the duration of Toxic Nests by <c val=\"#TooltipNumbers\">45</c> seconds, reduce its cooldown by <c val=\"#TooltipNumbers\">2</c> seconds, and add <c val=\"#TooltipNumbers\">2</c> additional charges.",
                    "abilityType": "W",
                    "isQuest": false,
                    "upgradesAbilityType": true,
                    "sort": 3,
                    "tooltipAbilityLinkIds": [
                      "AbathurToxicNest|AbathurToxicNest|W"
                    ]
                  }
                ]
              },
              "heroUnits": {
                "AbathurSymbiote": {
                  "name": "Symbiote",
                  "radius": 1.5,
                  "innerRadius": 0,
                  "sight": 4,
                  "speed": 0.0117,
                  "attributes": [
                    "IgnoredByTowerAI",
                    "ImmuneToAOE",
                    "ImmuneToFriendlyAbilities",
                    "ImmuneToSkillshots",
                    "NoMinionAggro",
                    "Summoned"
                  ],
                  "description": "Spawn and attach a Symbiote to a target ally or Structure. While active, Abathur controls the Symbiote, gaining access to new Abilities. The Symbiote is able to gain XP from nearby enemy deaths.",
                  "life": {
                    "amount": 1,
                    "scale": 0,
                    "regenRate": 0,
                    "regenScale": 0
                  },
                  "portraits": {
                    "minimap": "storm_ui_minimapicon_abathur_hat.png",
                    "targetInfo": "ui_targetportrait_hero_symbiote.png"
                  },
                  "abilities": {
                    "Basic": [
                      {
                        "linkId": "AbathurSymbioteStab|AbathurSymbioteStab|Q",
                        "abilityId": "AbathurSymbioteStab",
                        "buttonId": "AbathurSymbioteStab",
                        "name": "Stab",
                        "icon": "storm_ui_icon_abathur_stab.png",
                        "charges": {
                          "countMax": 2,
                          "countStart": 2,
                          "countUse": 1,
                          "recastCooldown": 1,
                          "isCountHidden": false
                        },
                        "cooldownText": "Charge Cooldown: 3 seconds",
                        "shortText": "Shoots a spike that deals damage to the first enemy it contacts.",
                        "fullText": "Shoots a spike towards target area that deals <c val=\"#TooltipNumbers\">119~~0.04~~</c> damage to the first enemy it contacts.",
                        "abilityType": "Q"
                      },
                      {
                        "linkId": "AbathurSymbioteSpikeBurst|AbathurSymbioteSpikeBurst|W",
                        "abilityId": "AbathurSymbioteSpikeBurst",
                        "buttonId": "AbathurSymbioteSpikeBurst",
                        "name": "Spike Burst",
                        "icon": "storm_ui_icon_abathur_spikeburst.png",
                        "cooldownText": "Cooldown: 6 seconds",
                        "shortText": "Damage nearby enemies",
                        "fullText": "Deals <c val=\"#TooltipNumbers\">120~~0.04~~</c> damage to nearby enemies.",
                        "abilityType": "W"
                      },
                      {
                        "linkId": "AbathurSymbioteCarapace|AbathurSymbioteCarapace|E",
                        "abilityId": "AbathurSymbioteCarapace",
                        "buttonId": "AbathurSymbioteCarapace",
                        "name": "Carapace",
                        "icon": "storm_ui_icon_abathur_carapace.png",
                        "cooldownText": "Cooldown: 6 seconds",
                        "fullText": "Shields the assisted ally for <c val=\"#TooltipNumbers\">150~~0.04~~</c>. Allied Heroes are healed for <c val=\"#TooltipNumbers\">22~~0.04~~</c> Health per second while the Shield is active. Lasts for <c val=\"#TooltipNumbers\">6</c> seconds.",
                        "abilityType": "E"
                      }
                    ],
                    "Heroic": [
                      {
                        "linkId": "AbathurAssumingDirectControlCancel|AbathurSymbioteCancel|Heroic",
                        "abilityId": "AbathurAssumingDirectControlCancel",
                        "buttonId": "AbathurSymbioteCancel",
                        "name": "Cancel Symbiote",
                        "icon": "hud_btn_bg_ability_cancel.png",
                        "cooldownText": "Cooldown: 1.5 seconds",
                        "fullText": "Cancels the Symbiote ability.",
                        "abilityType": "Heroic"
                      }
                    ],
                    "Mount": [
                      {
                        "linkId": "AbathurMonstrosityDeepTunnel|AbathurMonstrosityDeepTunnel|Z",
                        "abilityId": "AbathurMonstrosityDeepTunnel",
                        "buttonId": "AbathurMonstrosityDeepTunnel",
                        "name": "Deep Tunnel",
                        "icon": "storm_ui_icon_abathur_mount.png",
                        "cooldownText": "Cooldown: 80 seconds",
                        "shortText": "Tunnel to a location.",
                        "fullText": "Order your Evolved Monstrosity to quickly tunnel to a visible location",
                        "abilityType": "Z"
                      }
                    ]
                  }
                }
              }
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // assert
        bool returnResult = heroData.TryGetElementById("Abathur", out Hero? hero);

        // act
        returnResult.Should().BeTrue();
        hero.Should().NotBeNull();
        hero.AttributeId.Should().Be("Abat");
        hero.Attributes.Should().ContainInConsecutiveOrder("Heroic");
        hero.Armor[ArmorSet.Hero].BasicArmor.Should().Be(10);
        hero.Armor[ArmorSet.Hero].AbilityArmor.Should().Be(5);
        hero.Armor[ArmorSet.Hero].SplashArmor.Should().Be(2);
        hero.Category.Should().Be("none");
        hero.DamageType.Should().BeNull();
        hero.DefaultMountId.Should().Be("slime");
        hero.Description!.RawText.Should().Be("A unique Hero that can manipulate the battle from anywhere on the map.");
        hero.Difficulty!.RawText.Should().Be("Very Hard");
        hero.Energy.EnergyMax.Should().Be(500);
        hero.Energy.EnergyRegenerationRate.Should().Be(3);
        hero.Energy.EnergyType!.RawText.Should().Be("Mana");
        hero.Event.Should().Be("no");
        hero.ExpandedRole!.RawText.Should().Be("Support");
        hero.Franchise.Should().Be(Franchise.Starcraft);
        hero.Gender.Should().Be(Gender.Neutral);
        hero.HeroPlayStyles.Should().HaveCount(2)
            .And.ContainInConsecutiveOrder("Ganker", "Helper");
        hero.HeroPortraits.DraftScreen.Should().Be("storm_ui_glues_draft_portrait_abathur.png");
        hero.HeroPortraits.HeroSelectPortrait.Should().Be("storm_ui_ingame_heroselect_btn_infestor.png");
        hero.HeroPortraits.LeaderboardPortrait.Should().Be("storm_ui_ingame_hero_leaderboard_abathur.png");
        hero.HeroPortraits.LoadingScreenPortrait.Should().Be("storm_ui_ingame_hero_loadingscreen_abathur.png");
        hero.HeroPortraits.MiniMapIcon.Should().Be("storm_ui_minimapicon_heros_infestor.png");
        hero.HeroPortraits.PartyFrames.Should().HaveCount(1)
            .And.ContainInConsecutiveOrder("storm_ui_ingame_partyframe_abathur.png");
        hero.HeroPortraits.PartyPanelPortrait.Should().Be("storm_ui_ingame_partypanel_btn_abathur.png");
        hero.HeroPortraits.TargetInfoPanel.Should().Be("storm_ui_ingame_partyframe_abathur.png");
        hero.HeroPortraits.TargetPortrait.Should().Be("ui_targetportrait_hero_abathur.png");
        hero.HyperlinkId.Should().Be("Abathur");
        hero.Id.Should().Be("Abathur");
        hero.InfoText!.RawText.Should().Be("Abathur, the Evolution Master of Kerrigan's Swarm, works ceaselessly to improve the zerg from the genetic level up. His hate for chaos and imperfection almost rivals his hatred of pronouns.");
        hero.InnerRadius.Should().Be(0.75);
        hero.IsMelee.Should().BeTrue();
        hero.KillXP.Should().BeNull();
        hero.Life.LifeMax.Should().Be(685);
        hero.Life.LifeMaxScaling.Should().Be(0.04);
        hero.Life.LifeRegenerationRate.Should().Be(1.4257);
        hero.Life.LifeRegenerationRateScaling.Should().Be(0.04);
        hero.Life.LifeType!.RawText.Should().Be("Health");
        hero.MountCategoryIds.Should().HaveCount(2)
            .And.ContainInConsecutiveOrder("AlarakTaldarimMarch", "Ridesurf");
        hero.Name!.RawText.Should().Be("Abathur");
        hero.Radius.Should().Be(0.75);
        hero.Rarity.Should().Be(Rarity.Legendary);
        hero.Ratings.Complexity.Should().Be(9);
        hero.Ratings.Damage.Should().Be(3);
        hero.Ratings.Survivability.Should().Be(1);
        hero.Ratings.Utility.Should().Be(7);
        hero.ReleaseDate.Should().Be(new DateOnly(2014, 3, 13));
        hero.Roles.Should().ContainSingle();
        hero.Roles.First().RawText.Should().Be("Specialist");
        hero.ScalingLinkIds.Should().ContainSingle()
            .And.ContainInConsecutiveOrder("HeroDummyVeterancy");
        hero.SearchText!.RawText.Should().Be("Abathur Zerg Swarm HotS Heart of the Swarm StarCraft II 2 SC2 Star2 Starcraft2 SC slug Double Soak");
        hero.Shield.ShieldMax.Should().Be(760);
        hero.Shield.ShieldMaxScaling.Should().Be(0.04);
        hero.Shield.ShieldRegenerationDelay.Should().Be(5);
        hero.Shield.ShieldRegenerationRate.Should().Be(76);
        hero.Shield.ShieldRegenerationRateScaling.Should().Be(0.04);
        hero.Sight.Should().Be(12);
        hero.SkinIds.Should().HaveCount(2)
            .And.ContainInConsecutiveOrder("AbathurMecha", "AbathurPajamathur");
        hero.SortName!.RawText.Should().Be("some sortname");
        hero.Speed.Should().Be(4.8398);
        hero.Title!.RawText.Should().Be("Evolution Master");
        hero.UnitId.Should().Be("HeroAbathur");
        hero.VariationSkinIds.Should().HaveCount(2)
            .And.ContainInConsecutiveOrder("AbathurBaseVar3", "AbathurBone");
        hero.VoiceLineIds.Should().HaveCount(2)
            .And.ContainInConsecutiveOrder("AbathurBase_VoiceLine01", "AbathurBase_VoiceLine02");
        hero.Weapons.Should().HaveCount(2);
        hero.Weapons[0].Damage.Should().Be(26);
        hero.Weapons[0].DamageScaling.Should().Be(0.04);
        hero.Weapons[0].IsDisabled.Should().BeFalse();
        hero.Weapons[0].MinimumRange.Should().Be(0);
        hero.Weapons[0].NameId.Should().Be("HeroAbathur");
        hero.Weapons[0].Period.Should().Be(0.7);
        hero.Weapons[0].Range.Should().Be(1);
        hero.Weapons[1].AttacksPerSecond.Should().Be(0.5);
        hero.Weapons[1].NameId.Should().Be("testweapon");
        hero.Weapons[1].IsDisabled.Should().BeTrue();
        hero.Weapons[1].AttributeFactors.Should().HaveCount(4)
            .And.Contain(
                new KeyValuePair<string, double>("MapCreature", 1),
                new KeyValuePair<string, double>("Merc", 1),
                new KeyValuePair<string, double>("Minion", 1),
                new KeyValuePair<string, double>("Structure", 2));

        // abilities
        Ability ability1 = hero.Abilities[AbilityTier.Basic][0];
        ability1.LinkId.Id.Should().Be("AbathurSymbiote|AbathurSymbiote|Q");
        ability1.AbilityElementId.Should().Be("AbathurSymbiote");
        ability1.AbilityType.Should().Be(AbilityType.Q);
        ability1.ButtonElementId.Should().Be("AbathurSymbiote");
        ability1.Charges.Should().BeNull();
        ability1.CooldownText!.RawText.Should().Be("Cooldown: 4 seconds");
        ability1.EnergyText.Should().BeNull();
        ability1.FullText!.RawText.Should().Be("Spawn and attach a Symbiote to a target ally or Structure. While active, Abathur controls the Symbiote, gaining access to new Abilities. The Symbiote is able to gain XP from nearby enemy deaths.");
        ability1.Icon.Should().Be("storm_ui_icon_abathur_symbiote.png");
        ability1.LifeText.Should().BeNull();
        ability1.Name!.RawText.Should().Be("Symbiote");
        ability1.ShortText!.RawText.Should().Be("Assist an ally and gain new abilities");
        ability1.Tier.Should().Be(AbilityTier.Basic);
        ability1.ToggleCooldown.Should().BeNull();

        Ability ability2 = hero.Abilities[AbilityTier.Basic][1];
        ability2.LinkId.Id.Should().Be("AbathurToxicNest|AbathurToxicNest|W");
        ability2.Charges!.CountMax.Should().Be(3);
        ability2.Charges.CountStart.Should().Be(3);
        ability2.Charges.CountUse.Should().Be(1);
        ability2.Charges.HasCharges.Should().BeTrue();
        ability2.Charges.IsCountHidden.Should().BeFalse();
        ability2.Charges.RecastCooldown.Should().Be(0.0625);
        ability2.CooldownText!.RawText.Should().Be("Charge Cooldown: 10 seconds");
        ability2.EnergyText!.RawText.Should().Be("<s val=\"StandardTooltipDetails\">Mana: 55</s>");
        ability2.LifeText!.RawText.Should().Be("<s val=\"StandardTooltipDetails\">Health: </s><s val=\"StandardTooltipDetails\">15%</s>");
        Ability abilityHeroic1 = hero.Abilities[AbilityTier.Heroic][0];
        abilityHeroic1.LinkId.ToString().Should().Be("AbathurEvolveMonstrosity|AbathurEvolveMonstrosityHotbar|Heroic");
        abilityHeroic1.FullText!.RawText.Should().Be("Turn an allied Minion or Locust into a Monstrosity. When enemy Minions near the Monstrosity die, it gains <c val=\"#TooltipNumbers\">2%</c> Health and <c val=\"#TooltipNumbers\">2%</c> Basic Attack damage, stacking up to <c val=\"#TooltipNumbers\">40</c> times. The Monstrosity can be healed by Carapace and has the ability to Burrow to a visible location every <c val=\"#TooltipNumbers\">80</c> seconds.<n/><n/>Using Symbiote on the Monstrosity allows Abathur to control it, in addition to Symbiote's normal benefits. This Ability can be reactivated to automatically cast Symbiote on his Monstrosity.");
        abilityHeroic1.Tier.Should().Be(AbilityTier.Heroic);
        abilityHeroic1.AbilityType.Should().Be(AbilityType.Heroic);

        Ability abilityHeroic2 = hero.Abilities[AbilityTier.Heroic][1];
        abilityHeroic2.LinkId.ToString().Should().Be("AbathurUltimateEvolution|AbathurUltimateEvolution|Heroic");
        abilityHeroic2.Tier.Should().Be(AbilityTier.Heroic);
        abilityHeroic2.AbilityType.Should().Be(AbilityType.Heroic);

        Ability abilityTrait1 = hero.Abilities[AbilityTier.Trait][0];
        abilityTrait1.LinkId.ToString().Should().Be("AbathurSpawnLocusts|AbathurLocustStrain|Trait");
        abilityTrait1.Tier.Should().Be(AbilityTier.Trait);
        abilityTrait1.AbilityType.Should().Be(AbilityType.Trait);

        Ability abilityMount1 = hero.Abilities[AbilityTier.Mount][0];
        abilityMount1.LinkId.ToString().Should().Be("AbathurDeepTunnel|AbathurDeepTunnel|Z");
        abilityMount1.Tier.Should().Be(AbilityTier.Mount);
        abilityMount1.AbilityType.Should().Be(AbilityType.Z);

        Ability abilityHearth1 = hero.Abilities[AbilityTier.Hearth][0];
        abilityHearth1.LinkId.ToString().Should().Be("Hearthstone|HearthstoneNoMana|B");
        abilityHearth1.Tier.Should().Be(AbilityTier.Hearth);
        abilityHearth1.AbilityType.Should().Be(AbilityType.B);

        // subabilities
        Ability subAbility1 = hero.SubAbilities[new AbilityLinkId("AbathurEvolveMonstrosity", "AbathurEvolveMonstrosityHotbar", AbilityType.Heroic)][AbilityTier.Heroic][0];
        subAbility1.LinkId.ToString().Should().Be("AbathurEvolveMonstrosityActiveSymbiote|EvolveMonstrosityActiveHotbar|Heroic");
        subAbility1.Tier.Should().Be(AbilityTier.Heroic);

        Ability subAbility2 = hero.SubAbilities[new TalentLinkId("GenericTalentCalldownMULE", "GenericCalldownMule", AbilityType.Active, TalentTier.Level7)][AbilityTier.Activable][0];
        subAbility2.LinkId.ToString().Should().Be("TalentBucketCalldownMule|GenericCalldownMule|Active");
        subAbility2.Tier.Should().Be(AbilityTier.Activable);

        // talents
        hero.Talents[TalentTier.Level1].Should().HaveCount(4);
        hero.Talents[TalentTier.Level4].Should().HaveCount(3);

        Talent talentLevel1 = hero.Talents[TalentTier.Level1][0];
        talentLevel1.TalentElementId.Should().Be("AbathurMasteryPressurizedGlands");
        talentLevel1.ButtonElementId.Should().Be("AbathurSymbiotePressurizedGlandsTalent");
        talentLevel1.AbilityElementId.Should().Be("AbathurSymbioteSpikeBurst");
        talentLevel1.Name!.RawText.Should().Be("Pressurized Glands");
        talentLevel1.AbilityType.Should().Be(AbilityType.W);
        talentLevel1.Charges.Should().BeNull();
        talentLevel1.CooldownText.Should().BeNull();
        talentLevel1.FullText!.RawText.Should().Be("Increases the range of Symbiote's Spike Burst by <c val=\"#TooltipNumbers\">25%</c> and decreases the cooldown by <c val=\"#TooltipNumbers\">1</c> second.");
        talentLevel1.Icon.Should().Be("storm_ui_icon_abathur_spikeburst.png");
        talentLevel1.LifeText.Should().BeNull();
        talentLevel1.LinkId.Id.Should().Be("AbathurMasteryPressurizedGlands|AbathurSymbiotePressurizedGlandsTalent|W|Level1");
        talentLevel1.LinkId.ToString().Should().Be(talentLevel1.LinkId.Id);
        talentLevel1.Tier.Should().Be(TalentTier.Level1);
        talentLevel1.ToggleCooldown.Should().BeNull();
        talentLevel1.UpgradesAbilityType.Should().BeTrue();
        talentLevel1.IsQuest.Should().BeFalse();
        talentLevel1.TooltipAbilityLinkIds.Should().HaveCount(2)
            .And.ContainInConsecutiveOrder(
                new AbilityLinkId("AbathurSymbiote", "AbathurSymbiote", AbilityType.Q),
                new AbilityLinkId("AbathurSymbioteSpikeBurst", "AbathurSymbioteSpikeBurst", AbilityType.W));

        // hero unit
        hero.HeroUnits.Should().ContainSingle();
        Unit heroUnit = hero.HeroUnits["AbathurSymbiote"];
        heroUnit.Id.Should().Be("AbathurSymbiote");
        heroUnit.Abilities.Should().HaveCount(3);
        heroUnit.Attributes.Should().HaveCount(6);
        heroUnit.Gender.Should().BeNull();
        heroUnit.KillXP.Should().BeNull();
        heroUnit.Sight.Should().Be(4);
        heroUnit.Speed.Should().Be(0.0117);
        heroUnit.UnitPortraits.MiniMapIcon.Should().Be("storm_ui_minimapicon_abathur_hat.png");
        heroUnit.UnitPortraits.TargetInfoPanel.Should().Be("ui_targetportrait_hero_symbiote.png");
    }

    [TestMethod]
    public void TryGetElementByIdd_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        bool result = heroData.TryGetElementById("other", out Hero? hero);

        // assert
        result.Should().BeFalse();
        hero.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        Hero hero = heroData.GetElementById("Alarak");

        // assert
        AlarakBasicAssertions(hero);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        Action act = () => heroData.GetElementById("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetHeroByUnitId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        bool result = heroData.TryGetHeroByUnitId("HeroAlarak", out Hero? hero);

        // assert
        result.Should().BeTrue();
        hero.Should().NotBeNull();

        AlarakBasicAssertions(hero);
    }

    [TestMethod]
    public void TryGetHeroByUnitId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        bool result = heroData.TryGetHeroByUnitId("other", out Hero? hero);

        // assert
        result.Should().BeFalse();
        hero.Should().BeNull();
    }

    [TestMethod]
    public void GetHeroByUnitId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        Hero hero = heroData.GetHeroByUnitId("HeroAlarak");

        // assert
        AlarakBasicAssertions(hero);
    }

    [TestMethod]
    public void GetHeroByUnitId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        Action act = () => heroData.GetHeroByUnitId("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void GetHeroById_WithGameStringDocument_UpdatesGameStrings()
    {
        // arrange
        string jsonData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0"
          },
          "items": {
            "Alarak": {
              "name": "Alarak",
              "unitId": "HeroAlarak",
              "searchText": "Search terms",
              "sortName": "Sort Alarak"
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
              "locale": "FRFR",
              "textType": "RawText",
              "replaceFontStyles": true,
              "preserveFontStyleConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "items": {
            "hero": {
              "name": {
                "Alarak": "Alarak Localized"
              },
              "searchText": {
                "Alarak": "Localized Search Terms"
              },
              "sortName": {
                "Alarak": "Localized Sort Name"
              }
            }
          }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument, gameStringDocument);

        // act
        Hero hero = heroData.GetElementById("Alarak");

        // assert
        hero.Should().NotBeNull();
        hero.Name!.RawText.Should().Be("Alarak Localized");
        hero.Name.GameStringLocale.Should().Be(StormLocale.FRFR);
        hero.SearchText!.RawText.Should().Be("Localized Search Terms");
        hero.SearchText.GameStringLocale.Should().Be(StormLocale.FRFR);
        hero.SortName!.RawText.Should().Be("Localized Sort Name");
        hero.SortName.GameStringLocale.Should().Be(StormLocale.FRFR);
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        bool result = heroData.TryGetElementByHyperlinkId("HeroAlarak(hyperlink)", out Hero? hero);

        // assert
        result.Should().BeTrue();
        hero.Should().NotBeNull();

        AlarakBasicAssertions(hero);
    }

    [TestMethod]
    public void TryGetElementByHyperlinkId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        bool result = heroData.TryGetElementByHyperlinkId("other", out Hero? hero);

        // assert
        result.Should().BeFalse();
        hero.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByHyperlinkId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        Hero hero = heroData.GetElementByHyperlinkId("HeroAlarak(hyperlink)");

        // assert
        AlarakBasicAssertions(hero);
    }

    [TestMethod]
    public void GetElementByHyperlinkId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        Action act = () => heroData.GetElementByHyperlinkId("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void TryGetElementByAttributeId_Found_ReturnsTrue()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        bool result = heroData.TryGetElementByAttributeId("Alar", out Hero? hero);

        // assert
        result.Should().BeTrue();
        hero.Should().NotBeNull();

        AlarakBasicAssertions(hero);
    }

    [TestMethod]
    public void TryGetElementByAttributeId_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        bool result = heroData.TryGetElementByAttributeId("other", out Hero? hero);

        // assert
        result.Should().BeFalse();
        hero.Should().BeNull();
    }

    [TestMethod]
    public void GetElementByAttributeId_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        Hero hero = heroData.GetElementByAttributeId("Alar");

        // assert
        AlarakBasicAssertions(hero);
    }

    [TestMethod]
    public void GetElementByAttributeId_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        Action act = () => heroData.GetElementByAttributeId("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void GetElements_WithItems_ReturnsAllElements()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        List<Hero> result = [.. heroData.GetElements()];

        // assert
        result.Should().HaveCount(2);
        result.Should().Contain(h => h.Id == "Abathur");
        result.Should().Contain(h => h.Id == "Alarak");
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
        HeroDataDocument heroData = HeroDataDocument.Load(jsonDocument);

        // act
        List<Hero> result = [.. heroData.GetElements()];

        // assert
        result.Should().BeEmpty();
    }

    private static void AlarakBasicAssertions(Hero hero)
    {
        hero.Id.Should().Be("Alarak");
        hero.Name!.RawText.Should().Be("Alarak");
        hero.UnitId.Should().Be("HeroAlarak");
        hero.HyperlinkId.Should().Be("HeroAlarak(hyperlink)");
        hero.AttributeId.Should().Be("Alar");
    }
}