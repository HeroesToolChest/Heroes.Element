namespace Heroes.Element.Models;

/// <summary>
/// Contains the spray data.
/// </summary>
public class Spray : LoadoutItem, IImage, IImagePath
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Spray"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Spray(string id)
        : base(id)
    {
        Rarity = Types.Rarity.Common;
    }

    /// <inheritdoc/>
    public string? Image { get; set; }

    /// <summary>
    /// Gets or sets the animation properties.
    /// </summary>
    [JsonPropertyName("animation")]
    public SprayAnimation? Animation { get; set; }

    RelativeFilePath? IImagePath.ImagePath { get; set; }
}
