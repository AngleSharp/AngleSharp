namespace AngleSharp.Html.Parser;

using System;
using System.Collections.Generic;
using AngleSharp.Dom;
using Common;

internal interface IHtmlElementFactory<TDocument, TElement, TValue>
    where TElement: class, IConstructableElement
    where TDocument: class, IConstructableDocument
{
    TElement Create(TDocument document, TValue localName, TValue prefix = default!, NodeFlags flags = NodeFlags.None);

    TElement CreateNoScript(TDocument document, Boolean scripting);
    IConstructableNode CreateDocumentType(TDocument document, TValue name, TValue publicIdentifier, TValue systemIdentifier);
    IMathElement CreateMath(TDocument document, TValue name = default!);
    ISvgElement CreateSvg(TDocument document, TValue name = default!);
    TElement CreateUnknown(TDocument document, TValue tagName);

    IMetaElement CreateMeta(TDocument document);
    IScriptElement CreateScript(TDocument document, Boolean parserInserted, Boolean started);
    IFrameElement CreateFrame(TDocument document);
    ITemplateElement CreateTemplate(TDocument document);
}

internal interface IReadOnlyFactory : IHtmlElementFactory<ReadOnlyDocument, ReadOnlyHtmlElement, StringOrMemory>;

sealed class ReadOnlyHtmlElementFactory : IReadOnlyFactory
{
    public ReadOnlyHtmlElement Create(ReadOnlyDocument document, StringOrMemory localName, StringOrMemory prefix = default, NodeFlags flags = NodeFlags.None)
    {
        _creators.TryGetValue(localName, out var creator);
        return creator?.Invoke(document, prefix) ?? new ReadOnlyHtmlElement(document, localName, prefix, flags);
    }

    public IMetaElement CreateMeta(ReadOnlyDocument document)
    {
        return new ReadOnlyHtmlMeta(document);
    }

    public IScriptElement CreateScript(ReadOnlyDocument document, bool parserInserted, bool started)
    {
        return new ReadOnlyHtmlScript(document);
    }

    public IFrameElement CreateFrame(ReadOnlyDocument document)
    {
        return new ReadOnlyHtmlFrameElement(document);
    }

    public ITemplateElement CreateTemplate(ReadOnlyDocument document)
    {
        return new ReadOnlyHtmlTemplateElement(document);
    }

    public ReadOnlyHtmlElement CreateNoScript(ReadOnlyDocument document, bool scripting)
    {
        return new ReadOnlyHtmlElement(document, TagNames.NoScript, default, NodeFlags.Special);
    }

    public IMathElement CreateMath(ReadOnlyDocument document, StringOrMemory name = default)
    {
        return new ReadOnlyHtmlElement(document, TagNames.Math);
    }

    public ISvgElement CreateSvg(ReadOnlyDocument document, StringOrMemory name = default)
    {
        return new ReadOnlyHtmlElement(document, TagNames.Svg);
    }

    public ReadOnlyHtmlElement CreateUnknown(ReadOnlyDocument document, StringOrMemory tagName)
    {
        return new ReadOnlyHtmlElement(document, tagName);
    }

    public IConstructableNode CreateDocumentType(
        ReadOnlyDocument document,
        StringOrMemory name,
        StringOrMemory publicIdentifier,
        StringOrMemory systemIdentifier)
    {
        var doctype = new ReadOnlyDocumentType(document, name)
        {
            SystemIdentifier = systemIdentifier,
            PublicIdentifier = publicIdentifier
        };

        return doctype;
    }

    internal delegate ReadOnlyHtmlElement Creator(ReadOnlyDocument owner, StringOrMemory prefix);

