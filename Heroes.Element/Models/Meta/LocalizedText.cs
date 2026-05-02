namespace Heroes.Element.Models.Meta;

/// <summary>
/// Specifies the status of the <see cref="GameStringText"/> properties.
/// </summary>
public enum LocalizedText
{
    /// <summary>
    /// Indicates the <see cref="GameStringText"/> properties are in the file.
    /// </summary>
    None,

    /// <summary>
    /// Indicates the <see cref="GameStringText"/> properties are not in the file.
    /// </summary>
    Extract,
}
