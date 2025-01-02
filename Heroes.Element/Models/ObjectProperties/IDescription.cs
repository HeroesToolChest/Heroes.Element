namespace Heroes.Element.Models.ObjectProperties;

/// <summary>
/// An interface for a description.
/// </summary>
public interface IDescription
{
    /// <summary>
    /// Gets or sets the description text.
    /// </summary>
    TooltipDescription? Description { get; set; }
}
