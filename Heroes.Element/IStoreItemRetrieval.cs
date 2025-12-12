namespace Heroes.Element;

/// <summary>
/// Represents an interface for retrieving elements by their store item type ids.
/// </summary>
/// <typeparam name="T">The type of the element, which must implement the <see cref="IStoreItem"/> and <see cref="IElementObject"/> interface.</typeparam>
public interface IStoreItemRetrieval<T> : IHyperlinkIdRetrieval<T>
        where T : IStoreItem, IElementObject
{
}
