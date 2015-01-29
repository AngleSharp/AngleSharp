namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The s HTML element.
    /// </summary>
    sealed class HTMLStruckElement : HTMLElement
    {
        public HTMLStruckElement(Document owner)
            : base(owner, Tags.S, NodeFlags.HtmlFormatting)
        {
        }
    }
}
