namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

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
