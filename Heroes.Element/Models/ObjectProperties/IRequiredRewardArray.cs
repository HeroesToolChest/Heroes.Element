namespace Heroes.Element.Models.ObjectProperties;

/// <summary>
/// An interface for required reward array ids.
/// </summary>
public interface IRequiredRewardArray
{
    /// <summary>
    /// Gets or sets a unique collection of required <see cref="RewardLicense"/> ids associated with this object.
    /// </summary>
    ISet<string> RequiredRewardArray { get; set; }
}