    private static Dictionary<StringOrMemory, Creator> _creators = new Dictionary<StringOrMemory, Creator>
    {
        { TagNames.Address, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Address, prefix, NodeFlags.Special)},
        { TagNames.A, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.A, prefix, NodeFlags.HtmlFormatting)},
        { TagNames.Applet, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Applet, prefix, NodeFlags.Special | NodeFlags.Scoped)},
        { TagNames.Area, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Area, prefix, NodeFlags.Special | NodeFlags.SelfClosing)},
        { TagNames.Audio, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Audio, prefix)},
        { TagNames.Base, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Base, prefix, NodeFlags.Special | NodeFlags.SelfClosing)},
        { TagNames.BaseFont, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.BaseFont, prefix, NodeFlags.Special | NodeFlags.SelfClosing)},
        { TagNames.Bgsound, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Bgsound, prefix, NodeFlags.Special | NodeFlags.SelfClosing)},
        { TagNames.Big, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Big, prefix, NodeFlags.HtmlFormatting)},
        { TagNames.Body, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Body, prefix, NodeFlags.Special | NodeFlags.ImplicitlyClosed)},
        { TagNames.B, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.B, prefix, NodeFlags.HtmlFormatting)},
        { TagNames.Br, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Br, prefix, NodeFlags.Special | NodeFlags.SelfClosing)},
        { TagNames.Button, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Button, prefix)},
        { TagNames.Canvas, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Canvas, prefix)},
        { TagNames.Code, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Code, prefix, NodeFlags.HtmlFormatting)},
        { TagNames.Data, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Data, prefix)},
        { TagNames.Datalist, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Datalist, prefix)},
        { TagNames.Dl, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Dl, prefix, NodeFlags.Special)},
        { TagNames.Details, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Details, prefix, NodeFlags.Special)},
        { TagNames.Dialog, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Dialog, prefix)},
        { TagNames.Dir, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Dir, prefix, NodeFlags.Special)},
        { TagNames.Div, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Div, prefix, NodeFlags.Special)},
        { TagNames.Embed, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Embed, prefix, NodeFlags.Special | NodeFlags.SelfClosing)},
        { TagNames.Em, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Em, prefix, NodeFlags.HtmlFormatting)},
        { TagNames.Fieldset, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Fieldset, prefix)},
        { TagNames.Font, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Font, prefix, NodeFlags.HtmlFormatting)},
        { TagNames.Form, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Form, prefix, NodeFlags.Special)},
        { TagNames.Frame, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Frame, prefix, NodeFlags.SelfClosing)},
        { TagNames.Frameset, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Frameset, prefix, NodeFlags.Special)},
        { TagNames.Head, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Head, prefix, NodeFlags.Special)},
        { TagNames.Hr, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Hr, prefix, NodeFlags.Special | NodeFlags.SelfClosing)},
        { TagNames.Html, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Html, prefix, NodeFlags.Special | NodeFlags.ImplicitlyClosed | NodeFlags.Scoped | NodeFlags.HtmlTableScoped | NodeFlags.HtmlTableSectionScoped)},
        { TagNames.Iframe, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Iframe, prefix, NodeFlags.LiteralText)},
        { TagNames.Img, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Img, prefix, NodeFlags.Special | NodeFlags.SelfClosing)},
        { TagNames.Input, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Input, prefix, NodeFlags.SelfClosing)},
        { TagNames.IsIndex, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.IsIndex, prefix, NodeFlags.Special)},
        { TagNames.I, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.I, prefix, NodeFlags.HtmlFormatting)},
        { TagNames.Keygen, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Keygen, prefix, NodeFlags.SelfClosing)},
        { TagNames.Label, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Label, prefix)},
        { TagNames.Legend, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Legend, prefix)},
        { TagNames.Link, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Link, prefix, NodeFlags.Special | NodeFlags.SelfClosing)},
        { TagNames.Map, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Map, prefix)},
        { TagNames.Marquee, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Marquee, prefix, NodeFlags.Special | NodeFlags.Scoped)},
        { TagNames.Menu, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Menu, prefix, NodeFlags.Special)},
        { TagNames.MenuItem, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.MenuItem, prefix, NodeFlags.Special | NodeFlags.SelfClosing)},
        { TagNames.Meta, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Meta, prefix, NodeFlags.Special | NodeFlags.SelfClosing)},
        { TagNames.Meter, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Meter, prefix)},

        { TagNames.NoEmbed, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.NoEmbed, prefix, NodeFlags.Special | NodeFlags.LiteralText)},
        { TagNames.NoFrames, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.NoFrames, prefix, NodeFlags.Special | NodeFlags.LiteralText)},
        { TagNames.NoBr, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.NoBr, prefix, NodeFlags.HtmlFormatting)},
        { TagNames.NoScript, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.NoScript, prefix, NodeFlags.Special) /*GetFlags(scripting ?? owner.Context.IsScripting()))*/ },
        { TagNames.Object, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Object, prefix, NodeFlags.Scoped)},
        { TagNames.Option, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Option, prefix, NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd | NodeFlags.HtmlSelectScoped)},
        { TagNames.Optgroup, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Optgroup, prefix, NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd | NodeFlags.HtmlSelectScoped)},
        { TagNames.Ol, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Ol, prefix, NodeFlags.Special | NodeFlags.HtmlListScoped)},
        { TagNames.Output, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Output, prefix)},
        { TagNames.P, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.P, prefix, NodeFlags.Special | NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd)},
        { TagNames.Param, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Param, prefix, NodeFlags.Special | NodeFlags.SelfClosing)},
        { TagNames.Picture, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Picture, prefix)},
        { TagNames.Pre, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Pre, prefix, NodeFlags.Special | NodeFlags.LineTolerance)},
        { TagNames.Progress, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Progress, prefix)},
        { TagNames.Rb, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Rb, prefix, NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd)},
        { TagNames.Rp, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Rp, prefix, NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd)},
        { TagNames.Rtc, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Rtc, prefix, NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd)},
        { TagNames.Rt, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Rt, prefix, NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd)},
        { TagNames.Ruby, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Ruby, prefix)},
        { TagNames.Script, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Script, prefix, NodeFlags.Special | NodeFlags.LiteralText)},
        { TagNames.Select, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Select, prefix)},
        { TagNames.Slot, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Slot, prefix)},
        { TagNames.Small, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Small, prefix, NodeFlags.HtmlFormatting)},
        { TagNames.Source, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Source, prefix, NodeFlags.Special | NodeFlags.SelfClosing)},
        { TagNames.Span, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Span, prefix)},
        { TagNames.Strike, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Strike, prefix, NodeFlags.HtmlFormatting)},
        { TagNames.Strong, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Strong, prefix, NodeFlags.HtmlFormatting)},
        { TagNames.S, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.S, prefix, NodeFlags.HtmlFormatting)},
        { TagNames.Style, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Style, prefix, NodeFlags.Special | NodeFlags.LiteralText)},
        { TagNames.Caption, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Caption, prefix, NodeFlags.Special | NodeFlags.Scoped)},
        { TagNames.Col, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Col, prefix, NodeFlags.Special | NodeFlags.SelfClosing)},
        { TagNames.Colgroup, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Colgroup, prefix, NodeFlags.Special)},
        { TagNames.Td, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Td, prefix)},
        { TagNames.Table, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Table, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.HtmlTableScoped | NodeFlags.HtmlTableSectionScoped)},
        { TagNames.Th, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Th, prefix)},
        { TagNames.Tr, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Tr, prefix, NodeFlags.Special | NodeFlags.ImplicitlyClosed)},
        { TagNames.Tt, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Tt, prefix, NodeFlags.HtmlFormatting)},
        { TagNames.Template, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Template, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.HtmlTableScoped | NodeFlags.HtmlTableSectionScoped)},
        { TagNames.Textarea, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Textarea, prefix, NodeFlags.LineTolerance)},
        { TagNames.Time, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Time, prefix, NodeFlags.Special)},
        { TagNames.Title, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Title, prefix, NodeFlags.Special)},
        { TagNames.Track, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Track, prefix, NodeFlags.Special | NodeFlags.SelfClosing)},
        { TagNames.U, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.U, prefix, NodeFlags.HtmlFormatting)},
        { TagNames.Ul, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Ul, prefix, NodeFlags.Special | NodeFlags.HtmlListScoped)},
        { TagNames.Video, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Video, prefix)},
        { TagNames.Wbr, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Wbr, prefix, NodeFlags.Special | NodeFlags.SelfClosing)},
        { TagNames.Xmp, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Xmp, prefix, NodeFlags.Special | NodeFlags.LiteralText)},

        { TagNames.Li, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Li, prefix, NodeFlags.Special | NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd) },

        { TagNames.H1, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.H1, prefix, NodeFlags.Special) },
        { TagNames.H2, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.H2, prefix, NodeFlags.Special) },
        { TagNames.H3, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.H3, prefix, NodeFlags.Special) },
        { TagNames.H4, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.H4, prefix, NodeFlags.Special) },
        { TagNames.H5, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.H5, prefix, NodeFlags.Special) },
        { TagNames.H6, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.H6, prefix, NodeFlags.Special) },

        { TagNames.BlockQuote, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.BlockQuote, prefix, NodeFlags.Special ) },
        { TagNames.Quote, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Quote, prefix) },
        { TagNames.Q, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Q, prefix) },

        { TagNames.Tbody, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Tbody, prefix, NodeFlags.Special | NodeFlags.ImplicitlyClosed | NodeFlags.HtmlTableSectionScoped) },
        { TagNames.Tfoot, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Tfoot, prefix, NodeFlags.Special | NodeFlags.ImplicitlyClosed | NodeFlags.HtmlTableSectionScoped) },
        { TagNames.Thead, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Thead, prefix, NodeFlags.Special | NodeFlags.ImplicitlyClosed | NodeFlags.HtmlTableSectionScoped) },

        { TagNames.Del, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Del, prefix) },
        { TagNames.Ins, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Ins, prefix) },
        { TagNames.Dd, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Dd, prefix, NodeFlags.Special | NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd) },
        { TagNames.Dt, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Dt, prefix, NodeFlags.Special | NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd) },

        { TagNames.Main, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Main, prefix, NodeFlags.Special) },
        { TagNames.Summary, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Summary, prefix, NodeFlags.Special) },
        { TagNames.Center, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Center, prefix, NodeFlags.Special) },
        { TagNames.Listing, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Listing, prefix, NodeFlags.Special) },
        { TagNames.Nav, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Nav, prefix, NodeFlags.Special) },
        { TagNames.Article, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Article, prefix, NodeFlags.Special) },
        { TagNames.Aside, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Aside, prefix, NodeFlags.Special) },
        { TagNames.Figcaption, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Figcaption, prefix, NodeFlags.Special) },
        { TagNames.Figure, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Figure, prefix, NodeFlags.Special) },
        { TagNames.Section, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Section, prefix, NodeFlags.Special) },
        { TagNames.Footer, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Footer, prefix, NodeFlags.Special) },
        { TagNames.Header, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Header, prefix, NodeFlags.Special) },
        { TagNames.Hgroup, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Hgroup, prefix, NodeFlags.Special) },

        { TagNames.Plaintext, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Plaintext, prefix, NodeFlags.Special | NodeFlags.LiteralText)},

        { TagNames.Cite, (document, prefix) => new ReadOnlyHtmlElement(document, TagNames.Cite, prefix) },
        { TagNames.Mark, (document, _) => new ReadOnlyHtmlElement(document, TagNames.Mark) },
        { TagNames.Sub, (document, _) => new ReadOnlyHtmlElement(document, TagNames.Sub) },
        { TagNames.Sup, (document, _) => new ReadOnlyHtmlElement(document, TagNames.Sup) },
        { TagNames.Dfn, (document, _) => new ReadOnlyHtmlElement(document, TagNames.Dfn) },
        { TagNames.Kbd, (document, _) => new ReadOnlyHtmlElement(document, TagNames.Kbd) },
        { TagNames.Var, (document, _) => new ReadOnlyHtmlElement(document, TagNames.Var) },
        { TagNames.Samp, (document, _) => new ReadOnlyHtmlElement(document, TagNames.Samp) },
        { TagNames.Abbr, (document, _) => new ReadOnlyHtmlElement(document, TagNames.Abbr) },
        { TagNames.Bdi, (document, _) => new ReadOnlyHtmlElement(document, TagNames.Bdi) },
        { TagNames.Bdo, (document, _) => new ReadOnlyHtmlElement(document, TagNames.Bdo) },
    };
}