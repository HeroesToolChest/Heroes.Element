namespace Heroes.Element.Models;

/// <summary>
/// Contains the properties for hero portraits.
/// </summary>
public class HeroPortrait : UnitPortrait
{
    /// <summary>
    /// Gets or sets the hero select portrait file name.
    /// </summary>
    [JsonPropertyName("heroSelect")]
    public string? HeroSelectPortrait { get; set; }

    /// <summary>
    /// Gets or sets the score screen portrait file name.
    /// </summary>
    [JsonPropertyName("leaderboard")]
    public string? LeaderboardPortrait { get; set; }

    /// <summary>
    /// Gets or sets the loading screen portrait file name.
    /// </summary>
    [JsonPropertyName("loading")]
    public string? LoadingScreenPortrait { get; set; }

    /// <summary>
    /// Gets or sets the party panel portrait file name.
    /// </summary>
    [JsonPropertyName("partyPanel")]
    public string? PartyPanelPortrait { get; set; }

    /// <summary>
    /// Gets or sets the target portrait file name.
    /// </summary>
    [JsonPropertyName("target")]
    public string? TargetPortrait { get; set; }

    /// <summary>
    /// Gets or sets the draft screen file name.
    /// </summary>
    [JsonPropertyName("draftScreen")]
    public string? DraftScreen { get; set; }

    /// <summary>
    /// Gets the party frame file name.
    /// </summary>
    [JsonPropertyName("partyFrames")]
    public ICollection<string> PartyFrames { get; } = [];

    /// <summary>
    /// Gets or sets the relative path of the hero select portrait that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? HeroSelectPortraitPath { get; set; }

    /// <summary>
    /// Gets or sets the relative path of the score screen portrait that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? LeaderboardPortraitPath { get; set; }

    /// <summary>
    /// Gets or sets the relative path of the loading screen portrait that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? LoadingScreenPortraitPath { get; set; }

    /// <summary>
    /// Gets or sets the relative path of the party panel portrait that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? PartyPanelPortraitPath { get; set; }

    /// <summary>
    /// Gets or sets the relative path of the target portrait that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? TargetPortraitPath { get; set; }

    /// <summary>
    /// Gets or sets the relative path of the draft screen file that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? DraftScreenPath { get; set; }

    /// <summary>
    /// Gets the relative path of the party frame files that resides in CASC or on file.
    /// </summary>
    internal IList<RelativeFilePath> PartyFramePaths { get; } = [];
}
