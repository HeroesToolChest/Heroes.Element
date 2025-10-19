namespace Heroes.Element.Models.GameStrings;

/// <summary>
/// Contains the gamestring properties for a hero.
/// </summary>
public class HeroGameStringGroup : UnitGameStringGroup
{
    /// <summary>
    /// Gets or sets the sortname property gamestrings.
    /// </summary>
    public SortNameGameStrings SortName { get; set; } = new();

    /// <summary>
    /// Gets or sets the title property gamestrings.
    /// </summary>
    public TitleGameStrings Title { get; set; } = new();

    /// <summary>
    /// Gets or sets the difficulty property gamestrings.
    /// </summary>
    public DifficultyGameStrings Difficulty { get; set; } = new();

    /// <summary>
    /// Gets or sets the roles property gamestrings.
    /// </summary>
    public RolesGameStrings Roles { get; set; } = new();

    /// <summary>
    /// Gets or sets the expanded role property gamestrings.
    /// </summary>
    public ExpandedRoleGameStrings ExpandedRole { get; set; } = new();

    /// <summary>
    /// Gets or sets the searchtext property gamestrings.
    /// </summary>
    public SearchTextGameStrings SearchText { get; set; } = new();

    /// <summary>
    /// Gets or sets the infotext property gamestrings.
    /// </summary>
    public InfoTextGameStrings InfoText { get; set; } = new();
}
