namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The small HTML element.
    /// </summary>
    sealed class HtmlSmallElement : HtmlElement
    {
        public HtmlSmallElement(Document owner, String prefix = null)
            : base(owner, Tags.Small, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
