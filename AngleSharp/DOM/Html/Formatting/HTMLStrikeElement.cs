namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The strike HTML element.
    /// </summary>
    sealed class HTMLStrikeElement : HTMLElement
    {
        public HTMLStrikeElement(Document owner)
            : base(Tags.Strike, NodeFlags.HtmlFormatting)
        {
            Owner = owner;
        }
    }
}
