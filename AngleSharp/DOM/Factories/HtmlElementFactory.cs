namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Provides string to HTMLElement instance creation mappings.
    /// </summary>
    sealed class HtmlElementFactory : ElementFactory<HTMLElement>
    {
        static readonly HtmlElementFactory Instance = new HtmlElementFactory();

        HtmlElementFactory()
        {
            creators.Add(Tags.Base, document => new HTMLBaseElement(document));
            creators.Add(Tags.BaseFont, document => new HTMLBaseFontElement(document));
            creators.Add(Tags.Link, document => new HTMLLinkElement(document));
            creators.Add(Tags.A, document => new HTMLAnchorElement(document));
            creators.Add(Tags.Button, document => new HTMLButtonElement(document));
            creators.Add(Tags.Input, document => new HTMLInputElement(document));
            creators.Add(Tags.Html, document => new HTMLHtmlElement(document));
            creators.Add(Tags.IsIndex, document => new HTMLIsIndexElement(document));
            creators.Add(Tags.Br, document => new HTMLBRElement(document));
            creators.Add(Tags.Embed, document => new HTMLEmbedElement(document));
            creators.Add(Tags.Div, document => new HTMLDivElement(document));
            creators.Add(Tags.Area, document => new HTMLAreaElement(document));
            creators.Add(Tags.Img, document => new HTMLImageElement { Owner = document });
            creators.Add(Tags.Dl, document => new HTMLDListElement { Owner = document });
            creators.Add(Tags.Body, document => new HTMLBodyElement { Owner = document });
            creators.Add(Tags.Fieldset, document => new HTMLFieldSetElement { Owner = document });
            creators.Add(Tags.Head, document => new HTMLHeadElement { Owner = document });
            creators.Add(Tags.Menu, document => new HTMLMenuElement { Owner = document });
            creators.Add(Tags.Meta, document => new HTMLMetaElement { Owner = document });
            creators.Add(Tags.Ol, document => new HTMLOListElement { Owner = document });
            creators.Add(Tags.P, document => new HTMLParagraphElement { Owner = document });
            creators.Add(Tags.Select, document => new HTMLSelectElement { Owner = document });
            creators.Add(Tags.Ul, document => new HTMLUListElement { Owner = document });
            creators.Add(Tags.Hr, document => new HTMLHRElement { Owner = document });
            creators.Add(Tags.Dir, document => new HTMLDirectoryElement { Owner = document });
            creators.Add(Tags.Font, document => new HTMLFontElement { Owner = document });
            creators.Add(Tags.Form, document => new HTMLFormElement { Owner = document });
            creators.Add(Tags.Param, document => new HTMLParamElement { Owner = document });
            creators.Add(Tags.Pre, document => new HTMLPreElement { Owner = document });
            creators.Add(Tags.Textarea, document => new HTMLTextAreaElement { Owner = document });
            creators.Add(Tags.BlockQuote, document => new HTMLQuoteElement(Tags.BlockQuote) { Owner = document });
            creators.Add(Tags.Quote, document => new HTMLQuoteElement(Tags.Quote) { Owner = document });
            creators.Add(Tags.Q, document => new HTMLQuoteElement(Tags.Q) { Owner = document });
            creators.Add(Tags.Canvas, document => new HTMLCanvasElement { Owner = document });
            creators.Add(Tags.Caption, document => new HTMLTableCaptionElement { Owner = document });
            creators.Add(Tags.Th, document => new HTMLTableHeaderCellElement { Owner = document });
            creators.Add(Tags.Td, document => new HTMLTableDataCellElement { Owner = document });
            creators.Add(Tags.Tr, document => new HTMLTableRowElement { Owner = document });
            creators.Add(Tags.Tbody, document => new HTMLTableSectionElement(Tags.Tbody) { Owner = document });
            creators.Add(Tags.Tfoot, document => new HTMLTableSectionElement(Tags.Tfoot) { Owner = document });
            creators.Add(Tags.Thead, document => new HTMLTableSectionElement(Tags.Thead) { Owner = document });
            creators.Add(Tags.Table, document => new HTMLTableElement { Owner = document });
            creators.Add(Tags.Colgroup, document => new HTMLTableColgroupElement { Owner = document });
            creators.Add(Tags.Col, document => new HTMLTableColElement { Owner = document });
            creators.Add(Tags.Del, document => new HTMLModElement(Tags.Del) { Owner = document });
            creators.Add(Tags.Ins, document => new HTMLModElement(Tags.Ins) { Owner = document });
            creators.Add(Tags.Legend, document => new HTMLLegendElement { Owner = document });
            creators.Add(Tags.Label, document => new HTMLLabelElement { Owner = document });
            creators.Add(Tags.Applet, document => new HTMLAppletElement { Owner = document });
            creators.Add(Tags.Object, document => new HTMLObjectElement { Owner = document });
            creators.Add(Tags.Optgroup, document => new HTMLOptGroupElement { Owner = document });
            creators.Add(Tags.Option, document => new HTMLOptionElement { Owner = document });
            creators.Add(Tags.Style, document => new HTMLStyleElement { Owner = document });
            creators.Add(Tags.Script, document => new HTMLScriptElement { Owner = document });
            creators.Add(Tags.Iframe, document => new HTMLIFrameElement { Owner = document });
            creators.Add(Tags.Title, document => new HTMLTitleElement { Owner = document });
            creators.Add(Tags.Li, document => new HTMLLIElement(Tags.Li) { Owner = document });
            creators.Add(Tags.Dd, document => new HTMLLIElement(Tags.Dd) { Owner = document });
            creators.Add(Tags.Dt, document => new HTMLLIElement(Tags.Dt) { Owner = document });
            creators.Add(Tags.Frameset, document => new HTMLFrameSetElement  { Owner = document });
            creators.Add(Tags.Frame, document => new HTMLFrameElement { Owner = document });
            creators.Add(Tags.H1, document => new HTMLHeadingElement(Tags.H1) { Owner = document });
            creators.Add(Tags.H2, document => new HTMLHeadingElement(Tags.H2) { Owner = document });
            creators.Add(Tags.H3, document => new HTMLHeadingElement(Tags.H3) { Owner = document });
            creators.Add(Tags.H4, document => new HTMLHeadingElement(Tags.H4) { Owner = document });
            creators.Add(Tags.H5, document => new HTMLHeadingElement(Tags.H5) { Owner = document });
            creators.Add(Tags.H6, document => new HTMLHeadingElement(Tags.H6) { Owner = document });
            creators.Add(Tags.Audio, document => new HTMLAudioElement { Owner = document });
            creators.Add(Tags.Video, document => new HTMLVideoElement { Owner = document });
            creators.Add(Tags.Span, document => new HTMLSpanElement { Owner = document });
            creators.Add(Tags.Dialog, document => new HTMLDialogElement { Owner = document });
            creators.Add(Tags.Details, document => new HTMLDetailsElement { Owner = document });
            creators.Add(Tags.Source, document => new HTMLSourceElement { Owner = document });
            creators.Add(Tags.Track, document => new HTMLTrackElement { Owner = document });
            creators.Add(Tags.Wbr, document => new HTMLWbrElement { Owner = document });
            creators.Add(Tags.B, document => new HTMLBoldElement { Owner = document });
            creators.Add(Tags.Big, document => new HTMLBigElement { Owner = document });
            creators.Add(Tags.Strike, document => new HTMLStrikeElement { Owner = document });
            creators.Add(Tags.Code, document => new HTMLCodeElement { Owner = document });
            creators.Add(Tags.Em, document => new HTMLEmphasizeElement { Owner = document });
            creators.Add(Tags.I, document => new HTMLItalicElement { Owner = document });
            creators.Add(Tags.S, document => new HTMLStruckElement { Owner = document });
            creators.Add(Tags.Small, document => new HTMLSmallElement  { Owner = document });
            creators.Add(Tags.Strong, document => new HTMLStrongElement  { Owner = document });
            creators.Add(Tags.U, document => new HTMLUnderlineElement { Owner = document });
            creators.Add(Tags.Tt, document => new HTMLTeletypeTextElement { Owner = document });
            creators.Add(Tags.NoBr, document => new HTMLNoNewlineElement { Owner = document });
            creators.Add(Tags.Address, document => new HTMLAddressElement { Owner = document });
            creators.Add(Tags.Main, document => new HTMLSemanticElement(Tags.Main) { Owner = document });
            creators.Add(Tags.Summary, document => new HTMLSemanticElement(Tags.Summary) { Owner = document });
            creators.Add(Tags.Xmp, document => new HTMLSemanticElement(Tags.Xmp) { Owner = document });
            creators.Add(Tags.Center, document => new HTMLSemanticElement(Tags.Center) { Owner = document });
            creators.Add(Tags.Listing, document => new HTMLSemanticElement(Tags.Listing) { Owner = document });
            creators.Add(Tags.Nav, document => new HTMLSemanticElement(Tags.Nav) { Owner = document });
            creators.Add(Tags.Article, document => new HTMLSemanticElement(Tags.Article) { Owner = document });
            creators.Add(Tags.Aside, document => new HTMLSemanticElement(Tags.Aside) { Owner = document });
            creators.Add(Tags.Figcaption, document => new HTMLSemanticElement(Tags.Figcaption) { Owner = document });
            creators.Add(Tags.Figure, document => new HTMLSemanticElement(Tags.Figure) { Owner = document });
            creators.Add(Tags.Section, document => new HTMLSemanticElement(Tags.Section) { Owner = document });
            creators.Add(Tags.Footer, document => new HTMLSemanticElement(Tags.Footer) { Owner = document });
            creators.Add(Tags.Header, document => new HTMLSemanticElement(Tags.Header) { Owner = document });
            creators.Add(Tags.Hgroup, document => new HTMLSemanticElement(Tags.Hgroup) { Owner = document });
            creators.Add(Tags.Plaintext, document => new HTMLSemanticElement(Tags.Plaintext) { Owner = document });
            creators.Add(Tags.Bgsound, document => new HTMLBgsoundElement { Owner = document });
            creators.Add(Tags.Marquee, document => new HTMLMarqueeElement { Owner = document });
            creators.Add(Tags.NoEmbed, document => new HTMLNoEmbedElement { Owner = document });
            creators.Add(Tags.NoFrames, document => new HTMLNoFramesElement { Owner = document });
            creators.Add(Tags.NoScript, document => new HTMLNoScriptElement { Owner = document });
            creators.Add(Tags.MenuItem, document => new HTMLMenuItemElement  { Owner = document });
            creators.Add(Tags.Cite, document => new HTMLElement(Tags.Cite) { Owner = document });
            creators.Add(Tags.Ruby, document => new HTMLRubyElement { Owner = document });
            creators.Add(Tags.Rt, document => new HTMLRTElement { Owner = document });
            creators.Add(Tags.Rp, document => new HTMLRPElement { Owner = document });
            creators.Add(Tags.Time, document => new HTMLTimeElement { Owner = document });
            creators.Add(Tags.Progress, document => new HTMLProgressElement { Owner = document });
            creators.Add(Tags.Output, document => new HTMLOutputElement { Owner = document });
            creators.Add(Tags.Map, document => new HTMLMapElement { Owner = document });
            creators.Add(Tags.Datalist, document => new HTMLDataListElement { Owner = document });
            creators.Add(Tags.Keygen, document => new HTMLKeygenElement { Owner = document });
        }

        protected override HTMLElement CreateDefault(String name, Document document)
        {
            return new HTMLUnknownElement(name.ToLowerInvariant()) { Owner = document };
        }

        /// <summary>
        /// Returns a specialized HTMLElement instance for the given tag name.
        /// </summary>
        /// <param name="tag">The given tag name.</param>
        /// <param name="document">The document that owns the element.</param>
        /// <returns>The specialized HTMLElement instance.</returns>
        public static HTMLElement Create(String tag, Document document)
        {
            return Instance.CreateSpecific(tag, document);
        }
    }
}
