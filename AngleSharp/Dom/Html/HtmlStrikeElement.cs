namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
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
