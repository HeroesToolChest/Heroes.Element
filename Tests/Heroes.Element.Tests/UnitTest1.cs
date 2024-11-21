//using Heroes.Element.Models;
//using System.Text.Json;

namespace Heroes.Element.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void Test()
    {
        Assert.IsTrue(true);
    }
}
//    [TestMethod]
//    public void TestMethod1()
//    {
//        AnnouncerData announcerJsonFile = AnnouncerData.Parse(JsonDocument.Parse(File.ReadAllBytes(@"F:\heroes\heroes_92264\data\json\announcerdata_92264_enus.json")));
//        if (announcerJsonFile.TryGetAnnouncerById("Adjutant", out Announcer? value))
//        {
//            var x = value.AttributeId;
//        }

//    }

//    [TestMethod]
//    public void TestMethod33()
//    {
//        BundleData bundleJsonFile = BundleData.Parse(JsonDocument.Parse(File.ReadAllBytes(@"F:\heroes\heroes_92264\data\json\bundledata_92264_enus.json")));
//        //BundleData bundleJsonFile = BundleData.Parse(JsonDocument.Parse(File.ReadAllBytes(@"C:\Users\kevin\Source\Repos\HeroesDataParserVNext\HeroesDataParser\bin\Debug\net8.0\bundletest.json")));
//        if (bundleJsonFile.TryGetBundleId("AllStarsBundle", out Bundle? value))
//        {
//            var x = value.AttributeId;
//        }

//    }

//    [TestMethod]
//    public async Task TestMethod2()
//    {
//        Announcer announcer = new Announcer("id1")
//        {
//            AttributeId = "sdfs",
//        };

//        Dictionary<string, Announcer> keyValuePairs = new Dictionary<string, Announcer>();
//        keyValuePairs.Add(announcer.Id, announcer);

//        await using FileStream fileStream = File.Create("filetest.json");
//        await JsonSerializer.SerializeAsync(fileStream, keyValuePairs, new JsonSerializerOptions()
//        {
//            WriteIndented = true,
//            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
//        });

//    }
//}