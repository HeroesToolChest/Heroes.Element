namespace Heroes.Element.JsonConverters.Tests;

[TestClass]
public class TalentLinkIdConverterTests
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public TalentLinkIdConverterTests()
    {
        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Converters =
            {
                new TalentLinkIdConverter(),
            },
        };
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void Read_PropertyIsNullOrEmpty_ReturnsTalentLinkIdAsNull(string? value)
    {
        // arrange
        string json =
        $$"""
        {
          "TalentLinkId": "{{value}}"
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.TalentLinkId.Should().BeNull();
    }

    [TestMethod]
    public void Read_HasFourParts_ReturnsTalentLinkId()
    {
        // arrange
        string json =
        """
        {
          "TalentLinkId": "TalentElement|ButtonElement|Heroic|Level10"
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.TalentLinkId!.ElementId.Should().Be("TalentElement");
        testClass.TalentLinkId.ButtonElementId.Should().Be("ButtonElement");
        testClass.TalentLinkId.AbilityType.Should().Be(AbilityType.Heroic);
        testClass.TalentLinkId.TalentTier.Should().Be(TalentTier.Level10);
    }

    [TestMethod]
    public void Read_HasLessThanFourParts_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "TalentLinkId": "TalentElement|ButtonElement|Q"
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Read_HasMoreThanFourParts_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "TalentLinkId": "A|B|Q|Level1|Extra"
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
          "TalentLinkId": "TalentElement|ButtonElement|InvalidType|Level1"
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Read_FourthPartNotTalentTier_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "TalentLinkId": "TalentElement|ButtonElement|Q|InvalidTier"
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Write_HasTalentLinkId_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            TalentLinkId = new TalentLinkId("TalentElement", "ButtonElement", AbilityType.Heroic, TalentTier.Level10),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "TalentLinkId": "TalentElement|ButtonElement|Heroic|Level10"
        }
        """);
    }

    [TestMethod]
    public void ReadAsPropertyName_HasFourParts_ReturnsTalentLinkId()
    {
        // arrange
        string json =
        """
        {
          "LinkIdMap": {
            "TalentElement|ButtonElement|Q|Level4": "value1"
          }
        }
        """;

        // act
        PropertyNameTestClass testClass = JsonSerializer.Deserialize<PropertyNameTestClass>(json, _jsonSerializerOptions)!;

        // assert
        TalentLinkId expectedKey = new("TalentElement", "ButtonElement", AbilityType.Q, TalentTier.Level4);
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
    public void WriteAsPropertyName_HasTalentLinkId_ReturnsJsonWithPropertyName()
    {
        // arrange
        PropertyNameTestClass testClass = new()
        {
            LinkIdMap = new Dictionary<TalentLinkId, string>
            {
                [new TalentLinkId("TalentElement", "ButtonElement", AbilityType.Q, TalentTier.Level4)] = "value1",
            },
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Contain("\"TalentElement|ButtonElement|Q|Level4\": \"value1\"");
    }

    public class TestClass
    {
        public TalentLinkId? TalentLinkId { get; set; }
    }

    public class PropertyNameTestClass
    {
        public Dictionary<TalentLinkId, string>? LinkIdMap { get; set; }
    }
}
