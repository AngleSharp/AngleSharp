namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the hr element.
    /// </summary>
    sealed class HtmlHrElement : HtmlElement, IHtmlHrElement
    {
        public HtmlHrElement(Document owner, String prefix = null)
            : base(owner, TagNames.Hr, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }
    }
}
