namespace Heroes.Element.Models;

/// <summary>
/// Contains the basic animation data.
/// </summary>
public class Animation
{
    /// <summary>
    /// Gets or sets the original image file name (as .png).
    /// </summary>
    public string? Texture { get; set; }

    /// <summary>
    /// Gets or sets the animation count.
    /// </summary>
    public int Frames { get; set; }

    /// <summary>
    /// Gets or sets the animation duration.
    /// </summary>
    public int Duration { get; set; }
}
