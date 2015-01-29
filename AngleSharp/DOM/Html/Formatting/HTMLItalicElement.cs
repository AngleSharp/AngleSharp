namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The i HTML element.
    /// </summary>
    sealed class HTMLItalicElement : HTMLElement
    {
        public HTMLItalicElement(Document owner)
            : base(owner, Tags.I, NodeFlags.HtmlFormatting)
        {
        }
    }
}
