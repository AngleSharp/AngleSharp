namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The nobr HTML element.
    /// </summary>
    sealed class HtmlNoNewlineElement : HtmlElement
    {
        public HtmlNoNewlineElement(Document owner, String prefix = null)
            : base(owner, TagNames.NoBr, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
