namespace Heroes.Element.Models.GameStrings;

/// <summary>
/// Contains the base implementation for game string properties that hold localized text values.
/// </summary>
public abstract class GameStringsBase : IGameStringProperty
{
    /// <inheritdoc/>
    public Dictionary<string, string> ValueByKey { get; set; } = new(StringComparer.Ordinal);
}
