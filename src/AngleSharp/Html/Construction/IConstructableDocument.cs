namespace AngleSharp.Html.Construction;

/// <summary>
/// Represents a constructable document that is a DOM-shaped node and participates in a browsing
/// host's script and load lifecycle. Handle-oriented construction backends only need to implement
/// <see cref="IConstructableDocumentState"/>.
/// </summary>
public interface IConstructableDocument : IConstructableDocumentHost, IConstructableNode
{
    /// <summary>
    /// Head element of the document.
    /// </summary>
    IConstructableElement? Head { get; }

    /// <summary>
    /// Document element of the document.
    /// </summary>
    IConstructableElement DocumentElement { get; }

    /// <summary>
    /// Clears the document.
    /// </summary>
    void Clear();
}
