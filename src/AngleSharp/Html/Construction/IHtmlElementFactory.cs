namespace AngleSharp.Html.Construction;

using System;
using AngleSharp.Common;
using AngleSharp.Dom;

internal interface IHtmlElementFactory<TDocument, TElement>
    where TElement: class, IConstructableElement
    where TDocument: class, IConstructableDocument
{
    TElement Create(TDocument document, StringOrMemory localName, StringOrMemory prefix = default!, NodeFlags flags = NodeFlags.None);

    TElement CreateNoScript(TDocument document, Boolean scripting);
    IConstructableNode CreateDocumentType(TDocument document, StringOrMemory name, StringOrMemory publicIdentifier, StringOrMemory systemIdentifier);
    IConstructableMathElement CreateMath(TDocument document, StringOrMemory name = default!);
    IConstructableSvgElement CreateSvg(TDocument document, StringOrMemory name = default!);
    TElement CreateUnknown(TDocument document, StringOrMemory tagName);

    IConstructableMetaElement CreateMeta(TDocument document);
    IConstructableScriptElement CreateScript(TDocument document, Boolean parserInserted, Boolean started);
    IConstructableFrameElement CreateFrame(TDocument document);
    IConstructableTemplateElement CreateTemplate(TDocument document);
}