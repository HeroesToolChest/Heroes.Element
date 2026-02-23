namespace Heroes.Element.Tests;

[TestClass]
public class DataDocumentTests
{
    [TestMethod]
    [DataRow("HeroData", typeof(HeroDataDocument))]
    [DataRow("UnitData", typeof(UnitDataDocument))]
    [DataRow("AnnouncerData", typeof(AnnouncerDataDocument))]
    [DataRow("BannerData", typeof(BannerDataDocument))]
    [DataRow("BoostData", typeof(BoostDataDocument))]
    [DataRow("BundleData", typeof(BundleDataDocument))]
    [DataRow("EmoticonData", typeof(EmoticonDataDocument))]
    [DataRow("EmoticonPackData", typeof(EmoticonPackDataDocument))]
    [DataRow("LootChestData", typeof(LootChestDataDocument))]
    [DataRow("MapData", typeof(MapDataDocument))]
    [DataRow("MatchAwardData", typeof(MatchAwardDataDocument))]
    [DataRow("MountData", typeof(MountDataDocument))]
    [DataRow("PortraitPackData", typeof(PortraitPackDataDocument))]
    [DataRow("RewardPortraitData", typeof(RewardPortraitDataDocument))]
    [DataRow("SkinData", typeof(SkinDataDocument))]
    [DataRow("SprayData", typeof(SprayDataDocument))]
    [DataRow("TypeDescriptionData", typeof(TypeDescriptionDataDocument))]
    [DataRow("VeterancyData", typeof(VeterancyDataDocument))]
    [DataRow("VoiceLineData", typeof(VoiceLineDataDocument))]
    public void Load_WithValidDataType_ReturnsExpectedDocumentType(string dataType, Type expectedType)
    {
        // arrange
        string json = $$"""
        {
            "meta": {
                "hdpVersion": "5.0.0",
                "dataType": "{{dataType}}"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);

        // act
        IElementDocument result = DataDocument.Load(jsonDocument);

        // assert
        result.Should().BeOfType(expectedType);
    }

    [TestMethod]
    public void Load_WithCaseInsensitiveDataType_ReturnsExpectedDocumentType()
    {
        // arrange
        string json =
        """
        {
            "meta": {
                "hdpVersion": "5.0.0",
                "dataType": "herodata"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);

        // act
        IElementDocument result = DataDocument.Load(jsonDocument);

        // assert
        result.Should().BeOfType<HeroDataDocument>();
    }

    [TestMethod]
    public void Load_WithGameStringDocument_ReturnsDocumentWithGameStrings()
    {
        // arrange
        string json =
        """
        {
            "meta": {
                "hdpVersion": "5.0.0",
                "dataType": "AnnouncerData"
            },
            "items": {}
        }
        """;

        string gameStringJson =
        """
        {
            "meta": {
                "hdpVersion": "5.0.0"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringJson);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);

        // act
        IElementDocument result = DataDocument.Load(jsonDocument, gameStringDocument);

        // assert
        result.Should().BeOfType<AnnouncerDataDocument>();
    }

    [TestMethod]
    public void Load_WithoutMetaProperty_ThrowsJsonException()
    {
        // arrange
        string json =
        """
        {
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);

        // act
        Action act = () => DataDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Load_WithoutDataTypeProperty_ThrowsJsonException()
    {
        // arrange
        string json =
        """
        {
            "meta": {
                "hdpVersion": "5.0.0"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);

        // act
        Action act = () => DataDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Load_WithNullDataTypeValue_ThrowsJsonException()
    {
        // arrange
        string json =
        """
        {
            "meta": {
                "hdpVersion": "5.0.0",
                "dataType": null
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);

        // act
        Action act = () => DataDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Load_WithInvalidDataTypeValue_ThrowsJsonException()
    {
        // arrange
        string json =
        """
        {
            "meta": {
                "hdpVersion": "5.0.0",
                "dataType": "NotARealDataType"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);

        // act
        Action act = () => DataDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Load_WithUnknownDataType_ThrowsArgumentException()
    {
        // arrange
        string json =
        """
        {
            "meta": {
                "hdpVersion": "5.0.0",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);

        // act
        Action act = () => DataDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<ArgumentException>();
    }

    [TestMethod]
    public void Load_WithGameStringsDataType_ThrowsArgumentException()
    {
        // arrange
        string json =
        """
        {
            "meta": {
                "hdpVersion": "5.0.0",
                "dataType": "GameStrings"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);

        // act
        Action act = () => DataDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<ArgumentException>();
    }
}