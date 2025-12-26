namespace Heroes.Element.Models;

/// <summary>
/// Contains the voiceline data.
/// </summary>
public class VoiceLine : LoadoutItem, IImage, IImagePath
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VoiceLine"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public VoiceLine(string id)
        : base(id)
    {
        Rarity = Types.Rarity.Common;
    }

    /// <summary>
    /// Gets or sets the <see cref="Hero"/> ids associated with the voiceline.
    /// </summary>
    public string? HeroId { get; set; }

    /// <inheritdoc/>
    public string? Image { get; set; }

    RelativeFilePath? IImagePath.ImagePath { get; set; }
}
