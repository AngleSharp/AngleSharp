namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML head element.
    /// </summary>
    sealed class HtmlHeadElement : HtmlElement, IHtmlHeadElement
    {
        public HtmlHeadElement(Document owner, String prefix = null)
            : base(owner, TagNames.Head, prefix, NodeFlags.Special)
        {
        }
    }
}