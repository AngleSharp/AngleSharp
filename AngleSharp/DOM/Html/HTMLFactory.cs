namespace AngleSharp.DOM.Html
{
    using System;

    internal class HTMLFactory
    {
        /// <summary>
        /// Returns a specialized HTMLElement instance for the given tag name.
        /// </summary>
        /// <param name="tag">The given tag name.</param>
        /// <param name="document">The document that owns the element.</param>
        /// <returns>The specialized HTMLElement instance.</returns>
        public static HTMLElement Create(String tag, Document document)
        {
            switch (tag)
            {
                case Tags.BASE:         return new HTMLBaseElement { OwnerDocument = document };
                case Tags.BASEFONT:     return new HTMLBaseFontElement { OwnerDocument = document };
                case Tags.LINK:         return new HTMLLinkElement { OwnerDocument = document };
                case Tags.A:            return new HTMLAnchorElement { OwnerDocument = document };
                case Tags.BUTTON:       return new HTMLButtonElement { OwnerDocument = document };
                case Tags.INPUT:        return new HTMLInputElement { OwnerDocument = document };
                case Tags.HTML:         return new HTMLHtmlElement { OwnerDocument = document };
                case Tags.ISINDEX:      return new HTMLIsIndexElement { OwnerDocument = document };
                case Tags.BR:           return new HTMLBRElement { OwnerDocument = document };
                case Tags.EMBED:        return new HTMLEmbedElement { OwnerDocument = document };
                case Tags.DIV:          return new HTMLDivElement { OwnerDocument = document };
                case Tags.AREA:         return new HTMLAreaElement { OwnerDocument = document };
                case Tags.IMG:          return new HTMLImageElement { OwnerDocument = document };
                case Tags.DL:           return new HTMLDListElement { OwnerDocument = document };
                case Tags.BODY:         return new HTMLBodyElement { OwnerDocument = document };
                case Tags.FIELDSET:     return new HTMLFieldSetElement { OwnerDocument = document };
                case Tags.HEAD:         return new HTMLHeadElement { OwnerDocument = document };
                case Tags.MENU:         return new HTMLMenuElement { OwnerDocument = document };
                case Tags.META:         return new HTMLMetaElement { OwnerDocument = document };
                case Tags.OL:           return new HTMLOListElement { OwnerDocument = document };
                case Tags.P:            return new HTMLParagraphElement { OwnerDocument = document };
                case Tags.SELECT:       return new HTMLSelectElement { OwnerDocument = document };
                case Tags.UL:           return new HTMLUListElement { OwnerDocument = document };
                case Tags.HR:           return new HTMLHRElement { OwnerDocument = document };
                case Tags.DIR:          return new HTMLDirectoryElement { OwnerDocument = document };
                case Tags.FONT:         return new HTMLFontElement { OwnerDocument = document };
                case Tags.FORM:         return new HTMLFormElement { OwnerDocument = document };
                case Tags.PARAM:        return new HTMLParamElement { OwnerDocument = document };
                case Tags.PRE:          return new HTMLPreElement { OwnerDocument = document };
                case Tags.TEXTAREA:     return new HTMLTextAreaElement { OwnerDocument = document };
                case Tags.BLOCKQUOTE:   return new HTMLQuoteElement { NodeName = Tags.BLOCKQUOTE, OwnerDocument = document};
                case Tags.QUOTE:        return new HTMLQuoteElement { NodeName = Tags.QUOTE, OwnerDocument = document };
                case Tags.Q:            return new HTMLQuoteElement { NodeName = Tags.Q, OwnerDocument = document };
                case Tags.CANVAS:       return new HTMLCanvasElement { OwnerDocument = document };
                case Tags.CAPTION:      return new HTMLTableCaptionElement { OwnerDocument = document };
                case Tags.TH:           return new HTMLTableCellElement { NodeName = Tags.TH, OwnerDocument = document };
                case Tags.TD:           return new HTMLTableCellElement { OwnerDocument = document };
                case Tags.TR:           return new HTMLTableRowElement { OwnerDocument = document };
                case Tags.TBODY:        return new HTMLTableSectionElement { OwnerDocument = document };
                case Tags.TFOOT:        return new HTMLTableSectionElement { NodeName = Tags.TFOOT, OwnerDocument = document };
                case Tags.THEAD:        return new HTMLTableSectionElement { NodeName = Tags.THEAD, OwnerDocument = document };
                case Tags.TABLE:        return new HTMLTableElement { OwnerDocument = document };
                case Tags.COLGROUP:     return new HTMLTableColElement { NodeName = Tags.COLGROUP, OwnerDocument = document };
                case Tags.COL:          return new HTMLTableColElement { OwnerDocument = document };
                case Tags.DEL:          return new HTMLModElement { NodeName = Tags.DEL, OwnerDocument = document };
                case Tags.INS:          return new HTMLModElement { OwnerDocument = document };
                case Tags.LEGEND:       return new HTMLLegendElement { OwnerDocument = document };
                case Tags.LABEL:        return new HTMLLabelElement { OwnerDocument = document };
                case Tags.APPLET:       return new HTMLAppletElement { OwnerDocument = document };
                case Tags.OBJECT:       return new HTMLObjectElement { OwnerDocument = document };
                case Tags.OPTGROUP:     return new HTMLOptGroupElement { OwnerDocument = document };
                case Tags.OPTION:       return new HTMLOptionElement { OwnerDocument = document };
                case Tags.STYLE:        return new HTMLStyleElement { OwnerDocument = document };
                case Tags.SCRIPT:       return new HTMLScriptElement { OwnerDocument = document };
                case Tags.IFRAME:       return new HTMLIFrameElement { OwnerDocument = document };
                case Tags.TITLE:        return new HTMLTitleElement { OwnerDocument = document };
                case Tags.LI:           return new HTMLLIElement { NodeName = Tags.LI, OwnerDocument = document };
                case Tags.DD:           return new HTMLLIElement { NodeName = Tags.DD, OwnerDocument = document };
                case Tags.DT:           return new HTMLLIElement { NodeName = Tags.DT, OwnerDocument = document };
                case Tags.FRAMESET:     return new HTMLFrameSetElement { OwnerDocument = document };
                case Tags.FRAME:        return new HTMLFrameElement { OwnerDocument = document };
                case Tags.H1:           return new HTMLHeadingElement { NodeName = Tags.H1, OwnerDocument = document };
                case Tags.H2:           return new HTMLHeadingElement { NodeName = Tags.H2, OwnerDocument = document };
                case Tags.H3:           return new HTMLHeadingElement { NodeName = Tags.H3, OwnerDocument = document };
                case Tags.H4:           return new HTMLHeadingElement { NodeName = Tags.H4, OwnerDocument = document };
                case Tags.H5:           return new HTMLHeadingElement { NodeName = Tags.H5, OwnerDocument = document };
                case Tags.H6:           return new HTMLHeadingElement { NodeName = Tags.H6, OwnerDocument = document };
                case Tags.AUDIO:        return new HTMLAudioElement { OwnerDocument = document };
                case Tags.VIDEO:        return new HTMLVideoElement { OwnerDocument = document };
                case Tags.DETAILS:      return new HTMLDetailsElement { OwnerDocument = document };
                case Tags.SPAN:         return new HTMLSpanElement { OwnerDocument = document };
                case Tags.DIALOG:       return new HTMLDialogElement { OwnerDocument = document };
                case Tags.SOURCE:       return new HTMLSourceElement { OwnerDocument = document };
                case Tags.TRACK:        return new HTMLTrackElement { OwnerDocument = document };
                case Tags.WBR:          return new HTMLWbrElement { OwnerDocument = document };
                case Tags.B:            return new HTMLBoldElement { OwnerDocument = document };
                case Tags.BIG:          return new HTMLBigElement { OwnerDocument = document };
                case Tags.STRIKE:       return new HTMLStrikeElement { OwnerDocument = document };
                case Tags.CODE:         return new HTMLCodeElement { OwnerDocument = document };
                case Tags.EM:           return new HTMLEmphasizeElement { OwnerDocument = document };
                case Tags.I:            return new HTMLItalicElement { OwnerDocument = document };
                case Tags.S:            return new HTMLStruckElement { OwnerDocument = document };
                case Tags.SMALL:        return new HTMLSmallElement { OwnerDocument = document };
                case Tags.STRONG:       return new HTMLStrongElement { OwnerDocument = document };
                case Tags.U:            return new HTMLUnderlineElement { OwnerDocument = document };
                case Tags.TT:           return new HTMLTeletypeTextElement { OwnerDocument = document };
                case Tags.NOBR:         return new HTMLNoNewlineElement { OwnerDocument = document };
                case Tags.ADDRESS:      return new HTMLAddressElement { OwnerDocument = document };
                case Tags.MAIN:
                case Tags.SUMMARY:
                case Tags.XMP:
                case Tags.CENTER:
                case Tags.LISTING:
                case Tags.NAV:
                case Tags.ARTICLE:
                case Tags.ASIDE:
                case Tags.FIGCAPTION:
                case Tags.FIGURE:
                case Tags.SECTION:
                case Tags.FOOTER:
                case Tags.HEADER:
                case Tags.HGROUP:
                case Tags.PLAINTEXT:    return new HTMLSemanticElement { NodeName = tag, OwnerDocument = document };
                case Tags.BGSOUND:      return new HTMLBgsoundElement { OwnerDocument = document };
                case Tags.MARQUEE:      return new HTMLMarqueeElement { OwnerDocument = document };
                case Tags.NOEMBED:      return new HTMLNoElement { NodeName = Tags.NOEMBED, OwnerDocument = document };
                case Tags.NOFRAMES:     return new HTMLNoElement { NodeName = Tags.NOFRAMES, OwnerDocument = document };
                case Tags.NOSCRIPT:     return new HTMLNoElement { NodeName = Tags.NOSCRIPT, OwnerDocument = document };
                case Tags.MENUITEM:     return new HTMLMenuItemElement { OwnerDocument = document };
                case Tags.CITE:         return new HTMLElement { NodeName = Tags.CITE, OwnerDocument = document };
                case Tags.RUBY:         return new HTMLRubyElement { OwnerDocument = document };
                case Tags.RT:           return new HTMLRTElement { OwnerDocument = document };
                case Tags.RP:           return new HTMLRPElement { OwnerDocument = document };
                default:                return new HTMLUnknownElement { NodeName = tag, OwnerDocument = document };
            }
        }
    }
}
