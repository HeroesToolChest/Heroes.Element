namespace Heroes.Element;

/// <summary>
/// A static class to load a general JSON document and return the appropriate
/// <see cref="ElementDocument{T}"/> subclass based on the <c>DataType</c> in the metadata.
/// </summary>
public static class DataDocument
{
    /// <summary>
    /// Parses the <paramref name="dataDocument"/> metadata to determine the <c>DataType</c> and returns the corresponding data document instance.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringsDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>An <see cref="IElementDocument"/> instance of the appropriate data document type.</returns>
    /// <exception cref="JsonException">The JSON document does not contain a valid <c>meta</c> property or <c>dataType</c>.</exception>
    /// <exception cref="ArgumentException">The <c>dataType</c> value is not recognized.</exception>
    public static IElementDocument Load(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    {
        DataType dataType = GetDataType(dataDocument);

        return LoadDataDocument(dataDocument, gameStringsDocument, dataType);
    }

    private static DataType GetDataType(JsonDocument dataDocument)
    {
        if (!dataDocument.RootElement.TryGetProperty(Constants.RootMetaPropertyName, out JsonElement metaElement))
            throw new JsonException("No 'meta' property found in the JSON document.");

        if (!metaElement.TryGetProperty("dataType", out JsonElement dataTypeElement))
            throw new JsonException("No 'dataType' property found in the 'meta' object.");

        string dataTypeString = dataTypeElement.GetString() ?? throw new JsonException("The 'dataType' value is null.");

        if (!Enum.TryParse(dataTypeString, true, out DataType dataType))
            throw new JsonException($"Invalid data type '{dataTypeString}'.");

        return dataType;
    }

    private static IElementDocument LoadDataDocument(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument, DataType dataType)
    {
        return dataType switch
        {
            DataType.HeroData => HeroDataDocument.Load(dataDocument, gameStringsDocument),
            DataType.UnitData => UnitDataDocument.Load(dataDocument, gameStringsDocument),
            DataType.AnnouncerPackData => AnnouncerDataDocument.Load(dataDocument, gameStringsDocument),
            DataType.BannerData => BannerDataDocument.Load(dataDocument, gameStringsDocument),
            DataType.BoostData => BoostDataDocument.Load(dataDocument, gameStringsDocument),
            DataType.BundleData => BundleDataDocument.Load(dataDocument, gameStringsDocument),
            DataType.EmoticonData => EmoticonDataDocument.Load(dataDocument, gameStringsDocument),
            DataType.EmoticonPackData => EmoticonPackDataDocument.Load(dataDocument, gameStringsDocument),
            DataType.LootChestData => LootChestDataDocument.Load(dataDocument, gameStringsDocument),
            DataType.MapData => MapDataDocument.Load(dataDocument, gameStringsDocument),
            DataType.MatchAwardData => MatchAwardDataDocument.Load(dataDocument, gameStringsDocument),
            DataType.MountData => MountDataDocument.Load(dataDocument, gameStringsDocument),
            DataType.PortraitPackData => PortraitPackDataDocument.Load(dataDocument, gameStringsDocument),
            DataType.RewardPortraitData => RewardPortraitDataDocument.Load(dataDocument, gameStringsDocument),
            DataType.SkinData => SkinDataDocument.Load(dataDocument, gameStringsDocument),
            DataType.SprayData => SprayDataDocument.Load(dataDocument, gameStringsDocument),
            DataType.TypeDescriptionData => TypeDescriptionDataDocument.Load(dataDocument, gameStringsDocument),
            DataType.VeterancyData => VeterancyDataDocument.Load(dataDocument),
            DataType.VoiceLineData => VoiceLineDataDocument.Load(dataDocument, gameStringsDocument),
            _ => throw new ArgumentException($"Unknown or not valid data type '{dataType}'.", nameof(dataDocument)),
        };
    }
}
