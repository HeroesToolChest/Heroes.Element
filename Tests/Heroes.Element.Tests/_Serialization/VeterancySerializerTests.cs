namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class VeterancySerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        Veterancy veterancy = new("veterancy_id")
        {
            // Veterancy properties
            CombineModifications = true,
            CombineXP = false,
            VeterancyLevels =
            [
                new VeterancyLevel
                {
                    MinimumVeterancyXP = 0,
                    VeterancyModification = new VeterancyModification
                    {
                        KillXpBonus = 10.0,
                        VitalMaxValue = new VeterancyVitalType
                        {
                            Life = 100.0,
                            Energy = 50.0,
                            Shield = 25.0,
                        },
                        VitalMaxFraction = new VeterancyVitalType
                        {
                            Life = 0.05,
                            Energy = 0.03,
                            Shield = 0.02,
                        },
                        VitalRegeneration = new VeterancyVitalType
                        {
                            Life = 2.5,
                            Energy = 1.5,
                            Shield = 1.0,
                        },
                        VitalRegenerationFraction = new VeterancyVitalType
                        {
                            Life = 0.01,
                            Energy = 0.02,
                            Shield = 0.015,
                        },
                        DamageDealtScaled = new VeterancyDamageType
                        {
                            Basic = 15.0,
                            Ability = 20.0,
                            Splash = 10.0,
                        },
                        DamageDealtFraction = new VeterancyDamageType
                        {
                            Basic = 0.04,
                            Ability = 0.05,
                            Splash = 0.03,
                        },
                    },
                },
                new VeterancyLevel
                {
                    MinimumVeterancyXP = 500,
                    VeterancyModification = new VeterancyModification
                    {
                        KillXpBonus = 20.0,
                        VitalMaxValue = new VeterancyVitalType
                        {
                            Life = 200.0,
                            Energy = 100.0,
                            Shield = 50.0,
                        },
                        VitalMaxFraction = new VeterancyVitalType
                        {
                            Life = 0.1,
                            Energy = 0.06,
                            Shield = 0.04,
                        },
                        VitalRegeneration = new VeterancyVitalType
                        {
                            Life = 5.0,
                            Energy = 3.0,
                            Shield = 2.0,
                        },
                        VitalRegenerationFraction = new VeterancyVitalType
                        {
                            Life = 0.02,
                            Energy = 0.04,
                            Shield = 0.03,
                        },
                        DamageDealtScaled = new VeterancyDamageType
                        {
                            Basic = 30.0,
                            Ability = 40.0,
                            Splash = 20.0,
                        },
                        DamageDealtFraction = new VeterancyDamageType
                        {
                            Basic = 0.08,
                            Ability = 0.1,
                            Splash = 0.06,
                        },
                    },
                },
                new VeterancyLevel
                {
                    MinimumVeterancyXP = 1000,
                    VeterancyModification = new VeterancyModification
                    {
                        KillXpBonus = 30.0,
                        VitalMaxValue = new VeterancyVitalType
                        {
                            Life = 300.0,
                            Energy = 150.0,
                            Shield = 75.0,
                        },
                        VitalMaxFraction = new VeterancyVitalType
                        {
                            Life = 0.15,
                            Energy = 0.09,
                            Shield = 0.06,
                        },
                        VitalRegeneration = new VeterancyVitalType
                        {
                            Life = 7.5,
                            Energy = 4.5,
                            Shield = 3.0,
                        },
                        VitalRegenerationFraction = new VeterancyVitalType
                        {
                            Life = 0.03,
                            Energy = 0.06,
                            Shield = 0.045,
                        },
                        DamageDealtScaled = new VeterancyDamageType
                        {
                            Basic = 45.0,
                            Ability = 60.0,
                            Splash = 30.0,
                        },
                        DamageDealtFraction = new VeterancyDamageType
                        {
                            Basic = 0.12,
                            Ability = 0.15,
                            Splash = 0.09,
                        },
                    },
                },
            ],
        };

        // act
        string json = JsonSerializer.Serialize(veterancy, SerializerSettings.GetJsonSerializerDataOptions());

        // assert
        json.Should().Be(
            """
            {
              "combineModifications": true,
              "combineXP": false,
              "veterancyLevels": [
                {
                  "minimumVeterancyXP": 0,
                  "veterancyModification": {
                    "killXPBonus": 10,
                    "vitalMax": {
                      "life": 100,
                      "energy": 50,
                      "shield": 25
                    },
                    "vitalMaxFraction": {
                      "life": 0.05,
                      "energy": 0.03,
                      "shield": 0.02
                    },
                    "vitalRegen": {
                      "life": 2.5,
                      "energy": 1.5,
                      "shield": 1
                    },
                    "vitalRegenFraction": {
                      "life": 0.01,
                      "energy": 0.02,
                      "shield": 0.015
                    },
                    "damageDealtScaled": {
                      "basic": 15,
                      "ability": 20,
                      "splash": 10
                    },
                    "damageDealtFraction": {
                      "basic": 0.04,
                      "ability": 0.05,
                      "splash": 0.03
                    }
                  }
                },
                {
                  "minimumVeterancyXP": 500,
                  "veterancyModification": {
                    "killXPBonus": 20,
                    "vitalMax": {
                      "life": 200,
                      "energy": 100,
                      "shield": 50
                    },
                    "vitalMaxFraction": {
                      "life": 0.1,
                      "energy": 0.06,
                      "shield": 0.04
                    },
                    "vitalRegen": {
                      "life": 5,
                      "energy": 3,
                      "shield": 2
                    },
                    "vitalRegenFraction": {
                      "life": 0.02,
                      "energy": 0.04,
                      "shield": 0.03
                    },
                    "damageDealtScaled": {
                      "basic": 30,
                      "ability": 40,
                      "splash": 20
                    },
                    "damageDealtFraction": {
                      "basic": 0.08,
                      "ability": 0.1,
                      "splash": 0.06
                    }
                  }
                },
                {
                  "minimumVeterancyXP": 1000,
                  "veterancyModification": {
                    "killXPBonus": 30,
                    "vitalMax": {
                      "life": 300,
                      "energy": 150,
                      "shield": 75
                    },
                    "vitalMaxFraction": {
                      "life": 0.15,
                      "energy": 0.09,
                      "shield": 0.06
                    },
                    "vitalRegen": {
                      "life": 7.5,
                      "energy": 4.5,
                      "shield": 3
                    },
                    "vitalRegenFraction": {
                      "life": 0.03,
                      "energy": 0.06,
                      "shield": 0.045
                    },
                    "damageDealtScaled": {
                      "basic": 45,
                      "ability": 60,
                      "splash": 30
                    },
                    "damageDealtFraction": {
                      "basic": 0.12,
                      "ability": 0.15,
                      "splash": 0.09
                    }
                  }
                }
              ]
            }
            """);
    }
}
