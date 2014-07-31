namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The s HTML element.
    /// </summary>
    sealed class HTMLStruckElement : HTMLElement
    {
        internal HTMLStruckElement()
            : base(Tags.S, NodeFlags.HtmlFormatting)
        {
        }
    }
}
