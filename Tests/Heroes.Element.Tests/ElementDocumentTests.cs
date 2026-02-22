namespace Heroes.Element.Tests;

[TestClass]
public class ElementDocumentTests
{
    [TestMethod]
    public void MismatchedHeroesVersion_WithNullGameStringDocument_ReturnsFalse()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        TestElementBaseData elementData = new(jsonDocument, null);

        // act
        bool result = elementData.MismatchedHeroesVersion;

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void MismatchedHeroesVersion_WithMatchingVersions_ReturnsFalse()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0"
            },
            "gamestrings": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.MismatchedHeroesVersion;

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void MismatchedHeroesVersion_WithDifferentVersions_ReturnsTrue()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.56.0.88988",
                "hdpVersion": "5.0.0"
            },
            "gamestrings": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.MismatchedHeroesVersion;

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void MismatchedHeroesVersion_WithDifferentPtrFlags_ReturnsTrue()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122_ptr",
                "hdpVersion": "5.0.0"
            },
            "gamestrings": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.MismatchedHeroesVersion;

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void MismatchedHdpVersion_WithNullGameStringDocument_ReturnsFalse()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        TestElementBaseData elementData = new(jsonDocument, null);

        // act
        bool result = elementData.MismatchedHdpVersion;

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void MismatchedHdpVersion_WithMatchingVersions_ReturnsFalse()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0"
            },
            "gamestrings": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.MismatchedHdpVersion;

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void MismatchedHdpVersion_WithDifferentVersions_ReturnsTrue()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.1.0"
            },
            "gamestrings": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.MismatchedHdpVersion;

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    [DataRow("5.0.0-beta", "5.0.0-BETA")]
    public void MismatchedHdpVersion_WithCaseInsensitiveMatch_ReturnsFalse(string version1, string version2)
    {
        // arrange
        string jsonData = $$"""
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "{{version1}}"
            },
            "items": {}
        }
        """;

        string gameStringData = $$"""
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "{{version2}}"
            },
            "gamestrings": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.MismatchedHdpVersion;

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    [DataRow("5.0.0", "5.0.1")]
    [DataRow("5.0.0", "5.1.0")]
    [DataRow("5.0.0-beta", "5.0.0-alpha")]
    public void MismatchedHdpVersion_WithDifferentVersionStrings_ReturnsTrue(string version1, string version2)
    {
        // arrange
        string jsonData = $$"""
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "{{version1}}"
            },
            "items": {}
        }
        """;

        string gameStringData = $$"""
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "{{version2}}"
            },
            "gamestrings": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        bool result = elementData.MismatchedHdpVersion;

        // assert
        result.Should().BeTrue();
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
                "dataType": "HeroData",
                "localizedText": "none"
            },
            "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        TestElementBaseData elementData = new(jsonDocument, null);

        // act
        MetaDataProperties result = elementData.MetaProperties;

        // assert
        result.Should().NotBeNull();
        result.HeroesVersion.Should().Be(HeroesDataVersion.Parse("2.55.1.88122"));
        result.HdpVersion.Should().Be("5.0.0");
        result.DataType.Should().Be(DataType.HeroData);
        result.LocalizedText.Should().Be(LocalizedTextOption.None);
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
            _ = new TestElementBaseData(jsonDocument, null).MetaProperties;
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
            _ = new TestElementBaseData(jsonDocument, null).MetaProperties;
        };

        // assert
        act.Should().Throw<JsonException>();
    }

    [TestMethod]
    public void MetaProperties_WithGameStringDocument_OverridesDescriptionText()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "dataType": "HeroData"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "locale": "enus"
            },
            "gamestrings": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringDocument gameStringDocument = GameStringDocument.Load(gameStringJsonDocument);
        TestElementBaseData elementData = new(jsonDocument, gameStringDocument);

        // act
        MetaDataProperties result = elementData.MetaProperties;

        // assert
        result.Should().NotBeNull();
        result.DescriptionText.Should().NotBeNull();
        result.DescriptionText!.Locale.Should().Be(StormLocale.ENUS);
    }

    [TestMethod]
    public void Dispose_CalledOnce_DisposesJsonDocument()
    {
        // arrange
        string jsonData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0"
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
                "hdpVersion": "5.0.0"
            },
            "items": {}
        }
        """;

        string gameStringData = """
        {
            "meta": {
                "heroesVersion": "2.55.1.88122",
                "hdpVersion": "5.0.0",
                "locale": "enus"
            },
            "gamestrings": {}
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

    // Test implementation of ElementBaseData for testing purposes
    private class TestElementBaseData : ElementDocument<TestElementObject>
    {
        public TestElementBaseData(JsonDocument jsonDocument, GameStringDocument? gameStringDocument = null)
            : base(jsonDocument, gameStringDocument)
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
    }

    // Test implementation of IElementObject for testing purposes
    private class TestElementObject : IElementObject
    {
        public string Id { get; private set; } = string.Empty;

        public void SetId(string id) => Id = id;
    }
}