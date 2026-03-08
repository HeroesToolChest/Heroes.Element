namespace Heroes.Element.JsonConverters.Tests;

[TestClass]
public class DoubleRoundingConverterTests
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public DoubleRoundingConverterTests()
    {
        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Converters =
            {
                new DoubleRoundingConverter(),
            },
        };
    }

    [TestMethod]
    public void Read_HasDoubleValue_ReturnsDouble()
    {
        // arrange
        string json =
        """
        {
          "Value": 1.23456789
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Value.Should().Be(1.23456789);
    }

    [TestMethod]
    public void Read_HasIntegerValue_ReturnsDouble()
    {
        // arrange
        string json =
        """
        {
          "Value": 5
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Value.Should().Be(5.0);
    }

    [TestMethod]
    public void Read_HasZeroValue_ReturnsZero()
    {
        // arrange
        string json =
        """
        {
          "Value": 0
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Value.Should().Be(0.0);
    }

    [TestMethod]
    public void Write_HasValueWithMoreThanFourDecimals_ReturnsJsonRoundedToFourDecimals()
    {
        // arrange
        TestClass testClass = new()
        {
            Value = 1.23456789,
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "Value": 1.2346
        }
        """);
    }

    [TestMethod]
    public void Write_HasValueWithExactlyFourDecimals_ReturnsJsonUnchanged()
    {
        // arrange
        TestClass testClass = new()
        {
            Value = 1.2345,
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "Value": 1.2345
        }
        """);
    }

    [TestMethod]
    public void Write_HasValueWithFewerThanFourDecimals_ReturnsJsonUnchanged()
    {
        // arrange
        TestClass testClass = new()
        {
            Value = 1.5,
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "Value": 1.5
        }
        """);
    }

    [TestMethod]
    public void Write_HasZeroValue_ReturnsJsonWithZero()
    {
        // arrange
        TestClass testClass = new()
        {
            Value = 0.0,
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "Value": 0
        }
        """);
    }

    [TestMethod]
    public void Write_HasNegativeValueWithMoreThanFourDecimals_ReturnsJsonRoundedToFourDecimals()
    {
        // arrange
        TestClass testClass = new()
        {
            Value = -3.141592653,
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "Value": -3.1416
        }
        """);
    }

    public class TestClass
    {
        public double Value { get; set; }
    }
}
