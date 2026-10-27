namespace Heroes.Element.Models;

/// <summary>
/// Contains the reward license data.
/// </summary>
public class RewardLicense : ElementObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RewardLicense"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public RewardLicense(string id)
        : base(id)
    {
    }

    /// <summary>
    /// Gets or sets the license id.
    /// </summary>
    public long LicenseId { get; set; }
}
