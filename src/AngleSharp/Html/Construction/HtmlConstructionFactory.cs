namespace AngleSharp.Html.Construction;

using System;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Mathml;
using AngleSharp.Mathml.Dom;
using AngleSharp.Svg;
using AngleSharp.Svg.Dom;

/// <summary>
/// Shortcut interface inherited from <see cref="IDomConstructionElementFactory&lt;Document, HtmlElement&gt;"/> with fixed generic parameters for <see cref="HtmlElement" />.
/// </summary>
public interface IHtmlElementConstructionFactory : IDomConstructionElementFactory<Document, HtmlElement>
{
}

internal sealed class HtmlDomConstructionFactory : IHtmlElementConstructionFactory
{
    public static readonly HtmlDomConstructionFactory Instance =
        new(HtmlElementFactory.Instance, MathElementFactory.Instance, SvgElementFactory.Instance);

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

    public HtmlElement Create(Document document, StringOrMemory localName, StringOrMemory prefix = default, NodeFlags flags = NodeFlags.None) =>
        _html.Create(document, localName.String, prefix.IsNullOrEmpty ? null : prefix.String, flags);

    public IConstructableMetaElement CreateMeta(Document document) => new HtmlMetaElement(document);

    public IConstructableScriptElement CreateScript(Document document, bool parserInserted, bool started) =>
        new HtmlScriptElement(document, null, parserInserted, started);

    public IConstructableFrameElement CreateFrame(Document document) => new HtmlFrameElement(document);

    public IConstructableTemplateElement CreateTemplate(Document document) => new HtmlTemplateElement(document);

    public IConstructableFormElement CreateForm(Document document) => new HtmlFormElement(document);

    public HtmlElement CreateNoScript(Document document, Boolean scripting) =>
        new HtmlNoScriptElement(document, null, scripting);

    public IConstructableMathElement CreateMath(Document document, StringOrMemory name = default) =>
        _math.Create(document, name.String);

    public IConstructableSvgElement CreateSvg(Document document, StringOrMemory name = default) =>
        _svg.Create(document, name.String);

    public HtmlElement CreateUnknown(Document document, StringOrMemory tagName) =>
        new HtmlUnknownElement(document, tagName.String);

    public IConstructableNode CreateDocumentType(
        Document document,
        StringOrMemory name,
        StringOrMemory publicIdentifier,
        StringOrMemory systemIdentifier)
    {
        var doctype = new DocumentType(document, name.String)
        {
            SystemIdentifier = systemIdentifier.String,
            PublicIdentifier = publicIdentifier.String
        };

        return doctype;
    }
}