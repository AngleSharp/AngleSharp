namespace AngleSharp.Html.Construction;

using System;
using AngleSharp.Dom;
using Parser.Tokens.Struct;
using Text;

/// <summary>
/// Represents the document state required by the HTML tree-construction algorithm. This is
/// deliberately free of node and tree members: a construction backend reaches tree topology through
/// <see cref="IHtmlTreeConstructionFactory{TDocument,TNode}"/>, so a backend whose nodes are
/// value-type identities never needs a DOM-shaped document object. Backends that also participate in
/// a browsing host's script and load lifecycle additionally implement
/// <see cref="IConstructableDocumentHost"/>.
/// </summary>
public interface IConstructableDocumentState
{
    /// <summary>
    /// Document source.
    /// </summary>
    TextSource Source { get; }

    /// <summary>
    /// Builder instance to dispose and tie lifetime to the document.
    /// </summary>
    IDisposable? Builder { get; set; }

    /// <summary>
    /// Quirks mode of the document.
    /// </summary>
    QuirksMode QuirksMode { get; set; }

    /// <summary>
    /// Adds a new DOM representation of a comment to the document.
    /// </summary>
    /// <param name="token">The token to use.</param>
    void AddComment(ref StructHtmlToken token);

    /// <summary>
    /// Tracks the given exception which happened during parsing.
    /// </summary>
    void TrackError(Exception exception);
}
