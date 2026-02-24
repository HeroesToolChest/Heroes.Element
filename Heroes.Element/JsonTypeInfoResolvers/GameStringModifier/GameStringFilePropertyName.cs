namespace Heroes.Element.JsonTypeInfoResolvers.GameStringModifier;

/// <summary>
/// Represents a dictionary of game string file property names for the gamestring json file.
/// </summary>
public class GameStringFilePropertyName : SortedDictionary<string, GameStringFilePropertyId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GameStringFilePropertyName"/> class.
    /// </summary>
    public GameStringFilePropertyName()
        : base(StringComparer.OrdinalIgnoreCase)
    {
    }
}
