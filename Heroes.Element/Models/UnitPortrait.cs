namespace Heroes.Element.Models;

/// <summary>
/// Contains the unit portrait data.
/// </summary>
public class UnitPortrait
{
    /// <summary>
    /// Gets or sets the minimap icon file name.
    /// </summary>
    [JsonPropertyName("minimap")]
    public string? MiniMapIconFileName { get; set; }

    /// <summary>
    /// Gets or sets the target info panel file name.
    /// </summary>
    [JsonPropertyName("targetInfo")]
    public string? TargetInfoPanelFileName { get; set; }
}
