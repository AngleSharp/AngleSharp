namespace AngleSharp.Html.Construction;

using System;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Mathml;
using AngleSharp.Mathml.Dom;
using AngleSharp.Svg;
using AngleSharp.Svg.Dom;
using Text;

/// <summary>
/// Shortcut interface inherited from <see cref="IDomConstructionElementFactory&lt;Document, HtmlElement&gt;"/> with fixed generic parameters for <see cref="HtmlElement" />.
/// </summary>
public interface IHtmlElementConstructionFactory : IDomConstructionElementFactory<Document, Element>;

internal sealed class HtmlDomConstructionFactory : IHtmlElementConstructionFactory
{
    public static readonly IHtmlElementConstructionFactory Instance =
        new HtmlDomConstructionFactory(HtmlElementFactory.Instance, MathElementFactory.Instance, SvgElementFactory.Instance);

    private readonly IElementFactory<Document, HtmlElement> _html;
    private readonly IElementFactory<Document, MathElement> _math;
    private readonly IElementFactory<Document, SvgElement> _svg;

    public HtmlDomConstructionFactory(IBrowsingContext context)
    {
        _html = context.GetService<IElementFactory<Document, HtmlElement>>()!;
        _math = context.GetService<IElementFactory<Document, MathElement>>()!;
        _svg = context.GetService<IElementFactory<Document, SvgElement>>()!;
    }

    public HtmlDomConstructionFactory(
        IElementFactory<Document, HtmlElement> html,
        IElementFactory<Document, MathElement> math,
        IElementFactory<Document, SvgElement> svg)
    {
        _html = html;
        _math = math;
        _svg = svg;
    }

    public Element Create(Document document, StringOrMemory localName, StringOrMemory prefix = default, NodeFlags flags = NodeFlags.None) =>
        _html.Create(document, localName.ToString(), prefix.IsNullOrEmpty ? null : prefix.ToString(), flags);

    public IConstructableMetaElement CreateMeta(Document document) => new HtmlMetaElement(document);

    public IConstructableScriptElement CreateScript(Document document, Boolean parserInserted, Boolean started) =>
        new HtmlScriptElement(document, null, parserInserted, started);

    public IConstructableFrameElement CreateFrame(Document document) => new HtmlFrameElement(document);

    public IConstructableTemplateElement CreateTemplate(Document document) => new HtmlTemplateElement(document);

    public IConstructableFormElement CreateForm(Document document) => new HtmlFormElement(document);

    public Element CreateNoScript(Document document, Boolean scripting) =>
        new HtmlNoScriptElement(document, null, scripting);

    public IConstructableMathElement CreateMath(Document document, StringOrMemory name = default) =>
        _math.Create(document, name.ToString());

    public IConstructableSvgElement CreateSvg(Document document, StringOrMemory name = default) =>
        _svg.Create(document, name.ToString());

    public Element CreateUnknown(Document document, StringOrMemory tagName) =>
        new HtmlUnknownElement(document, tagName.ToString());

    public Document CreateDocument(TextSource source, IBrowsingContext? context = null) =>
        new HtmlDocument(context, source);

    public IConstructableNode CreateDocumentType(
        Document document,
        StringOrMemory name,
        StringOrMemory publicIdentifier,
        StringOrMemory systemIdentifier)
    {
        var doctype = new DocumentType(document, name.ToString())
        {
            SystemIdentifier = systemIdentifier.ToString(),
            PublicIdentifier = publicIdentifier.ToString()
        };

        return doctype;
    }
}