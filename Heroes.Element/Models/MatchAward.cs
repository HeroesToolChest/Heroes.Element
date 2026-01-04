namespace Heroes.Element.Models;

/// <summary>
/// Contains the match award data.
/// </summary>
public class MatchAward : ElementObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MatchAward"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public MatchAward(string id)
        : base(id)
    {
    }

    /// <summary>
    /// Gets or sets the game link.
    /// </summary>
    public string? GameLink { get; set; }

    /// <summary>
    /// Gets or sets the unique tag.
    /// </summary>
    public string? Tag { get; set; }

    /// <summary>
    /// Gets or sets the score screen name.
    /// </summary>
    public GameStringText? ScoreScreenName { get; set; }

    /// <summary>
    /// Gets or sets the score screen description.
    /// </summary>
    public GameStringText? ScoreScreenDescription { get; set; }

    /// <summary>
    /// Gets or sets the end of match name.
    /// </summary>
    public GameStringText? EndOfMatchName { get; set; }

    /// <summary>
    /// Gets or sets the end of match description.
    /// </summary>
    public GameStringText? EndOfMatchDescription { get; set; }

    /// <summary>
    /// Gets or sets the end of match tooltip text.
    /// </summary>
    public GameStringText? EndOfMatchTooltipText { get; set; }

    /// <summary>
    /// Gets or sets the MVP screen image file name.
    /// </summary>
    [JsonPropertyName("mvpScreenIcon")]
    public string? MVPScreenImage { get; set; }

    /// <summary>
    /// Gets or sets the score screen image file name.
    /// </summary>
    [JsonPropertyName("scoreScreenIcon")]
    public string? ScoreScreenImage { get; set; }

    /// <summary>
    /// Gets or sets the relative path of the MVP screen image file that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? MVPScreenImagePath { get; set; }

    /// <summary>
    /// Gets or sets the relative path of the score screen image blue file that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? ScoreScreenImageBluePath { get; set; }

    /// <summary>
    /// Gets or sets the relative path of the score screen image red file that resides in CASC or on file.
    /// </summary>
    internal RelativeFilePath? ScoreScreenImageRedPath { get; set; }
}
