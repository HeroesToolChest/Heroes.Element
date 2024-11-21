namespace Heroes.Element.Models;

/// <summary>
/// Contains the announcer data.
/// </summary>
public class Announcer : HeroesCollectionObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Announcer"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Announcer(string id)
        : base(id)
    {
    }

    //[JsonPropertyOrder(1)]
    public string? Gender { get; set; }

    //[JsonPropertyOrder(2)]
    public string? HeroId { get; set; }

    //[JsonPropertyOrder(3)]
    public string? Image { get; set; }

    internal string? ImagePath { get; set; }
}
