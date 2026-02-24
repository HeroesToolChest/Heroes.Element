namespace Heroes.Element.JsonTypeInfoResolvers.GameStringModifier;

/// <summary>
/// Represents a dictionary of game string items for the gamestring json file.
/// </summary>
public class GameStringItemDictionary : SortedDictionary<string, GameStringFilePropertyName>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GameStringItemDictionary"/> class.
    /// </summary>
    public GameStringItemDictionary()
        : base(StringComparer.OrdinalIgnoreCase)
    {
    }
}
