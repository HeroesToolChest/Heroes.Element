namespace Heroes.Element.Models;

/// <summary>
/// Contains the texture sheet data.
/// </summary>
public class TextureSheet
{
    /// <summary>
    /// Gets or sets the image file name.
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// Gets or sets the number of rows in the texture.
    /// </summary>
    public int? Rows { get; set; }

    /// <summary>
    /// Gets or sets the number of column in the texture.
    /// </summary>
    public int? Columns { get; set; }

    /// <summary>
    /// Gets or sets the original image file name with path (as .dds). Path separators are not normalized.
    /// </summary>
    /// <remarks><see cref="Image"/> is for serialization purposes, so this property is for keeping track of the image.</remarks>
    internal string? ImagePath { get; set; }
}
