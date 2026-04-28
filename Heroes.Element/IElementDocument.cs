namespace Heroes.Element;

/// <summary>
/// An interface representing a json document containing elements, such as heroes, units, announcers, etc.
/// </summary>
public interface IElementDocument : IDisposable
{
    /// <summary>
    /// Gets the element type that this data document represents.
    /// </summary>
    Type ElementType { get; }

    /// <summary>
    /// Gets the underlying JSON document. This is only the data document and not the optional <see cref="GameStringDocument"/>.
    /// </summary>
    JsonDocument JsonDocument { get; }

    /// <summary>
    /// Gets the optional underlying JSON gamestring document.
    /// </summary>
    GameStringDocument? GameStringDocument { get; }

    /// <summary>
    /// Gets the data meta properties.
    /// </summary>
    MetaDataProperties MetaDataProperties { get; }

    /// <summary>
    /// Gets a value indicating whether the HeroesVersion in the JSON data matches the version in the <see cref="GameStringDocument"/>.
    /// This returns <see langword="true"/> if <see cref="GameStringDocument"/> is <see langword="null"/>.
    /// </summary>
    bool IsMatchedHeroesVersion { get; }

    /// <summary>
    /// Gets a value indicating whether the HDP version in the JSON data matches the version in the <see cref="GameStringDocument"/>.
    /// This returns <see langword="true"/> if <see cref="GameStringDocument"/> is <see langword="null"/>.
    /// </summary>
    bool IsMatchedHdpVersion { get; }

    /// <summary>
    /// Gets a value indicating whether one of the data types in the <see cref="GameStringDocument"/> match with the JSON data <see cref="DataType"/>.
    /// This returns <see langword="true"/> if <see cref="GameStringDocument"/> is <see langword="null"/>.
    /// </summary>
    bool IsMatchedDataType { get; }

    /// <summary>
    /// Gets a value indicating whether the map name in the JSON data matches with the map name in the <see cref="GameStringDocument"/>.
    /// This returns <see langword="true"/> if <see cref="GameStringDocument"/> is <see langword="null"/>.
    /// </summary>
    bool IsMatchedMapName { get; }

    /// <summary>
    /// Gets all elements as <see cref="IElementObject"/>.
    /// </summary>
    /// <returns>A collection of all elements.</returns>
    IEnumerable <IElementObject> GetElementObjects();
}
