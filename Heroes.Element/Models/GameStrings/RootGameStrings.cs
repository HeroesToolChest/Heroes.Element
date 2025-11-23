namespace Heroes.Element.Models.GameStrings;

/// <summary>
/// Contains the meta and the gamestrings.
/// </summary>
public class RootGameStrings
{
    internal const string GameStringItemPropertyName = "gamestrings";

    /// <summary>
    /// Gets or sets the meta properties.
    /// </summary>
    public MetaGameStringProperties Meta { get; set; } = new();
}
