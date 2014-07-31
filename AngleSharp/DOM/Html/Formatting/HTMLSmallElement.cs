namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The small HTML element.
    /// </summary>
    sealed class HTMLSmallElement : HTMLElement
    {
        internal HTMLSmallElement()
            : base(Tags.Small, NodeFlags.HtmlFormatting)
        {
        }
    }
}
