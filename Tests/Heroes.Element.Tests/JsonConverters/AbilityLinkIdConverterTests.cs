namespace Heroes.Element.JsonConverters.Tests;

[TestClass]
public class AbilityLinkIdConverterTests
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public AbilityLinkIdConverterTests()
    {
        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Converters =
            {
                new AbilityLinkIdConverter(),
            },
        };
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void Read_PropertyIsNullOrEmtpy_ReturnsAbilityLinkIdAsNull(string? value)
    {
        // arrange
        string json =
        $$"""
        {
          "AbilityLinkId": "{{value}}"
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.AbilityLinkId.Should().BeNull();
    }

    [TestMethod]
    public void Read_HasThreeParts_ReturnsAbilityLinkId()
    {
        // arrange
        string json =
        """
        {
          "AbilityLinkId": "AlexstraszaGiftOfLife|AlexstraszaGiftOfLifeButton|Q"
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.AbilityLinkId!.ElementId.Should().Be("AlexstraszaGiftOfLife");
        testClass.AbilityLinkId.ButtonElementId.Should().Be("AlexstraszaGiftOfLifeButton");
        testClass.AbilityLinkId.AbilityType.Should().Be(AbilityType.Q);
    }

    [TestMethod]
    public void Read_HasLessThanThreeParts_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "AbilityLinkId": "AlexstraszaGiftOfLife|AlexstraszaGiftOfLifeButton"
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Read_ThirdPartNotAbilityType_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "AbilityLinkId": "AlexstraszaGiftOfLife|AlexstraszaGiftOfLifeButton|M"
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Read_NotStringType_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "AbilityLinkId": 6
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Write_HasAbilityLinkId_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            AbilityLinkId = new AbilityLinkId("AlexstraszaGiftOfLife", "AlexstraszaGiftOfLifeButton", AbilityType.Q),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "AbilityLinkId": "AlexstraszaGiftOfLife|AlexstraszaGiftOfLifeButton|Q"
        }
        """);
    }

    [TestMethod]
    public void ReadAsPropertyName_HasThreeParts_ReturnsAbilityLinkId()
    {
        // arrange
        string json =
        """
        {
          "LinkIdMap": {
            "AlexstraszaGiftOfLife|AlexstraszaGiftOfLifeButton|Q": "value1"
          }
        }
        """;

        // act
        PropertyNameTestClass testClass = JsonSerializer.Deserialize<PropertyNameTestClass>(json, _jsonSerializerOptions)!;

        // assert
        AbilityLinkId expectedKey = new("AlexstraszaGiftOfLife", "AlexstraszaGiftOfLifeButton", AbilityType.Q);
        testClass.LinkIdMap.Should().ContainKey(expectedKey);
        testClass.LinkIdMap![expectedKey].Should().Be("value1");
    }

    [TestMethod]
    public void ReadAsPropertyName_HasNullOrEmpty_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "LinkIdMap": {
            "": "value1"
          }
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<PropertyNameTestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void WriteAsPropertyName_HasAbilityLinkId_ReturnsJsonWithPropertyName()
    {
        // arrange
        PropertyNameTestClass testClass = new()
        {
            LinkIdMap = new Dictionary<AbilityLinkId, string>
            {
                [new AbilityLinkId("AlexstraszaGiftOfLife", "AlexstraszaGiftOfLifeButton", AbilityType.Q)] = "value1",
            },
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Contain("\"AlexstraszaGiftOfLife|AlexstraszaGiftOfLifeButton|Q\": \"value1\"");
    }

    public class TestClass
    {
        public AbilityLinkId? AbilityLinkId { get; set; }
    }

    public class PropertyNameTestClass
    {
        public Dictionary<AbilityLinkId, string>? LinkIdMap { get; set; }
    }
}
