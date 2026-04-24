namespace Heroes.Element.Tests;

[TestClass]
public class VeterancyDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {
        "itemsType": "Data",
        "dataType": "VeterancyData"
      },
      "items": {
        "TestVeterancy": {
          "combineModifications": true,
          "combineXP": false
        },
        "BasicVeterancy": {
          "combineModifications": false,
          "combineXP": true
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
          "meta": {
            "itemsType": "Data",
            "dataType": "VeterancyData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VeterancyDataDocument veterancyData = VeterancyDataDocument.Load(jsonDocument);

        // act
        bool result = veterancyData.TryGetElementById("other", out Veterancy? veterancy);

        // assert
        result.Should().BeFalse();
        veterancy.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_TestVeterancy_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "VeterancyData"
          },
          "items": {
            "TestVeterancy": {
              "combineModifications": true,
              "combineXP": false,
              "veterancyLevels": [
                {
                  "minimumVeterancyXP": 0,
                  "veterancyModification": {
                    "killXPBonus": 0.5,
                    "vitalMax": {
                      "life": 100,
                      "energy": 50
                    },
                    "vitalMaxFraction": {
                      "life": 0.25,
                      "shield": 0.15
                    }
                  }
                },
                {
                  "minimumVeterancyXP": 100,
                  "veterancyModification": {
                    "killXPBonus": 1.0,
                    "vitalRegen": {
                      "life": 2.5,
                      "energy": 1.0
                    },
                    "damageDealtScaled": {
                      "basic": 10,
                      "ability": 15
                    }
                  }
                },
                {
                  "minimumVeterancyXP": 200,
                  "veterancyModification": {
                    "vitalRegenFraction": {
                      "life": 0.10,
                      "shield": 0.05
                    },
                    "damageDealtFraction": {
                      "basic": 0.20,
                      "ability": 0.25,
                      "splash": 0.15
                    }
                  }
                }
              ]
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VeterancyDataDocument veterancyData = VeterancyDataDocument.Load(jsonDocument);

        // act
        bool returnResult = veterancyData.TryGetElementById("TestVeterancy", out Veterancy? veterancy);

        // assert
        returnResult.Should().BeTrue();
        veterancy.Should().NotBeNull();
        veterancy.Id.Should().Be("TestVeterancy");
        veterancy.CombineModifications.Should().BeTrue();
        veterancy.CombineXP.Should().BeFalse();
        veterancy.VeterancyLevels.Should().HaveCount(3);

        // Level 0 assertions
        veterancy.VeterancyLevels[0].MinimumVeterancyXP.Should().Be(0);
        veterancy.VeterancyLevels[0].VeterancyModification.Should().NotBeNull();
        veterancy.VeterancyLevels[0].VeterancyModification!.KillXpBonus.Should().Be(0.5);
        veterancy.VeterancyLevels[0].VeterancyModification!.VitalMaxValue.Should().NotBeNull();
        veterancy.VeterancyLevels[0].VeterancyModification!.VitalMaxValue!.Life.Should().Be(100);
        veterancy.VeterancyLevels[0].VeterancyModification!.VitalMaxValue!.Energy.Should().Be(50);
        veterancy.VeterancyLevels[0].VeterancyModification!.VitalMaxValue!.Shield.Should().BeNull();
        veterancy.VeterancyLevels[0].VeterancyModification!.VitalMaxFraction.Should().NotBeNull();
        veterancy.VeterancyLevels[0].VeterancyModification!.VitalMaxFraction!.Life.Should().Be(0.25);
        veterancy.VeterancyLevels[0].VeterancyModification!.VitalMaxFraction!.Shield.Should().Be(0.15);

        // Level 1 assertions
        veterancy.VeterancyLevels[1].MinimumVeterancyXP.Should().Be(100);
        veterancy.VeterancyLevels[1].VeterancyModification.Should().NotBeNull();
        veterancy.VeterancyLevels[1].VeterancyModification!.KillXpBonus.Should().Be(1.0);
        veterancy.VeterancyLevels[1].VeterancyModification!.VitalRegeneration.Should().NotBeNull();
        veterancy.VeterancyLevels[1].VeterancyModification!.VitalRegeneration!.Life.Should().Be(2.5);
        veterancy.VeterancyLevels[1].VeterancyModification!.VitalRegeneration!.Energy.Should().Be(1.0);
        veterancy.VeterancyLevels[1].VeterancyModification!.DamageDealtScaled.Should().NotBeNull();
        veterancy.VeterancyLevels[1].VeterancyModification!.DamageDealtScaled!.Basic.Should().Be(10);
        veterancy.VeterancyLevels[1].VeterancyModification!.DamageDealtScaled!.Ability.Should().Be(15);

        // Level 2 assertions
        veterancy.VeterancyLevels[2].MinimumVeterancyXP.Should().Be(200);
        veterancy.VeterancyLevels[2].VeterancyModification.Should().NotBeNull();
        veterancy.VeterancyLevels[2].VeterancyModification!.VitalRegenerationFraction.Should().NotBeNull();
        veterancy.VeterancyLevels[2].VeterancyModification!.VitalRegenerationFraction!.Life.Should().Be(0.10);
        veterancy.VeterancyLevels[2].VeterancyModification!.VitalRegenerationFraction!.Shield.Should().Be(0.05);
        veterancy.VeterancyLevels[2].VeterancyModification!.DamageDealtFraction.Should().NotBeNull();
        veterancy.VeterancyLevels[2].VeterancyModification!.DamageDealtFraction!.Basic.Should().Be(0.20);
        veterancy.VeterancyLevels[2].VeterancyModification!.DamageDealtFraction!.Ability.Should().Be(0.25);
        veterancy.VeterancyLevels[2].VeterancyModification!.DamageDealtFraction!.Splash.Should().Be(0.15);
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VeterancyDataDocument veterancyData = VeterancyDataDocument.Load(jsonDocument);

        // act
        bool result = veterancyData.TryGetElementById("other", out Veterancy? veterancy);

        // assert
        result.Should().BeFalse();
        veterancy.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VeterancyDataDocument veterancyData = VeterancyDataDocument.Load(jsonDocument);

        // act
        Veterancy veterancy = veterancyData.GetElementById("BasicVeterancy");

        // assert
        BasicVeterancyAssertions(veterancy);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VeterancyDataDocument veterancyData = VeterancyDataDocument.Load(jsonDocument);

        // act
        Action act = () => veterancyData.GetElementById("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void GetElements_WithItems_ReturnsAllElements()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VeterancyDataDocument veterancyData = VeterancyDataDocument.Load(jsonDocument);

        // act
        List<Veterancy> result = [.. veterancyData.GetElements()];

        // assert
        result.Should().HaveCount(2);
        result.Should().Contain(v => v.Id == "TestVeterancy");
        result.Should().Contain(v => v.Id == "BasicVeterancy");
    }

    [TestMethod]
    public void GetElements_WithEmptyItems_ReturnsEmpty()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "VeterancyData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VeterancyDataDocument veterancyData = VeterancyDataDocument.Load(jsonDocument);

        // act
        List<Veterancy> result = [.. veterancyData.GetElements()];

        // assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    [DataRow("Unknown")]
    [DataRow("HeroData")]
    [DataRow("UnitData")]
    public void Load_WithMismatchedDataType_ThrowsJsonException(string dataType)
    {
        // arrange
        string json = $$"""
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "{{dataType}}"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);

        // act
        Action act = () => VeterancyDataDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<JsonException>().WithMessage("*does not match the expected data type*");
    }

    [TestMethod]
    public void GetElementObjects_WithItems_ReturnsAllElementsAsObjects()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VeterancyDataDocument veterancyData = VeterancyDataDocument.Load(jsonDocument);

        // act
        List<object> result = [.. veterancyData.GetElementObjects()];

        // assert
        result.Should().HaveCount(2);
        result.Should().AllBeOfType<Veterancy>();
        result.OfType<Veterancy>().Should().Contain(v => v.Id == "TestVeterancy");
        result.OfType<Veterancy>().Should().Contain(v => v.Id == "BasicVeterancy");
    }

    [TestMethod]
    public void GetElementObjects_WithEmptyItems_ReturnsEmpty()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "VeterancyData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VeterancyDataDocument veterancyData = VeterancyDataDocument.Load(jsonDocument);

        // act
        List<object> result = [.. veterancyData.GetElementObjects()];

        // assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void GetElementObjects_WithItems_ReturnsObjectsWithCorrectProperties()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        VeterancyDataDocument veterancyData = VeterancyDataDocument.Load(jsonDocument);

        // act
        List<object> result = [.. veterancyData.GetElementObjects()];

        // assert
        result.Should().HaveCount(2);

        Veterancy basicVeterancy = result.OfType<Veterancy>().First(v => v.Id == "BasicVeterancy");
        BasicVeterancyAssertions(basicVeterancy);
    }

    private static void BasicVeterancyAssertions(Veterancy veterancy)
    {
        veterancy.Id.Should().Be("BasicVeterancy");
        veterancy.CombineModifications.Should().BeFalse();
        veterancy.CombineXP.Should().BeTrue();
    }
}