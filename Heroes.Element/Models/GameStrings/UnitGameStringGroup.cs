namespace Heroes.Element.Models.GameStrings;

/// <summary>
/// Contains the gamestring properties for a unit.
/// </summary>
public class UnitGameStringGroup : IGameStringGroup
{
    /// <summary>
    /// Gets or sets the name property gamestrings.
    /// </summary>
    public NameGameStrings Name { get; set; } = new();

    /// <summary>
    /// Gets or sets the description property gamestrings.
    /// </summary>
    public DescriptionGameStrings Description { get; set; } = new();

    /// <summary>
    /// Gets or sets the life type property gamestrings.
    /// </summary>
    public LifeTypeGameStrings LifeType { get; set; } = new();

    /// <summary>
    /// Gets or sets the energy type property gamestrings.
    /// </summary>
    public EnergyTypeGameStrings EnergyType { get; set; } = new();

    /// <summary>
    /// Gets or sets the shield type property gamestrings.
    /// </summary>
    public ShieldTypeGameStrings ShieldType { get; set; } = new();
}
