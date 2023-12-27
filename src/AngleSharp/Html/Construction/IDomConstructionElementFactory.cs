namespace AngleSharp.Html.Construction;

using System;
using AngleSharp.Common;
using AngleSharp.Dom;
using Text;

/// <summary>
/// Represents a factory for creating DOM during parsing of HTML.
/// </summary>
/// <typeparam name="TDocument">Type of the document, should implement <see cref="IConstructableDocument"/></typeparam>
/// <typeparam name="TElement">Type of the element, should implement <see cref="IConstructableElement"/></typeparam>
public interface IDomConstructionElementFactory<TDocument, TElement>
    where TDocument: class, IConstructableDocument
    where TElement: class, IConstructableElement
{
    /// <summary>
    /// Creates a new element with the given name, prefix and flags and sets the owner document.
    /// </summary>
    /// <param name="document">Owner document</param>
    /// <param name="localName">Node name</param>
    /// <param name="prefix">Node name prefix</param>
    /// <param name="flags">Node flags</param>
    /// <returns>Created element</returns>
    TElement Create(TDocument document, StringOrMemory localName, StringOrMemory prefix = default!, NodeFlags flags = NodeFlags.None);

    /// <summary>
    /// Creates a specialized no script element.
    /// </summary>
    /// <param name="document">Owner</param>
    /// <param name="scripting">Is scripting supported</param>
    /// <returns>Created element</returns>
    TElement CreateNoScript(TDocument document, Boolean scripting);

    /// <summary>
    /// Creates a specialized doctype element
    /// </summary>
    /// <param name="document">Owner</param>
    /// <param name="name">Name</param>
    /// <param name="publicIdentifier">Public identifier</param>
    /// <param name="systemIdentifier">System identifier</param>
    /// <returns>Created element</returns>
    IConstructableNode CreateDocumentType(TDocument document, StringOrMemory name, StringOrMemory publicIdentifier, StringOrMemory systemIdentifier);

    /// <summary>
    /// Creates a specialized math element
    /// </summary>
    /// <param name="document">Owner</param>
    /// <param name="name">Name</param>
    /// <returns>Created element</returns>
    IConstructableMathElement CreateMath(TDocument document, StringOrMemory name = default!);

    /// <summary>
    /// Creates a specialized svg element
    /// </summary>
    /// <param name="document">Owner</param>
    /// <param name="name">Name</param>
    /// <returns>Created element</returns>
    IConstructableSvgElement CreateSvg(TDocument document, StringOrMemory name = default!);

    /// <summary>
    /// Creates a specialized meta element
    /// </summary>
    /// <param name="document"></param>
    /// <returns>Created element</returns>
    IConstructableMetaElement CreateMeta(TDocument document);

    /// <summary>
    /// Creates a specialized script element
    /// </summary>
    /// <param name="document">Owner</param>
    /// <param name="parserInserted">Is inserted by the parser</param>
    /// <param name="started">Is script execution already started</param>
    /// <returns>Created element</returns>
    IConstructableScriptElement CreateScript(TDocument document, Boolean parserInserted, Boolean started);

    /// <summary>
    /// Creates a specialized frame element
    /// </summary>
    /// <param name="document">Owner</param>
    /// <returns>Created element</returns>
    IConstructableFrameElement CreateFrame(TDocument document);

    /// <summary>
    /// Creates a specialized template element
    /// </summary>
    /// <param name="document">Owner</param>
    /// <returns>Created element</returns>
    IConstructableTemplateElement CreateTemplate(TDocument document);

    /// <summary>
    /// creates a specialized form element
    /// </summary>
    /// <param name="document">Owner</param>
    /// <returns>Created element</returns>
    IConstructableFormElement CreateForm(TDocument document);

    /// <summary>
    /// Creates an unknown element
    /// </summary>
    /// <param name="document">Owner</param>
    /// <param name="tagName">Tag name</param>
    /// <returns>Created element</returns>
    TElement CreateUnknown(TDocument document, StringOrMemory tagName);

    /// <summary>
    /// Creates a new document to hold parsed DOM
    /// </summary>
    /// <param name="source">Source of the document</param>
    /// <param name="context">Optional browsing context</param>
    TDocument CreateDocument(TextSource source, IBrowsingContext? context = null);
}