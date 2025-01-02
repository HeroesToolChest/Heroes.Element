namespace Heroes.Element.Models;

/// <summary>
/// Contains the loot chest data.
/// </summary>
public class LootChest : ElementObject, IName, IHyperlinkId, IRarity, IEventName, IDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LootChest"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public LootChest(string id)
        : base(id)
    {
    }

    /// <inheritdoc/>
    public TooltipDescription? Name { get; set; }

    /// <inheritdoc/>
    public string? HyperlinkId { get; set; }

    /// <inheritdoc/>
    public Rarity? Rarity { get; set; } = Types.Rarity.Common;

    /// <inheritdoc/>
    public string? Event { get; set; }

    /// <summary>
    /// Gets or sets the maximum numbers of re-rolls for this loot chest.
    /// </summary>
    public int MaxRerolls { get; set; }

    /// <summary>
    /// Gets or sets the type description id.
    /// </summary>
    public string? TypeDescription { get; set; } = string.Empty;

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public TooltipDescription? Description { get; set; }
}
