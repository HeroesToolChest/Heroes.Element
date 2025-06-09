namespace Heroes.Element.Models;

/// <summary>
/// An abstract class for all the game data models.
/// </summary>
public abstract class ElementObject : IEquatable<ElementObject>, IElementObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ElementObject"/> class.
    /// </summary>
    /// <param name="id">A unique identifier.</param>
    public ElementObject(string id)
    {
        Id = id;
    }

    /// <inheritdoc/>
    [JsonIgnore]
    public string Id { get; internal set; } = string.Empty;

    /// <summary>
    /// Gets the debugger display.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    protected string DebuggerDisplay
    {
        get
        {
            return $"{{{Id}}}";
        }

    }

    /// <inheritdoc/>
    public bool Equals(ElementObject? other)
    {
        if (!Id.Equals(other?.Id, StringComparison.Ordinal))
            return false;

        return Id.Equals(other.Id, StringComparison.Ordinal);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as ElementObject);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(Id.ToUpperInvariant());
    }
}
