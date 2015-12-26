namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

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
