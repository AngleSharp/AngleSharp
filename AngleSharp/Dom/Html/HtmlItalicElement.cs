namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The i HTML element.
    /// </summary>
    sealed class HtmlItalicElement : HtmlElement
    {
        public HtmlItalicElement(Document owner, String prefix = null)
            : base(owner, Tags.I, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
