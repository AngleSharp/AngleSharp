namespace AngleSharp.DOM.Html
{
    using System;
    using System.Collections.Generic;

    internal class HTMLFactory
    {
        static readonly Dictionary<String, Func<HTMLElement>> elements = new Dictionary<String, Func<HTMLElement>>(StringComparer.OrdinalIgnoreCase);

        static HTMLFactory()
        {
            elements.Add(Tags.Base, () => new HTMLBaseElement());
            elements.Add(Tags.BaseFont, () => new HTMLBaseFontElement());
            elements.Add(Tags.Link, () => new HTMLLinkElement());
            elements.Add(Tags.A, () => new HTMLAnchorElement());
            elements.Add(Tags.Button, () => new HTMLButtonElement());
            elements.Add(Tags.Input, () => new HTMLInputElement());
            elements.Add(Tags.Html, () => new HTMLHtmlElement());
            elements.Add(Tags.IsIndex, () => new HTMLIsIndexElement());
            elements.Add(Tags.Br, () => new HTMLBRElement());
            elements.Add(Tags.Embed, () => new HTMLEmbedElement());
            elements.Add(Tags.Div, () => new HTMLDivElement());
            elements.Add(Tags.Area, () => new HTMLAreaElement());
            elements.Add(Tags.Img, () => new HTMLImageElement());
            elements.Add(Tags.Dl, () => new HTMLDListElement());
            elements.Add(Tags.Body, () => new HTMLBodyElement());
            elements.Add(Tags.Fieldset, () => new HTMLFieldSetElement());
            elements.Add(Tags.Head, () => new HTMLHeadElement());
            elements.Add(Tags.Menu, () => new HTMLMenuElement());
            elements.Add(Tags.Meta, () => new HTMLMetaElement());
            elements.Add(Tags.Ol, () => new HTMLOListElement());
            elements.Add(Tags.P, () => new HTMLParagraphElement());
            elements.Add(Tags.Select, () => new HTMLSelectElement());
            elements.Add(Tags.Ul, () => new HTMLUListElement());
            elements.Add(Tags.Hr, () => new HTMLHRElement());
            elements.Add(Tags.Dir, () => new HTMLDirectoryElement());
            elements.Add(Tags.Font, () => new HTMLFontElement ());
            elements.Add(Tags.Form, () => new HTMLFormElement ());
            elements.Add(Tags.Param, () => new HTMLParamElement ());
            elements.Add(Tags.Pre, () => new HTMLPreElement ());
            elements.Add(Tags.Textarea, () => new HTMLTextAreaElement ());
            elements.Add(Tags.BlockQuote, () => new HTMLQuoteElement { NodeName = Tags.BlockQuote });
            elements.Add(Tags.Quote, () => new HTMLQuoteElement { NodeName = Tags.Quote });
            elements.Add(Tags.Q, () => new HTMLQuoteElement { NodeName = Tags.Q });
            elements.Add(Tags.Canvas, () => new HTMLCanvasElement ());
            elements.Add(Tags.Caption, () => new HTMLTableCaptionElement ());
            elements.Add(Tags.Th, () => new HTMLTableCellElement { NodeName = Tags.Th });
            elements.Add(Tags.Td, () => new HTMLTableCellElement ());
            elements.Add(Tags.Tr, () => new HTMLTableRowElement ());
            elements.Add(Tags.Tbody, () => new HTMLTableSectionElement ());
            elements.Add(Tags.Tfoot, () => new HTMLTableSectionElement { NodeName = Tags.Tfoot });
            elements.Add(Tags.Thead, () => new HTMLTableSectionElement { NodeName = Tags.Thead });
            elements.Add(Tags.Table, () => new HTMLTableElement ());
            elements.Add(Tags.Colgroup, () => new HTMLTableColElement { NodeName = Tags.Colgroup });
            elements.Add(Tags.Col, () => new HTMLTableColElement ());
            elements.Add(Tags.Del, () => new HTMLModElement { NodeName = Tags.Del });
            elements.Add(Tags.Ins, () => new HTMLModElement ());
            elements.Add(Tags.Legend, () => new HTMLLegendElement ());
            elements.Add(Tags.Label, () => new HTMLLabelElement ());
            elements.Add(Tags.Applet, () => new HTMLAppletElement ());
            elements.Add(Tags.Object, () => new HTMLObjectElement ());
            elements.Add(Tags.Optgroup, () => new HTMLOptGroupElement ());
            elements.Add(Tags.Option, () => new HTMLOptionElement ());
            elements.Add(Tags.Style, () => new HTMLStyleElement ());
            elements.Add(Tags.Script, () => new HTMLScriptElement ());
            elements.Add(Tags.Iframe, () => new HTMLIFrameElement ());
            elements.Add(Tags.Title, () => new HTMLTitleElement ());
            elements.Add(Tags.Li, () => new HTMLLIElement { NodeName = Tags.Li });
            elements.Add(Tags.Dd, () => new HTMLLIElement { NodeName = Tags.Dd });
            elements.Add(Tags.Dt, () => new HTMLLIElement { NodeName = Tags.Dt });
            elements.Add(Tags.Frameset, () => new HTMLFrameSetElement ());
            elements.Add(Tags.Frame, () => new HTMLFrameElement ());
            elements.Add(Tags.H1, () => new HTMLHeadingElement { NodeName = Tags.H1 });
            elements.Add(Tags.H2, () => new HTMLHeadingElement { NodeName = Tags.H2 });
            elements.Add(Tags.H3, () => new HTMLHeadingElement { NodeName = Tags.H3 });
            elements.Add(Tags.H4, () => new HTMLHeadingElement { NodeName = Tags.H4 });
            elements.Add(Tags.H5, () => new HTMLHeadingElement { NodeName = Tags.H5 });
            elements.Add(Tags.H6, () => new HTMLHeadingElement { NodeName = Tags.H6 });
            elements.Add(Tags.Audio, () => new HTMLAudioElement ());
            elements.Add(Tags.Video, () => new HTMLVideoElement ());
            elements.Add(Tags.Details, () => new HTMLDetailsElement ());
            elements.Add(Tags.Span, () => new HTMLSpanElement ());
            elements.Add(Tags.Dialog, () => new HTMLDialogElement ());
            elements.Add(Tags.Source, () => new HTMLSourceElement ());
            elements.Add(Tags.Track, () => new HTMLTrackElement ());
            elements.Add(Tags.Wbr, () => new HTMLWbrElement ());
            elements.Add(Tags.B, () => new HTMLBoldElement ());
            elements.Add(Tags.Big, () => new HTMLBigElement ());
            elements.Add(Tags.Strike, () => new HTMLStrikeElement ());
            elements.Add(Tags.Code, () => new HTMLCodeElement ());
            elements.Add(Tags.Em, () => new HTMLEmphasizeElement ());
            elements.Add(Tags.I, () => new HTMLItalicElement ());
            elements.Add(Tags.S, () => new HTMLStruckElement ());
            elements.Add(Tags.Small, () => new HTMLSmallElement ());
            elements.Add(Tags.Strong, () => new HTMLStrongElement ());
            elements.Add(Tags.U, () => new HTMLUnderlineElement ());
            elements.Add(Tags.Tt, () => new HTMLTeletypeTextElement ());
            elements.Add(Tags.NoBr, () => new HTMLNoNewlineElement ());
            elements.Add(Tags.Address, () => new HTMLAddressElement ());
            elements.Add(Tags.Main, () => new HTMLSemanticElement { NodeName = Tags.Main });
            elements.Add(Tags.Summary, () => new HTMLSemanticElement { NodeName = Tags.Summary });
            elements.Add(Tags.Xmp, () => new HTMLSemanticElement { NodeName = Tags.Xmp });
            elements.Add(Tags.Center, () => new HTMLSemanticElement { NodeName = Tags.Center });
            elements.Add(Tags.Listing, () => new HTMLSemanticElement { NodeName = Tags.Listing });
            elements.Add(Tags.Nav, () => new HTMLSemanticElement { NodeName = Tags.Nav });
            elements.Add(Tags.Article, () => new HTMLSemanticElement { NodeName = Tags.Article });
            elements.Add(Tags.Aside, () => new HTMLSemanticElement { NodeName = Tags.Aside });
            elements.Add(Tags.Figcaption, () => new HTMLSemanticElement { NodeName = Tags.Figcaption });
            elements.Add(Tags.Figure, () => new HTMLSemanticElement { NodeName = Tags.Figure });
            elements.Add(Tags.Section, () => new HTMLSemanticElement { NodeName = Tags.Section });
            elements.Add(Tags.Footer, () => new HTMLSemanticElement { NodeName = Tags.Footer });
            elements.Add(Tags.Header, () => new HTMLSemanticElement { NodeName = Tags.Header });
            elements.Add(Tags.Hgroup, () => new HTMLSemanticElement { NodeName = Tags.Hgroup });
            elements.Add(Tags.Plaintext, () => new HTMLSemanticElement { NodeName = Tags.Plaintext });
            elements.Add(Tags.Bgsound, () => new HTMLBgsoundElement ());
            elements.Add(Tags.Marquee, () => new HTMLMarqueeElement ());
            elements.Add(Tags.NoEmbed, () => new HTMLNoElement { NodeName = Tags.NoEmbed });
            elements.Add(Tags.NoFrames, () => new HTMLNoElement { NodeName = Tags.NoFrames });
            elements.Add(Tags.NoScript, () => new HTMLNoElement { NodeName = Tags.NoScript });
            elements.Add(Tags.MenuItem, () => new HTMLMenuItemElement ());
            elements.Add(Tags.Cite, () => new HTMLElement { NodeName = Tags.Cite });
            elements.Add(Tags.Ruby, () => new HTMLRubyElement ());
            elements.Add(Tags.Rt, () => new HTMLRTElement ());
            elements.Add(Tags.Rp, () => new HTMLRPElement ());
            elements.Add(Tags.Time, () => new HTMLTimeElement());
        }

        /// <summary>
        /// Returns a specialized HTMLElement instance for the given tag name.
        /// </summary>
        /// <param name="tag">The given tag name.</param>
        /// <param name="document">The document that owns the element.</param>
        /// <returns>The specialized HTMLElement instance.</returns>
        public static HTMLElement Create(String tag, Document document)
        {
            Func<HTMLElement> elementCreator;

            if (elements.TryGetValue(tag, out elementCreator))
            {
                var element = elementCreator();
                element.Owner = document;
                return element;
            }

            return new HTMLUnknownElement { NodeName = tag, Owner = document };
        }
    }
}
