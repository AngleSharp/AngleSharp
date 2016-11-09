namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The strike HTML element.
    /// </summary>
    sealed class HtmlStrikeElement : HtmlElement
    {
        public HtmlStrikeElement(Document owner, String prefix = null)
            : base(owner, TagNames.Strike, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
