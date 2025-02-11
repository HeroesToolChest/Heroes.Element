using System.ComponentModel;

namespace Heroes.Element.Types;

/// <summary>
/// Specifies the tier of a talent.
/// </summary>
[Flags]
public enum TalentTiers
{
    /// <summary>
    /// Indicates the tier does not exist, is unknown, or has no tier.
    /// </summary>
    Old = 0,

    /// <summary>
    /// Indicates a level 1 tier.
    /// </summary>
    [Description("Level 1")]
    Level1 = 1 << 0,

    /// <summary>
    /// Indicates a level 4 tier.
    /// </summary>
    [Description("Level 4")]
    Level4 = 1 << 1,

    /// <summary>
    /// Indicates a level 7 tier.
    /// </summary>
    [Description("Level 7")]
    Level7 = 1 << 2,

    /// <summary>
    /// Indicates a level 10 tier.
    /// </summary>
    [Description("Level 10")]
    Level10 = 1 << 3,

    /// <summary>
    /// Indicates a level 13 tier.
    /// </summary>
    [Description("Level 13")]
    Level13 = 1 << 4,

    /// <summary>
    /// Indicates a level 16 tier.
    /// </summary>
    [Description("Level 16")]
    Level16 = 1 << 5,

    /// <summary>
    /// Indicates a level 20 tier.
    /// </summary>
    [Description("Level 20")]
    Level20 = 1 << 6,
}
