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

                case HTMLEmbedElement.Tag:
                    return new HTMLEmbedElement();

                case HTMLDivElement.Tag:
                    return new HTMLDivElement();

                case HTMLAreaElement.Tag:
                    return new HTMLAreaElement();

                case HTMLImageElement.Tag:
                    return new HTMLImageElement();

                case HTMLDListElement.Tag:
                    return new HTMLDListElement();

                case Tags.BODY:
                    return new HTMLBodyElement();

                case HTMLFieldSetElement.Tag:
                    return new HTMLFieldSetElement();

                case Tags.HEAD:
                    return new HTMLHeadElement();

                case HTMLMenuElement.Tag:
                    return new HTMLMenuElement();

                case HTMLMetaElement.Tag:
                    return new HTMLMetaElement();

                case HTMLOListElement.Tag:
                    return new HTMLOListElement();

                case HTMLParagraphElement.Tag:
                    return new HTMLParagraphElement();

                case Tags.SELECT:
                    return new HTMLSelectElement();

                case HTMLUListElement.Tag:
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

                case HTMLTextAreaElement.Tag:
                    return new HTMLTextAreaElement();

                case HTMLQuoteElement.BlockTag:
                case HTMLQuoteElement.NormalTag:
                case HTMLQuoteElement.ShortTag:
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

                case HTMLModElement.DelTag:
                case HTMLModElement.InsTag:
                    return new HTMLModElement { _name = tag };

                case HTMLLegendElement.Tag:
                    return new HTMLLegendElement();

                case HTMLLabelElement.Tag:
                    return new HTMLLabelElement();

                case HTMLAppletElement.Tag:
                    return new HTMLAppletElement();

                case HTMLObjectElement.Tag:
                    return new HTMLObjectElement();

                case Tags.OPTGROUP:
                    return new HTMLOptGroupElement();

                case Tags.OPTION:
                    return new HTMLOptionElement();

                case HTMLStyleElement.Tag:
                    return new HTMLStyleElement();

                case HTMLScriptElement.Tag:
                    return new HTMLScriptElement();

                case HTMLIFrameElement.Tag:
                    return new HTMLIFrameElement();

                case HTMLTitleElement.Tag:
                    return new HTMLTitleElement();

                case Tags.LI:
                case Tags.DD:
                case Tags.DT:
                    return new HTMLLIElement { _name = tag };

                case HTMLFrameSetElement.Tag:
                    return new HTMLFrameSetElement();

                case HTMLFrameElement.Tag:
                    return new HTMLFrameElement();

                case HTMLHeadingElement.ChapterTag:
                case HTMLHeadingElement.SubSubSubSubSectionTag:
                case HTMLHeadingElement.SubSubSubSectionTag:
                case HTMLHeadingElement.SubSubSectionTag:
                case HTMLHeadingElement.SubSectionTag:
                case HTMLHeadingElement.SectionTag:
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

                case HTMLFormattingElement.BTag:
                case HTMLFormattingElement.BigTag:
                case HTMLFormattingElement.StrikeTag:
                case HTMLFormattingElement.CodeTag:
                case HTMLFormattingElement.EmTag:
                case HTMLFormattingElement.ITag:
                case HTMLFormattingElement.STag:
                case HTMLFormattingElement.SmallTag:
                case HTMLFormattingElement.StrongTag:
                case HTMLFormattingElement.UTag:
                case HTMLFormattingElement.TtTag:
                case HTMLFormattingElement.NobrTag:
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

                case HTMLBgsoundElement.Tag:
                    return new HTMLBgsoundElement();

                case HTMLMarqueeElement.Tag:
                    return new HTMLMarqueeElement();

                case HTMLNoElement.NoEmbedTag:
                case HTMLNoElement.NoFramesTag:
                case HTMLNoElement.NoScriptTag:
                    return new HTMLNoElement { _name = tag };

                case HTMLMenuItemElement.Tag:
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
