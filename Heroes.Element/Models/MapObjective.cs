namespace Heroes.Element.Models;

/// <summary>
/// Contains the map objective data.
/// </summary>
public class MapObjective
{
    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    public GameStringText? Title { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public GameStringText? Description { get; set; }

    /// <summary>
    /// Gets or sets the icons for this map objective.
    /// </summary>
    public IList<MapObjectiveIcon> Icons { get; set; } = [];
}
