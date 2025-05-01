namespace Heroes.Element.Models;

/// <summary>
/// Contains the properties for hero ratings.
/// </summary>
public class HeroRatings
{
    /// <summary>
    /// The default rating.
    /// </summary>
    public const double DefaultRating = 1.0;

    /// <summary>
    /// Gets or sets the complexity rating.
    /// </summary>
    public double Complexity { get; set; } = DefaultRating;

    /// <summary>
    /// Gets or sets the damage rating.
    /// </summary>
    public double Damage { get; set; } = DefaultRating;

    /// <summary>
    /// Gets or sets the survivability rating.
    /// </summary>
    public double Survivability { get; set; } = DefaultRating;

    /// <summary>
    /// Gets or sets the utility rating.
    /// </summary>
    public double Utility { get; set; } = DefaultRating;

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{nameof(Damage)}: {Damage} - {nameof(Utility)}: {Utility} - {nameof(Survivability)}: {Survivability} - {nameof(Complexity)}: {Complexity}";
    }
}
