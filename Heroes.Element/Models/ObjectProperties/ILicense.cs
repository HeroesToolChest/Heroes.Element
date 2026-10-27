namespace Heroes.Element.Models.ObjectProperties;

/// <summary>
/// An interface for license ids.
/// </summary>
public interface ILicense
{
    /// <summary>
    /// Gets or sets a unique collection of license ids associated with this object.
    /// </summary>
    ISet<long> Licenses { get; set; }
}
