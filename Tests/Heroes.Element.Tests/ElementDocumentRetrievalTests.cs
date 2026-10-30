using Heroes.Element.Models.ObjectProperties;

namespace Heroes.Element.Tests;

[TestClass]
public class ElementDocumentRetrievalTests
{
    // every data document whose items have a hyperlink id, so they can be looked up by more than their id
    public static IEnumerable<object[]> LookupDocumentTypes =>
        typeof(ElementDocument<>).Assembly.GetTypes()
            .Where(type => type is { IsClass: true, IsAbstract: false } && GetElementType(type) is Type elementType && typeof(IHyperlinkId).IsAssignableFrom(elementType))
            .OrderBy(type => type.Name, StringComparer.Ordinal)
            .Select(type => new object[] { type });

    [TestMethod]
    public void LookupDocumentTypes_FromAssembly_ContainsKnownDocuments()
    {
        // act
        List<Type> documentTypes = [.. LookupDocumentTypes.Select(x => (Type)x[0])];

        // assert
        documentTypes.Should().Contain(
        [
            typeof(AnnouncerPackDataDocument),
            typeof(BannerDataDocument),
            typeof(BoostDataDocument),
            typeof(BundleDataDocument),
            typeof(EmoticonPackDataDocument),
            typeof(HeroDataDocument),
            typeof(LootChestDataDocument),
            typeof(MountDataDocument),
            typeof(PortraitPackDataDocument),
            typeof(RewardPortraitDataDocument),
            typeof(SkinDataDocument),
            typeof(SprayDataDocument),
            typeof(VoiceLineDataDocument),
        ]);
    }

    [TestMethod]
    [DynamicData(nameof(LookupDocumentTypes))]
    public void DataDocument_ItemsHaveLookupIds_ImplementsMatchingRetrievalInterface(Type documentType)
    {
        // arrange
        Type elementType = GetElementType(documentType)!;

        // act
        Type expectedInterface = GetExpectedRetrievalInterface(elementType);

        // assert
        documentType.Should().Implement(expectedInterface);
    }

    private static Type? GetElementType(Type documentType)
    {
        for (Type? type = documentType.BaseType; type is not null; type = type.BaseType)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ElementDocument<>))
                return type.GetGenericArguments()[0];
        }

        return null;
    }

    // the most specific retrieval interface the element type supports
    private static Type GetExpectedRetrievalInterface(Type elementType)
    {
        if (typeof(ILoadoutItem).IsAssignableFrom(elementType))
            return typeof(ILoadoutItemRetrieval<>).MakeGenericType(elementType);

        if (typeof(IStoreItem).IsAssignableFrom(elementType))
            return typeof(IStoreItemRetrieval<>).MakeGenericType(elementType);

        return typeof(IHyperlinkIdRetrieval<>).MakeGenericType(elementType);
    }
}
