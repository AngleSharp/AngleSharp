namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// The i HTML element.
    /// </summary>
    sealed class HtmlItalicElement : HtmlElement, IHtmlItalicElement
    {
        public HtmlItalicElement(Document owner, String prefix = null)
            : base(owner, TagNames.I, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
