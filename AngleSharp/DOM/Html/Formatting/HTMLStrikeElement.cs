namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The strike HTML element.
    /// </summary>
    sealed class HTMLStrikeElement : HTMLElement
    {
        internal HTMLStrikeElement()
            : base(Tags.Strike, NodeFlags.HtmlFormatting)
        {
        }
    }
}
