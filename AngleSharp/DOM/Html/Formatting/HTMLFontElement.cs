namespace AngleSharp.DOM.Html
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Represents a font element.
    /// See (19) obsolete features of [WHATWG].
    /// </summary>
    [DomHistorical]
    sealed class HTMLFontElement : HTMLElement
    {
        internal HTMLFontElement()
            : base(Tags.Font, NodeFlags.HtmlFormatting)
        {
        }
    }
}
