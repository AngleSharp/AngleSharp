namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

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
