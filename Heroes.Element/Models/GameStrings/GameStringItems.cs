namespace Heroes.Element.Models.GameStrings;

/// <summary>
/// Contains the different types of game string items.
/// </summary>
public class GameStringItems
{
    /// <summary>
    /// Gets or sets the ability talent gamestrings.
    /// </summary>
    [JsonPropertyName("abiltalent")]
    public AbilityTalentGameStringGroup AbilityTalent { get; set; } = new();

    /// <summary>
    /// Gets or sets the hero data gamestrings.
    /// </summary>
    public HeroGameStringGroup Hero { get; set; } = new();

    /// <summary>
    /// Gets or sets the unit data gamestrings.
    /// </summary>
    public UnitGameStringGroup Unit { get; set; } = new();
}
