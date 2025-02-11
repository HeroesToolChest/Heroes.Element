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
    public string? MiniMapIcon { get; set; }

    /// <summary>
    /// Gets or sets the target info panel file name.
    /// </summary>
    [JsonPropertyName("targetInfo")]
    public string? TargetInfoPanel { get; set; }

    /// <summary>
    /// Gets or sets the relative path of the mini map icon that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? MiniMapIconPath { get; set; }

    /// <summary>
    /// Gets or sets the relative path of the target info panel file that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? TargetInfoPanelPath { get; set; }
}
