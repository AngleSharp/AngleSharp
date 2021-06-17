namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The i HTML element.
    /// </summary>
    sealed class HtmlItalicElement : HtmlElement, IHtmlItalicElement
    {
        public HtmlItalicElement(Document owner, String? prefix = null)
            : base(owner, TagNames.I, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
