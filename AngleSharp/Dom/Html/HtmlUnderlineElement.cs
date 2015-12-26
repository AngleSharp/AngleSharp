namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// The u HTML element.
    /// </summary>
    sealed class HtmlUnderlineElement : HtmlElement
    {
        public HtmlUnderlineElement(Document owner, String prefix = null)
            : base(owner, Tags.U, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
