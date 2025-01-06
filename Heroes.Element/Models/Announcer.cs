namespace Heroes.Element.Models;

/// <summary>
/// Contains the announcer data.
/// </summary>
public class Announcer : HeroesCollectionObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Announcer"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Announcer(string id)
        : base(id)
    {
        Rarity = Types.Rarity.None;
    }

    /// <summary>
    /// Gets or sets the gender.
    /// </summary>
    public string? Gender { get; set; }

    /// <summary>
    /// Gets or sets the hero id.
    /// </summary>
    public string? HeroId { get; set; }

    /// <summary>
    /// Gets or sets the file name of the image.
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// Gets or sets the relative path of the image that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? ImagePath { get; set; }
}
