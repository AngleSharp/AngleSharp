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
            // REVIEW: is ToLowerInvariant() the right approach here? are TagNames always lowercase?
            return localName.ToLowerInvariant() switch
            {
                TagNames.Div => new HtmlDivElement(document, prefix),
                TagNames.A => new HtmlAnchorElement(document, prefix),
                TagNames.Img => new HtmlImageElement(document, prefix),
                TagNames.P => new HtmlParagraphElement(document, prefix),
                TagNames.Br => new HtmlBreakRowElement(document, prefix),
                TagNames.Input => new HtmlInputElement(document, prefix),
                TagNames.Button => new HtmlButtonElement(document, prefix),
                TagNames.Textarea => new HtmlTextAreaElement(document, prefix),
                TagNames.Li => new HtmlListItemElement(document, TagNames.Li, prefix),
                TagNames.H1 => new HtmlHeadingElement(document, TagNames.H1, prefix),
                TagNames.H2 => new HtmlHeadingElement(document, TagNames.H2, prefix),
                TagNames.H3 => new HtmlHeadingElement(document, TagNames.H3, prefix),
                TagNames.H4 => new HtmlHeadingElement(document, TagNames.H4, prefix),
                TagNames.H5 => new HtmlHeadingElement(document, TagNames.H5, prefix),
                TagNames.H6 => new HtmlHeadingElement(document, TagNames.H6, prefix),
                TagNames.Ul => new HtmlUnorderedListElement(document, prefix),
                TagNames.Ol => new HtmlOrderedListElement(document, prefix),
                TagNames.Dl => new HtmlDefinitionListElement(document, prefix),
                TagNames.Link => new HtmlLinkElement(document, prefix),
                TagNames.Meta => new HtmlMetaElement(document, prefix),
                TagNames.Label => new HtmlLabelElement(document, prefix),
                TagNames.Fieldset => new HtmlFieldSetElement(document, prefix),
                TagNames.Legend => new HtmlLegendElement(document, prefix),
                TagNames.Form => new HtmlFormElement(document, prefix),
                TagNames.Select => new HtmlSelectElement(document, prefix),
                TagNames.Pre => new HtmlPreElement(document, prefix),
                TagNames.Hr => new HtmlHrElement(document, prefix),
                TagNames.Dir => new HtmlDirectoryElement(document, prefix),
                TagNames.Font => new HtmlFontElement(document, prefix),
                TagNames.Param => new HtmlParamElement(document, prefix),
                TagNames.BlockQuote => new HtmlQuoteElement(document, TagNames.BlockQuote, prefix),
                TagNames.Quote => new HtmlQuoteElement(document, TagNames.Quote, prefix),
                TagNames.Q => new HtmlQuoteElement(document, TagNames.Q, prefix),
                TagNames.Canvas => new HtmlCanvasElement(document, prefix),
                TagNames.Caption => new HtmlTableCaptionElement(document, prefix),
                TagNames.Td => new HtmlTableDataCellElement(document, prefix),
                TagNames.Tr => new HtmlTableRowElement(document, prefix),
                TagNames.Table => new HtmlTableElement(document, prefix),
                TagNames.Tbody => new HtmlTableSectionElement(document, TagNames.Tbody, prefix),
                TagNames.Th => new HtmlTableHeaderCellElement(document, prefix),
                TagNames.Tfoot => new HtmlTableSectionElement(document, TagNames.Tfoot, prefix),
                TagNames.Thead => new HtmlTableSectionElement(document, TagNames.Thead, prefix),
                TagNames.Colgroup => new HtmlTableColgroupElement(document, prefix),
                TagNames.Col => new HtmlTableColElement(document, prefix),
                TagNames.Del => new HtmlModElement(document, TagNames.Del, prefix),
                TagNames.Ins => new HtmlModElement(document, TagNames.Ins, prefix),
                TagNames.Applet => new HtmlAppletElement(document, prefix),
                TagNames.Object => new HtmlObjectElement(document, prefix),
                TagNames.Optgroup => new HtmlOptionsGroupElement(document, prefix),
                TagNames.Option => new HtmlOptionElement(document, prefix),
                TagNames.Style => new HtmlStyleElement(document, prefix),
                TagNames.Script => new HtmlScriptElement(document, prefix),
                TagNames.Iframe => new HtmlIFrameElement(document, prefix),
                TagNames.Dd => new HtmlListItemElement(document, TagNames.Dd, prefix),
                TagNames.Dt => new HtmlListItemElement(document, TagNames.Dt, prefix),
                TagNames.Frameset => new HtmlFrameSetElement(document, prefix),
                TagNames.Frame => new HtmlFrameElement(document, prefix),
                TagNames.Audio => new HtmlAudioElement(document, prefix),
                TagNames.Video => new HtmlVideoElement(document, prefix),
                TagNames.Span => new HtmlSpanElement(document, prefix),
                TagNames.Dialog => new HtmlDialogElement(document, prefix),
                TagNames.Details => new HtmlDetailsElement(document, prefix),
                TagNames.Source => new HtmlSourceElement(document, prefix),
                TagNames.Track => new HtmlTrackElement(document, prefix),
                TagNames.Wbr => new HtmlWbrElement(document, prefix),
                TagNames.B => new HtmlBoldElement(document, prefix),
                TagNames.Big => new HtmlBigElement(document, prefix),
                TagNames.Strike => new HtmlStrikeElement(document, prefix),
                TagNames.Code => new HtmlCodeElement(document, prefix),
                TagNames.Em => new HtmlEmphasizeElement(document, prefix),
                TagNames.I => new HtmlItalicElement(document, prefix),
                TagNames.S => new HtmlStruckElement(document, prefix),
                TagNames.Small => new HtmlSmallElement(document, prefix),
                TagNames.Strong => new HtmlStrongElement(document, prefix),
                TagNames.U => new HtmlUnderlineElement(document, prefix),
                TagNames.Tt => new HtmlTeletypeTextElement(document, prefix),
                TagNames.Address => new HtmlAddressElement(document, prefix),
                TagNames.Main => new HtmlSemanticElement(document, TagNames.Main, prefix),
                TagNames.Summary => new HtmlSemanticElement(document, TagNames.Summary, prefix),
                TagNames.Center => new HtmlSemanticElement(document, TagNames.Center, prefix),
                TagNames.Listing => new HtmlSemanticElement(document, TagNames.Listing, prefix),
                TagNames.Nav => new HtmlSemanticElement(document, TagNames.Nav, prefix),
                TagNames.Article => new HtmlSemanticElement(document, TagNames.Article, prefix),
                TagNames.Aside => new HtmlSemanticElement(document, TagNames.Aside, prefix),
                TagNames.Figcaption => new HtmlSemanticElement(document, TagNames.Figcaption, prefix),
                TagNames.Figure => new HtmlSemanticElement(document, TagNames.Figure, prefix),
                TagNames.Section => new HtmlSemanticElement(document, TagNames.Section, prefix),
                TagNames.Footer => new HtmlSemanticElement(document, TagNames.Footer, prefix),
                TagNames.Header => new HtmlSemanticElement(document, TagNames.Header, prefix),
                TagNames.Hgroup => new HtmlSemanticElement(document, TagNames.Hgroup, prefix),
                TagNames.Cite => new HtmlElement(document, TagNames.Cite, prefix),
                TagNames.Ruby => new HtmlRubyElement(document, prefix),
                TagNames.Rt => new HtmlRtElement(document, prefix),
                TagNames.Rp => new HtmlRpElement(document, prefix),
                TagNames.Rtc => new HtmlRtcElement(document, prefix),
                TagNames.Rb => new HtmlRbElement(document, prefix),
                TagNames.Map => new HtmlMapElement(document, prefix),
                TagNames.Datalist => new HtmlDataListElement(document, prefix),
                TagNames.Xmp => new HtmlXmpElement(document, prefix),
                TagNames.Picture => new HtmlPictureElement(document, prefix),
                TagNames.Template => new HtmlTemplateElement(document, prefix),
                TagNames.Time => new HtmlTimeElement(document, prefix),
                TagNames.Progress => new HtmlProgressElement(document, prefix),
                TagNames.Meter => new HtmlMeterElement(document, prefix),
                TagNames.Output => new HtmlOutputElement(document, prefix),
                TagNames.Keygen => new HtmlKeygenElement(document, prefix),
                TagNames.Title => new HtmlTitleElement(document, prefix),
                TagNames.Head => new HtmlHeadElement(document, prefix),
                TagNames.Body => new HtmlBodyElement(document, prefix),
                TagNames.Html => new HtmlHtmlElement(document, prefix),
                TagNames.Area => new HtmlAreaElement(document, prefix),
                TagNames.Embed => new HtmlEmbedElement(document, prefix),
                TagNames.MenuItem => new HtmlMenuItemElement(document, prefix),
                TagNames.Slot => new HtmlSlotElement(document, prefix),
                TagNames.NoScript => new HtmlNoScriptElement(document, prefix),
                TagNames.NoEmbed => new HtmlNoEmbedElement(document, prefix),
                TagNames.NoFrames => new HtmlNoFramesElement(document, prefix),
                TagNames.NoBr => new HtmlNoNewlineElement(document, prefix),
                TagNames.Menu => new HtmlMenuElement(document, prefix),
                TagNames.Base => new HtmlBaseElement(document, prefix),
                TagNames.BaseFont => new HtmlBaseFontElement(document, prefix),
                TagNames.Bgsound => new HtmlBgsoundElement(document, prefix),
                TagNames.Marquee => new HtmlMarqueeElement(document, prefix),
                TagNames.Data => new HtmlDataElement(document, prefix),
                TagNames.Plaintext => new HtmlSemanticElement(document, TagNames.Plaintext, prefix),
                TagNames.IsIndex => new HtmlIsIndexElement(document, prefix),
                TagNames.Mark => new HtmlElement(document, TagNames.Mark),
                TagNames.Sub => new HtmlElement(document, TagNames.Sub),
                TagNames.Sup => new HtmlElement(document, TagNames.Sup),
                TagNames.Dfn => new HtmlElement(document, TagNames.Dfn),
                TagNames.Kbd => new HtmlElement(document, TagNames.Kbd),
                TagNames.Var => new HtmlElement(document, TagNames.Var),
                TagNames.Samp => new HtmlElement(document, TagNames.Samp),
                TagNames.Abbr => new HtmlElement(document, TagNames.Abbr),
                TagNames.Bdi => new HtmlElement(document, TagNames.Bdi),
                TagNames.Bdo => new HtmlElement(document, TagNames.Bdo),
                _ => new HtmlUnknownElement(document, localName.HtmlLower(), prefix, flags)
            };
        }
    }
}