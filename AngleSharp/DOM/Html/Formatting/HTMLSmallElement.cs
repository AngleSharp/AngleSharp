namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The small HTML element.
    /// </summary>
    sealed class HTMLSmallElement : HTMLElement
    {
        public HTMLSmallElement(Document owner)
            : base(owner, Tags.Small, NodeFlags.HtmlFormatting)
        {
        }
    }
}
