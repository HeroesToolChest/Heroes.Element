namespace Heroes.Element.JsonConverters.Tests;

[TestClass]
public class HeroUnitsConverterTests
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public HeroUnitsConverterTests()
    {
        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new GameStringTextConverter(),
                new AbilityLinkIdConverter(),
                new TalentLinkIdConverter(),
            },
        };
    }

    [TestMethod]
    public void Read_HasSingleUnit_ReturnsDictionaryWithIdSetFromKey()
    {
        // arrange
        string json =
        """
        {
          "heroUnits": {
            "UnitId1": {
              "name": "Test Unit"
            }
          }
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.HeroUnits.Should().ContainKey("UnitId1");
        testClass.HeroUnits!["UnitId1"].Id.Should().Be("UnitId1");
        testClass.HeroUnits["UnitId1"].Name!.RawText.Should().Be("Test Unit");
    }

    [TestMethod]
    public void Read_HasMultipleUnits_ReturnsDictionaryWithIdSetOnEachUnit()
    {
        // arrange
        string json =
        """
        {
          "heroUnits": {
            "UnitA": {
              "name": "Unit A"
            },
            "UnitB": {
              "name": "Unit B"
            }
          }
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.HeroUnits.Should().HaveCount(2);
        testClass.HeroUnits!["UnitA"].Id.Should().Be("UnitA");
        testClass.HeroUnits["UnitB"].Id.Should().Be("UnitB");
    }

    [TestMethod]
    public void Read_HasNullValue_ReturnsNull()
    {
        // arrange
        string json =
        """
        {
          "heroUnits": null
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.HeroUnits.Should().BeNull();
    }

    [TestMethod]
    public void Read_HasEmptyObject_ReturnsEmptyDictionary()
    {
        // arrange
        string json =
        """
        {
          "heroUnits": {}
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.HeroUnits.Should().BeEmpty();
    }

    [TestMethod]
    public void Write_HasSingleUnit_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            HeroUnits = new Dictionary<string, Unit>(StringComparer.Ordinal)
            {
                ["UnitId1"] = new Unit("UnitId1")
                {
                    Name = new GameStringText("Test Unit"),
                },
            },
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
            """
            {
              "heroUnits": {
                "UnitId1": {
                  "name": "Test Unit",
                  "gender": null,
                  "damageType": null,
                  "radius": 0,
                  "innerRadius": 0,
                  "sight": 0,
                  "speed": 0,
                  "killXP": null,
                  "attributes": [],
                  "scalingLinkIds": [],
                  "description": null,
                  "life": {
                    "amount": 0,
                    "scale": 0,
                    "regenRate": 0,
                    "regenScale": 0,
                    "type": null
                  },
                  "energy": {
                    "amount": 0,
                    "regenRate": 0,
                    "type": null
                  },
                  "shield": {
                    "amount": 0,
                    "scale": 0,
                    "regenRate": 0,
                    "regenScale": 0,
                    "regenDelay": 0,
                    "type": null
                  },
                  "armor": {},
                  "playstyles": [],
                  "portraits": {
                    "minimap": null,
                    "targetInfo": null
                  },
                  "summonedUnitIds": [],
                  "weapons": [],
                  "abilities": {},
                  "subAbilities": {}
                }
              }
            }
            """);
    }

    [TestMethod]
    public void Write_HasEmptyDictionary_ReturnsEmptyJson()
    {
        // arrange
        TestClass testClass = new()
        {
            HeroUnits = new Dictionary<string, Unit>(StringComparer.Ordinal),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "heroUnits": {}
        }
        """);
    }

    public class TestClass
    {
        [JsonConverter(typeof(HeroUnitsConverter))]
        public IDictionary<string, Unit>? HeroUnits { get; set; }
    }
}
