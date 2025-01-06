namespace Heroes.Element.Models;

/// <summary>
/// Contains the map objective icon data.
/// </summary>
public class MapObjectiveIcon
{
    /// <summary>
    /// Gets or sets the image used in the loading screen.
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// Gets or sets the (adjusted) height of the image.
    /// </summary>
    public int? Height { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the width should be scaled according to the height.
    /// </summary>
    public bool ScaleWidth { get; set; }

    /// <summary>
    /// Gets or sets the relative path of the image that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? ImagePath { get; set; }
}
