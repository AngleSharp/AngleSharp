namespace AngleSharp.Factories
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Html;

    /// <summary>
    /// Provides string to HTMLElement instance creation mappings.
    /// </summary>
    sealed class HtmlElementFactory
    {
        delegate HtmlElement Creator(Document owner, String prefix);

        readonly Dictionary<String, Creator> creators = new Dictionary<String, Creator>(StringComparer.OrdinalIgnoreCase)
        {
            { Tags.Base, (document, prefix) => new HtmlBaseElement(document, prefix) },
            { Tags.BaseFont, (document, prefix) => new HtmlBaseFontElement(document, prefix) },
            { Tags.Link, (document, prefix) => new HtmlLinkElement(document, prefix) },
            { Tags.A, (document, prefix) => new HtmlAnchorElement(document, prefix) },
            { Tags.Button, (document, prefix) => new HtmlButtonElement(document, prefix) },
            { Tags.Input, (document, prefix) => new HtmlInputElement(document, prefix) },
            { Tags.Html, (document, prefix) => new HtmlHtmlElement(document, prefix) },
            { Tags.IsIndex, (document, prefix) => new HtmlIsIndexElement(document, prefix) },
            { Tags.Br, (document, prefix) => new HtmlBreakRowElement(document, prefix) },
            { Tags.Embed, (document, prefix) => new HtmlEmbedElement(document, prefix) },
            { Tags.Div, (document, prefix) => new HtmlDivElement(document, prefix) },
            { Tags.Area, (document, prefix) => new HtmlAreaElement(document, prefix) },
            { Tags.Img, (document, prefix) => new HtmlImageElement(document, prefix) },
            { Tags.Dl, (document, prefix) => new HtmlDefinitionListElement( document, prefix) },
            { Tags.Body, (document, prefix) => new HtmlBodyElement(document, prefix) },
            { Tags.Fieldset, (document, prefix) => new HtmlFieldSetElement(document, prefix) },
            { Tags.Head, (document, prefix) => new HtmlHeadElement(document, prefix) },
            { Tags.Menu, (document, prefix) => new HtmlMenuElement(document, prefix) },
            { Tags.Meta, (document, prefix) => new HtmlMetaElement(document, prefix) },
            { Tags.Ol, (document, prefix) => new HtmlOrderedListElement(document, prefix) },
            { Tags.P, (document, prefix) => new HtmlParagraphElement(document, prefix) },
            { Tags.Select, (document, prefix) => new HtmlSelectElement(document, prefix) },
            { Tags.Ul, (document, prefix) => new HtmlUnorderedListElement(document, prefix) },
            { Tags.Hr, (document, prefix) => new HtmlHrElement(document, prefix) },
            { Tags.Dir, (document, prefix) => new HtmlDirectoryElement(document, prefix) },
            { Tags.Font, (document, prefix) => new HtmlFontElement(document, prefix) },
            { Tags.Form, (document, prefix) => new HtmlFormElement(document, prefix) },
            { Tags.Param, (document, prefix) => new HtmlParamElement(document, prefix) },
            { Tags.Pre, (document, prefix) => new HtmlPreElement(document, prefix) },
            { Tags.Textarea, (document, prefix) => new HtmlTextAreaElement(document, prefix) },
            { Tags.BlockQuote, (document, prefix) => new HtmlQuoteElement(document, Tags.BlockQuote, prefix) },
            { Tags.Quote, (document, prefix) => new HtmlQuoteElement(document, Tags.Quote, prefix) },
            { Tags.Q, (document, prefix) => new HtmlQuoteElement(document, Tags.Q, prefix) },
            { Tags.Canvas, (document, prefix) => new HtmlCanvasElement(document, prefix) },
            { Tags.Caption, (document, prefix) => new HtmlTableCaptionElement(document, prefix) },
            { Tags.Th, (document, prefix) => new HtmlTableHeaderCellElement(document, prefix) },
            { Tags.Td, (document, prefix) => new HtmlTableDataCellElement(document, prefix) },
            { Tags.Tr, (document, prefix) => new HtmlTableRowElement(document, prefix) },
            { Tags.Tbody, (document, prefix) => new HtmlTableSectionElement(document, Tags.Tbody, prefix) },
            { Tags.Tfoot, (document, prefix) => new HtmlTableSectionElement(document, Tags.Tfoot, prefix) },
            { Tags.Thead, (document, prefix) => new HtmlTableSectionElement(document, Tags.Thead, prefix) },
            { Tags.Table, (document, prefix) => new HtmlTableElement(document, prefix) },
            { Tags.Colgroup, (document, prefix) => new HtmlTableColgroupElement(document, prefix) },
            { Tags.Col, (document, prefix) => new HtmlTableColElement(document, prefix) },
            { Tags.Del, (document, prefix) => new HtmlModElement(document, Tags.Del, prefix) },
            { Tags.Ins, (document, prefix) => new HtmlModElement(document, Tags.Ins, prefix) },
            { Tags.Legend, (document, prefix) => new HtmlLegendElement(document, prefix) },
            { Tags.Label, (document, prefix) => new HtmlLabelElement(document, prefix) },
            { Tags.Applet, (document, prefix) => new HtmlAppletElement(document, prefix) },
            { Tags.Object, (document, prefix) => new HtmlObjectElement(document, prefix) },
            { Tags.Optgroup, (document, prefix) => new HtmlOptionsGroupElement(document, prefix) },
            { Tags.Option, (document, prefix) => new HtmlOptionElement(document, prefix) },
            { Tags.Style, (document, prefix) => new HtmlStyleElement(document, prefix) },
            { Tags.Script, (document, prefix) => new HtmlScriptElement(document, prefix) },
            { Tags.Iframe, (document, prefix) => new HtmlIFrameElement(document, prefix) },
            { Tags.Title, (document, prefix) => new HtmlTitleElement(document, prefix) },
            { Tags.Li, (document, prefix) => new HtmlListItemElement(document, Tags.Li, prefix) },
            { Tags.Dd, (document, prefix) => new HtmlListItemElement(document, Tags.Dd, prefix) },
            { Tags.Dt, (document, prefix) => new HtmlListItemElement(document, Tags.Dt, prefix) },
            { Tags.Frameset, (document, prefix) => new HtmlFrameSetElement(document, prefix) },
            { Tags.Frame, (document, prefix) => new HtmlFrameElement(document, prefix) },
            { Tags.H1, (document, prefix) => new HtmlHeadingElement(document, Tags.H1, prefix) },
            { Tags.H2, (document, prefix) => new HtmlHeadingElement(document, Tags.H2, prefix) },
            { Tags.H3, (document, prefix) => new HtmlHeadingElement(document, Tags.H3, prefix) },
            { Tags.H4, (document, prefix) => new HtmlHeadingElement(document, Tags.H4, prefix) },
            { Tags.H5, (document, prefix) => new HtmlHeadingElement(document, Tags.H5, prefix) },
            { Tags.H6, (document, prefix) => new HtmlHeadingElement(document, Tags.H6, prefix) },
            { Tags.Audio, (document, prefix) => new HtmlAudioElement(document, prefix) },
            { Tags.Video, (document, prefix) => new HtmlVideoElement(document, prefix) },
            { Tags.Span, (document, prefix) => new HtmlSpanElement(document, prefix) },
            { Tags.Dialog, (document, prefix) => new HtmlDialogElement(document, prefix) },
            { Tags.Details, (document, prefix) => new HtmlDetailsElement(document, prefix) },
            { Tags.Source, (document, prefix) => new HtmlSourceElement(document, prefix) },
            { Tags.Track, (document, prefix) => new HtmlTrackElement(document, prefix) },
            { Tags.Wbr, (document, prefix) => new HtmlWbrElement(document, prefix) },
            { Tags.B, (document, prefix) => new HtmlBoldElement(document, prefix) },
            { Tags.Big, (document, prefix) => new HtmlBigElement(document, prefix) },
            { Tags.Strike, (document, prefix) => new HtmlStrikeElement(document, prefix) },
            { Tags.Code, (document, prefix) => new HtmlCodeElement(document, prefix) },
            { Tags.Em, (document, prefix) => new HtmlEmphasizeElement(document, prefix) },
            { Tags.I, (document, prefix) => new HtmlItalicElement(document, prefix) },
            { Tags.S, (document, prefix) => new HtmlStruckElement(document, prefix) },
            { Tags.Small, (document, prefix) => new HtmlSmallElement(document, prefix) },
            { Tags.Strong, (document, prefix) => new HtmlStrongElement(document, prefix) },
            { Tags.U, (document, prefix) => new HtmlUnderlineElement(document, prefix) },
            { Tags.Tt, (document, prefix) => new HtmlTeletypeTextElement(document, prefix) },
            { Tags.NoBr, (document, prefix) => new HtmlNoNewlineElement(document, prefix) },
            { Tags.Address, (document, prefix) => new HtmlAddressElement(document, prefix) },
            { Tags.Main, (document, prefix) => new HtmlSemanticElement(document, Tags.Main, prefix) },
            { Tags.Summary, (document, prefix) => new HtmlSemanticElement(document, Tags.Summary, prefix) },
            { Tags.Center, (document, prefix) => new HtmlSemanticElement(document, Tags.Center, prefix) },
            { Tags.Listing, (document, prefix) => new HtmlSemanticElement(document, Tags.Listing, prefix) },
            { Tags.Nav, (document, prefix) => new HtmlSemanticElement(document, Tags.Nav, prefix) },
            { Tags.Article, (document, prefix) => new HtmlSemanticElement(document, Tags.Article, prefix) },
            { Tags.Aside, (document, prefix) => new HtmlSemanticElement(document, Tags.Aside, prefix) },
            { Tags.Figcaption, (document, prefix) => new HtmlSemanticElement(document, Tags.Figcaption, prefix) },
            { Tags.Figure, (document, prefix) => new HtmlSemanticElement(document, Tags.Figure, prefix) },
            { Tags.Section, (document, prefix) => new HtmlSemanticElement(document, Tags.Section, prefix) },
            { Tags.Footer, (document, prefix) => new HtmlSemanticElement(document, Tags.Footer, prefix) },
            { Tags.Header, (document, prefix) => new HtmlSemanticElement(document, Tags.Header, prefix) },
            { Tags.Hgroup, (document, prefix) => new HtmlSemanticElement(document, Tags.Hgroup, prefix) },
            { Tags.Plaintext, (document, prefix) => new HtmlSemanticElement(document, Tags.Plaintext, prefix) },
            { Tags.Bgsound, (document, prefix) => new HtmlBgsoundElement(document, prefix) },
            { Tags.Marquee, (document, prefix) => new HtmlMarqueeElement(document, prefix) },
            { Tags.NoEmbed, (document, prefix) => new HtmlNoEmbedElement(document, prefix) },
            { Tags.NoFrames, (document, prefix) => new HtmlNoFramesElement(document, prefix) },
            { Tags.NoScript, (document, prefix) => new HtmlNoScriptElement(document, prefix) },
            { Tags.MenuItem, (document, prefix) => new HtmlMenuItemElement(document, prefix) },
            { Tags.Cite, (document, prefix) => new HtmlElement(document, Tags.Cite, prefix) },
            { Tags.Ruby, (document, prefix) => new HtmlRubyElement(document, prefix) },
            { Tags.Rt, (document, prefix) => new HtmlRtElement(document, prefix) },
            { Tags.Rp, (document, prefix) => new HtmlRpElement(document, prefix) },
            { Tags.Rtc, (document, prefix) => new HtmlRtcElement(document, prefix) },
            { Tags.Rb, (document, prefix) => new HtmlRbElement(document, prefix) },
            { Tags.Time, (document, prefix) => new HtmlTimeElement(document, prefix) },
            { Tags.Progress, (document, prefix) => new HtmlProgressElement(document, prefix) },
            { Tags.Meter, (document, prefix) => new HtmlMeterElement(document, prefix) },
            { Tags.Output, (document, prefix) => new HtmlOutputElement(document, prefix) },
            { Tags.Map, (document, prefix) => new HtmlMapElement(document, prefix) },
            { Tags.Datalist, (document, prefix) => new HtmlDataListElement(document, prefix) },
            { Tags.Keygen, (document, prefix) => new HtmlKeygenElement(document, prefix) },
            { Tags.Xmp, (document, prefix) => new HtmlXmpElement(document, prefix) },
            { Tags.Template, (document, prefix) => new HtmlTemplateElement(document, prefix) },
            { Tags.Picture, (document, prefix) => new HtmlPictureElement(document, prefix) },
            { Tags.Data, (document, prefix) => new HtmlDataElement(document, prefix) }
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
            Creator creator;

            if (creators.TryGetValue(localName, out creator))
                return creator(document, prefix);

            return new HtmlUnknownElement(document, localName.ToLowerInvariant(), prefix);
        }
    }
}
