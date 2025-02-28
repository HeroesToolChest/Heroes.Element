namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Contains the neccessary properties for a unique identifier for abilities and talents.
/// </summary>
public class AbilityTalentId
{
    ///// <summary>
    ///// Initializes a new instance of the <see cref="AbilityTalentId"/> class.
    ///// </summary>
    ///// <param name="referenceId">The ability or talent id.</param>
    ///// <param name="buttonId">The button id.</param>
    //public AbilityTalentId(string referenceId, string buttonId)
    //{
    //    ReferenceId = referenceId;
    //    ButtonId = buttonId;
    //}

    /// <summary>
    /// Gets the unique id. Same as <see cref="ToString()"/>.
    /// <br/>
    /// <br/>
    /// Id is as follows: <see cref="ReferenceId"/>|<see cref="ButtonId"/>|<see cref="AbilityType"/>|<see cref="IsPassive"/>.
    /// </summary>
    public string Id => ToString();

    /// <summary>
    /// Gets or sets the reference id. This ability id or talent id.
    /// </summary>
    public string ReferenceId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the button id.
    /// </summary>
    public string ButtonId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the abilityType.
    /// </summary>
    public AbilityType AbilityType { get; set; } = AbilityType.Unknown;

    /// <summary>
    /// Gets or sets a value indicating whether this is a passive ability.
    /// </summary>
    public bool IsPassive { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{ReferenceId}|{ButtonId}|{AbilityType}|{IsPassive}";
    }
}
