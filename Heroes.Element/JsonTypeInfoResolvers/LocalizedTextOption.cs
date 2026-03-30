namespace Heroes.Element.JsonTypeInfoResolvers;

/// <summary>
/// Specifies how <see cref="GameStringText"/> properties were handled during serialization.
/// </summary>
public enum LocalizedTextOption
{
    /// <summary>
    /// Indicates that <see cref="GameStringText"/> properties are in the file.
    /// </summary>
    None,

    /// <summary>
    /// Indicates <see cref="GameStringText"/> properties have been removed in the file.
    /// </summary>
    Extract,

    /// <summary>
    /// Indicates <see cref="GameStringText"/> properties are in the file and have been copied to a gamestrings file.
    /// </summary>
    Copy,
}
