namespace Heroes.Element.Types;

/// <summary>
/// Specifies the tier of an ability (e.g Basic, Heroic).
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
    Basic = 1,

    /// <summary>
    /// Indicates a heroic tier.
    /// </summary>
    Heroic = 2,

    /// <summary>
    /// Indicates a trait tier.
    /// </summary>
    Trait = 3,

    /// <summary>
    /// Indicates a mount tier.
    /// </summary>
    Mount = 4,

    /// <summary>
    /// Indicates an activable tier.
    /// </summary>
    Activable = 5,

    /// <summary>
    /// Indicates a hearth tier.
    /// </summary>
    Hearth = 6,

    /// <summary>
    /// Indicates a taunt tier.
    /// </summary>
    Taunt = 7,

    /// <summary>
    /// Indicates a dance tier.
    /// </summary>
    Dance = 8,

    /// <summary>
    /// Indicates a spray tier.
    /// </summary>
    Spray = 9,

    /// <summary>
    /// Indicates a voice tier.
    /// </summary>
    Voice = 10,

    /// <summary>
    /// Indicates a map mechanic tier.
    /// </summary>
    MapMechanic = 11,

    /// <summary>
    /// Indicates an interact tier.
    /// </summary>
    Interact = 12,

    /// <summary>
    /// Indicates an action tier.
    /// </summary>
    Action = 13,

    /// <summary>
    /// Indicates a hidden tier.
    /// </summary>
    Hidden = 14,
}
