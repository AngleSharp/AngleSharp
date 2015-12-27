namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// The code HTML element.
    /// </summary>
    sealed class HtmlCodeElement : HtmlElement
    {
        public HtmlCodeElement(Document owner, String prefix = null)
            : base(owner, TagNames.Code, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
