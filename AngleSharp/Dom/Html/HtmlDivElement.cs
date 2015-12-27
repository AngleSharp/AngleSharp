namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML div element.
    /// </summary>
    sealed class HtmlDivElement : HtmlElement, IHtmlDivElement
    {
        /// <summary>
        /// Creates a new HTML div element.
        /// </summary>
        public HtmlDivElement(Document owner, String prefix = null)
            : base(owner, Tags.Div, prefix, NodeFlags.Special)
        {
        }
    }
}
