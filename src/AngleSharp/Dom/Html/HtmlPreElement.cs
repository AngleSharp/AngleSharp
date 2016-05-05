namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML pre element.
    /// </summary>
    sealed class HtmlPreElement : HtmlElement, IHtmlPreElement
    {
        public HtmlPreElement(Document owner, String prefix = null)
            : base(owner, TagNames.Pre, prefix, NodeFlags.Special | NodeFlags.LineTolerance)
        {
        }
    }
}
