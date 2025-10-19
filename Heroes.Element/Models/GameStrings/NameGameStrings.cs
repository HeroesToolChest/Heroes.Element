namespace Heroes.Element.Models.GameStrings;

/// <summary>
/// Represents the gamestrings for the name property.
/// </summary>
public class NameGameStrings : IGameStringProperty
{
    /// <inheritdoc/>
    public Dictionary<string, string> ValueByKey { get; set; } = new(StringComparer.Ordinal);
}
