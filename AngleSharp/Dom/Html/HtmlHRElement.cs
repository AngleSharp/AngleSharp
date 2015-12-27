namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the hr element.
    /// </summary>
    sealed class HtmlHrElement : HtmlElement, IHtmlHrElement
    {
        /// <summary>
        /// Creates a new hr element.
        /// </summary>
        public HtmlHrElement(Document owner, String prefix = null)
            : base(owner, Tags.Hr, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }
    }
}
