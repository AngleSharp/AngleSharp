namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The i HTML element.
    /// </summary>
    sealed class HTMLItalicElement : HTMLElement
    {
        internal HTMLItalicElement()
            : base(Tags.I, NodeFlags.HtmlFormatting)
        {
        }
    }
}
