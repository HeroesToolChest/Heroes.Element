namespace Heroes.Element.Models.GameStrings;

/// <summary>
/// Contains the gamestring properties for an ability talent.
/// </summary>
public class AbilityTalentGameStringGroup : IGameStringGroup
{
    /// <summary>
    /// Gets or sets the name property gamestrings.
    /// </summary>
    public NameGameStrings Name { get; set; } = new();

    /// <summary>
    /// Gets or sets the energy text property gamestrings.
    /// </summary>
    public EnergyTextGameStrings EnergyText { get; set; } = new();

    /// <summary>
    /// Gets or sets the life text property gamestrings.
    /// </summary>
    public LifeTextGameStrings LifeText { get; set; } = new();

    /// <summary>
    /// Gets or sets the cooldown text property gamestrings.
    /// </summary>
    public CooldownTextGameStrings CooldownText { get; set; } = new();

    /// <summary>
    /// Gets or sets the short text property gamestrings.
    /// </summary>
    public ShortTextGameStrings ShortText { get; set; } = new();

    /// <summary>
    /// Gets or sets the full text property gamestrings.
    /// </summary>
    public FullTextGameStrings FullText { get; set; } = new();
}
