namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// The strong HTML element.
    /// </summary>
    sealed class HtmlStrongElement : HtmlElement, IHtmlStrongElement
    {
        public HtmlStrongElement(Document owner, String prefix = null)
            : base(owner, TagNames.Strong, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
