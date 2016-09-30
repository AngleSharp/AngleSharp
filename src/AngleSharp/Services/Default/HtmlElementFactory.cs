namespace AngleSharp.Services.Default
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to HTMLElement instance creation mappings.
    /// </summary>
    sealed class HtmlElementFactory : IElementFactory<HtmlElement>
    {
        private delegate HtmlElement Creator(Document owner, String prefix);

        private readonly Dictionary<String, Creator> creators = new Dictionary<String, Creator>(StringComparer.OrdinalIgnoreCase)
        {
            { TagNames.Div, (document, prefix) => new HtmlDivElement(document, prefix) },
            { TagNames.A, (document, prefix) => new HtmlAnchorElement(document, prefix) },
            { TagNames.Img, (document, prefix) => new HtmlImageElement(document, prefix) },
            { TagNames.P, (document, prefix) => new HtmlParagraphElement(document, prefix) },
            { TagNames.Br, (document, prefix) => new HtmlBreakRowElement(document, prefix) },
            { TagNames.Input, (document, prefix) => new HtmlInputElement(document, prefix) },
            { TagNames.Button, (document, prefix) => new HtmlButtonElement(document, prefix) },
            { TagNames.Textarea, (document, prefix) => new HtmlTextAreaElement(document, prefix) },
            { TagNames.Li, (document, prefix) => new HtmlListItemElement(document, TagNames.Li, prefix) },
            { TagNames.H1, (document, prefix) => new HtmlHeadingElement(document, TagNames.H1, prefix) },
            { TagNames.H2, (document, prefix) => new HtmlHeadingElement(document, TagNames.H2, prefix) },
            { TagNames.H3, (document, prefix) => new HtmlHeadingElement(document, TagNames.H3, prefix) },
            { TagNames.H4, (document, prefix) => new HtmlHeadingElement(document, TagNames.H4, prefix) },
            { TagNames.H5, (document, prefix) => new HtmlHeadingElement(document, TagNames.H5, prefix) },
            { TagNames.H6, (document, prefix) => new HtmlHeadingElement(document, TagNames.H6, prefix) },
            { TagNames.Ul, (document, prefix) => new HtmlUnorderedListElement(document, prefix) },
            { TagNames.Ol, (document, prefix) => new HtmlOrderedListElement(document, prefix) },
            { TagNames.Dl, (document, prefix) => new HtmlDefinitionListElement( document, prefix) },
            { TagNames.Link, (document, prefix) => new HtmlLinkElement(document, prefix) },
            { TagNames.Meta, (document, prefix) => new HtmlMetaElement(document, prefix) },
            { TagNames.Label, (document, prefix) => new HtmlLabelElement(document, prefix) },
            { TagNames.Fieldset, (document, prefix) => new HtmlFieldSetElement(document, prefix) },
            { TagNames.Legend, (document, prefix) => new HtmlLegendElement(document, prefix) },
            { TagNames.Form, (document, prefix) => new HtmlFormElement(document, prefix) },
            { TagNames.Select, (document, prefix) => new HtmlSelectElement(document, prefix) },
            { TagNames.Pre, (document, prefix) => new HtmlPreElement(document, prefix) },
            { TagNames.Hr, (document, prefix) => new HtmlHrElement(document, prefix) },
            { TagNames.Dir, (document, prefix) => new HtmlDirectoryElement(document, prefix) },
            { TagNames.Font, (document, prefix) => new HtmlFontElement(document, prefix) },
            { TagNames.Param, (document, prefix) => new HtmlParamElement(document, prefix) },
            { TagNames.BlockQuote, (document, prefix) => new HtmlQuoteElement(document, TagNames.BlockQuote, prefix) },
            { TagNames.Quote, (document, prefix) => new HtmlQuoteElement(document, TagNames.Quote, prefix) },
            { TagNames.Q, (document, prefix) => new HtmlQuoteElement(document, TagNames.Q, prefix) },
            { TagNames.Canvas, (document, prefix) => new HtmlCanvasElement(document, prefix) },
            { TagNames.Caption, (document, prefix) => new HtmlTableCaptionElement(document, prefix) },
            { TagNames.Td, (document, prefix) => new HtmlTableDataCellElement(document, prefix) },
            { TagNames.Tr, (document, prefix) => new HtmlTableRowElement(document, prefix) },
            { TagNames.Table, (document, prefix) => new HtmlTableElement(document, prefix) },
            { TagNames.Tbody, (document, prefix) => new HtmlTableSectionElement(document, TagNames.Tbody, prefix) },
            { TagNames.Th, (document, prefix) => new HtmlTableHeaderCellElement(document, prefix) },
            { TagNames.Tfoot, (document, prefix) => new HtmlTableSectionElement(document, TagNames.Tfoot, prefix) },
            { TagNames.Thead, (document, prefix) => new HtmlTableSectionElement(document, TagNames.Thead, prefix) },
            { TagNames.Colgroup, (document, prefix) => new HtmlTableColgroupElement(document, prefix) },
            { TagNames.Col, (document, prefix) => new HtmlTableColElement(document, prefix) },
            { TagNames.Del, (document, prefix) => new HtmlModElement(document, TagNames.Del, prefix) },
            { TagNames.Ins, (document, prefix) => new HtmlModElement(document, TagNames.Ins, prefix) },
            { TagNames.Applet, (document, prefix) => new HtmlAppletElement(document, prefix) },
            { TagNames.Object, (document, prefix) => new HtmlObjectElement(document, prefix) },
            { TagNames.Optgroup, (document, prefix) => new HtmlOptionsGroupElement(document, prefix) },
            { TagNames.Option, (document, prefix) => new HtmlOptionElement(document, prefix) },
            { TagNames.Style, (document, prefix) => new HtmlStyleElement(document, prefix) },
            { TagNames.Script, (document, prefix) => new HtmlScriptElement(document, prefix) },
            { TagNames.Iframe, (document, prefix) => new HtmlIFrameElement(document, prefix) },
            { TagNames.Dd, (document, prefix) => new HtmlListItemElement(document, TagNames.Dd, prefix) },
            { TagNames.Dt, (document, prefix) => new HtmlListItemElement(document, TagNames.Dt, prefix) },
            { TagNames.Frameset, (document, prefix) => new HtmlFrameSetElement(document, prefix) },
            { TagNames.Frame, (document, prefix) => new HtmlFrameElement(document, prefix) },
            { TagNames.Audio, (document, prefix) => new HtmlAudioElement(document, prefix) },
            { TagNames.Video, (document, prefix) => new HtmlVideoElement(document, prefix) },
            { TagNames.Span, (document, prefix) => new HtmlSpanElement(document, prefix) },
            { TagNames.Dialog, (document, prefix) => new HtmlDialogElement(document, prefix) },
            { TagNames.Details, (document, prefix) => new HtmlDetailsElement(document, prefix) },
            { TagNames.Source, (document, prefix) => new HtmlSourceElement(document, prefix) },
            { TagNames.Track, (document, prefix) => new HtmlTrackElement(document, prefix) },
            { TagNames.Wbr, (document, prefix) => new HtmlWbrElement(document, prefix) },
            { TagNames.B, (document, prefix) => new HtmlBoldElement(document, prefix) },
            { TagNames.Big, (document, prefix) => new HtmlBigElement(document, prefix) },
            { TagNames.Strike, (document, prefix) => new HtmlStrikeElement(document, prefix) },
            { TagNames.Code, (document, prefix) => new HtmlCodeElement(document, prefix) },
            { TagNames.Em, (document, prefix) => new HtmlEmphasizeElement(document, prefix) },
            { TagNames.I, (document, prefix) => new HtmlItalicElement(document, prefix) },
            { TagNames.S, (document, prefix) => new HtmlStruckElement(document, prefix) },
            { TagNames.Small, (document, prefix) => new HtmlSmallElement(document, prefix) },
            { TagNames.Strong, (document, prefix) => new HtmlStrongElement(document, prefix) },
            { TagNames.U, (document, prefix) => new HtmlUnderlineElement(document, prefix) },
            { TagNames.Tt, (document, prefix) => new HtmlTeletypeTextElement(document, prefix) },
            { TagNames.Address, (document, prefix) => new HtmlAddressElement(document, prefix) },
            { TagNames.Main, (document, prefix) => new HtmlSemanticElement(document, TagNames.Main, prefix) },
            { TagNames.Summary, (document, prefix) => new HtmlSemanticElement(document, TagNames.Summary, prefix) },
            { TagNames.Center, (document, prefix) => new HtmlSemanticElement(document, TagNames.Center, prefix) },
            { TagNames.Listing, (document, prefix) => new HtmlSemanticElement(document, TagNames.Listing, prefix) },
            { TagNames.Nav, (document, prefix) => new HtmlSemanticElement(document, TagNames.Nav, prefix) },
            { TagNames.Article, (document, prefix) => new HtmlSemanticElement(document, TagNames.Article, prefix) },
            { TagNames.Aside, (document, prefix) => new HtmlSemanticElement(document, TagNames.Aside, prefix) },
            { TagNames.Figcaption, (document, prefix) => new HtmlSemanticElement(document, TagNames.Figcaption, prefix) },
            { TagNames.Figure, (document, prefix) => new HtmlSemanticElement(document, TagNames.Figure, prefix) },
            { TagNames.Section, (document, prefix) => new HtmlSemanticElement(document, TagNames.Section, prefix) },
            { TagNames.Footer, (document, prefix) => new HtmlSemanticElement(document, TagNames.Footer, prefix) },
            { TagNames.Header, (document, prefix) => new HtmlSemanticElement(document, TagNames.Header, prefix) },
            { TagNames.Hgroup, (document, prefix) => new HtmlSemanticElement(document, TagNames.Hgroup, prefix) },
            { TagNames.Cite, (document, prefix) => new HtmlElement(document, TagNames.Cite, prefix) },
            { TagNames.Ruby, (document, prefix) => new HtmlRubyElement(document, prefix) },
            { TagNames.Rt, (document, prefix) => new HtmlRtElement(document, prefix) },
            { TagNames.Rp, (document, prefix) => new HtmlRpElement(document, prefix) },
            { TagNames.Rtc, (document, prefix) => new HtmlRtcElement(document, prefix) },
            { TagNames.Rb, (document, prefix) => new HtmlRbElement(document, prefix) },
            { TagNames.Map, (document, prefix) => new HtmlMapElement(document, prefix) },
            { TagNames.Datalist, (document, prefix) => new HtmlDataListElement(document, prefix) },
            { TagNames.Xmp, (document, prefix) => new HtmlXmpElement(document, prefix) },
            { TagNames.Picture, (document, prefix) => new HtmlPictureElement(document, prefix) },
            { TagNames.Template, (document, prefix) => new HtmlTemplateElement(document, prefix) },
            { TagNames.Time, (document, prefix) => new HtmlTimeElement(document, prefix) },
            { TagNames.Progress, (document, prefix) => new HtmlProgressElement(document, prefix) },
            { TagNames.Meter, (document, prefix) => new HtmlMeterElement(document, prefix) },
            { TagNames.Output, (document, prefix) => new HtmlOutputElement(document, prefix) },
            { TagNames.Keygen, (document, prefix) => new HtmlKeygenElement(document, prefix) },
            { TagNames.Title, (document, prefix) => new HtmlTitleElement(document, prefix) },
            { TagNames.Head, (document, prefix) => new HtmlHeadElement(document, prefix) },
            { TagNames.Body, (document, prefix) => new HtmlBodyElement(document, prefix) },
            { TagNames.Html, (document, prefix) => new HtmlHtmlElement(document, prefix) },
            { TagNames.Area, (document, prefix) => new HtmlAreaElement(document, prefix) },
            { TagNames.Embed, (document, prefix) => new HtmlEmbedElement(document, prefix) },
            { TagNames.MenuItem, (document, prefix) => new HtmlMenuItemElement(document, prefix) },
            { TagNames.Slot, (document, prefix) => new HtmlSlotElement(document, prefix) },
            { TagNames.NoScript, (document, prefix) => new HtmlNoScriptElement(document, prefix) },
            { TagNames.NoEmbed, (document, prefix) => new HtmlNoEmbedElement(document, prefix) },
            { TagNames.NoFrames, (document, prefix) => new HtmlNoFramesElement(document, prefix) },
            { TagNames.NoBr, (document, prefix) => new HtmlNoNewlineElement(document, prefix) },
            { TagNames.Menu, (document, prefix) => new HtmlMenuElement(document, prefix) },
            { TagNames.Base, (document, prefix) => new HtmlBaseElement(document, prefix) },
            { TagNames.BaseFont, (document, prefix) => new HtmlBaseFontElement(document, prefix) },
            { TagNames.Bgsound, (document, prefix) => new HtmlBgsoundElement(document, prefix) },
            { TagNames.Marquee, (document, prefix) => new HtmlMarqueeElement(document, prefix) },
            { TagNames.Data, (document, prefix) => new HtmlDataElement(document, prefix) },
            { TagNames.Plaintext, (document, prefix) => new HtmlSemanticElement(document, TagNames.Plaintext, prefix) },
            { TagNames.IsIndex, (document, prefix) => new HtmlIsIndexElement(document, prefix) },
            { TagNames.Mark, (document, prefix) => new HtmlElement(document, TagNames.Mark) },
            { TagNames.Sub, (document, prefix) => new HtmlElement(document, TagNames.Sub) },//
            { TagNames.Sup, (document, prefix) => new HtmlElement(document, TagNames.Sup) },
            { TagNames.Dfn, (document, prefix) => new HtmlElement(document, TagNames.Dfn) },
            { TagNames.Kbd, (document, prefix) => new HtmlElement(document, TagNames.Kbd) },
            { TagNames.Var, (document, prefix) => new HtmlElement(document, TagNames.Var) },
            { TagNames.Samp, (document, prefix) => new HtmlElement(document, TagNames.Samp) },
            { TagNames.Abbr, (document, prefix) => new HtmlElement(document, TagNames.Abbr) },
            { TagNames.Bdi, (document, prefix) => new HtmlElement(document, TagNames.Bdi) },
            { TagNames.Bdo, (document, prefix) => new HtmlElement(document, TagNames.Bdo) },
        };

        /// <summary>
        /// Returns a specialized HTMLElement instance for the given tag name.
        /// </summary>
        /// <param name="document">The document that owns the element.</param>
        /// <param name="localName">The given tag name.</param>
        /// <param name="prefix">The prefix of the element, if any.</param>
        /// <returns>The specialized HTMLElement instance.</returns>
        public HtmlElement Create(Document document, String localName, String prefix = null)
        {
            var creator = default(Creator);

            if (creators.TryGetValue(localName, out creator))
            {
                return creator(document, prefix);
            }

            return new HtmlUnknownElement(document, localName.HtmlLower(), prefix);
        }
    }
}
