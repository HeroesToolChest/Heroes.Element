namespace Heroes.Element.JsonTypeInfoResolvers;

/// <summary>
/// Specifies the location of the <see cref="GameStringText"/> properties.
/// </summary>
public enum LocalizedTextOption
{
    /// <summary>
    /// Indicates the <see cref="GameStringText"/> properties are in the file.
    /// </summary>
    None,

    /// <summary>
    /// Indicates the <see cref="GameStringText"/> properties are not in the file.
    /// </summary>
    Extract,

    /// <summary>
    /// Indicates the <see cref="GameStringText"/> properties are in the file and have been copied to a gamestrings file.
    /// </summary>
    Copy,
}
