namespace Heroes.Element.Models;

/// <summary>
/// An abstract class for a element object found in the in-game store collection.
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
    [JsonPropertyOrder(-10)]
    public TooltipDescription? Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-9)]
    public TooltipDescription? SortName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-8)]
    public string? HyperlinkId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-7)]
    public string? AttributeId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-6)]
    public Rarity? Rarity { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(-5)]
    public DateOnly? ReleaseDate { get; set; }

    /// <inheritdoc/>
    [JsonPropertyName("category")]
    [JsonPropertyOrder(-4)]
    public string? CollectionCategory { get; set; }

    /// <inheritdoc/>
    [JsonPropertyName("event")]
    [JsonPropertyOrder(-3)]
    public string? EventName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public ISet<string> SearchTexts { get; } = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public TooltipDescription? Description { get; set; }
}
