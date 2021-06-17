namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The code HTML element.
    /// </summary>
    sealed class HtmlCodeElement : HtmlElement, IHtmlCodeElement
    {
        public HtmlCodeElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Code, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
