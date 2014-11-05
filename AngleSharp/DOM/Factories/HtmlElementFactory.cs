namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Factories;
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
            creators.Add(Tags.Base, () => new HTMLBaseElement());
            creators.Add(Tags.BaseFont, () => new HTMLBaseFontElement());
            creators.Add(Tags.Link, () => new HTMLLinkElement());
            creators.Add(Tags.A, () => new HTMLAnchorElement());
            creators.Add(Tags.Button, () => new HTMLButtonElement());
            creators.Add(Tags.Input, () => new HTMLInputElement());
            creators.Add(Tags.Html, () => new HTMLHtmlElement());
            creators.Add(Tags.IsIndex, () => new HTMLIsIndexElement());
            creators.Add(Tags.Br, () => new HTMLBRElement());
            creators.Add(Tags.Embed, () => new HTMLEmbedElement());
            creators.Add(Tags.Div, () => new HTMLDivElement());
            creators.Add(Tags.Area, () => new HTMLAreaElement());
            creators.Add(Tags.Img, () => new HTMLImageElement());
            creators.Add(Tags.Dl, () => new HTMLDListElement());
            creators.Add(Tags.Body, () => new HTMLBodyElement());
            creators.Add(Tags.Fieldset, () => new HTMLFieldSetElement());
            creators.Add(Tags.Head, () => new HTMLHeadElement());
            creators.Add(Tags.Menu, () => new HTMLMenuElement());
            creators.Add(Tags.Meta, () => new HTMLMetaElement());
            creators.Add(Tags.Ol, () => new HTMLOListElement());
            creators.Add(Tags.P, () => new HTMLParagraphElement());
            creators.Add(Tags.Select, () => new HTMLSelectElement());
            creators.Add(Tags.Ul, () => new HTMLUListElement());
            creators.Add(Tags.Hr, () => new HTMLHRElement());
            creators.Add(Tags.Dir, () => new HTMLDirectoryElement());
            creators.Add(Tags.Font, () => new HTMLFontElement ());
            creators.Add(Tags.Form, () => new HTMLFormElement ());
            creators.Add(Tags.Param, () => new HTMLParamElement ());
            creators.Add(Tags.Pre, () => new HTMLPreElement ());
            creators.Add(Tags.Textarea, () => new HTMLTextAreaElement ());
            creators.Add(Tags.BlockQuote, () => new HTMLQuoteElement(Tags.BlockQuote));
            creators.Add(Tags.Quote, () => new HTMLQuoteElement(Tags.Quote));
            creators.Add(Tags.Q, () => new HTMLQuoteElement(Tags.Q));
            creators.Add(Tags.Canvas, () => new HTMLCanvasElement ());
            creators.Add(Tags.Caption, () => new HTMLTableCaptionElement ());
            creators.Add(Tags.Th, () => new HTMLTableHeaderCellElement());
            creators.Add(Tags.Td, () => new HTMLTableDataCellElement ());
            creators.Add(Tags.Tr, () => new HTMLTableRowElement ());
            creators.Add(Tags.Tbody, () => new HTMLTableSectionElement(Tags.Tbody));
            creators.Add(Tags.Tfoot, () => new HTMLTableSectionElement(Tags.Tfoot));
            creators.Add(Tags.Thead, () => new HTMLTableSectionElement(Tags.Thead));
            creators.Add(Tags.Table, () => new HTMLTableElement ());
            creators.Add(Tags.Colgroup, () => new HTMLTableColgroupElement());
            creators.Add(Tags.Col, () => new HTMLTableColElement());
            creators.Add(Tags.Del, () => new HTMLModElement(Tags.Del));
            creators.Add(Tags.Ins, () => new HTMLModElement(Tags.Ins));
            creators.Add(Tags.Legend, () => new HTMLLegendElement ());
            creators.Add(Tags.Label, () => new HTMLLabelElement ());
            creators.Add(Tags.Applet, () => new HTMLAppletElement ());
            creators.Add(Tags.Object, () => new HTMLObjectElement ());
            creators.Add(Tags.Optgroup, () => new HTMLOptGroupElement ());
            creators.Add(Tags.Option, () => new HTMLOptionElement ());
            creators.Add(Tags.Style, () => new HTMLStyleElement ());
            creators.Add(Tags.Script, () => new HTMLScriptElement ());
            creators.Add(Tags.Iframe, () => new HTMLIFrameElement ());
            creators.Add(Tags.Title, () => new HTMLTitleElement ());
            creators.Add(Tags.Li, () => new HTMLLIElement(Tags.Li));
            creators.Add(Tags.Dd, () => new HTMLLIElement(Tags.Dd));
            creators.Add(Tags.Dt, () => new HTMLLIElement(Tags.Dt));
            creators.Add(Tags.Frameset, () => new HTMLFrameSetElement ());
            creators.Add(Tags.Frame, () => new HTMLFrameElement ());
            creators.Add(Tags.H1, () => new HTMLHeadingElement(Tags.H1));
            creators.Add(Tags.H2, () => new HTMLHeadingElement(Tags.H2));
            creators.Add(Tags.H3, () => new HTMLHeadingElement(Tags.H3));
            creators.Add(Tags.H4, () => new HTMLHeadingElement(Tags.H4));
            creators.Add(Tags.H5, () => new HTMLHeadingElement(Tags.H5));
            creators.Add(Tags.H6, () => new HTMLHeadingElement(Tags.H6));
            creators.Add(Tags.Audio, () => new HTMLAudioElement ());
            creators.Add(Tags.Video, () => new HTMLVideoElement ());
            creators.Add(Tags.Details, () => new HTMLDetailsElement ());
            creators.Add(Tags.Span, () => new HTMLSpanElement ());
            creators.Add(Tags.Dialog, () => new HTMLDialogElement ());
            creators.Add(Tags.Source, () => new HTMLSourceElement ());
            creators.Add(Tags.Track, () => new HTMLTrackElement ());
            creators.Add(Tags.Wbr, () => new HTMLWbrElement ());
            creators.Add(Tags.B, () => new HTMLBoldElement ());
            creators.Add(Tags.Big, () => new HTMLBigElement ());
            creators.Add(Tags.Strike, () => new HTMLStrikeElement ());
            creators.Add(Tags.Code, () => new HTMLCodeElement ());
            creators.Add(Tags.Em, () => new HTMLEmphasizeElement ());
            creators.Add(Tags.I, () => new HTMLItalicElement ());
            creators.Add(Tags.S, () => new HTMLStruckElement ());
            creators.Add(Tags.Small, () => new HTMLSmallElement ());
            creators.Add(Tags.Strong, () => new HTMLStrongElement ());
            creators.Add(Tags.U, () => new HTMLUnderlineElement ());
            creators.Add(Tags.Tt, () => new HTMLTeletypeTextElement ());
            creators.Add(Tags.NoBr, () => new HTMLNoNewlineElement ());
            creators.Add(Tags.Address, () => new HTMLAddressElement ());
            creators.Add(Tags.Main, () => new HTMLSemanticElement(Tags.Main));
            creators.Add(Tags.Summary, () => new HTMLSemanticElement(Tags.Summary));
            creators.Add(Tags.Xmp, () => new HTMLSemanticElement(Tags.Xmp));
            creators.Add(Tags.Center, () => new HTMLSemanticElement(Tags.Center));
            creators.Add(Tags.Listing, () => new HTMLSemanticElement(Tags.Listing));
            creators.Add(Tags.Nav, () => new HTMLSemanticElement(Tags.Nav));
            creators.Add(Tags.Article, () => new HTMLSemanticElement(Tags.Article));
            creators.Add(Tags.Aside, () => new HTMLSemanticElement(Tags.Aside));
            creators.Add(Tags.Figcaption, () => new HTMLSemanticElement(Tags.Figcaption));
            creators.Add(Tags.Figure, () => new HTMLSemanticElement(Tags.Figure));
            creators.Add(Tags.Section, () => new HTMLSemanticElement(Tags.Section));
            creators.Add(Tags.Footer, () => new HTMLSemanticElement(Tags.Footer));
            creators.Add(Tags.Header, () => new HTMLSemanticElement(Tags.Header));
            creators.Add(Tags.Hgroup, () => new HTMLSemanticElement(Tags.Hgroup));
            creators.Add(Tags.Plaintext, () => new HTMLSemanticElement(Tags.Plaintext));
            creators.Add(Tags.Bgsound, () => new HTMLBgsoundElement ());
            creators.Add(Tags.Marquee, () => new HTMLMarqueeElement ());
            creators.Add(Tags.NoEmbed, () => new HTMLNoEmbedElement());
            creators.Add(Tags.NoFrames, () => new HTMLNoFramesElement());
            creators.Add(Tags.NoScript, () => new HTMLNoScriptElement());
            creators.Add(Tags.MenuItem, () => new HTMLMenuItemElement ());
            creators.Add(Tags.Cite, () => new HTMLElement(Tags.Cite));
            creators.Add(Tags.Ruby, () => new HTMLRubyElement ());
            creators.Add(Tags.Rt, () => new HTMLRTElement ());
            creators.Add(Tags.Rp, () => new HTMLRPElement ());
            creators.Add(Tags.Time, () => new HTMLTimeElement());
            creators.Add(Tags.Progress, () => new HTMLProgressElement());
            creators.Add(Tags.Output, () => new HTMLOutputElement());
            creators.Add(Tags.Map, () => new HTMLMapElement());
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
