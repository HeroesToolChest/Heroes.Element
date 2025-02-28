namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Contains the data for talent data.
/// </summary>
public class Talent : AbilityTalentBase
{
    ///// <summary>
    ///// Initializes a new instance of the <see cref="Talent"/> class.
    ///// </summary>
    ///// <param name="abilityTalentId">Used for a unique id.</param>
    //public Talent(AbilityTalentId abilityTalentId)
    //    : base(abilityTalentId)
    //{
    //}

    /// <summary>
    /// Gets or sets the tier of the talent.
    /// </summary>
    public TalentTiers Tier { get; set; }

    /// <summary>
    /// Gets or sets the column number, also known as the sort index number.
    /// </summary>
    public int Column { get; set; }

    /// <summary>
    /// Gets a collection of ability and talent ids that the talent affects or upgrades.
    /// </summary>
    public ISet<string> AbilityTalentLinkIds { get; } = new HashSet<string>();

    /// <summary>
    /// Gets a collection of prerequisite talent ids.
    /// </summary>
    public ISet<string> PrerequisiteTalentIds { get; } = new HashSet<string>();
}
