namespace Heroes.Element.Models.GameStrings;

/// <summary>
/// Contains extensions for <see cref="IElementObject"/>s.
/// </summary>
public static class ElementExtensions
{
    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties from the <paramref name="gameStringDocument"/>.
    /// </summary>
    /// <param name="hero">.</param>
    /// <param name="gameStringDocument">Instance of a <see cref="GameStringDocument"/>.</param>
    public static void UpdateGameStringTexts(this Hero hero, GameStringDocument gameStringDocument)
    {
        gameStringDocument.UpdateGameStrings(hero);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties from the <paramref name="gameStringDocument"/>.
    /// </summary>
    /// <param name="unit">.</param>
    /// <param name="gameStringDocument">Instance of a <see cref="GameStringDocument"/>.</param>
    public static void UpdateGameStringTexts(this Unit unit, GameStringDocument gameStringDocument)
    {
        gameStringDocument.UpdateGameStrings(unit);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties from the <paramref name="gameStringDocument"/>.
    /// </summary>
    /// <param name="announcer">.</param>
    /// <param name="gameStringDocument">Instance of a <see cref="GameStringDocument"/>.</param>
    public static void UpdateGameStringTexts(this Announcer announcer, GameStringDocument gameStringDocument)
    {
        gameStringDocument.UpdateGameStrings(announcer);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties from the <paramref name="gameStringDocument"/>.
    /// </summary>
    /// <param name="banner">.</param>
    /// <param name="gameStringDocument">Instance of a <see cref="GameStringDocument"/>.</param>
    public static void UpdateGameStringTexts(this Banner banner, GameStringDocument gameStringDocument)
    {
        gameStringDocument.UpdateGameStrings(banner);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties from the <paramref name="gameStringDocument"/>.
    /// </summary>
    /// <param name="boost">.</param>
    /// <param name="gameStringDocument">Instance of a <see cref="GameStringDocument"/>.</param>
    public static void UpdateGameStringTexts(this Boost boost, GameStringDocument gameStringDocument)
    {
        gameStringDocument.UpdateGameStrings(boost);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties from the <paramref name="gameStringDocument"/>.
    /// </summary>
    /// <param name="bundle">.</param>
    /// <param name="gameStringDocument">Instance of a <see cref="GameStringDocument"/>.</param>
    public static void UpdateGameStringTexts(this Bundle bundle, GameStringDocument gameStringDocument)
    {
        gameStringDocument.UpdateGameStrings(bundle);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties from the <paramref name="gameStringDocument"/>.
    /// </summary>
    /// <param name="lootChest">.</param>
    /// <param name="gameStringDocument">Instance of a <see cref="GameStringDocument"/>.</param>
    public static void UpdateGameStringTexts(this LootChest lootChest, GameStringDocument gameStringDocument)
    {
        gameStringDocument.UpdateGameStrings(lootChest);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties from the <paramref name="gameStringDocument"/>.
    /// </summary>
    /// <param name="map">.</param>
    /// <param name="gameStringDocument">Instance of a <see cref="GameStringDocument"/>.</param>
    public static void UpdateGameStringTexts(this Map map, GameStringDocument gameStringDocument)
    {
        gameStringDocument.UpdateGameStrings(map);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties from the <paramref name="gameStringDocument"/>.
    /// </summary>
    /// <param name="skin">.</param>
    /// <param name="gameStringDocument">Instance of a <see cref="GameStringDocument"/>.</param>
    public static void UpdateGameStringTexts(this Skin skin, GameStringDocument gameStringDocument)
    {
        gameStringDocument.UpdateGameStrings(skin);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties from the <paramref name="gameStringDocument"/>.
    /// </summary>
    /// <param name="voiceLine">.</param>
    /// <param name="gameStringDocument">Instance of a <see cref="GameStringDocument"/>.</param>
    public static void UpdateGameStringTexts(this VoiceLine voiceLine, GameStringDocument gameStringDocument)
    {
        gameStringDocument.UpdateGameStrings(voiceLine);
    }

    /// <summary>
    /// Updates the <see cref="GameStringText"/> properties from the <paramref name="gameStringDocument"/>.
    /// </summary>
    /// <param name="mount">.</param>
    /// <param name="gameStringDocument">Instance of a <see cref="GameStringDocument"/>.</param>
    public static void UpdateGameStringTexts(this Mount mount, GameStringDocument gameStringDocument)
    {
        gameStringDocument.UpdateGameStrings(mount);
    }
}
