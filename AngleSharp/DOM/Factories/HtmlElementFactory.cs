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
            creators.Add(Tags.Img, document => new HTMLImageElement(document));
            creators.Add(Tags.Dl, document => new HTMLDListElement( document));
            creators.Add(Tags.Body, document => new HTMLBodyElement(document));
            creators.Add(Tags.Fieldset, document => new HTMLFieldSetElement(document));
            creators.Add(Tags.Head, document => new HTMLHeadElement(document));
            creators.Add(Tags.Menu, document => new HTMLMenuElement(document));
            creators.Add(Tags.Meta, document => new HTMLMetaElement(document));
            creators.Add(Tags.Ol, document => new HTMLOListElement(document));
            creators.Add(Tags.P, document => new HTMLParagraphElement(document));
            creators.Add(Tags.Select, document => new HTMLSelectElement(document));
            creators.Add(Tags.Ul, document => new HTMLUListElement(document));
            creators.Add(Tags.Hr, document => new HTMLHRElement(document));
            creators.Add(Tags.Dir, document => new HTMLDirectoryElement(document));
            creators.Add(Tags.Font, document => new HTMLFontElement(document));
            creators.Add(Tags.Form, document => new HTMLFormElement(document));
            creators.Add(Tags.Param, document => new HTMLParamElement(document));
            creators.Add(Tags.Pre, document => new HTMLPreElement(document));
            creators.Add(Tags.Textarea, document => new HTMLTextAreaElement(document));
            creators.Add(Tags.BlockQuote, document => new HTMLQuoteElement(document, Tags.BlockQuote));
            creators.Add(Tags.Quote, document => new HTMLQuoteElement(document, Tags.Quote));
            creators.Add(Tags.Q, document => new HTMLQuoteElement(document, Tags.Q));
            creators.Add(Tags.Canvas, document => new HTMLCanvasElement(document));
            creators.Add(Tags.Caption, document => new HTMLTableCaptionElement(document));
            creators.Add(Tags.Th, document => new HTMLTableHeaderCellElement(document));
            creators.Add(Tags.Td, document => new HTMLTableDataCellElement(document));
            creators.Add(Tags.Tr, document => new HTMLTableRowElement(document));
            creators.Add(Tags.Tbody, document => new HTMLTableSectionElement(document, Tags.Tbody));
            creators.Add(Tags.Tfoot, document => new HTMLTableSectionElement(document, Tags.Tfoot));
            creators.Add(Tags.Thead, document => new HTMLTableSectionElement(document, Tags.Thead));
            creators.Add(Tags.Table, document => new HTMLTableElement(document));
            creators.Add(Tags.Colgroup, document => new HTMLTableColgroupElement(document));
            creators.Add(Tags.Col, document => new HTMLTableColElement(document));
            creators.Add(Tags.Del, document => new HTMLModElement(document, Tags.Del));
            creators.Add(Tags.Ins, document => new HTMLModElement(document, Tags.Ins));
            creators.Add(Tags.Legend, document => new HTMLLegendElement(document));
            creators.Add(Tags.Label, document => new HTMLLabelElement(document));
            creators.Add(Tags.Applet, document => new HTMLAppletElement(document));
            creators.Add(Tags.Object, document => new HTMLObjectElement(document));
            creators.Add(Tags.Optgroup, document => new HTMLOptGroupElement(document));
            creators.Add(Tags.Option, document => new HTMLOptionElement(document));
            creators.Add(Tags.Style, document => new HTMLStyleElement(document));
            creators.Add(Tags.Script, document => new HTMLScriptElement(document));
            creators.Add(Tags.Iframe, document => new HTMLIFrameElement(document));
            creators.Add(Tags.Title, document => new HTMLTitleElement(document));
            creators.Add(Tags.Li, document => new HTMLLIElement(document, Tags.Li));
            creators.Add(Tags.Dd, document => new HTMLLIElement(document, Tags.Dd));
            creators.Add(Tags.Dt, document => new HTMLLIElement(document, Tags.Dt));
            creators.Add(Tags.Frameset, document => new HTMLFrameSetElement(document));
            creators.Add(Tags.Frame, document => new HTMLFrameElement(document));
            creators.Add(Tags.H1, document => new HTMLHeadingElement(document, Tags.H1));
            creators.Add(Tags.H2, document => new HTMLHeadingElement(document, Tags.H2));
            creators.Add(Tags.H3, document => new HTMLHeadingElement(document, Tags.H3));
            creators.Add(Tags.H4, document => new HTMLHeadingElement(document, Tags.H4));
            creators.Add(Tags.H5, document => new HTMLHeadingElement(document, Tags.H5));
            creators.Add(Tags.H6, document => new HTMLHeadingElement(document, Tags.H6));
            creators.Add(Tags.Audio, document => new HTMLAudioElement(document));
            creators.Add(Tags.Video, document => new HTMLVideoElement(document));
            creators.Add(Tags.Span, document => new HTMLSpanElement(document));
            creators.Add(Tags.Dialog, document => new HTMLDialogElement(document));
            creators.Add(Tags.Details, document => new HTMLDetailsElement(document));
            creators.Add(Tags.Source, document => new HTMLSourceElement(document));
            creators.Add(Tags.Track, document => new HTMLTrackElement(document));
            creators.Add(Tags.Wbr, document => new HTMLWbrElement(document));
            creators.Add(Tags.B, document => new HTMLBoldElement(document));
            creators.Add(Tags.Big, document => new HTMLBigElement(document));
            creators.Add(Tags.Strike, document => new HTMLStrikeElement(document));
            creators.Add(Tags.Code, document => new HTMLCodeElement(document));
            creators.Add(Tags.Em, document => new HTMLEmphasizeElement(document));
            creators.Add(Tags.I, document => new HTMLItalicElement(document));
            creators.Add(Tags.S, document => new HTMLStruckElement(document));
            creators.Add(Tags.Small, document => new HTMLSmallElement(document));
            creators.Add(Tags.Strong, document => new HTMLStrongElement(document));
            creators.Add(Tags.U, document => new HTMLUnderlineElement(document));
            creators.Add(Tags.Tt, document => new HTMLTeletypeTextElement(document));
            creators.Add(Tags.NoBr, document => new HTMLNoNewlineElement(document));
            creators.Add(Tags.Address, document => new HTMLAddressElement(document));
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
