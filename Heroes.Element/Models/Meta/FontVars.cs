namespace Heroes.Element.Models.Meta;

/// <summary>
/// Properties of the font variables in the gamestring text.
/// </summary>
public class FontVars
{
    /// <summary>
    /// Gets or sets a value indicating whether the font variables have been replaced with the actual color hex values.
    /// </summary>
    public bool Replaced { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the font variables have been preserved with the <c>hlt-name</c> attribute.
    /// </summary>
    public bool Preserved { get; set; }
}
