namespace Heroes.Element.Types;

/// <summary>
/// Specifies the type of an ability.
/// </summary>
[Flags]
public enum AbilityTypes
{
    /// <summary>
    /// Indicates an unknown type.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Indicates a Q type.
    /// </summary>
    Q = 1 << 0,

    /// <summary>
    /// Indicates a W type.
    /// </summary>
    W = 1 << 1,

    /// <summary>
    /// Indicates am E type.
    /// </summary>
    E = 1 << 2,

    /// <summary>
    /// Indicates a heroic type.
    /// </summary>
    Heroic = 1 << 3,

    /// <summary>
    /// Indicates a Z type.
    /// </summary>
    Z = 1 << 4,

    /// <summary>
    /// Indicates a trait type.
    /// </summary>
    Trait = 1 << 5,

    /// <summary>
    /// Indicates an active type.
    /// </summary>
    Active = 1 << 6,

    /// <summary>
    /// Indicates a passive type.
    /// </summary>
    Passive = 1 << 7,

    /// <summary>
    /// Indicates a B type.
    /// </summary>
    B = 1 << 8,

    /// <summary>
    /// Indicates an attack type.
    /// </summary>
    Attack = 1 << 9,

    /// <summary>
    /// Indicates a stop type.
    /// </summary>
    Stop = 1 << 10,

    /// <summary>
    /// Indicates a hold type.
    /// </summary>
    Hold = 1 << 11,

    /// <summary>
    /// Indicates a cancel type.
    /// </summary>
    Cancel = 1 << 12,

    /// <summary>
    /// Indicates an interact type.
    /// </summary>
    Interact = 1 << 13,

    /// <summary>
    /// Indicates a taunt type.
    /// </summary>
    Taunt = 1 << 14,

    /// <summary>
    /// Indicates a dance type.
    /// </summary>
    Dance = 1 << 15,

    /// <summary>
    /// Indicates a spray type.
    /// </summary>
    Spray = 1 << 16,

    /// <summary>
    /// Indicates a voice type.
    /// </summary>
    Voice = 1 << 17,

    /// <summary>
    /// Indicates a force move type.
    /// </summary>
    ForceMove = 1 << 18,

    /// <summary>
    /// Indicates a map mechanic type.
    /// </summary>
    MapMechanic = 1 << 19,

    /// <summary>
    /// Indicates a hidden type.
    /// </summary>
    Hidden = 1 << 20,

    /// <summary>
    /// Indicates a standard type.
    /// </summary>
    Standard = Q | W | E | Heroic | Z | Trait | Active | Passive | B,

    /// <summary>
    /// Indicates a special type.
    /// </summary>
    Special = Taunt | Dance | Spray | Voice | MapMechanic,

    /// <summary>
    /// Indicates a misc type.
    /// </summary>
    Misc = Attack | Stop | Hold | Cancel | Interact | ForceMove,
}