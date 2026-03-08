namespace Heroes.Element.JsonConverters.Tests;

[TestClass]
public class LinkIdConverterTests
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public LinkIdConverterTests()
    {
        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Converters =
            {
                new LinkIdConverter(),
            },
        };
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void Read_PropertyIsNullOrEmpty_ReturnsNull(string? value)
    {
        // arrange
        string json =
        $$"""
        {
          "LinkId": "{{value}}"
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.LinkId.Should().BeNull();
    }

    [TestMethod]
    public void Read_HasThreeParts_ReturnsAbilityLinkId()
    {
        // arrange
        string json =
        """
        {
          "LinkId": "AbilityElement|ButtonElement|Q"
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.LinkId.Should().BeOfType<AbilityLinkId>();
        testClass.LinkId!.ElementId.Should().Be("AbilityElement");
        testClass.LinkId.ButtonElementId.Should().Be("ButtonElement");
        testClass.LinkId.AbilityType.Should().Be(AbilityType.Q);
    }

    [TestMethod]
    public void Read_HasFourParts_ReturnsTalentLinkId()
    {
        // arrange
        string json =
        """
        {
          "LinkId": "TalentElement|ButtonElement|Heroic|Level10"
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        TalentLinkId talentLinkId = testClass.LinkId.Should().BeOfType<TalentLinkId>().Subject;
        talentLinkId.ElementId.Should().Be("TalentElement");
        talentLinkId.ButtonElementId.Should().Be("ButtonElement");
        talentLinkId.AbilityType.Should().Be(AbilityType.Heroic);
        talentLinkId.TalentTier.Should().Be(TalentTier.Level10);
    }

    [TestMethod]
    public void Read_HasThreePartsWithInvalidAbilityType_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "LinkId": "Element|Button|InvalidType"
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Read_HasFourPartsWithInvalidAbilityType_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "LinkId": "Element|Button|InvalidType|Level1"
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Read_HasFourPartsWithInvalidTalentTier_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "LinkId": "Element|Button|Q|InvalidTier"
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Read_HasLessThanThreeParts_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "LinkId": "Element|Button"
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
          "LinkId": "A|B|Q|Level1|Extra"
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
          "LinkId": 6
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
            LinkId = new AbilityLinkId("AbilityElement", "ButtonElement", AbilityType.W),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "LinkId": "AbilityElement|ButtonElement|W"
        }
        """);
    }

    [TestMethod]
    public void Write_HasTalentLinkId_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            LinkId = new TalentLinkId("TalentElement", "ButtonElement", AbilityType.Heroic, TalentTier.Level10),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "LinkId": "TalentElement|ButtonElement|Heroic|Level10"
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
            "AbilityElement|ButtonElement|E": "value1"
          }
        }
        """;

        // act
        PropertyNameTestClass testClass = JsonSerializer.Deserialize<PropertyNameTestClass>(json, _jsonSerializerOptions)!;

        // assert
        AbilityLinkId expectedKey = new("AbilityElement", "ButtonElement", AbilityType.E);
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
            LinkIdMap = new Dictionary<LinkId, string>
            {
                [new AbilityLinkId("AbilityElement", "ButtonElement", AbilityType.Trait)] = "value1",
            },
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Contain("\"AbilityElement|ButtonElement|Trait\": \"value1\"");
    }

    public class TestClass
    {
        public LinkId? LinkId { get; set; }
    }

    public class PropertyNameTestClass
    {
        public Dictionary<LinkId, string>? LinkIdMap { get; set; }
    }
}
