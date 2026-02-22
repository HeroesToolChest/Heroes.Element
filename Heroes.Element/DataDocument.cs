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
    /// <param name="gameStringDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>An <see cref="IElementDocument"/> instance of the appropriate data document type.</returns>
    /// <exception cref="JsonException">The JSON document does not contain a valid <c>meta</c> property or <c>dataType</c>.</exception>
    /// <exception cref="ArgumentException">The <c>dataType</c> value is not recognized.</exception>
    public static IElementDocument Load(JsonDocument dataDocument, GameStringDocument? gameStringDocument = null)
    {
        DataType dataType = GetDataType(dataDocument);

        return LoadDataDocument(dataDocument, gameStringDocument, dataType);
    }

    private static DataType GetDataType(JsonDocument dataDocument)
    {
        if (!dataDocument.RootElement.TryGetProperty("meta", out JsonElement metaElement))
            throw new JsonException("No 'meta' property found in the JSON document.");

        if (!metaElement.TryGetProperty("dataType", out JsonElement dataTypeElement))
            throw new JsonException("No 'dataType' property found in the 'meta' object.");

        string dataTypeString = dataTypeElement.GetString() ?? throw new JsonException("The 'dataType' value is null.");

        if (!Enum.TryParse(dataTypeString, true, out DataType dataType))
            throw new JsonException($"Invalid data type '{dataTypeString}'.");

        return dataType;
    }

    private static IElementDocument LoadDataDocument(JsonDocument dataDocument, GameStringDocument? gameStringDocument, DataType dataType)
    {
        return dataType switch
        {
            DataType.HeroData => HeroDataDocument.Load(dataDocument, gameStringDocument),
            DataType.UnitData => UnitDataDocument.Load(dataDocument, gameStringDocument),
            DataType.AnnouncerData => AnnouncerDataDocument.Load(dataDocument, gameStringDocument),
            DataType.BannerData => BannerDataDocument.Load(dataDocument, gameStringDocument),
            DataType.BoostData => BoostDataDocument.Load(dataDocument, gameStringDocument),
            DataType.BundleData => BundleDataDocument.Load(dataDocument, gameStringDocument),
            DataType.EmoticonData => EmoticonDataDocument.Load(dataDocument, gameStringDocument),
            DataType.EmoticonPackData => EmoticonPackDataDocument.Load(dataDocument, gameStringDocument),
            DataType.LootChestData => LootChestDataDocument.Load(dataDocument, gameStringDocument),
            DataType.MapData => MapDataDocument.Load(dataDocument, gameStringDocument),
            DataType.MatchAwardData => MatchAwardDataDocument.Load(dataDocument, gameStringDocument),
            DataType.MountData => MountDataDocument.Load(dataDocument, gameStringDocument),
            DataType.PortraitPackData => PortraitPackDataDocument.Load(dataDocument, gameStringDocument),
            DataType.RewardPortraitData => RewardPortraitDataDocument.Load(dataDocument, gameStringDocument),
            DataType.SkinData => SkinDataDocument.Load(dataDocument, gameStringDocument),
            DataType.SprayData => SprayDataDocument.Load(dataDocument, gameStringDocument),
            DataType.TypeDescriptionData => TypeDescriptionDataDocument.Load(dataDocument, gameStringDocument),
            DataType.VeterancyData => VeterancyDataDocument.Load(dataDocument),
            DataType.VoiceLineData => VoiceLineDataDocument.Load(dataDocument, gameStringDocument),
            _ => throw new ArgumentException($"Unknown or not valid data type '{dataType}'.", nameof(dataDocument)),
        };
    }
}
