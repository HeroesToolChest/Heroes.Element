namespace Heroes.Element;

/// <summary>
/// An interface representing a json document containing elements, such as heroes, units, announcers, etc.
/// </summary>
public interface IElementDocument : IDisposable
{
    /// <summary>
    /// Gets the element type that this data document represents.
    /// </summary>
    Type GetElementType { get; }

    /// <summary>
    /// Gets the underlying JSON document. This is only the data document and not the optional gamestring document.
    /// </summary>
    JsonDocument JsonDocument { get; }

    /// <summary>
    /// Gets the optional underlying JSON gamestring document.
    /// </summary>
    GameStringDocument? GameStringDocument { get; }

    /// <summary>
    /// Gets the metadata properties associated with the JSON data. This includes properties overridden from the optional gamestring document.
    /// </summary>
    MetaDataProperties MetaProperties { get; }

    /// <summary>
    /// Gets a value indicating whether the HeroesVersion in the JSON data does not match the version in the <see cref="GameStringDocument"/>.
    /// This returns <see langword="false"/> if there is no <see cref="GameStringDocument"/>.
    /// </summary>
    bool MismatchedHeroesVersion { get; }

    /// <summary>
    /// Gets a value indicating whether the HDP version in the JSON data does not match the version in the <see cref="GameStringDocument"/>.
    /// </summary>
    bool MismatchedHdpVersion { get; }
}
