namespace Heroes.Element.Models;

/// <summary>
/// Contains the announcer data.
/// </summary>
public class Announcer : LoadoutItem, IImage, IImagePath
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Announcer"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Announcer(string id)
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

    RelativeFilePath? IImagePath.ImagePath { get; set; }
}
