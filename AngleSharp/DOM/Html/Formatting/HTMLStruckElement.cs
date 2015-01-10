namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The s HTML element.
    /// </summary>
    sealed class HTMLStruckElement : HTMLElement
    {
        public HTMLStruckElement(Document owner)
            : base(Tags.S, NodeFlags.HtmlFormatting)
        {
            Owner = owner;
        }
    }
}
