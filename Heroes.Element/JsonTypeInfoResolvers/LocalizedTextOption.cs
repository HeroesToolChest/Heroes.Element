namespace Heroes.Element.JsonTypeInfoResolvers;

/// <summary>
/// Specifies the serialization options for the <see cref="GameStringText"/> properties.
/// </summary>
public enum LocalizedTextOption
{
    /// <summary>
    /// Indicates the <see cref="GameStringText"/> properties should be left in the file.
    /// </summary>
    None,

    /// <summary>
    /// Indicates the <see cref="GameStringText"/> properties should be removed from the file and copied to a gamestring file.
    /// </summary>
    Extract,

    /// <summary>
    /// Indicates the <see cref="GameStringText"/> properties should be left in the file and copied to a gamestrings file.
    /// </summary>
    Copy,
}
