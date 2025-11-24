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
}
