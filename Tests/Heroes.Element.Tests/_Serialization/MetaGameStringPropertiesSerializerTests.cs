namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class MetaGameStringPropertiesSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaGameStringProperties metaGameStringProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623, true),
            HdpVersion = "5.0.0",
            ItemsType = ItemsType.GameStrings,
            DataTypes = [DataType.HeroData, DataType.UnitData],
            MapName = "Alterac Pass",
            GameStringTextProperties = new GameStringTextProperties()
            {
                Locale = StormLocale.ENUS,
                GameStringTextType = GameStringTextType.RawText,
                ConstantVars = new ConstantVars()
                {
                    Replaced = true,
                    Preserved = true,
                },
                StyleVars = new StyleVars()
                {
                    Replaced = false,
                    Preserved = false,
                },
            },
        };

        // act
        string json = JsonSerializer.Serialize(metaGameStringProperties, options);

        // assert
        json.Should().Be(
            """
            {
              "heroesVersion": "2.55.14.95623_ptr",
              "hdpVersion": "5.0.0",
              "itemsType": "GameStrings",
              "dataTypes": [
                "HeroData",
                "UnitData"
              ],
              "mapName": "Alterac Pass",
              "gameStringText": {
                "locale": "ENUS",
                "textType": "RawText",
                "constantVars": {
                  "replaced": true,
                  "preserved": true
                },
                "styleVars": {
                  "replaced": false,
                  "preserved": false
                }
              }
            }
            """);
    }

    [TestMethod]
    public void Serialize_DefaultValues_ReturnsJsonWithDefaults()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaGameStringProperties metaGameStringProperties = new();

        // act
        string json = JsonSerializer.Serialize(metaGameStringProperties, options);

        // assert
        json.Should().Be(
            """
            {
              "heroesVersion": "-1.-1.-1.-1",
              "hdpVersion": "",
              "itemsType": "Other",
              "dataTypes": [],
              "gameStringText": {
                "locale": "ENUS",
                "constantVars": {
                  "replaced": false,
                  "preserved": false
                },
                "styleVars": {
                  "replaced": false,
                  "preserved": false
                }
              }
            }
            """);
    }

    [TestMethod]
    public void Serialize_WithMapName_ReturnsJsonWithMapName()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaGameStringProperties metaGameStringProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623),
            HdpVersion = "5.0.0",
            DataTypes = [DataType.MapData],
            MapName = "Battlefield of Eternity",
        };

        // act
        string json = JsonSerializer.Serialize(metaGameStringProperties, options);

        // assert
        json.Should().Contain("\"mapName\": \"Battlefield of Eternity\"");
    }

    [TestMethod]
    public void Serialize_WithoutMapName_OmitsMapName()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaGameStringProperties metaGameStringProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623),
            HdpVersion = "5.0.0",
        };

        // act
        string json = JsonSerializer.Serialize(metaGameStringProperties, options);

        // assert
        json.Should().NotContain("mapName");
    }

    [TestMethod]
    public void Serialize_SingleDataType_ReturnsJsonWithSingleElement()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaGameStringProperties metaGameStringProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623),
            HdpVersion = "5.0.0",
            DataTypes = [DataType.AnnouncerData],
        };

        // act
        string json = JsonSerializer.Serialize(metaGameStringProperties, options);

        // assert
        json.Should().Be(
            """
            {
              "heroesVersion": "2.55.14.95623",
              "hdpVersion": "5.0.0",
              "itemsType": "Other",
              "dataTypes": [
                "AnnouncerData"
              ],
              "gameStringText": {
                "locale": "ENUS",
                "constantVars": {
                  "replaced": false,
                  "preserved": false
                },
                "styleVars": {
                  "replaced": false,
                  "preserved": false
                }
              }
            }
            """);
    }

    [TestMethod]
    public void Serialize_MultipleDataTypes_ReturnsSortedCollection()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaGameStringProperties metaGameStringProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623),
            HdpVersion = "5.0.0",
            DataTypes = [DataType.VoiceLineData, DataType.Unknown, DataType.HeroData, DataType.AnnouncerData],
        };

        // act
        string json = JsonSerializer.Serialize(metaGameStringProperties, options);

        // assert
        using JsonDocument doc = JsonDocument.Parse(json);
        List<string> dataTypes = [.. doc.RootElement.GetProperty("dataTypes").EnumerateArray().Select(e => e.GetString()!)];

        dataTypes.Should().HaveCount(4).And
            .ContainInOrder("Unknown", "HeroData", "AnnouncerData", "VoiceLineData");
    }

    [TestMethod]
    public void Serialize_EmptyDataTypes_ReturnsEmptyArray()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaGameStringProperties metaGameStringProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623),
            HdpVersion = "5.0.0",
            DataTypes = [],
        };

        // act
        string json = JsonSerializer.Serialize(metaGameStringProperties, options);

        // assert
        json.Should().Contain("\"dataTypes\": []");
    }

    [TestMethod]
    public void Serialize_GameStringTextWithColoredText_ReturnsCorrectTextType()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaGameStringProperties metaGameStringProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623),
            HdpVersion = "5.0.0",
            DataTypes = [DataType.HeroData],
            GameStringTextProperties = new GameStringTextProperties()
            {
                Locale = StormLocale.FRFR,
                GameStringTextType = GameStringTextType.ColoredText,
                ConstantVars = new ConstantVars()
                {
                    Replaced = false,
                    Preserved = true,
                },
                StyleVars = new StyleVars()
                {
                    Replaced = false,
                    Preserved = true,
                },
            },
        };

        // act
        string json = JsonSerializer.Serialize(metaGameStringProperties, options);

        // assert
        json.Should().Be(
            """
            {
              "heroesVersion": "2.55.14.95623",
              "hdpVersion": "5.0.0",
              "itemsType": "Other",
              "dataTypes": [
                "HeroData"
              ],
              "gameStringText": {
                "locale": "FRFR",
                "textType": "ColoredText",
                "constantVars": {
                  "replaced": false,
                  "preserved": true
                },
                "styleVars": {
                  "replaced": false,
                  "preserved": true
                }
              }
            }
            """);
    }

    [TestMethod]
    public void Serialize_NonPtrVersion_ReturnsVersionWithoutPtrSuffix()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaGameStringProperties metaGameStringProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623),
            HdpVersion = "5.0.0",
        };

        // act
        string json = JsonSerializer.Serialize(metaGameStringProperties, options);

        // assert
        json.Should().Contain("\"heroesVersion\": \"2.55.14.95623\"");
        json.Should().NotContain("_ptr");
    }

    [TestMethod]
    public void Serialize_PtrVersion_ReturnsVersionWithPtrSuffix()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaGameStringProperties metaGameStringProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623, true),
            HdpVersion = "5.0.0",
        };

        // act
        string json = JsonSerializer.Serialize(metaGameStringProperties, options);

        // assert
        json.Should().Contain("\"heroesVersion\": \"2.55.14.95623_ptr\"");
    }

    [TestMethod]
    public void Serialize_GameStringTextPropertiesAllFlagsEnabled_ReturnsAllTrue()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaGameStringProperties metaGameStringProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623),
            HdpVersion = "5.0.0",
            GameStringTextProperties = new GameStringTextProperties()
            {
                Locale = StormLocale.ENUS,
                GameStringTextType = GameStringTextType.RawText,
                ConstantVars = new ConstantVars()
                {
                    Replaced = true,
                    Preserved = true,
                },
                StyleVars = new StyleVars()
                {
                    Replaced = true,
                    Preserved = true,
                },
            },
        };

        // act
        string json = JsonSerializer.Serialize(metaGameStringProperties, options);

        // assert
        json.Should().Be("""
            {
              "heroesVersion": "2.55.14.95623",
              "hdpVersion": "5.0.0",
              "itemsType": "Other",
              "dataTypes": [],
              "gameStringText": {
                "locale": "ENUS",
                "textType": "RawText",
                "constantVars": {
                  "replaced": true,
                  "preserved": true
                },
                "styleVars": {
                  "replaced": true,
                  "preserved": true
                }
              }
            }
            """);
    }

    [TestMethod]
    public void Serialize_IsLegacyProperty_IsNotSerialized()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaGameStringProperties metaGameStringProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623),
            HdpVersion = "5.0.0",
            IsLegacy = true,
        };

        // act
        string json = JsonSerializer.Serialize(metaGameStringProperties, options);

        // assert
        json.Should().Be(
            """
            {
              "heroesVersion": "2.55.14.95623",
              "hdpVersion": "5.0.0",
              "itemsType": "Other",
              "dataTypes": [],
              "gameStringText": {
                "locale": "ENUS",
                "constantVars": {
                  "replaced": false,
                  "preserved": false
                },
                "styleVars": {
                  "replaced": false,
                  "preserved": false
                }
              }
            }
            """);
    }

    private static JsonSerializerOptions CreateMetaSerializerOptions()
    {
        return new JsonSerializerOptions()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters =
            {
                new JsonStringEnumConverter(),
                new HeroesDataVersionConverter(),
            },
        };
    }
}
