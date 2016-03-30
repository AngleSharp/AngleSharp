namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML br element.
    /// </summary>
    sealed class HtmlBreakRowElement : HtmlElement, IHtmlBreakRowElement
    {
        public HtmlBreakRowElement(Document owner, String prefix = null)
            : base(owner, TagNames.Br, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }
    }
}
