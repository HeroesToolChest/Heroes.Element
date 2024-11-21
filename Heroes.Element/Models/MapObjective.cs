namespace Heroes.Element.Models;

/// <summary>
/// 
/// </summary>
public class MapObjective
{
    /// <summary>
    /// 
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public IList<MapObjectiveIcon> Icons { get; set; } = [];
}
