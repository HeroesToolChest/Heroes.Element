namespace Heroes.Element.Models;

/// <summary>
/// Contains the banner data.
/// </summary>
public class Banner : LoadoutItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Banner"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Banner(string id)
        : base(id)
    {
        Rarity = Types.Rarity.None;
    }
}
