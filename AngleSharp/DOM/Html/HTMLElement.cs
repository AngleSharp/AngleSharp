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

        #region Internals

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
        internal static HTMLElement Factory(string tag)
        {
            switch (tag)
            {
                case HTMLBaseElement.Tag:
                    return new HTMLBaseElement();

                case HTMLBaseFontElement.Tag:
                    return new HTMLBaseFontElement();

                case HTMLLinkElement.Tag:
                    return new HTMLLinkElement();

                case HTMLAnchorElement.Tag:
                    return new HTMLAnchorElement();

                case HTMLButtonElement.Tag:
                    return new HTMLButtonElement();

                case HTMLInputElement.Tag:
                    return new HTMLInputElement();

                case HTMLHtmlElement.Tag:
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

                case HTMLBodyElement.Tag:
                    return new HTMLBodyElement();

                case HTMLFieldSetElement.Tag:
                    return new HTMLFieldSetElement();

                case HTMLHeadElement.Tag:
                    return new HTMLHeadElement();

                case HTMLMenuElement.Tag:
                    return new HTMLMenuElement();

                case HTMLMetaElement.Tag:
                    return new HTMLMetaElement();

                case HTMLOListElement.Tag:
                    return new HTMLOListElement();

                case HTMLParagraphElement.Tag:
                    return new HTMLParagraphElement();

                case HTMLSelectElement.Tag:
                    return new HTMLSelectElement();

                case HTMLUListElement.Tag:
                    return new HTMLUListElement();

                case HTMLHRElement.Tag:
                    return new HTMLHRElement();

                case HTMLDirectoryElement.Tag:
                    return new HTMLDirectoryElement();
                    
                case HTMLFontElement.Tag:
                    return new HTMLFontElement();

                case HTMLFormElement.Tag:
                    return new HTMLFormElement();

                case HTMLParamElement.Tag:
                    return new HTMLParamElement();

                case HTMLPreElement.Tag:
                    return new HTMLPreElement();

                case HTMLTextAreaElement.Tag:
                    return new HTMLTextAreaElement();

                case HTMLQuoteElement.BlockTag:
                case HTMLQuoteElement.NormalTag:
                case HTMLQuoteElement.ShortTag:
                    return new HTMLQuoteElement { _name = tag };

                case HTMLCanvasElement.Tag:
                    return new HTMLCanvasElement();

                case HTMLTableCaptionElement.Tag:
                    return new HTMLTableCaptionElement();

                case HTMLTableCellElement.HeadTag:
                case HTMLTableCellElement.NormalTag:
                    return new HTMLTableCellElement { _name = tag };

                case HTMLTableRowElement.Tag:
                    return new HTMLTableRowElement();

                case HTMLTableSectionElement.BodyTag:
                case HTMLTableSectionElement.FootTag:
                case HTMLTableSectionElement.HeadTag:
                    return new HTMLTableSectionElement { _name = tag };

                case HTMLTableElement.Tag:
                    return new HTMLTableElement();

                case HTMLTableColElement.ColgroupTag:
                case HTMLTableColElement.ColTag:
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

                case HTMLOptGroupElement.Tag:
                    return new HTMLOptGroupElement();

                case HTMLOptionElement.Tag:
                    return new HTMLOptionElement();

                case HTMLStyleElement.Tag:
                    return new HTMLStyleElement();

                case HTMLScriptElement.Tag:
                    return new HTMLScriptElement();

                case HTMLIFrameElement.Tag:
                    return new HTMLIFrameElement();

                case HTMLTitleElement.Tag:
                    return new HTMLTitleElement();

                case HTMLLIElement.DefinitionTag:
                case HTMLLIElement.DescriptionTag:
                case HTMLLIElement.ItemTag:
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

                case HTMLSemanticElement.CiteTag:
                    return new HTMLSemanticElement(false) { _name = tag };

                case HTMLSemanticElement.MainTag:
                case HTMLSemanticElement.SummaryTag:
                case HTMLSemanticElement.XmpTag:
                case HTMLSemanticElement.CenterTag:
                case HTMLSemanticElement.ListingTag:
                case HTMLSemanticElement.NavTag:
                case HTMLSemanticElement.AddressTag:
                case HTMLSemanticElement.ArticleTag:
                case HTMLSemanticElement.AsideTag:
                case HTMLSemanticElement.FigcaptionTag:
                case HTMLSemanticElement.FigureTag:
                case HTMLSemanticElement.SectionTag:
                case HTMLSemanticElement.FooterTag:
                case HTMLSemanticElement.HeaderTag:
                case HTMLSemanticElement.HgroupTag:
                case HTMLSemanticElement.PlaintextTag:
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
    }
}
