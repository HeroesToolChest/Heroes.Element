namespace Heroes.Element.Models;

/// <summary>
/// Contains the animation data for a spray image.
/// </summary>
public class SprayAnimation
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
