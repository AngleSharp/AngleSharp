namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents a font element.
    /// See (19) obsolete features of [WHATWG].
    /// </summary>
    [DomHistorical]
    sealed class HtmlFontElement : HtmlElement
    {
        public HtmlFontElement(Document owner, String prefix = null)
            : base(owner, TagNames.Font, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
