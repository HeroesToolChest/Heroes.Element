namespace Heroes.Element.Models;

/// <summary>
/// Contains the hero data.
/// </summary>
public class Hero : Unit, IFranchise
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Hero"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Hero(string id)
        : base(id)
    {
    }

    /// <summary>
    /// Gets or sets the difficulty of the hero.
    /// </summary>
    public TooltipDescription? Difficulty { get; set; }

    /// <inheritdoc/>
    public Franchise? Franchise { get; set; }

    /// <summary>
    /// Gets or sets the hero title.
    /// </summary>
    public TooltipDescription? Title { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this hero is melee.
    /// </summary>
    public bool IsMelee { get; set; } = false;

    /// <summary>
    /// Gets a unique collection roles of the hero, multiclass will be first if hero has multiple roles.
    /// </summary>
    public ISet<string> Roles { get; } = new HashSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets or sets the expanded role of the hero.
    /// </summary>
    public TooltipDescription? ExpandedRole { get; set; }

    /// <summary>
    /// Gets or sets the default mount id of this hero.
    /// </summary>
    public string? DefaultMountId { get; set; }

    /// <summary>
    /// Gets or sets the ratings of the hero.
    /// </summary>
    public HeroRatings Ratings { get; set; } = new HeroRatings();

    /// <summary>
    /// Gets or sets the hero portraits.
    /// </summary>
    public HeroPortrait Portraits { get; set; } = new HeroPortrait();

    /// <summary>
    /// Gets a unique collection of <see cref="HeroSkin"/> ids that are associated with this hero.
    /// </summary>
    public ISet<string> SkinIds { get; } = new HashSet<string>();

    /// <summary>
    /// Gets a unique collection of <see cref="HeroSkin"/> ids that are associated with this hero.
    /// </summary>
    public ISet<string> VariationSkinIds { get; } = new HashSet<string>();

    /// <summary>
    /// Gets a unique colection of <see cref="VoiceLine"/> ids that are associated with this hero.
    /// </summary>
    public ISet<string> VoiceLineIds { get; } = new HashSet<string>();

    /// <summary>
    /// Gets a unique collection of <see cref="Mount.MountCategory"/> ids that this hero is allowed to use.
    /// </summary>
    public ISet<string> MountCategoryIds { get; } = new HashSet<string>();

    // TODO: Hero Units
}
