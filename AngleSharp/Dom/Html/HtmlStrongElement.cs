namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The strong HTML element.
    /// </summary>
    sealed class HtmlStrongElement : HtmlElement
    {
        public HtmlStrongElement(Document owner, String prefix = null)
            : base(owner, Tags.Strong, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
