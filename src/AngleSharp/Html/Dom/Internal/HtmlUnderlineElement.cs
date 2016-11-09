namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The u HTML element.
    /// </summary>
    sealed class HtmlUnderlineElement : HtmlElement
    {
        public HtmlUnderlineElement(Document owner, String prefix = null)
            : base(owner, TagNames.U, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
