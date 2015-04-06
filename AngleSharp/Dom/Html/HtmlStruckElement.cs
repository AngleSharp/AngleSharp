namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The s HTML element.
    /// </summary>
    sealed class HtmlStruckElement : HtmlElement
    {
        public HtmlStruckElement(Document owner, String prefix = null)
            : base(owner, Tags.S, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
