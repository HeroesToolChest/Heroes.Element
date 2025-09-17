namespace Heroes.Element.Models.ObjectProperties;

/// <summary>
/// An interface for a name.
/// </summary>
public interface IName
{
    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    GameStringText? Name { get; set; }
}
