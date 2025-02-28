using System.ComponentModel;

namespace Heroes.Element.Types;

/// <summary>
/// Specifices the tier of an ability.
/// </summary>
public enum AbilityTier
{
    /// <summary>
    /// Indicates an unknown tier.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Indicates a basic tier.
    /// </summary>
    Basic = 1 << 0,

    /// <summary>
    /// Indicates a heroic tier.
    /// </summary>
    Heroic = 1 << 1,

    /// <summary>
    /// Indicates a trait tier.
    /// </summary>
    Trait = 1 << 2,

    /// <summary>
    /// Indicates a mount tier.
    /// </summary>
    Mount = 1 << 3,

    /// <summary>
    /// Indicates an activable tier.
    /// </summary>
    Activable = 1 << 4,

    /// <summary>
    /// Indicates a hearth tier.
    /// </summary>
    Hearth = 1 << 5,

    /// <summary>
    /// Indicates a taunt tier.
    /// </summary>
    Taunt = 1 << 6,

    /// <summary>
    /// Indicates a dance tier.
    /// </summary>
    Dance = 1 << 7,

    /// <summary>
    /// Indicates a spray tier.
    /// </summary>
    Spray = 1 << 8,

    /// <summary>
    /// Indicates a voice tier.
    /// </summary>
    Voice = 1 << 9,

    /// <summary>
    /// Indicates a map mechanic tier.
    /// </summary>
    MapMechanic = 1 << 10,

    /// <summary>
    /// Indicates an interact tier.
    /// </summary>
    Interact = 1 << 11,

    /// <summary>
    /// Indicates an action tier.
    /// </summary>
    Action = 1 << 12,

    /// <summary>
    /// Indicates a hidden tier.
    /// </summary>
    Hidden = 1 << 13,
}
