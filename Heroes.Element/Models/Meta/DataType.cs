namespace Heroes.Element.Models.Meta;

/// <summary>
/// Specifies the type of data contained in a json file.
/// </summary>
public enum DataType
{
    /// <summary>
    /// Indicates an unknown data type.
    /// </summary>
    Unknown,

    /// <summary>
    /// Indicates gamestring data.
    /// </summary>
    GameStrings,

    /// <summary>
    /// Indicates hero data.
    /// </summary>
    HeroData,

    /// <summary>
    /// Indicates unit data.
    /// </summary>
    UnitData,

    /// <summary>
    /// Indicates announcer data.
    /// </summary>
    AnnouncerData,

    /// <summary>
    /// Indicates banner data.
    /// </summary>
    BannerData,

    /// <summary>
    /// Indicates boost data.
    /// </summary>
    BoostData,

    /// <summary>
    /// Indicates bundle data.
    /// </summary>
    BundleData,

    /// <summary>
    /// Indicates emoticon data.
    /// </summary>
    EmoticonData,

    /// <summary>
    /// Indicates emoticon pack data.
    /// </summary>
    EmoticonPackData,

    /// <summary>
    /// Indicates loot chest data.
    /// </summary>
    LootChestData,

    /// <summary>
    /// Indicates map data.
    /// </summary>
    MapData,

    /// <summary>
    /// Indicates match award data.
    /// </summary>
    MatchAwardData,

    /// <summary>
    /// Indicates mount data.
    /// </summary>
    MountData,

    /// <summary>
    /// Indicates portrait pack data.
    /// </summary>
    PortraitPackData,

    /// <summary>
    /// Indicates reward portrait data.
    /// </summary>
    RewardPortraitData,

    /// <summary>
    /// Indicates skin data.
    /// </summary>
    SkinData,

    /// <summary>
    /// Indicates spray data.
    /// </summary>
    SprayData,

    /// <summary>
    /// Indicates type description data.
    /// </summary>
    TypeDescriptionData,

    /// <summary>
    /// Indicates veterancy data.
    /// </summary>
    VeterancyData,

    /// <summary>
    /// Indicates voiceline data.
    /// </summary>
    VoiceLineData,
}