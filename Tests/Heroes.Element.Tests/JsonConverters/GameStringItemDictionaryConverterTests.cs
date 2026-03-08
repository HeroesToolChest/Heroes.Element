namespace Heroes.Element.JsonConverters.Tests;

[TestClass]
public class GameStringItemDictionaryConverterTests
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public GameStringItemDictionaryConverterTests()
    {
        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Converters =
            {
                new GameStringItemDictionaryConverter(),
                new GameStringTextConverter(),
            },
        };
    }

    [TestMethod]
    public void Read_HasKeyValuePairs_ReturnsDictionary()
    {
        // arrange
        string json =
        """
        {
          "Dictionary": {
            "Item1": {
              "Name": {
                "Id1": "some text"
              }
            }
          }
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Dictionary.Should().ContainKey("Item1");
        testClass.Dictionary!["Item1"].Should().ContainKey("Name");
        testClass.Dictionary["Item1"]["Name"].KeyValuePairs.Should().ContainKey("Id1");
        testClass.Dictionary["Item1"]["Name"].KeyValuePairs["Id1"].RawText.Should().Be("some text");
    }

    [TestMethod]
    public void Read_HasKeyArrayPairs_ReturnsDictionary()
    {
        // arrange
        string json =
        """
        {
          "Dictionary": {
            "Item1": {
              "Name": {
                "Id1": ["text one", "text two"]
              }
            }
          }
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Dictionary.Should().ContainKey("Item1");
        testClass.Dictionary!["Item1"]["Name"].KeyArrayPairs.Should().ContainKey("Id1");
        testClass.Dictionary["Item1"]["Name"].KeyArrayPairs["Id1"].Should().HaveCount(2);
        testClass.Dictionary["Item1"]["Name"].KeyArrayPairs["Id1"][0].RawText.Should().Be("text one");
        testClass.Dictionary["Item1"]["Name"].KeyArrayPairs["Id1"][1].RawText.Should().Be("text two");
    }

    [TestMethod]
    public void Read_HasMultipleItems_ReturnsDictionary()
    {
        // arrange
        string json =
        """
        {
          "Dictionary": {
            "Item1": {
              "Name": {
                "Id1": "first"
              }
            },
            "Item2": {
              "Description": {
                "Id2": "second"
              }
            }
          }
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Dictionary.Should().HaveCount(2);
        testClass.Dictionary!["Item1"]["Name"].KeyValuePairs["Id1"].RawText.Should().Be("first");
        testClass.Dictionary["Item2"]["Description"].KeyValuePairs["Id2"].RawText.Should().Be("second");
    }

    [TestMethod]
    public void Read_EmptyObject_ReturnsEmptyDictionary()
    {
        // arrange
        string json =
        """
        {
          "Dictionary": {}
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions)!;

        // assert
        testClass.Dictionary.Should().BeEmpty();
    }

    [TestMethod]
    public void Read_NotObject_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "Dictionary": "invalid"
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Read_ItemValueNotObject_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "Dictionary": {
            "Item1": "invalid"
          }
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Read_PropertyNameValueNotObject_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "Dictionary": {
            "Item1": {
              "Name": "invalid"
            }
          }
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _jsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Write_HasKeyValuePairs_ReturnsJson()
    {
        // arrange
        GameStringFilePropertyId propertyId = new();
        propertyId.KeyValuePairs["Id1"] = new GameStringText("some text", StormLocale.ENUS);

        GameStringFilePropertyName propertyName = [];
        propertyName["Name"] = propertyId;

        GameStringItemDictionary dictionary = [];
        dictionary["Item1"] = propertyName;

        TestClass testClass = new()
        {
            Dictionary = dictionary,
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "Dictionary": {
            "Item1": {
              "Name": {
                "Id1": "some text"
              }
            }
          }
        }
        """);
    }

    [TestMethod]
    public void Write_HasKeyArrayPairs_ReturnsJson()
    {
        // arrange
        GameStringFilePropertyId propertyId = new();
        propertyId.KeyArrayPairs["Id1"] =
        [
            new GameStringText("text one", StormLocale.ENUS),
            new GameStringText("text two", StormLocale.ENUS),
        ];

        GameStringFilePropertyName propertyName = [];
        propertyName["Name"] = propertyId;

        GameStringItemDictionary dictionary = [];
        dictionary["Item1"] = propertyName;

        TestClass testClass = new()
        {
            Dictionary = dictionary,
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "Dictionary": {
            "Item1": {
              "Name": {
                "Id1": [
                  "text one",
                  "text two"
                ]
              }
            }
          }
        }
        """);
    }

    [TestMethod]
    public void Write_EmptyDictionary_ReturnsEmptyJson()
    {
        // arrange
        TestClass testClass = new()
        {
            Dictionary = [],
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "Dictionary": {}
        }
        """);
    }

    [TestMethod]
    public void Write_HasKeyArrayPairsOverKeyValuePairs_WritesKeyArrayPairs()
    {
        // arrange
        GameStringFilePropertyId propertyId = new();
        propertyId.KeyValuePairs["Id1"] = new GameStringText("single text", StormLocale.ENUS);
        propertyId.KeyArrayPairs["Id2"] =
        [
            new GameStringText("array text", StormLocale.ENUS),
        ];

        GameStringFilePropertyName propertyName = [];
        propertyName["Name"] = propertyId;

        GameStringItemDictionary dictionary = [];
        dictionary["Item1"] = propertyName;

        TestClass testClass = new()
        {
            Dictionary = dictionary,
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _jsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "Dictionary": {
            "Item1": {
              "Name": {
                "Id2": [
                  "array text"
                ]
              }
            }
          }
        }
        """);
    }

    public class TestClass
    {
        public GameStringItemDictionary? Dictionary { get; set; }
    }
}
