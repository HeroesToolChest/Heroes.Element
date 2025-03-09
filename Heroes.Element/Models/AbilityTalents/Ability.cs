namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Contains the data for ability data.
/// </summary>
public class Ability : AbilityTalentBase
{
    ///// <summary>
    ///// Initializes a new instance of the <see cref="Ability"/> class.
    ///// </summary>
    ///// <param name="abilityTalentId">Used for a unique id.</param>
    //public Ability(AbilityTalentId abilityTalentId)
    //    : base(abilityTalentId)
    //{
    //    IsActive = true;
    //}

    public Ability()
    {
        IsActive = true;
    }

    /// <summary>
    /// Gets or sets the tier of the ability.
    /// </summary>
    public AbilityTier Tier { get; set; } = AbilityTier.Unknown;
}
