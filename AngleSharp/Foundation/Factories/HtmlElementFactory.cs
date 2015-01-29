namespace AngleSharp.Factories
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to HTMLElement instance creation mappings.
    /// </summary>
    sealed class HtmlElementFactory
    {
        readonly Dictionary<String, Func<Document, HtmlElement>> creators = new Dictionary<String, Func<Document, HtmlElement>>(StringComparer.OrdinalIgnoreCase)
        {
            { Tags.Base, document => new HTMLBaseElement(document) },
            { Tags.BaseFont, document => new HTMLBaseFontElement(document) },
            { Tags.Link, document => new HTMLLinkElement(document) },
            { Tags.A, document => new HtmlAnchorElement(document) },
            { Tags.Button, document => new HTMLButtonElement(document) },
            { Tags.Input, document => new HTMLInputElement(document) },
            { Tags.Html, document => new HtmlHtmlElement(document) },
            { Tags.IsIndex, document => new HTMLIsIndexElement(document) },
            { Tags.Br, document => new HtmlBreakRowElement(document) },
            { Tags.Embed, document => new HTMLEmbedElement(document) },
            { Tags.Div, document => new HtmlDivElement(document) },
            { Tags.Area, document => new HtmlAreaElement(document) },
            { Tags.Img, document => new HtmlImageElement(document) },
            { Tags.Dl, document => new HTMLDListElement( document) },
            { Tags.Body, document => new HtmlBodyElement(document) },
            { Tags.Fieldset, document => new HTMLFieldSetElement(document) },
            { Tags.Head, document => new HTMLHeadElement(document) },
            { Tags.Menu, document => new HtmlMenuElement(document) },
            { Tags.Meta, document => new HTMLMetaElement(document) },
            { Tags.Ol, document => new HTMLOListElement(document) },
            { Tags.P, document => new HtmlParagraphElement(document) },
            { Tags.Select, document => new HTMLSelectElement(document) },
            { Tags.Ul, document => new HTMLUListElement(document) },
            { Tags.Hr, document => new HtmlHrElement(document) },
            { Tags.Dir, document => new HtmlDirectoryElement(document) },
            { Tags.Font, document => new HtmlFontElement(document) },
            { Tags.Form, document => new HTMLFormElement(document) },
            { Tags.Param, document => new HTMLParamElement(document) },
            { Tags.Pre, document => new HtmlPreElement(document) },
            { Tags.Textarea, document => new HTMLTextAreaElement(document) },
            { Tags.BlockQuote, document => new HTMLQuoteElement(document, Tags.BlockQuote) },
            { Tags.Quote, document => new HTMLQuoteElement(document, Tags.Quote) },
            { Tags.Q, document => new HTMLQuoteElement(document, Tags.Q) },
            { Tags.Canvas, document => new HTMLCanvasElement(document) },
            { Tags.Caption, document => new HTMLTableCaptionElement(document) },
            { Tags.Th, document => new HTMLTableHeaderCellElement(document) },
            { Tags.Td, document => new HTMLTableDataCellElement(document) },
            { Tags.Tr, document => new HTMLTableRowElement(document) },
            { Tags.Tbody, document => new HTMLTableSectionElement(document, Tags.Tbody) },
            { Tags.Tfoot, document => new HTMLTableSectionElement(document, Tags.Tfoot) },
            { Tags.Thead, document => new HTMLTableSectionElement(document, Tags.Thead) },
            { Tags.Table, document => new HTMLTableElement(document) },
            { Tags.Colgroup, document => new HTMLTableColgroupElement(document) },
            { Tags.Col, document => new HTMLTableColElement(document) },
            { Tags.Del, document => new HtmlModElement(document, Tags.Del) },
            { Tags.Ins, document => new HtmlModElement(document, Tags.Ins) },
            { Tags.Legend, document => new HTMLLegendElement(document) },
            { Tags.Label, document => new HTMLLabelElement(document) },
            { Tags.Applet, document => new HTMLAppletElement(document) },
            { Tags.Object, document => new HTMLObjectElement(document) },
            { Tags.Optgroup, document => new HTMLOptGroupElement(document) },
            { Tags.Option, document => new HTMLOptionElement(document) },
            { Tags.Style, document => new HtmlStyleElement(document) },
            { Tags.Script, document => new HtmlScriptElement(document) },
            { Tags.Iframe, document => new HtmlIFrameElement(document) },
            { Tags.Title, document => new HTMLTitleElement(document) },
            { Tags.Li, document => new HTMLLIElement(document, Tags.Li) },
            { Tags.Dd, document => new HTMLLIElement(document, Tags.Dd) },
            { Tags.Dt, document => new HTMLLIElement(document, Tags.Dt) },
            { Tags.Frameset, document => new HtmlFrameSetElement(document) },
            { Tags.Frame, document => new HtmlFrameElement(document) },
            { Tags.H1, document => new HTMLHeadingElement(document, Tags.H1) },
            { Tags.H2, document => new HTMLHeadingElement(document, Tags.H2) },
            { Tags.H3, document => new HTMLHeadingElement(document, Tags.H3) },
            { Tags.H4, document => new HTMLHeadingElement(document, Tags.H4) },
            { Tags.H5, document => new HTMLHeadingElement(document, Tags.H5) },
            { Tags.H6, document => new HTMLHeadingElement(document, Tags.H6) },
            { Tags.Audio, document => new HTMLAudioElement(document) },
            { Tags.Video, document => new HTMLVideoElement(document) },
            { Tags.Span, document => new HtmlSpanElement(document) },
            { Tags.Dialog, document => new HTMLDialogElement(document) },
            { Tags.Details, document => new HtmlDetailsElement(document) },
            { Tags.Source, document => new HTMLSourceElement(document) },
            { Tags.Track, document => new HTMLTrackElement(document) },
            { Tags.Wbr, document => new HtmlWbrElement(document) },
            { Tags.B, document => new HtmlBoldElement(document) },
            { Tags.Big, document => new HtmlBigElement(document) },
            { Tags.Strike, document => new HtmlStrikeElement(document) },
            { Tags.Code, document => new HtmlCodeElement(document) },
            { Tags.Em, document => new HtmlEmphasizeElement(document) },
            { Tags.I, document => new HtmlItalicElement(document) },
            { Tags.S, document => new HtmlStruckElement(document) },
            { Tags.Small, document => new HtmlSmallElement(document) },
            { Tags.Strong, document => new HtmlStrongElement(document) },
            { Tags.U, document => new HtmlUnderlineElement(document) },
            { Tags.Tt, document => new HtmlTeletypeTextElement(document) },
            { Tags.NoBr, document => new HtmlNoNewlineElement(document) },
            { Tags.Address, document => new HTMLAddressElement(document) },
            { Tags.Main, document => new HTMLSemanticElement(document, Tags.Main) },
            { Tags.Summary, document => new HTMLSemanticElement(document, Tags.Summary) },
            { Tags.Center, document => new HTMLSemanticElement(document, Tags.Center) },
            { Tags.Listing, document => new HTMLSemanticElement(document, Tags.Listing) },
            { Tags.Nav, document => new HTMLSemanticElement(document, Tags.Nav) },
            { Tags.Article, document => new HTMLSemanticElement(document, Tags.Article) },
            { Tags.Aside, document => new HTMLSemanticElement(document, Tags.Aside) },
            { Tags.Figcaption, document => new HTMLSemanticElement(document, Tags.Figcaption) },
            { Tags.Figure, document => new HTMLSemanticElement(document, Tags.Figure) },
            { Tags.Section, document => new HTMLSemanticElement(document, Tags.Section) },
            { Tags.Footer, document => new HTMLSemanticElement(document, Tags.Footer) },
            { Tags.Header, document => new HTMLSemanticElement(document, Tags.Header) },
            { Tags.Hgroup, document => new HTMLSemanticElement(document, Tags.Hgroup) },
            { Tags.Plaintext, document => new HTMLSemanticElement(document, Tags.Plaintext) },
            { Tags.Bgsound, document => new HTMLBgsoundElement(document) },
            { Tags.Marquee, document => new HTMLMarqueeElement(document) },
            { Tags.NoEmbed, document => new HTMLNoEmbedElement(document) },
            { Tags.NoFrames, document => new HtmlNoFramesElement(document) },
            { Tags.NoScript, document => new HtmlNoScriptElement(document) },
            { Tags.MenuItem, document => new HtmlMenuItemElement(document) },
            { Tags.Cite, document => new HtmlElement(document, Tags.Cite) },
            { Tags.Ruby, document => new HTMLRubyElement(document) },
            { Tags.Rt, document => new HTMLRTElement(document) },
            { Tags.Rp, document => new HTMLRPElement(document) },
            { Tags.Time, document => new HTMLTimeElement(document) },
            { Tags.Progress, document => new HTMLProgressElement(document) },
            { Tags.Output, document => new HTMLOutputElement(document) },
            { Tags.Map, document => new HtmlMapElement(document) },
            { Tags.Datalist, document => new HTMLDataListElement(document) },
            { Tags.Keygen, document => new HTMLKeygenElement(document) },
            { Tags.Xmp, document => new HTMLXmpElement(document) },
            { Tags.Template, document => new HtmlTemplateElement(document) }
        };

        /// <summary>
        /// Returns a specialized HTMLElement instance for the given tag name.
        /// </summary>
        /// <param name="tag">The given tag name.</param>
        /// <param name="document">The document that owns the element.</param>
        /// <returns>The specialized HTMLElement instance.</returns>
        public HtmlElement Create(String tag, Document document)
        {
            Func<Document, HtmlElement> creator;

            if (creators.TryGetValue(tag, out creator))
                return creator(document);

            return new HtmlUnknownElement(document, tag.ToLowerInvariant());
        }
    }
}
