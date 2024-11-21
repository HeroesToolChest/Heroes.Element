namespace Heroes.Element.Models;

/// <summary>
/// An interface to present a franchise.
/// </summary>
public interface IFranchise
{
    /// <summary>
    /// Gets or sets the franchise the bundle belongs to.
    /// </summary>
    public Franchise? Franchise { get; set; }
}
