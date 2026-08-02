namespace AngleSharp.Html.Construction;

/// <summary>
/// Represents a constructable document whose nodes are objects, so the document is itself a node in
/// the tree it is building. Backends whose nodes are value-type identities reach topology through
/// <see cref="IHtmlTreeConstructionFactory{TDocument,TNode}"/> instead and do not implement this.
/// </summary>
public interface IConstructableDocumentNode : IConstructableDocumentState, IConstructableNode
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
