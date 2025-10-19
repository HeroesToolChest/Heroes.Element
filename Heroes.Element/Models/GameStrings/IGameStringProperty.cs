namespace Heroes.Element.Models.GameStrings;

/// <summary>
/// Interface for game string properties that hold localized text values.
/// </summary>
public interface IGameStringProperty
{
    /// <summary>
    /// Gets or sets a dictionary that maps keys to their corresponding values. The values are the localized gamestring text.
    /// </summary>
    Dictionary<string, string> ValueByKey { get; set; }
}
