namespace Heroes.Element.Models.ObjectProperties;

/// <summary>
/// An interface for reward license ids.
/// </summary>
public interface IRewardLicenseIds
{
    /// <summary>
    /// Gets or sets a unique collection of <see cref="RewardLicense"/> ids associated with this object.
    /// </summary>
    ISet<string> RewardLicenseIds { get; set; }
}
