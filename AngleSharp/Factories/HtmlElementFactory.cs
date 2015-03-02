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
            { Tags.Base, document => new HtmlBaseElement(document) },
            { Tags.BaseFont, document => new HtmlBaseFontElement(document) },
            { Tags.Link, document => new HtmlLinkElement(document) },
            { Tags.A, document => new HtmlAnchorElement(document) },
            { Tags.Button, document => new HtmlButtonElement(document) },
            { Tags.Input, document => new HtmlInputElement(document) },
            { Tags.Html, document => new HtmlHtmlElement(document) },
            { Tags.IsIndex, document => new HtmlIsIndexElement(document) },
            { Tags.Br, document => new HtmlBreakRowElement(document) },
            { Tags.Embed, document => new HtmlEmbedElement(document) },
            { Tags.Div, document => new HtmlDivElement(document) },
            { Tags.Area, document => new HtmlAreaElement(document) },
            { Tags.Img, document => new HtmlImageElement(document) },
            { Tags.Dl, document => new HtmlDefinitionListElement( document) },
            { Tags.Body, document => new HtmlBodyElement(document) },
            { Tags.Fieldset, document => new HtmlFieldSetElement(document) },
            { Tags.Head, document => new HtmlHeadElement(document) },
            { Tags.Menu, document => new HtmlMenuElement(document) },
            { Tags.Meta, document => new HtmlMetaElement(document) },
            { Tags.Ol, document => new HtmlOrderedListElement(document) },
            { Tags.P, document => new HtmlParagraphElement(document) },
            { Tags.Select, document => new HtmlSelectElement(document) },
            { Tags.Ul, document => new HtmlUnorderedListElement(document) },
            { Tags.Hr, document => new HtmlHrElement(document) },
            { Tags.Dir, document => new HtmlDirectoryElement(document) },
            { Tags.Font, document => new HtmlFontElement(document) },
            { Tags.Form, document => new HtmlFormElement(document) },
            { Tags.Param, document => new HtmlParamElement(document) },
            { Tags.Pre, document => new HtmlPreElement(document) },
            { Tags.Textarea, document => new HtmlTextAreaElement(document) },
            { Tags.BlockQuote, document => new HtmlQuoteElement(document, Tags.BlockQuote) },
            { Tags.Quote, document => new HtmlQuoteElement(document, Tags.Quote) },
            { Tags.Q, document => new HtmlQuoteElement(document, Tags.Q) },
            { Tags.Canvas, document => new HtmlCanvasElement(document) },
            { Tags.Caption, document => new HtmlTableCaptionElement(document) },
            { Tags.Th, document => new HtmlTableHeaderCellElement(document) },
            { Tags.Td, document => new HtmlTableDataCellElement(document) },
            { Tags.Tr, document => new HtmlTableRowElement(document) },
            { Tags.Tbody, document => new HtmlTableSectionElement(document, Tags.Tbody) },
            { Tags.Tfoot, document => new HtmlTableSectionElement(document, Tags.Tfoot) },
            { Tags.Thead, document => new HtmlTableSectionElement(document, Tags.Thead) },
            { Tags.Table, document => new HtmlTableElement(document) },
            { Tags.Colgroup, document => new HtmlTableColgroupElement(document) },
            { Tags.Col, document => new HtmlTableColElement(document) },
            { Tags.Del, document => new HtmlModElement(document, Tags.Del) },
            { Tags.Ins, document => new HtmlModElement(document, Tags.Ins) },
            { Tags.Legend, document => new HtmlLegendElement(document) },
            { Tags.Label, document => new HtmlLabelElement(document) },
            { Tags.Applet, document => new HtmlAppletElement(document) },
            { Tags.Object, document => new HtmlObjectElement(document) },
            { Tags.Optgroup, document => new HtmlOptionsGroupElement(document) },
            { Tags.Option, document => new HtmlOptionElement(document) },
            { Tags.Style, document => new HtmlStyleElement(document) },
            { Tags.Script, document => new HtmlScriptElement(document) },
            { Tags.Iframe, document => new HtmlIFrameElement(document) },
            { Tags.Title, document => new HtmlTitleElement(document) },
            { Tags.Li, document => new HtmlListItemElement(document, Tags.Li) },
            { Tags.Dd, document => new HtmlListItemElement(document, Tags.Dd) },
            { Tags.Dt, document => new HtmlListItemElement(document, Tags.Dt) },
            { Tags.Frameset, document => new HtmlFrameSetElement(document) },
            { Tags.Frame, document => new HtmlFrameElement(document) },
            { Tags.H1, document => new HtmlHeadingElement(document, Tags.H1) },
            { Tags.H2, document => new HtmlHeadingElement(document, Tags.H2) },
            { Tags.H3, document => new HtmlHeadingElement(document, Tags.H3) },
            { Tags.H4, document => new HtmlHeadingElement(document, Tags.H4) },
            { Tags.H5, document => new HtmlHeadingElement(document, Tags.H5) },
            { Tags.H6, document => new HtmlHeadingElement(document, Tags.H6) },
            { Tags.Audio, document => new HtmlAudioElement(document) },
            { Tags.Video, document => new HtmlVideoElement(document) },
            { Tags.Span, document => new HtmlSpanElement(document) },
            { Tags.Dialog, document => new HtmlDialogElement(document) },
            { Tags.Details, document => new HtmlDetailsElement(document) },
            { Tags.Source, document => new HtmlSourceElement(document) },
            { Tags.Track, document => new HtmlTrackElement(document) },
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
            { Tags.Address, document => new HtmlAddressElement(document) },
            { Tags.Main, document => new HtmlSemanticElement(document, Tags.Main) },
            { Tags.Summary, document => new HtmlSemanticElement(document, Tags.Summary) },
            { Tags.Center, document => new HtmlSemanticElement(document, Tags.Center) },
            { Tags.Listing, document => new HtmlSemanticElement(document, Tags.Listing) },
            { Tags.Nav, document => new HtmlSemanticElement(document, Tags.Nav) },
            { Tags.Article, document => new HtmlSemanticElement(document, Tags.Article) },
            { Tags.Aside, document => new HtmlSemanticElement(document, Tags.Aside) },
            { Tags.Figcaption, document => new HtmlSemanticElement(document, Tags.Figcaption) },
            { Tags.Figure, document => new HtmlSemanticElement(document, Tags.Figure) },
            { Tags.Section, document => new HtmlSemanticElement(document, Tags.Section) },
            { Tags.Footer, document => new HtmlSemanticElement(document, Tags.Footer) },
            { Tags.Header, document => new HtmlSemanticElement(document, Tags.Header) },
            { Tags.Hgroup, document => new HtmlSemanticElement(document, Tags.Hgroup) },
            { Tags.Plaintext, document => new HtmlSemanticElement(document, Tags.Plaintext) },
            { Tags.Bgsound, document => new HtmlBgsoundElement(document) },
            { Tags.Marquee, document => new HtmlMarqueeElement(document) },
            { Tags.NoEmbed, document => new HtmlNoEmbedElement(document) },
            { Tags.NoFrames, document => new HtmlNoFramesElement(document) },
            { Tags.NoScript, document => new HtmlNoScriptElement(document) },
            { Tags.MenuItem, document => new HtmlMenuItemElement(document) },
            { Tags.Cite, document => new HtmlElement(document, Tags.Cite) },
            { Tags.Ruby, document => new HtmlRubyElement(document) },
            { Tags.Rt, document => new HtmlRtElement(document) },
            { Tags.Rp, document => new HtmlRpElement(document) },
            { Tags.Rtc, document => new HtmlRtcElement(document) },
            { Tags.Rb, document => new HtmlRbElement(document) },
            { Tags.Time, document => new HtmlTimeElement(document) },
            { Tags.Progress, document => new HtmlProgressElement(document) },
            { Tags.Meter, document => new HtmlMeterElement(document) },
            { Tags.Output, document => new HtmlOutputElement(document) },
            { Tags.Map, document => new HtmlMapElement(document) },
            { Tags.Datalist, document => new HtmlDataListElement(document) },
            { Tags.Keygen, document => new HtmlKeygenElement(document) },
            { Tags.Xmp, document => new HtmlXmpElement(document) },
            { Tags.Template, document => new HtmlTemplateElement(document) },
            { Tags.Picture, document => new HtmlPictureElement(document) },
            { Tags.Data, document => new HtmlDataElement(document) }
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
