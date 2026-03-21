namespace Heroes.Element.Models;

/// <summary>
/// Contains the texture sheet data.
/// </summary>
public class TextureSheet
{
    /// <summary>
    /// Gets or sets the original image file name with path (as .dds). File path separator is the original found data source.
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
}
