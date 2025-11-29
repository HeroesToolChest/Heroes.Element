namespace Heroes.Element.JsonTypeInfoResolvers.GameStringModifier;

/// <summary>
/// Represents a dictionary of game string file property ids for the gamestring json file.
/// </summary>
public class GameStringFilePropertyId //: SortedDictionary<string, GameStringText>;
{
    public SortedDictionary<string, GameStringText> KeyValuePairs { get; set; } = [];

    public SortedDictionary<string, List<GameStringText>> KeyArrayPairs { get; set; } = [];
}