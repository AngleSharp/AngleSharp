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
            var node = Factory(_name);
            CopyProperties(this, node, deep);
            return node;
        }

        /// <summary>
        /// Returns a specialized HTMLElement instance for the given tag name.
        /// </summary>
        /// <param name="tag">The given tag name.</param>
        /// <returns>The specialized HTMLElement instance.</returns>
        internal static HTMLElement Factory(String tag)
        {
            switch (tag)
            {
                case Tags.BASE:
                    return new HTMLBaseElement();

                case Tags.BASEFONT:
                    return new HTMLBaseFontElement();

                case Tags.LINK:
                    return new HTMLLinkElement();

                case Tags.A:
                    return new HTMLAnchorElement();

                case Tags.BUTTON:
                    return new HTMLButtonElement();

                case Tags.INPUT:
                    return new HTMLInputElement();

                case Tags.HTML:
                    return new HTMLHtmlElement();

                case HTMLIsIndexElement.Tag:
                    return new HTMLIsIndexElement();

                case HTMLBRElement.Tag:
                    return new HTMLBRElement();

                case Tags.EMBED:
                    return new HTMLEmbedElement();

                case Tags.DIV:
                    return new HTMLDivElement();

                case HTMLAreaElement.Tag:
                    return new HTMLAreaElement();

                case HTMLImageElement.Tag:
                    return new HTMLImageElement();

                case Tags.DL:
                    return new HTMLDListElement();

                case Tags.BODY:
                    return new HTMLBodyElement();

                case HTMLFieldSetElement.Tag:
                    return new HTMLFieldSetElement();

                case Tags.HEAD:
                    return new HTMLHeadElement();

                case Tags.MENU:
                    return new HTMLMenuElement();

                case Tags.META:
                    return new HTMLMetaElement();

                case Tags.OL:
                    return new HTMLOListElement();

                case Tags.P:
                    return new HTMLParagraphElement();

                case Tags.SELECT:
                    return new HTMLSelectElement();

                case Tags.UL:
                    return new HTMLUListElement();

                case HTMLHRElement.Tag:
                    return new HTMLHRElement();

                case HTMLDirectoryElement.Tag:
                    return new HTMLDirectoryElement();

                case Tags.FONT:
                    return new HTMLFontElement();

                case Tags.FORM:
                    return new HTMLFormElement();

                case HTMLParamElement.Tag:
                    return new HTMLParamElement();

                case Tags.PRE:
                    return new HTMLPreElement();

                case Tags.TEXTAREA:
                    return new HTMLTextAreaElement();

                case Tags.BLOCKQUOTE:
                case Tags.QUOTE:
                case Tags.Q:
                    return new HTMLQuoteElement { _name = tag };

                case HTMLCanvasElement.Tag:
                    return new HTMLCanvasElement();

                case Tags.CAPTION:
                    return new HTMLTableCaptionElement();

                case Tags.TH:
                case Tags.TD:
                    return new HTMLTableCellElement { _name = tag };

                case Tags.TR:
                    return new HTMLTableRowElement();

                case Tags.TBODY:
                case Tags.TFOOT:
                case Tags.THEAD:
                    return new HTMLTableSectionElement { _name = tag };

                case Tags.TABLE:
                    return new HTMLTableElement();

                case Tags.COLGROUP:
                case Tags.COL:
                    return new HTMLTableColElement { _name = tag };

                case Tags.DEL:
                case Tags.INS:
                    return new HTMLModElement { _name = tag };

                case HTMLLegendElement.Tag:
                    return new HTMLLegendElement();

                case HTMLLabelElement.Tag:
                    return new HTMLLabelElement();

                case Tags.APPLET:
                    return new HTMLAppletElement();

                case Tags.OBJECT:
                    return new HTMLObjectElement();

                case Tags.OPTGROUP:
                    return new HTMLOptGroupElement();

                case Tags.OPTION:
                    return new HTMLOptionElement();

                case Tags.STYLE:
                    return new HTMLStyleElement();

                case Tags.SCRIPT:
                    return new HTMLScriptElement();

                case Tags.IFRAME:
                    return new HTMLIFrameElement();

                case Tags.TITLE:
                    return new HTMLTitleElement();

                case Tags.LI:
                case Tags.DD:
                case Tags.DT:
                    return new HTMLLIElement { _name = tag };

                case Tags.FRAMESET:
                    return new HTMLFrameSetElement();

                case HTMLFrameElement.Tag:
                    return new HTMLFrameElement();

                case Tags.H1:
                case Tags.H2:
                case Tags.H3:
                case Tags.H4:
                case Tags.H5:
                case Tags.H6:
                    return new HTMLHeadingElement { _name = tag };

                case HTMLAudioElement.Tag:
                    return new HTMLAudioElement();

                case HTMLVideoElement.Tag:
                    return new HTMLVideoElement();

                case HTMLDetailsElement.Tag:
                    return new HTMLDetailsElement();

                case HTMLSpanElement.Tag:
                    return new HTMLSpanElement();

                case HTMLDialogElement.Tag:
                    return new HTMLDialogElement();

                case HTMLSourceElement.Tag:
                    return new HTMLSourceElement();

                case HTMLTrackElement.Tag:
                    return new HTMLTrackElement();

                case HTMLWbrElement.Tag:
                    return new HTMLWbrElement();

                case Tags.B:
                case Tags.BIG:
                case Tags.STRIKE:
                case Tags.CODE:
                case Tags.EM:
                case Tags.I:
                case Tags.S:
                case Tags.SMALL:
                case Tags.STRONG:
                case Tags.U:
                case Tags.TT:
                case Tags.NOBR:
                    return new HTMLFormattingElement { _name = tag };

                case Tags.CITE:
                    return new HTMLSemanticElement(false) { _name = tag };

                case Tags.MAIN:
                case Tags.SUMMARY:
                case Tags.XMP:
                case Tags.CENTER:
                case Tags.LISTING:
                case Tags.NAV:
                case Tags.ADDRESS:
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

                case Tags.BGSOUND:
                    return new HTMLBgsoundElement();

                case Tags.MARQUEE:
                    return new HTMLMarqueeElement();

                case Tags.NOEMBED:
                case Tags.NOFRAMES:
                case Tags.NOSCRIPT:
                    return new HTMLNoElement { _name = tag };

                case Tags.MENUITEM:
                    return new HTMLMenuItemElement();

                default:
                    return new HTMLUnknownElement { _name = tag };
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
