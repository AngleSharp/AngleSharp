namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The u HTML element.
    /// </summary>
    sealed class HtmlUnderlineElement : HtmlElement
    {
        public HtmlUnderlineElement(Document owner, String prefix)
            : base(owner, Tags.U, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
