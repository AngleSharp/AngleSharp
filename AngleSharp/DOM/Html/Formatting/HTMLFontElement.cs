namespace AngleSharp.DOM.Html
{
    using AngleSharp.Attributes;
    using AngleSharp.Html;

    /// <summary>
    /// Represents a font element.
    /// See (19) obsolete features of [WHATWG].
    /// </summary>
    [DomHistorical]
    sealed class HTMLFontElement : HTMLElement
    {
        public HTMLFontElement(Document owner)
            : base(owner, Tags.Font, NodeFlags.HtmlFormatting)
        {
        }
    }
}
