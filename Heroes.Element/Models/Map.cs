namespace Heroes.Element.Models;

/// <summary>
/// Contains the map data.
/// </summary>
public class Map : ElementObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Map"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Map(string id)
        : base(id)
    {
    }

    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    public TooltipDescription? Name { get; set; }

    /// <summary>
    /// Gets or sets the map id. This id is found in a replay's tracker events. It is not always set.
    /// </summary>
    public string? MapId { get; set; }

    /// <summary>
    /// Gets or sets the map link id. This is the id found in the xml files for CMap.
    /// <para>
    /// This is not unique. Cursed Hollow and Cursed Hollow (Sandbox) share the same map link.
    /// </para>
    /// </summary>
    public string? MapLink { get; set; }

    /// <summary>
    /// Gets or sets the map size.
    /// </summary>
    public MapSize? MapSize { get; set; }

    /// <summary>
    /// Gets or sets the image used in the replay tab.
    /// </summary>
    public string? ReplayPreviewImage { get; set; }

    /// <summary>
    /// Gets or sets the image used in the loading screen.
    /// </summary>
    public string? LoadingScreenImage { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public IList<MapObjective> MapObjectives { get; set; } = [];

    internal string? ReplayPreviewImagePath { get; set; }

    internal string? LoadingScreenImagePath { get; set; }
}
