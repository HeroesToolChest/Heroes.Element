namespace Heroes.Element.JsonTypeInfoResolvers;

/// <summary>
/// Specifies how to handle <see cref="GameStringText"/> properties during serialization.
/// </summary>
public enum LocalizedTextOption
{
    /// <summary>
    /// Indicates no localized text.
    /// </summary>
    None,

    /// <summary>
    /// Indicates <see cref="GameStringText"/> properties will be removed from the data files and saved to a separate file.
    /// </summary>
    Extract,

    /// <summary>
    /// Indicates <see cref="GameStringText"/> properties will be saved to a separate file and kept in the data files.
    /// </summary>
    Copy,
}
