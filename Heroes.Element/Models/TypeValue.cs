namespace Heroes.Element.Models;

/// <summary>
/// Represents the base class for type value pairs data.
/// </summary>
public abstract record TypeValue
{
    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public double Value { get; set; }

    ///// <inheritdoc/>
    //public override string ToString()
    //{
    //    return $"{nameof(Type)}: {Type}, {nameof(Value)}: {Value}";
    //}
}
