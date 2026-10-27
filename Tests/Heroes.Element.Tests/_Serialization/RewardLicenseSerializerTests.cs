namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class RewardLicenseSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        SerializerSettings serializerSettings = SerializerSettings.Create();

        RewardLicense rewardLicense = new("id")
        {
            LicenseId = 28708,
        };

        // act
        string json = JsonSerializer.Serialize(rewardLicense, serializerSettings.GetJsonSerializerDataOptions());

        // assert
        serializerSettings.ItemDictionary.Should().NotContainKey("rewardLicense", "it has no gamestringtext properties");

        json.Should().Be(
            """
            {
              "licenseId": 28708
            }
            """);
    }
}
