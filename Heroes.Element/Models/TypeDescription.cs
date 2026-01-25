namespace Heroes.Element.Models;

/// <summary>
/// Contains the type description data.
/// </summary>
public class TypeDescription : ElementObject, IName
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TypeDescription"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public TypeDescription(string id)
        : base(id)
    {
    }

    /// <inheritdoc/>
    public GameStringText? Name { get; set; }

    /// <summary>
    /// Gets or sets the reward icon.
    /// </summary>
    public string? RewardIcon { get; set; }

    /// <summary>
    /// Gets or sets the large icon.
    /// </summary>
    public string? LargeIcon { get; set; }

    /// <summary>
    /// Gets or sets the icon slot number on the <see cref="TextureSheet"/>. Zero index based.
    /// </summary>
    internal int IconSlot { get; set; }

    /// <summary>
    /// Gets or sets the original texture sheet.
    /// </summary>
    internal TextureSheet TextureSheet { get; set; } = new TextureSheet();

    internal RelativeFilePath? RewardIconPath { get; set; }

    internal RelativeFilePath? LargeIconPath { get; set; }
}
