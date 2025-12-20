namespace Heroes.Element.Models;

/// <summary>
/// Contains the properties for veterancy levels.
/// </summary>
public class VeterancyLevel
{
    /// <summary>
    /// Gets or sets the minimum xp for this level.
    /// </summary>
    public int MinimumVeterancyXP { get; set; }

    /// <summary>
    /// Gets or sets the veterancy modification.
    /// </summary>
    public VeterancyModification? VeterancyModification { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{nameof(MinimumVeterancyXP)}: {MinimumVeterancyXP}";
    }
}
