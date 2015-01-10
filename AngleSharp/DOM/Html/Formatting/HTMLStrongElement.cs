namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The strong HTML element.
    /// </summary>
    sealed class HTMLStrongElement : HTMLElement
    {
        public HTMLStrongElement(Document owner)
            : base(owner, Tags.Strong, NodeFlags.HtmlFormatting)
        {
        }
    }
}
