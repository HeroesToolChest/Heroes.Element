namespace Heroes.Element.Models;

/// <summary>
/// Contains the emoticon data.
/// </summary>
public class Emoticon : ElementObject, IImage, IImagePath, IDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Emoticon"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Emoticon(string id)
        : base(id)
    {
    }

    /// <summary>
    /// Gets or sets the expression name of the emoticon.
    /// </summary>
    public string? Expression { get; set; }

    /// <summary>
    /// Gets or sets the description text.
    /// </summary>
    public GameStringText? Description { get; set; }

    /// <summary>
    /// Gets or sets a unique collection of universal aliases.
    /// </summary>
    public ISet<string> UniversalAliases { get; set; } = new HashSet<string>();

    /// <summary>
    /// Gets or sets a unique collection of localized aliases.
    /// </summary>
    public ISet<GameStringText> LocalizedAliases { get; set; } = new HashSet<GameStringText>(new GameStringTextEqualityComparer());

    /// <summary>
    /// Gets or sets the search texts.
    /// </summary>
    public GameStringText? SearchText { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the aliases are case sensitive.
    /// </summary>
    public bool IsCaseSensitive { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the emoticon is hidden.
    /// </summary>
    public bool IsHidden { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Hero"/> id associated with the emoticon.
    /// </summary>
    public string? HeroId { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Skin"/> id associated with the emoticon.
    /// </summary>
    public string? SkinId { get; set; }

    /// <inheritdoc/>
    public string? Image { get; set; }

    /// <summary>
    /// Gets or sets the animation properties.
    /// </summary>
    public EmoticonAnimation? Animation { get; set; }

    RelativeFilePath? IImagePath.ImagePath { get; set; }

    internal TextureSheet TextureSheet { get; set; } = new TextureSheet();

    /// <summary>
    /// Gets or sets the index of the static image in the texture sheet.
    /// </summary>
    internal int Index { get; set; }

    /// <summary>
    /// Gets or sets the width of the static image in the texture sheet.
    /// </summary>
    internal int? Width { get; set; }
}
