namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The nobr HTML element.
    /// </summary>
    sealed class HtmlNoNewlineElement : HtmlElement
    {
        public HtmlNoNewlineElement(Document owner, String prefix = null)
            : base(owner, Tags.NoBr, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
