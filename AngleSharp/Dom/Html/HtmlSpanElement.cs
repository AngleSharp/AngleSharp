namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML span element.
    /// </summary>
    sealed class HtmlSpanElement : HtmlElement, IHtmlSpanElement
    {
        public HtmlSpanElement(Document owner, String prefix = null)
            : base(owner, TagNames.Span, prefix)
        {
        }
    }
}
