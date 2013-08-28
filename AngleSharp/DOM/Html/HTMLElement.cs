using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents a standard HTML element in the node tree.
    /// </summary>
    [DOM("HTMLElement")]
    public class HTMLElement : Element
    {
        #region ctor

        /// <summary>
        /// Creates a standard HTML element.
        /// </summary>
        internal HTMLElement()
        {
            _ns = Namespaces.Html; 
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets the status if this node is in the HTML namespace.
        /// </summary>
        internal protected override Boolean IsInHtml
        {
            get { return true; }
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return false; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        [DOM("cloneNode")]
        public override Node CloneNode(Boolean deep = true)
        {
            var node = Create(_name);
            CopyProperties(this, node, deep);
            return node;
        }

        #endregion

        #region Factory

        /// <summary>
        /// Returns a specialized HTMLElement instance for the given tag name.
        /// </summary>
        /// <param name="tag">The given tag name.</param>
        /// <returns>The specialized HTMLElement instance.</returns>
        internal static HTMLElement Create(String tag)
        {
            switch (tag)
            {
                case Tags.BASE: return new HTMLBaseElement();

                case Tags.BASEFONT: return new HTMLBaseFontElement();

                case Tags.LINK: return new HTMLLinkElement();

                case Tags.A: return new HTMLAnchorElement();

                case Tags.BUTTON: return new HTMLButtonElement();

                case Tags.INPUT: return new HTMLInputElement();

                case Tags.HTML: return new HTMLHtmlElement();

                case Tags.ISINDEX: return new HTMLIsIndexElement();

                case Tags.BR: return new HTMLBRElement();

                case Tags.EMBED: return new HTMLEmbedElement();

                case Tags.DIV: return new HTMLDivElement();

                case Tags.AREA: return new HTMLAreaElement();

                case Tags.IMG: return new HTMLImageElement();

                case Tags.DL: return new HTMLDListElement();

                case Tags.BODY: return new HTMLBodyElement();

                case Tags.FIELDSET: return new HTMLFieldSetElement();

                case Tags.HEAD: return new HTMLHeadElement();

                case Tags.MENU: return new HTMLMenuElement();

                case Tags.META: return new HTMLMetaElement();

                case Tags.OL: return new HTMLOListElement();

                case Tags.P: return new HTMLParagraphElement();

                case Tags.SELECT: return new HTMLSelectElement();

                case Tags.UL: return new HTMLUListElement();

                case Tags.HR: return new HTMLHRElement();

                case Tags.DIR: return new HTMLDirectoryElement();

                case Tags.FONT: return new HTMLFontElement();

                case Tags.FORM: return new HTMLFormElement();

                case Tags.PARAM: return new HTMLParamElement();

                case Tags.PRE: return new HTMLPreElement();

                case Tags.TEXTAREA: return new HTMLTextAreaElement();

                case Tags.BLOCKQUOTE: return new HTMLQuoteElement { _name = Tags.BLOCKQUOTE };

                case Tags.QUOTE: return new HTMLQuoteElement();

                case Tags.Q: return new HTMLQuoteElement { _name = Tags.Q };

                case Tags.CANVAS: return new HTMLCanvasElement();

                case Tags.CAPTION: return new HTMLTableCaptionElement();

                case Tags.TH: return new HTMLTableCellElement { _name = Tags.TH };

                case Tags.TD: return new HTMLTableCellElement();

                case Tags.TR: return new HTMLTableRowElement();

                case Tags.TBODY: return new HTMLTableSectionElement();

                case Tags.TFOOT: return new HTMLTableSectionElement { _name = Tags.TFOOT };

                case Tags.THEAD: return new HTMLTableSectionElement { _name = Tags.THEAD };

                case Tags.TABLE: return new HTMLTableElement();

                case Tags.COLGROUP: return new HTMLTableColElement { _name = Tags.COLGROUP };

                case Tags.COL: return new HTMLTableColElement();

                case Tags.DEL: return new HTMLModElement { _name = Tags.DEL };

                case Tags.INS: return new HTMLModElement();

                case Tags.LEGEND: return new HTMLLegendElement();

                case Tags.LABEL: return new HTMLLabelElement();

                case Tags.APPLET: return new HTMLAppletElement();

                case Tags.OBJECT: return new HTMLObjectElement();

                case Tags.OPTGROUP: return new HTMLOptGroupElement();

                case Tags.OPTION: return new HTMLOptionElement();

                case Tags.STYLE: return new HTMLStyleElement();

                case Tags.SCRIPT: return new HTMLScriptElement();

                case Tags.IFRAME: return new HTMLIFrameElement();

                case Tags.TITLE: return new HTMLTitleElement();

                case Tags.LI: return new HTMLLIElement();

                case Tags.DD: return new HTMLLIElement { _name = Tags.DD };

                case Tags.DT: return new HTMLLIElement { _name = Tags.DT };

                case Tags.FRAMESET: return new HTMLFrameSetElement();

                case Tags.FRAME: return new HTMLFrameElement();

                case Tags.H1: return new HTMLHeadingElement();

                case Tags.H2: return new HTMLHeadingElement { _name = Tags.H2 };

                case Tags.H3: return new HTMLHeadingElement { _name = Tags.H3 };

                case Tags.H4: return new HTMLHeadingElement { _name = Tags.H4 };

                case Tags.H5: return new HTMLHeadingElement { _name = Tags.H5 };

                case Tags.H6: return new HTMLHeadingElement { _name = Tags.H6 };

                case Tags.AUDIO: return new HTMLAudioElement();

                case Tags.VIDEO: return new HTMLVideoElement();

                case Tags.DETAILS: return new HTMLDetailsElement();

                case Tags.SPAN: return new HTMLSpanElement();

                case Tags.DIALOG: return new HTMLDialogElement();

                case Tags.SOURCE: return new HTMLSourceElement();

                case Tags.TRACK: return new HTMLTrackElement();

                case Tags.WBR: return new HTMLWbrElement();

                case Tags.B: return new HTMLBoldElement();

                case Tags.BIG: return new HTMLBigElement();

                case Tags.STRIKE: return new HTMLStrikeElement();

                case Tags.CODE: return new HTMLCodeElement();

                case Tags.EM: return new HTMLEmphasizeElement();

                case Tags.I: return new HTMLItalicElement();

                case Tags.S: return new HTMLStruckElement();

                case Tags.SMALL: return new HTMLSmallElement();

                case Tags.STRONG: return new HTMLStrongElement();

                case Tags.U: return new HTMLUnderlineElement();

                case Tags.TT: return new HTMLTeletypeTextElement();

                case Tags.NOBR: return new HTMLNoNewlineElement();

                case Tags.ADDRESS: return new HTMLAddressElement();

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
                case Tags.PLAINTEXT:
                    return new HTMLSemanticElement { _name = tag };

                case Tags.BGSOUND: return new HTMLBgsoundElement();

                case Tags.MARQUEE: return new HTMLMarqueeElement();

                case Tags.NOEMBED: return new HTMLNoElement { _name = Tags.NOEMBED };

                case Tags.NOFRAMES: return new HTMLNoElement { _name = Tags.NOFRAMES };

                case Tags.NOSCRIPT: return new HTMLNoElement();

                case Tags.MENUITEM: return new HTMLMenuItemElement();

                case Tags.CITE: return new HTMLElement { _name = tag };

                default: return new HTMLUnknownElement { _name = tag };
            }
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Gets the assigned form if any (use only on selected elements).
        /// </summary>
        /// <returns>The parent form OR assigned form if any.</returns>
        protected HTMLFormElement GetAssignedForm()
        {
            var par = _parent;

            while (!(par is HTMLFormElement))
            {
                if (par == null)
                    break;

                par = par.ParentElement;
            }

            if (par == null && _owner == null)
                return null;
            else if (par == null)
            {
                var formid = GetAttribute("form");

                if (par == null && !String.IsNullOrEmpty(formid))
                    par = _owner.GetElementById(formid);
                else
                    return null;
            }

            return par as HTMLFormElement;
        }

        #endregion
    }
}
