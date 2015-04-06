namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Attributes;
    using AngleSharp.Html;

    /// <summary>
    /// Represents a font element.
    /// See (19) obsolete features of [WHATWG].
    /// </summary>
    [DomHistorical]
    sealed class HtmlFontElement : HtmlElement
    {
        public HtmlFontElement(Document owner, String prefix = null)
            : base(owner, Tags.Font, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
