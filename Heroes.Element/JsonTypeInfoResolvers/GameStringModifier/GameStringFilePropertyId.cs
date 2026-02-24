namespace Heroes.Element.JsonTypeInfoResolvers.GameStringModifier;

/// <summary>
/// Represents a dictionary of game string file property ids for the gamestring json file.
/// </summary>
public class GameStringFilePropertyId
{
    /// <summary>
    /// Gets or sets the key-value pairs of <see cref="GameStringText"/>s.
    /// </summary>
    public SortedDictionary<string, GameStringText> KeyValuePairs { get; set; } = new SortedDictionary<string, GameStringText>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Gets or sets the key-array pairs of <see cref="GameStringText"/>s.
    /// </summary>
    public SortedDictionary<string, List<GameStringText>> KeyArrayPairs { get; set; } = new SortedDictionary<string, List<GameStringText>>(StringComparer.OrdinalIgnoreCase);
}