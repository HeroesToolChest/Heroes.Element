namespace Heroes.Element.Models;

/// <summary>
/// An abstract class for an element object found in the in-game store collection.
/// </summary>
public abstract class HeroesCollectionObject : ElementObject, IHeroesCollectionObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HeroesCollectionObject"/> class.
    /// </summary>
    /// <param name="id">A unique identifier.</param>
    public HeroesCollectionObject(string id)
        : base(id)
    {
    }

    /// <inheritdoc/>
    [JsonPropertyOrder(-110)]
    public GameStringText? Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-100)]
    public GameStringText? SortName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-90)]
    public string? HyperlinkId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-80)]
    public string? AttributeId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-70)]
    public Franchise? Franchise { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-60)]
    public Rarity? Rarity { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-50)]
    public DateOnly? ReleaseDate { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-40)]
    public string? Category { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-30)]
    public string? Event { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-20)]
    public bool IsShownInStore { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public GameStringText? SearchText { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public GameStringText? Description { get; set; }
}
