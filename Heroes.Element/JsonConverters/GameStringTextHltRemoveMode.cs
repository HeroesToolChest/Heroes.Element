namespace Heroes.Element.JsonConverters;

/// <summary>
/// Specifies how <c>hlt-name</c> attributes are handled during conversion.
/// </summary>
public enum GameStringTextHltRemoveMode
{
    /// <summary>
    /// Keep the <c>hlt-name</c> attribute as-is.
    /// </summary>
    None,

    /// <summary>
    ///  Remove the <c>hlt-name</c> attribute.
    /// </summary>
    Remove,

    /// <summary>
    /// Remove the <c>hlt-name</c> attribute and undo the font replacement value.
    /// </summary>
    RemoveAndUndo,
}
