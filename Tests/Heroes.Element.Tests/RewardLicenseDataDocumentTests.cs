namespace Heroes.Element.Tests;

[TestClass]
public class RewardLicenseDataDocumentTests
{
    private readonly string _defaultArrangeJson =
    """
    {
      "meta": {
        "itemsType": "Data",
        "dataType": "RewardLicenseData"
      },
      "items": {
        "AbathurMechaVar1": {
          "licenseId": 26708
        },
        "Adjutant": {
          "licenseId": 28708
        }
      }
    }
    """;

    [TestMethod]
    public void TryGetElementById_ItemsPropertyIsEmpty_ReturnsFalse()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "RewardLicenseData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardLicenseDataDocument rewardLicenseData = RewardLicenseDataDocument.Load(jsonDocument);

        // act
        bool result = rewardLicenseData.TryGetElementById("other", out RewardLicense? rewardLicense);

        // assert
        result.Should().BeFalse();
        rewardLicense.Should().BeNull();
    }

    [TestMethod]
    [TestCategory("FullRead")]
    public void TryGetElementById_AbathurMechaVar1_ReturnsProperties()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "RewardLicenseData"
          },
          "items": {
            "AbathurMechaVar1": {
              "licenseId": 26708
            }
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardLicenseDataDocument rewardLicenseData = RewardLicenseDataDocument.Load(jsonDocument);

        // act
        bool returnResult = rewardLicenseData.TryGetElementById("AbathurMechaVar1", out RewardLicense? rewardLicense);

        // assert
        returnResult.Should().BeTrue();
        rewardLicense.Should().NotBeNull();
        rewardLicense.Id.Should().Be("AbathurMechaVar1");
        rewardLicense.LicenseId.Should().Be(26708);
    }

    [TestMethod]
    public void TryGetElementById_LicenseIdNotSet_ReturnsZeroLicenseId()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "RewardLicenseData"
          },
          "items": {
            "AbathurBaseVar3": {}
          }
        }
        """;
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardLicenseDataDocument rewardLicenseData = RewardLicenseDataDocument.Load(jsonDocument);

        // act
        bool returnResult = rewardLicenseData.TryGetElementById("AbathurBaseVar3", out RewardLicense? rewardLicense);

        // assert
        returnResult.Should().BeTrue();
        rewardLicense.Should().NotBeNull();
        rewardLicense.Id.Should().Be("AbathurBaseVar3");
        rewardLicense.LicenseId.Should().Be(0);
    }

    [TestMethod]
    public void TryGetElementById_NotFound_ReturnsFalse()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardLicenseDataDocument rewardLicenseData = RewardLicenseDataDocument.Load(jsonDocument);

        // act
        bool result = rewardLicenseData.TryGetElementById("other", out RewardLicense? rewardLicense);

        // assert
        result.Should().BeFalse();
        rewardLicense.Should().BeNull();
    }

    [TestMethod]
    public void GetElementById_Found_ReturnsObject()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardLicenseDataDocument rewardLicenseData = RewardLicenseDataDocument.Load(jsonDocument);

        // act
        RewardLicense rewardLicense = rewardLicenseData.GetElementById("Adjutant");

        // assert
        AdjutantBasicAssertions(rewardLicense);
    }

    [TestMethod]
    public void GetElementById_NotFound_ThrowsException()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardLicenseDataDocument rewardLicenseData = RewardLicenseDataDocument.Load(jsonDocument);

        // act
        Action act = () => rewardLicenseData.GetElementById("other");

        // assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [TestMethod]
    public void GetElementById_WithGameStringsDocument_ReturnsObject()
    {
        // arrange
        string jsonData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0",
            "itemsType": "Data",
            "dataType": "RewardLicenseData"
          },
          "items": {
            "Adjutant": {
              "licenseId": 28708
            }
          }
        }
        """;

        string gameStringData = """
        {
          "meta": {
            "heroesVersion": "2.55.1.88122",
            "hdpVersion": "5.0.0",
            "itemsType": "GameStrings",
            "gameStringText": {
              "locale": "FRFR",
              "textType": "RawText",
              "replaceFontStyles": true,
              "preserveFontConstantVars": false,
              "preserveFontStyleVars": false
            }
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
        using JsonDocument gameStringJsonDocument = JsonDocument.Parse(gameStringData);
        using GameStringsDocument gameStringsDocument = GameStringsDocument.Load(gameStringJsonDocument);
        RewardLicenseDataDocument rewardLicenseData = RewardLicenseDataDocument.Load(jsonDocument, gameStringsDocument);

        // act
        RewardLicense rewardLicense = rewardLicenseData.GetElementById("Adjutant");

        // assert
        rewardLicenseData.GameStringsDocument.Should().BeSameAs(gameStringsDocument);
        AdjutantBasicAssertions(rewardLicense);
    }

    [TestMethod]
    public void GetElements_WithItems_ReturnsAllElements()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardLicenseDataDocument rewardLicenseData = RewardLicenseDataDocument.Load(jsonDocument);

        // act
        List<RewardLicense> result = [.. rewardLicenseData.GetElements()];

        // assert
        result.Should().HaveCount(2);
        result.Should().Contain(r => r.Id == "AbathurMechaVar1" && r.LicenseId == 26708);
        result.Should().Contain(r => r.Id == "Adjutant" && r.LicenseId == 28708);
    }

    [TestMethod]
    public void GetElements_WithEmptyItems_ReturnsEmpty()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "RewardLicenseData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardLicenseDataDocument rewardLicenseData = RewardLicenseDataDocument.Load(jsonDocument);

        // act
        List<RewardLicense> result = [.. rewardLicenseData.GetElements()];

        // assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    [DataRow("Unknown")]
    [DataRow("HeroData")]
    [DataRow("RewardPortraitData")]
    public void Load_WithMismatchedDataType_ThrowsJsonException(string dataType)
    {
        // arrange
        string json = $$"""
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "{{dataType}}"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);

        // act
        Action act = () => RewardLicenseDataDocument.Load(jsonDocument);

        // assert
        act.Should().Throw<JsonException>().WithMessage("*does not match the expected data type*");
    }

    [TestMethod]
    public void GetElementObjects_WithItems_ReturnsAllElementsAsObjects()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardLicenseDataDocument rewardLicenseData = RewardLicenseDataDocument.Load(jsonDocument);

        // act
        List<object> result = [.. rewardLicenseData.GetElementObjects()];

        // assert
        result.Should().HaveCount(2);
        result.Should().AllBeOfType<RewardLicense>();
        result.OfType<RewardLicense>().Should().Contain(r => r.Id == "AbathurMechaVar1");
        result.OfType<RewardLicense>().Should().Contain(r => r.Id == "Adjutant");
    }

    [TestMethod]
    public void GetElementObjects_WithEmptyItems_ReturnsEmpty()
    {
        // arrange
        string json =
        """
        {
          "meta": {
            "itemsType": "Data",
            "dataType": "RewardLicenseData"
          },
          "items": {}
        }
        """;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardLicenseDataDocument rewardLicenseData = RewardLicenseDataDocument.Load(jsonDocument);

        // act
        List<object> result = [.. rewardLicenseData.GetElementObjects()];

        // assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void GetElementObjects_WithItems_ReturnsObjectsWithCorrectProperties()
    {
        // arrange
        string json = _defaultArrangeJson;

        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        RewardLicenseDataDocument rewardLicenseData = RewardLicenseDataDocument.Load(jsonDocument);

        // act
        List<object> result = [.. rewardLicenseData.GetElementObjects()];

        // assert
        result.Should().HaveCount(2);

        RewardLicense adjutant = result.OfType<RewardLicense>().First(r => r.Id == "Adjutant");
        AdjutantBasicAssertions(adjutant);
    }

    private static void AdjutantBasicAssertions(RewardLicense rewardLicense)
    {
        rewardLicense.Id.Should().Be("Adjutant");
        rewardLicense.LicenseId.Should().Be(28708);
    }
}
