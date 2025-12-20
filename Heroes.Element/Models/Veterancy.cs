namespace Heroes.Element.Models;

/// <summary>
/// Contains the veterancy data.
/// </summary>
public class Veterancy : ElementObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Veterancy"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Veterancy(string id)
        : base(id)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether this is a combine modification type.
    /// </summary>
    public bool CombineModifications { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a combine xp type.
    /// </summary>
    public bool CombineXP { get; set; }

    /// <summary>
    /// Gets or sets the collection of veterancy levels.
    /// </summary>
    public IList<VeterancyLevel> VeterancyLevels { get; set; } = [];
}
