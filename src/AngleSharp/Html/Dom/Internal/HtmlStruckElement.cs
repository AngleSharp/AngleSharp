namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The s HTML element.
    /// </summary>
    sealed class HtmlStruckElement : HtmlElement
    {
        public HtmlStruckElement(Document owner, String prefix = null)
            : base(owner, TagNames.S, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
