namespace Heroes.Element.JsonConverters.Tests;

[TestClass]
public class GameStringTextConverterTests
{
    private readonly JsonSerializerOptions _defaultJsonSerializerOptions;

    public GameStringTextConverterTests()
    {
        _defaultJsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters =
            {
                new GameStringTextConverter(),
            },
        };
    }

    [TestMethod]
    public void Read_DefaultOptions_ReturnsGameStringText()
    {
        // arrange
        string json =
        """
        {
          "Text": "value"
        }
        """;

        // act
        TestClass testClass = JsonSerializer.Deserialize<TestClass>(json, _defaultJsonSerializerOptions)!;

        // assert
        testClass.Text!.RawText.Should().Be("value");
    }

    [TestMethod]
    public void Read_NotStringType_ThrowsException()
    {
        // arrange
        string json =
        """
        {
          "Text": 5
        }
        """;

        // act
        Action act = () => JsonSerializer.Deserialize<TestClass>(json, _defaultJsonSerializerOptions);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Write_DefaultOptions_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            Text = new GameStringText("Deals <c val=\"bfd4fd\" hlt-name=\"#TooltipNumbers\">153</c><c val=\"a7a7a7\" hlt-name=\"#ColorGray\">~~0.04~~</c> damage---<s val=\"bfd4fd\" hlt-name=\"StandardTooltipDetails\">Mana: 55</s>"),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, _defaultJsonSerializerOptions);

        // assert
        json.Should().Be(
        """
        {
          "Text": "Deals <c val=\"bfd4fd\" hlt-name=\"#TooltipNumbers\">153</c><c val=\"a7a7a7\" hlt-name=\"#ColorGray\">~~0.04~~</c> damage---<s val=\"bfd4fd\" hlt-name=\"StandardTooltipDetails\">Mana: 55</s>"
        }
        """);
    }

    [TestMethod]
    public void Write_PlainText_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            Text = new GameStringText("Deals</n> <c val=\"bfd4fd\" hlt-name=\"#TooltipNumbers\">153</c><c val=\"a7a7a7\" hlt-name=\"#ColorGray\">~~0.04~~</c> damage---<s val=\"bfd4fd\" hlt-name=\"StandardTooltipDetails\">Mana: 55</s>"),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters =
            {
                new GameStringTextConverter(new GameStringTextConverterOptions()
                {
                     GameStringTextType = GameStringTextType.PlainText,
                }),
            },
        });

        // assert
        json.Should().Be(
        """
        {
          "Text": "Deals  153 damage---Mana: 55"
        }
        """);
    }

    [TestMethod]
    public void Write_PlainTextWithNewlines_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            Text = new GameStringText("Deals</n> <c val=\"bfd4fd\" hlt-name=\"#TooltipNumbers\">153</c><c val=\"a7a7a7\" hlt-name=\"#ColorGray\">~~0.04~~</c> damage---<s val=\"bfd4fd\" hlt-name=\"StandardTooltipDetails\">Mana: 55</s>"),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters =
            {
                new GameStringTextConverter(new GameStringTextConverterOptions()
                {
                     GameStringTextType = GameStringTextType.PlainTextWithNewlines,
                }),
            },
        });

        // assert
        json.Should().Be(
        """
        {
          "Text": "Deals<n/> 153 damage---Mana: 55"
        }
        """);
    }

    [TestMethod]
    public void Write_PlainTextWithScaling_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            Text = new GameStringText("Deals</n> <c val=\"bfd4fd\" hlt-name=\"#TooltipNumbers\">153</c><c val=\"a7a7a7\" hlt-name=\"#ColorGray\">~~0.04~~</c> damage---<s val=\"bfd4fd\" hlt-name=\"StandardTooltipDetails\">Mana: 55</s>"),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters =
            {
                new GameStringTextConverter(new GameStringTextConverterOptions()
                {
                     GameStringTextType = GameStringTextType.PlainTextWithScaling,
                }),
            },
        });

        // assert
        json.Should().Be(
        """
        {
          "Text": "Deals  153 (+4% per level) damage---Mana: 55"
        }
        """);
    }

    [TestMethod]
    public void Write_PlainTextWithScalingWithNewlines_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            Text = new GameStringText("Deals</n> <c val=\"bfd4fd\" hlt-name=\"#TooltipNumbers\">153</c><c val=\"a7a7a7\" hlt-name=\"#ColorGray\">~~0.04~~</c> damage---<s val=\"bfd4fd\" hlt-name=\"StandardTooltipDetails\">Mana: 55</s>"),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters =
            {
                new GameStringTextConverter(new GameStringTextConverterOptions()
                {
                     GameStringTextType = GameStringTextType.PlainTextWithScalingWithNewlines,
                }),
            },
        });

        // assert
        json.Should().Be(
        """
        {
          "Text": "Deals<n/> 153 (+4% per level) damage---Mana: 55"
        }
        """);
    }

    [TestMethod]
    public void Write_ColoredText_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            Text = new GameStringText("Deals</n> <c val=\"bfd4fd\" hlt-name=\"#TooltipNumbers\">153</c><c val=\"a7a7a7\" hlt-name=\"#ColorGray\">~~0.04~~</c> damage---<s val=\"bfd4fd\" hlt-name=\"StandardTooltipDetails\">Mana: 55</s>"),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters =
            {
                new GameStringTextConverter(new GameStringTextConverterOptions()
                {
                     GameStringTextType = GameStringTextType.ColoredText,
                }),
            },
        });

        // assert
        json.Should().Be(
        """
        {
          "Text": "Deals<n/> <c val=\"bfd4fd\" hlt-name=\"#TooltipNumbers\">153</c> damage---<s val=\"bfd4fd\" hlt-name=\"StandardTooltipDetails\">Mana: 55</s>"
        }
        """);
    }

    [TestMethod]
    public void Write_ColoredTextWithScaling_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            Text = new GameStringText("Deals</n> <c val=\"bfd4fd\" hlt-name=\"#TooltipNumbers\">153</c><c val=\"a7a7a7\" hlt-name=\"#ColorGray\">~~0.04~~</c> damage---<s val=\"bfd4fd\" hlt-name=\"StandardTooltipDetails\">Mana: 55</s>"),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters =
            {
                new GameStringTextConverter(new GameStringTextConverterOptions()
                {
                     GameStringTextType = GameStringTextType.ColoredTextWithScaling,
                }),
            },
        });

        // assert
        json.Should().Be(
        """
        {
          "Text": "Deals<n/> <c val=\"bfd4fd\" hlt-name=\"#TooltipNumbers\">153</c><c val=\"a7a7a7\" hlt-name=\"#ColorGray\"> (+4% per level)</c> damage---<s val=\"bfd4fd\" hlt-name=\"StandardTooltipDetails\">Mana: 55</s>"
        }
        """);
    }

    [TestMethod]
    public void Write_HltRemoveForConstants_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            Text = new GameStringText("Deals</n> <c val=\"bfd4fd\" hlt-name=\"#TooltipNumbers\">153</c><c val=\"a7a7a7\" hlt-name=\"#ColorGray\">~~0.04~~</c> damage---<s val=\"bfd4fd\" hlt-name=\"StandardTooltipDetails\">Mana: 55</s>"),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters =
            {
                new GameStringTextConverter(new GameStringTextConverterOptions()
                {
                     RemoveHltForConstantTags = GameStringTextHltRemoveMode.Remove,
                }),
            },
        });

        // assert
        json.Should().Be(
        """
        {
          "Text": "Deals<n/> <c val=\"bfd4fd\">153</c><c val=\"a7a7a7\">~~0.04~~</c> damage---<s val=\"bfd4fd\" hlt-name=\"StandardTooltipDetails\">Mana: 55</s>"
        }
        """);
    }

    [TestMethod]
    public void Write_HltRemoveAndUndoForConstants_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            Text = new GameStringText("Deals <c val=\"bfd4fd\" hlt-name=\"#TooltipNumbers\">153</c><c val=\"a7a7a7\" hlt-name=\"#ColorGray\">~~0.04~~</c> damage---<s val=\"bfd4fd\" hlt-name=\"StandardTooltipDetails\">Mana: 55</s>"),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters =
            {
                new GameStringTextConverter(new GameStringTextConverterOptions()
                {
                     RemoveHltForConstantTags = GameStringTextHltRemoveMode.RemoveAndUndo,
                }),
            },
        });

        // assert
        json.Should().Be(
        """
        {
          "Text": "Deals <c val=\"#TooltipNumbers\">153</c><c val=\"#ColorGray\">~~0.04~~</c> damage---<s val=\"bfd4fd\" hlt-name=\"StandardTooltipDetails\">Mana: 55</s>"
        }
        """);
    }

    [TestMethod]
    public void Write_HltRemoveForStyle_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            Text = new GameStringText("Deals <c val=\"bfd4fd\" hlt-name=\"#TooltipNumbers\">153</c><c val=\"a7a7a7\" hlt-name=\"#ColorGray\">~~0.04~~</c> damage---<s val=\"bfd4fd\" hlt-name=\"StandardTooltipDetails\">Mana: 55</s>"),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters =
            {
                new GameStringTextConverter(new GameStringTextConverterOptions()
                {
                     RemoveHltForStyleTags = GameStringTextHltRemoveMode.Remove,
                }),
            },
        });

        // assert
        json.Should().Be(
        """
        {
          "Text": "Deals <c val=\"bfd4fd\" hlt-name=\"#TooltipNumbers\">153</c><c val=\"a7a7a7\" hlt-name=\"#ColorGray\">~~0.04~~</c> damage---<s val=\"bfd4fd\">Mana: 55</s>"
        }
        """);
    }

    [TestMethod]
    public void Write_HltRemoveAndUndoForStyle_ReturnsJson()
    {
        // arrange
        TestClass testClass = new()
        {
            Text = new GameStringText("Deals <c val=\"bfd4fd\" hlt-name=\"#TooltipNumbers\">153</c><c val=\"a7a7a7\" hlt-name=\"#ColorGray\">~~0.04~~</c> damage---<s val=\"bfd4fd\" hlt-name=\"StandardTooltipDetails\">Mana: 55</s>"),
        };

        // act
        string json = JsonSerializer.Serialize(testClass, new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters =
            {
                new GameStringTextConverter(new GameStringTextConverterOptions()
                {
                     RemoveHltForStyleTags = GameStringTextHltRemoveMode.RemoveAndUndo,
                }),
            },
        });

        // assert
        json.Should().Be(
        """
        {
          "Text": "Deals <c val=\"bfd4fd\" hlt-name=\"#TooltipNumbers\">153</c><c val=\"a7a7a7\" hlt-name=\"#ColorGray\">~~0.04~~</c> damage---<s val=\"StandardTooltipDetails\">Mana: 55</s>"
        }
        """);
    }

    public class TestClass
    {
        public GameStringText? Text { get; set; }
    }
}