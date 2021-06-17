namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The bold HTML element.
    /// </summary>
    sealed class HtmlBoldElement : HtmlElement, IHtmlBoldElement
    {
        public HtmlBoldElement(Document owner, String? prefix = null)
            : base(owner, TagNames.B, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
