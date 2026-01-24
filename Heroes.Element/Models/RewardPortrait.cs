namespace Heroes.Element.Models;

/// <summary>
/// Contains the reward portrait data.
/// </summary>
public class RewardPortrait : StoreItem, IImage, IImagePath
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RewardPortrait"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public RewardPortrait(string id)
        : base(id)
    {
        Rarity = Types.Rarity.Common;
    }

    /// <summary>
    /// Gets or sets the another description text.
    /// </summary>
    [JsonPropertyOrder(102)]
    public GameStringText? DescriptionUnearned { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="PortraitPack"/> id associated with the reward portrait.
    /// </summary>
    public string? PortraitPackId { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Hero"/> id associated with the reward portrait.
    /// </summary>
    public string? HeroId { get; set; }

    /// <summary>
    /// Gets or sets the icon slot number on the <see cref="TextureSheet"/>. Zero index based.
    /// </summary>
    public int IconSlot { get; set; }

    /// <inheritdoc/>
    public string? Image { get; set; }

    /// <summary>
    /// Gets or sets the original texture sheet.
    /// </summary>
    public TextureSheet TextureSheet { get; set; } = new TextureSheet();

    RelativeFilePath? IImagePath.ImagePath { get; set; }
}
