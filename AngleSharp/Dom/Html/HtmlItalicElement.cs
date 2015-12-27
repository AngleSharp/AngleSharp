namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

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
