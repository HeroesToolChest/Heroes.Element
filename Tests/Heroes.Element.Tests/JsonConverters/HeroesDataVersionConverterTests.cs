namespace Heroes.Element.JsonConverters.Tests;

[TestClass]
public class HeroesDataVersionConverterTests
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public HeroesDataVersionConverterTests()
    {
        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Converters =
            {
                new HeroesDataVersionConverter(),
            },
        };
    }

    [TestMethod]
    public void Read_HasValidVersionString_ReturnsHeroesDataVersion()
    {
        // arrange
        string json =
        """
        {
          "Version": "2.55.3.90670"
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Version.Should().NotBeNull();
        testClass.Version!.Major.Should().Be(2);
        testClass.Version.Minor.Should().Be(55);
        testClass.Version.Revision.Should().Be(3);
        testClass.Version.Build.Should().Be(90670);
        testClass.Version.IsPtr.Should().BeFalse();
    }

    [TestMethod]
    public void Read_HasValidPtrVersionString_ReturnsHeroesDataVersionWithIsPtr()
    {
        // arrange
        string json =
        """
        {
          "Version": "2.55.3.90670_ptr"
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Version.Should().NotBeNull();
        testClass.Version!.Major.Should().Be(2);
        testClass.Version.Minor.Should().Be(55);
        testClass.Version.Revision.Should().Be(3);
        testClass.Version.Build.Should().Be(90670);
        testClass.Version.IsPtr.Should().BeTrue();
    }

    [TestMethod]
    public void Read_HasNullValue_ReturnsNull()
    {
        // arrange
        string json =
        """
        {
          "Version": null
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Version.Should().BeNull();
    }

    [TestMethod]
    public void Read_NotStringType_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "Version": 12345
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Read_HasInvalidVersionFormat_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "Version": "notaversion"
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Read_HasIncompleteVersionString_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "Version": "2.55.3"
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Write_HasHeroesDataVersion_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            Version = new HeroesDataVersion(2, 55, 3, 90670),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "Version": "2.55.3.90670"
        }
        """);
    }

    [TestMethod]
    public void Write_HasPtrHeroesDataVersion_ReturnsJsonWithPtr()
    {
        // arrange
        TestClass testClass = new()
        {
            Version = new HeroesDataVersion(2, 55, 3, 90670, true),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "Version": "2.55.3.90670_ptr"
        }
        """);
    }

    public class TestClass
    {
        public HeroesDataVersion? Version { get; set; }
    }
}
