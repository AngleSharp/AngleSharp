namespace AngleSharp.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Provides string to HTMLElement instance creation mappings.
    /// </summary>
    sealed class HtmlElementFactory : IElementFactory<Document, HtmlElement>
    {
        /// <summary>
        /// Returns a specialized HTMLElement instance for the given tag name.
        /// </summary>
        /// <param name="document">The document that owns the element.</param>
        /// <param name="localName">The given tag name.</param>
        /// <param name="prefix">The prefix of the element, if any.</param>
        /// <param name="flags">The optional flags, if any.</param>
        /// <returns>The specialized HTMLElement instance.</returns>
        public HtmlElement Create(Document document, String localName, String? prefix = null, NodeFlags flags = NodeFlags.None)
        {
            if (localName.Equals(TagNames.Div, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlDivElement(document, prefix);
            }

            if (localName.Equals(TagNames.A, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlAnchorElement(document, prefix);
            }

            if (localName.Equals(TagNames.Img, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlImageElement(document, prefix);
            }

            if (localName.Equals(TagNames.P, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlParagraphElement(document, prefix);
            }

            if (localName.Equals(TagNames.Br, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlBreakRowElement(document, prefix);
            }

            if (localName.Equals(TagNames.Input, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlInputElement(document, prefix);
            }

            if (localName.Equals(TagNames.Button, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlButtonElement(document, prefix);
            }

            if (localName.Equals(TagNames.Textarea, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlTextAreaElement(document, prefix);
            }

            if (localName.Equals(TagNames.Li, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlListItemElement(document, TagNames.Li, prefix);
            }

            if (localName.Equals(TagNames.H1, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlHeadingElement(document, TagNames.H1, prefix);
            }

            if (localName.Equals(TagNames.H2, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlHeadingElement(document, TagNames.H2, prefix);
            }

            if (localName.Equals(TagNames.H3, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlHeadingElement(document, TagNames.H3, prefix);
            }

            if (localName.Equals(TagNames.H4, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlHeadingElement(document, TagNames.H4, prefix);
            }

            if (localName.Equals(TagNames.H5, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlHeadingElement(document, TagNames.H5, prefix);
            }

            if (localName.Equals(TagNames.H6, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlHeadingElement(document, TagNames.H6, prefix);
            }

            if (localName.Equals(TagNames.Ul, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlUnorderedListElement(document, prefix);
            }

            if (localName.Equals(TagNames.Ol, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlOrderedListElement(document, prefix);
            }

            if (localName.Equals(TagNames.Dl, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlDefinitionListElement(document, prefix);
            }

            if (localName.Equals(TagNames.Link, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlLinkElement(document, prefix);
            }

            if (localName.Equals(TagNames.Meta, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlMetaElement(document, prefix);
            }

            if (localName.Equals(TagNames.Label, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlLabelElement(document, prefix);
            }

            if (localName.Equals(TagNames.Fieldset, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlFieldSetElement(document, prefix);
            }

            if (localName.Equals(TagNames.Legend, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlLegendElement(document, prefix);
            }

            if (localName.Equals(TagNames.Form, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlFormElement(document, prefix);
            }

            if (localName.Equals(TagNames.Select, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSelectElement(document, prefix);
            }

            if (localName.Equals(TagNames.Pre, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlPreElement(document, prefix);
            }

            if (localName.Equals(TagNames.Hr, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlHrElement(document, prefix);
            }

            if (localName.Equals(TagNames.Dir, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlDirectoryElement(document, prefix);
            }

            if (localName.Equals(TagNames.Font, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlFontElement(document, prefix);
            }

            if (localName.Equals(TagNames.Param, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlParamElement(document, prefix);
            }

            if (localName.Equals(TagNames.BlockQuote, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlQuoteElement(document, TagNames.BlockQuote, prefix);
            }

            if (localName.Equals(TagNames.Quote, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlQuoteElement(document, TagNames.Quote, prefix);
            }

            if (localName.Equals(TagNames.Q, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlQuoteElement(document, TagNames.Q, prefix);
            }

            if (localName.Equals(TagNames.Canvas, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlCanvasElement(document, prefix);
            }

            if (localName.Equals(TagNames.Caption, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlTableCaptionElement(document, prefix);
            }

            if (localName.Equals(TagNames.Td, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlTableDataCellElement(document, prefix);
            }

            if (localName.Equals(TagNames.Tr, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlTableRowElement(document, prefix);
            }

            if (localName.Equals(TagNames.Table, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlTableElement(document, prefix);
            }

            if (localName.Equals(TagNames.Tbody, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlTableSectionElement(document, TagNames.Tbody, prefix);
            }

            if (localName.Equals(TagNames.Th, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlTableHeaderCellElement(document, prefix);
            }

            if (localName.Equals(TagNames.Tfoot, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlTableSectionElement(document, TagNames.Tfoot, prefix);
            }

            if (localName.Equals(TagNames.Thead, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlTableSectionElement(document, TagNames.Thead, prefix);
            }

            if (localName.Equals(TagNames.Colgroup, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlTableColgroupElement(document, prefix);
            }

            if (localName.Equals(TagNames.Col, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlTableColElement(document, prefix);
            }

            if (localName.Equals(TagNames.Del, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlModElement(document, TagNames.Del, prefix);
            }

            if (localName.Equals(TagNames.Ins, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlModElement(document, TagNames.Ins, prefix);
            }

            if (localName.Equals(TagNames.Applet, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlAppletElement(document, prefix);
            }

            if (localName.Equals(TagNames.Object, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlObjectElement(document, prefix);
            }

            if (localName.Equals(TagNames.Optgroup, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlOptionsGroupElement(document, prefix);
            }

            if (localName.Equals(TagNames.Option, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlOptionElement(document, prefix);
            }

            if (localName.Equals(TagNames.Style, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlStyleElement(document, prefix);
            }

            if (localName.Equals(TagNames.Script, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlScriptElement(document, prefix);
            }

            if (localName.Equals(TagNames.Iframe, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlIFrameElement(document, prefix);
            }

            if (localName.Equals(TagNames.Dd, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlListItemElement(document, TagNames.Dd, prefix);
            }

            if (localName.Equals(TagNames.Dt, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlListItemElement(document, TagNames.Dt, prefix);
            }

            if (localName.Equals(TagNames.Frameset, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlFrameSetElement(document, prefix);
            }

            if (localName.Equals(TagNames.Frame, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlFrameElement(document, prefix);
            }

            if (localName.Equals(TagNames.Audio, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlAudioElement(document, prefix);
            }

            if (localName.Equals(TagNames.Video, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlVideoElement(document, prefix);
            }

            if (localName.Equals(TagNames.Span, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSpanElement(document, prefix);
            }

            if (localName.Equals(TagNames.Dialog, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlDialogElement(document, prefix);
            }

            if (localName.Equals(TagNames.Details, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlDetailsElement(document, prefix);
            }

            if (localName.Equals(TagNames.Source, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSourceElement(document, prefix);
            }

            if (localName.Equals(TagNames.Track, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlTrackElement(document, prefix);
            }

            if (localName.Equals(TagNames.Wbr, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlWbrElement(document, prefix);
            }

            if (localName.Equals(TagNames.B, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlBoldElement(document, prefix);
            }

            if (localName.Equals(TagNames.Big, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlBigElement(document, prefix);
            }

            if (localName.Equals(TagNames.Strike, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlStrikeElement(document, prefix);
            }

            if (localName.Equals(TagNames.Code, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlCodeElement(document, prefix);
            }

            if (localName.Equals(TagNames.Em, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlEmphasizeElement(document, prefix);
            }

            if (localName.Equals(TagNames.I, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlItalicElement(document, prefix);
            }

            if (localName.Equals(TagNames.S, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlStruckElement(document, prefix);
            }

            if (localName.Equals(TagNames.Small, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSmallElement(document, prefix);
            }

            if (localName.Equals(TagNames.Strong, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlStrongElement(document, prefix);
            }

            if (localName.Equals(TagNames.U, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlUnderlineElement(document, prefix);
            }

            if (localName.Equals(TagNames.Tt, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlTeletypeTextElement(document, prefix);
            }

            if (localName.Equals(TagNames.Address, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlAddressElement(document, prefix);
            }

            if (localName.Equals(TagNames.Main, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSemanticElement(document, TagNames.Main, prefix);
            }

            if (localName.Equals(TagNames.Summary, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSemanticElement(document, TagNames.Summary, prefix);
            }

            if (localName.Equals(TagNames.Center, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSemanticElement(document, TagNames.Center, prefix);
            }

            if (localName.Equals(TagNames.Listing, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSemanticElement(document, TagNames.Listing, prefix);
            }

            if (localName.Equals(TagNames.Nav, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSemanticElement(document, TagNames.Nav, prefix);
            }

            if (localName.Equals(TagNames.Article, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSemanticElement(document, TagNames.Article, prefix);
            }

            if (localName.Equals(TagNames.Aside, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSemanticElement(document, TagNames.Aside, prefix);
            }

            if (localName.Equals(TagNames.Figcaption, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSemanticElement(document, TagNames.Figcaption, prefix);
            }

            if (localName.Equals(TagNames.Figure, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSemanticElement(document, TagNames.Figure, prefix);
            }

            if (localName.Equals(TagNames.Section, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSemanticElement(document, TagNames.Section, prefix);
            }

            if (localName.Equals(TagNames.Footer, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSemanticElement(document, TagNames.Footer, prefix);
            }

            if (localName.Equals(TagNames.Header, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSemanticElement(document, TagNames.Header, prefix);
            }

            if (localName.Equals(TagNames.Hgroup, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSemanticElement(document, TagNames.Hgroup, prefix);
            }

            if (localName.Equals(TagNames.Cite, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlElement(document, TagNames.Cite, prefix);
            }

            if (localName.Equals(TagNames.Ruby, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlRubyElement(document, prefix);
            }

            if (localName.Equals(TagNames.Rt, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlRtElement(document, prefix);
            }

            if (localName.Equals(TagNames.Rp, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlRpElement(document, prefix);
            }

            if (localName.Equals(TagNames.Rtc, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlRtcElement(document, prefix);
            }

            if (localName.Equals(TagNames.Rb, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlRbElement(document, prefix);
            }

            if (localName.Equals(TagNames.Map, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlMapElement(document, prefix);
            }

            if (localName.Equals(TagNames.Datalist, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlDataListElement(document, prefix);
            }

            if (localName.Equals(TagNames.Xmp, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlXmpElement(document, prefix);
            }

            if (localName.Equals(TagNames.Picture, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlPictureElement(document, prefix);
            }

            if (localName.Equals(TagNames.Template, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlTemplateElement(document, prefix);
            }

            if (localName.Equals(TagNames.Time, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlTimeElement(document, prefix);
            }

            if (localName.Equals(TagNames.Progress, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlProgressElement(document, prefix);
            }

            if (localName.Equals(TagNames.Meter, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlMeterElement(document, prefix);
            }

            if (localName.Equals(TagNames.Output, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlOutputElement(document, prefix);
            }

            if (localName.Equals(TagNames.Keygen, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlKeygenElement(document, prefix);
            }

            if (localName.Equals(TagNames.Title, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlTitleElement(document, prefix);
            }

            if (localName.Equals(TagNames.Head, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlHeadElement(document, prefix);
            }

            if (localName.Equals(TagNames.Body, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlBodyElement(document, prefix);
            }

            if (localName.Equals(TagNames.Html, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlHtmlElement(document, prefix);
            }

            if (localName.Equals(TagNames.Area, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlAreaElement(document, prefix);
            }

            if (localName.Equals(TagNames.Embed, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlEmbedElement(document, prefix);
            }

            if (localName.Equals(TagNames.MenuItem, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlMenuItemElement(document, prefix);
            }

            if (localName.Equals(TagNames.Slot, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSlotElement(document, prefix);
            }

            if (localName.Equals(TagNames.NoScript, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlNoScriptElement(document, prefix);
            }

            if (localName.Equals(TagNames.NoEmbed, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlNoEmbedElement(document, prefix);
            }

            if (localName.Equals(TagNames.NoFrames, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlNoFramesElement(document, prefix);
            }

            if (localName.Equals(TagNames.NoBr, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlNoNewlineElement(document, prefix);
            }

            if (localName.Equals(TagNames.Menu, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlMenuElement(document, prefix);
            }

            if (localName.Equals(TagNames.Base, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlBaseElement(document, prefix);
            }

            if (localName.Equals(TagNames.BaseFont, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlBaseFontElement(document, prefix);
            }

            if (localName.Equals(TagNames.Bgsound, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlBgsoundElement(document, prefix);
            }

            if (localName.Equals(TagNames.Marquee, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlMarqueeElement(document, prefix);
            }

            if (localName.Equals(TagNames.Data, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlDataElement(document, prefix);
            }

            if (localName.Equals(TagNames.Plaintext, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlSemanticElement(document, TagNames.Plaintext, prefix);
            }

            if (localName.Equals(TagNames.IsIndex, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlIsIndexElement(document, prefix);
            }

            if (localName.Equals(TagNames.Mark, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlElement(document, TagNames.Mark);
            }

            if (localName.Equals(TagNames.Sub, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlElement(document, TagNames.Sub);
            }

            if (localName.Equals(TagNames.Sup, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlElement(document, TagNames.Sup);
            }

            if (localName.Equals(TagNames.Dfn, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlElement(document, TagNames.Dfn);
            }

            if (localName.Equals(TagNames.Kbd, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlElement(document, TagNames.Kbd);
            }

            if (localName.Equals(TagNames.Var, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlElement(document, TagNames.Var);
            }

            if (localName.Equals(TagNames.Samp, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlElement(document, TagNames.Samp);
            }

            if (localName.Equals(TagNames.Abbr, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlElement(document, TagNames.Abbr);
            }

            if (localName.Equals(TagNames.Bdi, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlElement(document, TagNames.Bdi);
            }

            if (localName.Equals(TagNames.Bdo, StringComparison.OrdinalIgnoreCase))
            {
                return new HtmlElement(document, TagNames.Bdo);
            }

            return new HtmlUnknownElement(document, localName.HtmlLower(), prefix, flags);
        }
    }
}