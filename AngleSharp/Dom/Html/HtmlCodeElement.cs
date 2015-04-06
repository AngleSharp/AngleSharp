namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The code HTML element.
    /// </summary>
    sealed class HtmlCodeElement : HtmlElement
    {
        public HtmlCodeElement(Document owner, String prefix = null)
            : base(owner, Tags.Code, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
