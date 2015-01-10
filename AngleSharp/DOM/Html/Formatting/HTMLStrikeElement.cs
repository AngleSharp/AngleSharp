namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The strike HTML element.
    /// </summary>
    sealed class HTMLStrikeElement : HTMLElement
    {
        public HTMLStrikeElement(Document owner)
            : base(owner, Tags.Strike, NodeFlags.HtmlFormatting)
        {
        }
    }
}
