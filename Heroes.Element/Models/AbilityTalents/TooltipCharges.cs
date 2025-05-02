namespace Heroes.Element.Models.AbilityTalents;

/// <summary>
/// Contains the data for ability talent ingame-tooltip charges.
/// </summary>
public class TooltipCharges
{
    /// <summary>
    /// Gets or sets the maximum amount of charges, null if no charges available.
    /// </summary>
    public int? CountMax { get; set; }

    /// <summary>
    /// Gets or sets the starting amount of charges, null if no charges available.
    /// </summary>
    public int? CountStart { get; set; }

    /// <summary>
    /// Gets or sets the amount of charges consumed on use.
    /// </summary>
    public int? CountUse { get; set; }

    /// <summary>
    /// Gets or sets the cooldown between charge casts.
    /// </summary>
    public double? RecastCooldown { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the charge counts are hidden.
    /// </summary>
    public bool IsCountHidden { get; set; }

    /// <summary>
    /// Gets a value indicating whether max charges exists.
    /// </summary>
    [JsonIgnore]
    public bool HasCharges => CountMax.HasValue || (CountMax.HasValue && CountMax.Value > 0);

    /// <inheritdoc/>
    public override string ToString()
    {
        if (HasCharges)
            return $"Max Charges: {CountMax} - Start: {CountStart} - Use: {CountUse} - Hidden: {IsCountHidden}";
        else
            return "No charges";
    }
}
