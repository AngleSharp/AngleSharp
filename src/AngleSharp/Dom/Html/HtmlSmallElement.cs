namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// The small HTML element.
    /// </summary>
    sealed class HtmlSmallElement : HtmlElement, IHtmlSmallElement
    {
        public HtmlSmallElement(Document owner, String prefix = null)
            : base(owner, TagNames.Small, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
