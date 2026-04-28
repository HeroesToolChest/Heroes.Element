namespace Heroes.Element.Tests;

[TestClass]
public class ElementDocumentTests
{
    [TestMethod]
    public void IsIsMatchedHeroesVersion_WithNullGameStringDocument_ReturnsTrue()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        TestElementBaseData elementData = new(jsonDocument, null);

        // act
        bool result = elementData.IsMatchedHeroesVersion;

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsMatchedHeroesVersion_WithMatchingVersions_ReturnsTrue()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "GameStrings"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.IsMatchedHeroesVersion;

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsMatchedHeroesVersion_WithDifferentVersions_ReturnsFalse()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.56.0.88988",
                "hdpVersion": "5.0.0",
                "itemsType": "GameStrings"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.IsMatchedHeroesVersion;

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void IsMatchedHeroesVersion_WithDifferentPtrFlags_ReturnsFalse()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122_ptr",
                "hdpVersion": "5.0.0",
                "itemsType": "GameStrings"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.IsMatchedHeroesVersion;

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void IsMatchedHdpVersion_WithNullGameStringDocument_ReturnsTrue()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        TestElementBaseData elementData = new(jsonDocument, null);

        // act
        bool result = elementData.IsMatchedHdpVersion;

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsMatchedHdpVersion_WithMatchingVersions_ReturnsTrue()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "GameStrings"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.IsMatchedHdpVersion;

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsMatchedHdpVersion_WithDifferentVersions_ReturnsFalse()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.1.0",
                "itemsType": "GameStrings"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.IsMatchedHdpVersion;

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    [DataRow("5.0.0-beta", "5.0.0-BETA")]
    public void IsMatchedHdpVersion_WithCaseInsensitiveMatch_ReturnsTrue(string version1, string version2)
    {
        // arrange
        string jsonData = $$"""
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "{{version1}}",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        string gameStringData = $$"""
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "{{version2}}",
                "itemsType": "GameStrings"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.IsMatchedHdpVersion;

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    [DataRow("5.0.0", "5.0.1")]
    [DataRow("5.0.0", "5.1.0")]
    [DataRow("5.0.0-beta", "5.0.0-alpha")]
    public void IsMatchedHdpVersion_WithDifferentVersionStrings_ReturnsFalse(string version1, string version2)
    {
        // arrange
        string jsonData = $$"""
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "{{version1}}",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        string gameStringData = $$"""
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "{{version2}}",
                "itemsType": "GameStrings"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.IsMatchedHdpVersion;

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void MetaProperties_WithValidMetaObject_ReturnsPopulatedProperties()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
              "heroesVersion": "2.55.1.88122",
              "hdpVersion": "5.0.0",
              "itemsType": "Data",
              "dataType": "Unknown",
              "localizedText": "none",
              "mapName": "Test Map",
              "localizedText": "Copy",
              "gameStringText": {
                "locale": "ENUS",
                "textType": "RawText",
                "replaceFontConstantVars": true,
                "replaceFontStylesVars": true,
                "preserveFontConstantVars": true,
                "preserveFontStyleVars": true
              }
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        TestElementBaseData elementData = new(jsonDocument, null);

        // act
        MetaDataProperties result = elementData.Meta;

        // assert
        result.Should().NotBeNull();
        result.HeroesVersion.Should().Be(HeroesDataVersion.Parse("2.55.1.88122"));
        result.HdpVersion.Should().Be("5.0.0");
        result.ItemsType.Should().Be(ItemsType.Data);
        result.DataType.Should().Be(DataType.Unknown);
        result.LocalizedText.Should().Be(LocalizedTextOption.Copy);
        result.GameStringTextProperties!.Locale.Should().Be(StormLocale.ENUS);
        result.GameStringTextProperties.GameStringTextType.Should().Be(GameStringTextType.RawText);
        result.GameStringTextProperties.ReplaceFontConstantVars.Should().BeTrue();
        result.GameStringTextProperties.ReplaceFontStylesVars.Should().BeTrue();
        result.GameStringTextProperties.PreserveFontConstantVars.Should().BeTrue();
        result.GameStringTextProperties.PreserveFontStyleVars.Should().BeTrue();
        result.IsLegacy.Should().BeFalse();
    }

    [TestMethod]
    public void MetaProperties_WithoutMetaObject_ThrowsException()
    {
        // arrange
        string jsonData = """
        {
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);

        // act
        Action act = () =>
        {
            _ = new TestElementBaseData(jsonDocument, null).Meta;
        };

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void MetaProperties_WithoutItemsProperty_ThrowsException()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0"
            }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);

        // act
        Action act = () =>
        {
            _ = new TestElementBaseData(jsonDocument, null).Meta;
        };

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void Dispose_CalledOnce_DisposesJsonDocument()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data"
            },
            "items": {}
        }
        """;

        JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        TestElementBaseData elementData = new(jsonDocument, null);

        // act
        elementData.Dispose();

        // assert
        Action act = () => _ = elementData.JsonDocument.RootElement.GetString();
        act.Should().Throw<ObjectDisposedException>();
    }

    [TestMethod]
    public void DeserializeElement_WithGameStringDocument_CallsUpdateGameStrings()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "itemsType": "GameStrings",
                "hdpVersion": "5.0.0",
                "locale": "enus"
            },
            "items": {}
        }
        """;

        string elementJson = """
        {
            "id": "test-id"
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument elementJsonDocument = JsonDocument.Parse(elementJson);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        TestElementObject? result = elementData.CallDeserializeElement(elementJsonDocument.RootElement, "test-id");

        // assert
        result.Should().NotBeNull();
        elementData.UpdateGameStringsCalled.Should().BeTrue();
    }

    [TestMethod]
    [DataRow("GameStrings")]
    [DataRow("Other")]
    public void ValidateTypes_WithNonDataItemsType_ThrowsJsonException(string itemsType)
    {
        // arrange
        string jsonData = $$"""
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "{{itemsType}}",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);

        // act
        Action act = () => _ = new TestElementBaseData(jsonDocument, null);

        // assert
        act.Should().Throw<JsonException>().WithMessage("*does not match the expected items type*");
    }

    [TestMethod]
    [DataRow("HeroData")]
    [DataRow("AnnouncerData")]
    [DataRow("UnitData")]
    public void ValidateTypes_WithMismatchedDataType_ThrowsJsonException(string dataType)
    {
        // arrange
        string jsonData = $$"""
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "{{dataType}}"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);

        // act
        Action act = () => _ = new TestElementBaseData(jsonDocument, null);

        // assert
        act.Should().Throw<JsonException>().WithMessage("*does not match the expected data type*");
    }

    [TestMethod]
    public void ValidateTypes_WithMatchingItemsTypeAndDataType_DoesNotThrow()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);

        // act
        Action act = () => _ = new TestElementBaseData(jsonDocument, null);

        // assert
        act.Should().NotThrow();
    }

    [TestMethod]
    public void GetElementObjects_WithItems_ReturnsAllElementsAsObjects()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {
                "Element1": {
                    "id": "Element1"
                },
                "Element2": {
                    "id": "Element2"
                }
            }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        TestElementBaseData elementData = new(jsonDocument, null);

        // act
        List<object> result = [.. elementData.GetElementObjects()];

        // assert
        result.Should().HaveCount(2);
        result.Should().AllBeOfType<TestElementObject>();
        result.OfType<TestElementObject>().Should().Contain(e => e.Id == "Element1");
        result.OfType<TestElementObject>().Should().Contain(e => e.Id == "Element2");
    }

    [TestMethod]
    public void GetElementObjects_WithEmptyItems_ReturnsEmpty()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        TestElementBaseData elementData = new(jsonDocument, null);

        // act
        List<object> result = [.. elementData.GetElementObjects()];

        // assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void GetElementObjects_WithItems_ReturnsObjectsWithCorrectProperties()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {
                "TestElement1": {
                    "id": "TestElement1"
                },
                "TestElement2": {
                    "id": "TestElement2"
                }
            }
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        TestElementBaseData elementData = new(jsonDocument, null);

        // act
        List<object> result = [.. elementData.GetElementObjects()];

        // assert
        result.Should().HaveCount(2);

        TestElementObject element1 = result.OfType<TestElementObject>().First(e => e.Id == "TestElement1");
        element1.Id.Should().Be("TestElement1");

        TestElementObject element2 = result.OfType<TestElementObject>().First(e => e.Id == "TestElement2");
        element2.Id.Should().Be("TestElement2");
    }

    [TestMethod]
    public void IsMatchedDataType_WithNullGameStringDocument_ReturnsTrue()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        TestElementBaseData elementData = new(jsonDocument, null);

        // act
        bool result = elementData.IsMatchedDataType;

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsMatchedDataType_WithMatchingDataType_ReturnsTrue()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "GameStrings",
                "dataTypes": ["Unknown"]
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.IsMatchedDataType;

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsMatchedDataType_WithMultipleMatchingDataTypes_ReturnsTrue()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "GameStrings",
                "dataTypes": ["HeroData", "Unknown", "AnnouncerData"]
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.IsMatchedDataType;

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsMatchedDataType_WithNonMatchingDataType_ReturnsFalse()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "GameStrings",
                "dataTypes": ["HeroData"]
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.IsMatchedDataType;

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void IsMatchedDataType_WithEmptyDataTypes_ReturnsFalse()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "GameStrings",
                "dataTypes": []
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.IsMatchedDataType;

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void IsMatchedMapName_WithNullGameStringDocument_ReturnsTrue()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        TestElementBaseData elementData = new(jsonDocument, null);

        // act
        bool result = elementData.IsMatchedMapName;

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsMatchedMapName_WithBothMapNamesNull_ReturnsTrue()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "GameStrings"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.IsMatchedMapName;

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsMatchedMapName_WithMatchingMapNames_ReturnsTrue()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown",
                "mapName": "Test Map"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "GameStrings",
                "mapName": "Test Map"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.IsMatchedMapName;

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsMatchedMapName_WithDifferentMapNames_ReturnsFalse()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown",
                "mapName": "Map A"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "GameStrings",
                "mapName": "Map B"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.IsMatchedMapName;

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void IsMatchedMapName_WithMapNameCaseDifference_ReturnsFalse()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown",
                "mapName": "Test Map"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "GameStrings",
                "mapName": "test map"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.IsMatchedMapName;

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void IsMatchedMapName_WithGameStringMapNameNullAndDataMapNameNotNull_ReturnsFalse()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown",
                "mapName": "Test Map"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "GameStrings"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.IsMatchedMapName;

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void IsMatchedMapName_WithGameStringMapNameNotNullAndDataMapNameNull_ReturnsFalse()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "Data",
                "dataType": "Unknown"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "itemsType": "GameStrings",
                "mapName": "Test Map"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.IsMatchedMapName;

        // assert
        result.Should().BeFalse();
    }

    // Test implementation of ElementBaseData for testing purposes
    private class TestElementBaseData : ElementDocument<TestElementObject>
    {
        public TestElementBaseData(JsonDocument jsonDocument, GameStringDocument? gameStringDocument = null)
            : base(DataType.Unknown, jsonDocument, gameStringDocument)
        {
        }

        public bool UpdateGameStringsCalled { get; private set; }

        public TestElementObject? CallDeserializeElement(JsonElement jsonElement, string id)
        {
            return DeserializeElement(jsonElement, id);
        }

        protected override void UpdateGameStringTexts(TestElementObject element)
        {
            UpdateGameStringsCalled = true;
        }

        protected override void SetProperties(string id, TestElementObject element)
        {
            element.SetId(id);
        }
    }

    // Test implementation of IElementObject for testing purposes
    private class TestElementObject : IElementObject
    {
        public string Id { get; private set; } = string.Empty;

        public void SetId(string id) => Id = id;
    }
}