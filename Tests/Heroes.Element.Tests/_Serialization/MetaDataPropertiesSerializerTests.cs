namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class MetaDataPropertiesSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaDataProperties metaDataProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623, true),
            HdpVersion = "5.0.0",
            ItemsType = ItemsType.Data,
            DataType = DataType.HeroData,
            MapName = "Alterac Pass",
            LocalizedText = LocalizedText.None,
            GameStringTextProperties = new GameStringTextProperties()
            {
                Locale = StormLocale.ENUS,
                GameStringTextType = GameStringTextType.RawText,
                ConstantVars = new ConstantVars()
                {
                    Replaced = true,
                    Preserved = false,
                },
                StyleVars = new StyleVars()
                {
                    Replaced = true,
                    Preserved = false,
                },
            },
            TotalItems = 42,
        };

        // act
        string json = JsonSerializer.Serialize(metaDataProperties, options);

        // assert
        json.Should().Be(
            """
            {
              "heroesVersion": "2.55.14.95623_ptr",
              "hdpVersion": "5.0.0",
              "itemsType": "Data",
              "dataType": "HeroData",
              "mapName": "Alterac Pass",
              "localizedText": "None",
              "gameStringText": {
                "locale": "ENUS",
                "textType": "RawText",
                "constantVars": {
                  "replaced": true,
                  "preserved": false
                },
                "styleVars": {
                  "replaced": true,
                  "preserved": false
                }
              },
              "totalItems": 42
            }
            """);
    }

    [TestMethod]
    public void Serialize_DefaultValues_ReturnsJsonWithDefaults()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaDataProperties metaDataProperties = new();

        // act
        string json = JsonSerializer.Serialize(metaDataProperties, options);

        // assert
        json.Should().Be(
            """
            {
              "heroesVersion": "-1.-1.-1.-1",
              "hdpVersion": "",
              "itemsType": "Other",
              "dataType": "Unknown",
              "localizedText": "None",
              "totalItems": 0
            }
            """);
    }

    [TestMethod]
    public void Serialize_WithGameStringTextPropertiesNull_OmitsGameStringText()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaDataProperties metaDataProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623),
            HdpVersion = "5.0.0",
            DataType = DataType.AnnouncerData,
            GameStringTextProperties = null,
            TotalItems = 10,
        };

        // act
        string json = JsonSerializer.Serialize(metaDataProperties, options);

        // assert
        json.Should().NotContain("gameStringText");
    }

    [TestMethod]
    public void Serialize_WithMapName_ReturnsJsonWithMapName()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaDataProperties metaDataProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623),
            HdpVersion = "5.0.0",
            DataType = DataType.MapData,
            MapName = "Battlefield of Eternity",
            TotalItems = 1,
        };

        // act
        string json = JsonSerializer.Serialize(metaDataProperties, options);

        // assert
        json.Should().Contain("\"mapName\": \"Battlefield of Eternity\"");
    }

    [TestMethod]
    public void Serialize_WithoutMapName_OmitsMapName()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaDataProperties metaDataProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623),
            HdpVersion = "5.0.0",
            DataType = DataType.HeroData,
            TotalItems = 5,
        };

        // act
        string json = JsonSerializer.Serialize(metaDataProperties, options);

        // assert
        json.Should().NotContain("mapName");
    }

    [TestMethod]
    public void Serialize_LocalizedTextExtract_ReturnsExtractValue()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaDataProperties metaDataProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623),
            HdpVersion = "5.0.0",
            LocalizedText = LocalizedText.Extract,
            TotalItems = 3,
        };

        // act
        string json = JsonSerializer.Serialize(metaDataProperties, options);

        // assert
        json.Should().Contain("\"localizedText\": \"Extract\"");
    }

    [TestMethod]
    public void Serialize_GameStringTextWithColoredText_ReturnsCorrectTextType()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaDataProperties metaDataProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623),
            HdpVersion = "5.0.0",
            DataType = DataType.HeroData,
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
            TotalItems = 1,
        };

        // act
        string json = JsonSerializer.Serialize(metaDataProperties, options);

        // assert
        json.Should().Be(
            """
            {
              "heroesVersion": "2.55.14.95623",
              "hdpVersion": "5.0.0",
              "itemsType": "Other",
              "dataType": "HeroData",
              "localizedText": "None",
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
              },
              "totalItems": 1
            }
            """);
    }

    [TestMethod]
    [DataRow(DataType.Unknown)]
    [DataRow(DataType.HeroData)]
    [DataRow(DataType.UnitData)]
    [DataRow(DataType.AnnouncerData)]
    [DataRow(DataType.BannerData)]
    [DataRow(DataType.BoostData)]
    [DataRow(DataType.BundleData)]
    [DataRow(DataType.EmoticonData)]
    [DataRow(DataType.EmoticonPackData)]
    [DataRow(DataType.LootChestData)]
    [DataRow(DataType.MapData)]
    [DataRow(DataType.MatchAwardData)]
    [DataRow(DataType.MountData)]
    [DataRow(DataType.PortraitPackData)]
    [DataRow(DataType.RewardPortraitData)]
    [DataRow(DataType.SkinData)]
    [DataRow(DataType.SprayData)]
    [DataRow(DataType.TypeDescriptionData)]
    [DataRow(DataType.VeterancyData)]
    [DataRow(DataType.VoiceLineData)]
    public void Serialize_AllDataTypes_ReturnsCorrectDataTypeValue(DataType dataType)
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaDataProperties metaDataProperties = new()
        {
            DataType = dataType,
        };

        // act
        string json = JsonSerializer.Serialize(metaDataProperties, options);

        // assert
        json.Should().Contain($"\"dataType\": \"{dataType}\"");
    }

    [TestMethod]
    public void Serialize_NonPtrVersion_ReturnsVersionWithoutPtrSuffix()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaDataProperties metaDataProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623),
            HdpVersion = "5.0.0",
            TotalItems = 0,
        };

        // act
        string json = JsonSerializer.Serialize(metaDataProperties, options);

        // assert
        json.Should().Be(
            """
            {
              "heroesVersion": "2.55.14.95623",
              "hdpVersion": "5.0.0",
              "itemsType": "Other",
              "dataType": "Unknown",
              "localizedText": "None",
              "totalItems": 0
            }
            """);
    }

    [TestMethod]
    public void Serialize_PtrVersion_ReturnsVersionWithPtrSuffix()
    {
        // arrange
        JsonSerializerOptions options = CreateMetaSerializerOptions();

        MetaDataProperties metaDataProperties = new()
        {
            HeroesVersion = new HeroesDataVersion(2, 55, 14, 95623, true),
            HdpVersion = "5.0.0",
            TotalItems = 0,
        };

        // act
        string json = JsonSerializer.Serialize(metaDataProperties, options);

        // assert
        json.Should().Be(
            """
            {
              "heroesVersion": "2.55.14.95623_ptr",
              "hdpVersion": "5.0.0",
              "itemsType": "Other",
              "dataType": "Unknown",
              "localizedText": "None",
              "totalItems": 0
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
