namespace AngleSharp.Html.Construction;

using System;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Text;

/// <summary>
/// Creates value-type node identities for the HTML tree-construction algorithm.
/// </summary>
public interface IHtmlTreeConstructionFactory<TDocument, TNode>
    where TDocument : class, IConstructableDocument
    where TNode : struct, IHtmlTreeConstructionNode<TNode>
{
    /// <summary>Creates a normal HTML element.</summary>
    TNode Create(
        TDocument document,
        StringOrMemory localName,
        StringOrMemory prefix = default,
        NodeFlags flags = NodeFlags.None
    );

    /// <summary>Creates a noscript element.</summary>
    TNode CreateNoScript(TDocument document, Boolean scripting);

    /// <summary>Creates a document type node.</summary>
    TNode CreateDocumentType(
        TDocument document,
        StringOrMemory name,
        StringOrMemory publicIdentifier,
        StringOrMemory systemIdentifier
    );

    /// <summary>Creates a MathML element.</summary>
    TNode CreateMath(TDocument document, StringOrMemory name = default);

    /// <summary>Creates an SVG element.</summary>
    TNode CreateSvg(TDocument document, StringOrMemory name = default);

    /// <summary>Creates a meta element.</summary>
    TNode CreateMeta(TDocument document);

    /// <summary>Creates a script element.</summary>
    TNode CreateScript(TDocument document, Boolean parserInserted, Boolean started);

    /// <summary>Creates a frame element.</summary>
    TNode CreateFrame(TDocument document);

    /// <summary>Creates a template element.</summary>
    TNode CreateTemplate(TDocument document);

    /// <summary>Creates a form element.</summary>
    TNode CreateForm(TDocument document);

    /// <summary>Creates an unknown element.</summary>
    TNode CreateUnknown(TDocument document, StringOrMemory tagName);

    /// <summary>Creates the construction document.</summary>
    TDocument CreateDocument(TextSource source, IBrowsingContext? context = null);

    /// <summary>Gets the document node identity.</summary>
    TNode GetDocumentNode(TDocument document);

    /// <summary>Gets the current document element or the null node.</summary>
    TNode GetDocumentElement(TDocument document);

    /// <summary>Gets the current head element or the null node.</summary>
    TNode GetHead(TDocument document);
}
