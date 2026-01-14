namespace Heroes.Element.Models;

/// <summary>
/// Contains the animation data for a <see cref="Emoticon"/> image.
/// </summary>
public class EmoticonAnimation : Animation
{
    /// <summary>
    /// Gets or sets the width of the image.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Gets or sets the number of rows in the texture.
    /// </summary>
    public int? Rows { get; set; }

    /// <summary>
    /// Gets or sets the number of column in the texture.
    /// </summary>
    public int? Columns { get; set; }
}
