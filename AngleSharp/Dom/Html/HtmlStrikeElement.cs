namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The strike HTML element.
    /// </summary>
    sealed class HtmlStrikeElement : HtmlElement
    {
        public HtmlStrikeElement(Document owner, String prefix = null)
            : base(owner, Tags.Strike, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
