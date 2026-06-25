namespace Heroes.Element.Models;

/// <summary>
/// Contains the announcer pack data.
/// </summary>
public class AnnouncerPack : LoadoutItem, IImage, IImagePath
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AnnouncerPack"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public AnnouncerPack(string id)
        : base(id)
    {
        Rarity = Types.Rarity.Common;
    }

    /// <summary>
    /// Gets or sets the gender.
    /// </summary>
    public string? Gender { get; set; }

    /// <summary>
    /// Gets or sets the hero id.
    /// </summary>
    public string? HeroId { get; set; }

    /// <inheritdoc/>
    public string? Image { get; set; }

    ImagePath? IImagePath.ImagePath { get; set; }
}
