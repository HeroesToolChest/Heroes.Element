namespace Heroes.Element.Models;

/// <summary>
/// Contains the map data.
/// </summary>
public class Map : ElementObject, IName
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Map"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Map(string id)
        : base(id)
    {
    }

    /// <inheritdoc/>
    public GameStringText? Name { get; set; }

    /// <summary>
    /// Gets or sets the map id. This id is found in a replay's tracker events, but it is not always set.
    /// <para>
    /// This is not unique. e.g. Snow Brawl has the same map id as Cursed Hollow.
    /// </para>
    /// </summary>
    public string? MapId { get; set; }

    /// <summary>
    /// Gets or sets the map link id. This is the id found in the xml files for CMap.
    /// <para>
    /// This is not unique (unless you exclude the sandbox maps). e.g. Cursed Hollow and Cursed Hollow (Sandbox) have the same map link.
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
    /// Gets or sets the map objectives information.
    /// </summary>
    public IList<MapObjective> MapObjectives { get; set; } = [];

    /// <summary>
    /// Gets or sets the relative path of the image that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? ReplayPreviewImagePath { get; set; }

    /// <summary>
    /// Gets or sets the relative path of the image that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? LoadingScreenImagePath { get; set; }
}
