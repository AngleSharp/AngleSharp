namespace AngleSharp.Html.Parser;

using System;
using AngleSharp.Dom;
using Common;
using Construction;
using Dom;
using Mathml;
using Mathml.Dom;
using Svg;
using Svg.Dom;

internal sealed class MutableHtmlElementFactory : IHtmlConstructionFactory
{
    public static readonly MutableHtmlElementFactory Instance =
        new(HtmlElementFactory.Instance, MathElementFactory.Instance, SvgElementFactory.Instance);

    private readonly IElementFactory<Document,HtmlElement> _html;
    private readonly IElementFactory<Document,MathElement> _math;
    private readonly IElementFactory<Document,SvgElement> _svg;

    public MutableHtmlElementFactory(IBrowsingContext context)
    {
        _html = context.GetService<IElementFactory<Document, HtmlElement>>()!;
        _math = context.GetService<IElementFactory<Document, MathElement>>()!;
        _svg = context.GetService<IElementFactory<Document, SvgElement>>()!;
    }

    public MutableHtmlElementFactory(
        IElementFactory<Document,HtmlElement> html,
        IElementFactory<Document,MathElement> math,
        IElementFactory<Document,SvgElement> svg)
    {
        _html = html;
        _math = math;
        _svg = svg;
    }

    public Element Create(
        HtmlDocument document,
        StringOrMemory localName,
        StringOrMemory prefix = default,
        NodeFlags flags = NodeFlags.None)
    {
        return _html.Create(document, localName.String, prefix.IsNullOrEmpty ? null : prefix.String, flags);
    }

    public IConstructableMetaElement CreateMeta(HtmlDocument document)
    {
        return new HtmlMetaElement(document);
    }

    public IConstructableScriptElement CreateScript(HtmlDocument document, bool parserInserted, bool started)
    {
        return new HtmlScriptElement(document, null, parserInserted, started);
    }

    public IConstructableFrameElement CreateFrame(HtmlDocument document)
    {
        return new HtmlFrameElement(document);
    }

    public IConstructableTemplateElement CreateTemplate(HtmlDocument document)
    {
        return new HtmlTemplateElement(document);
    }

    public Element CreateNoScript(HtmlDocument document, Boolean scripting)
    {
        return new HtmlNoScriptElement(document, null, scripting);
    }

    public IConstructableMathElement CreateMath(HtmlDocument document, StringOrMemory name = default)
    {
        return _math.Create(document, name.String);
    }

    public IConstructableSvgElement CreateSvg(HtmlDocument document, StringOrMemory name = default)
    {
        return _svg.Create(document, name.String);
    }

    public Element CreateUnknown(HtmlDocument document, StringOrMemory tagName)
    {
        return new HtmlUnknownElement(document, tagName.String);
    }

    public IConstructableNode CreateDocumentType(
        HtmlDocument document,
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