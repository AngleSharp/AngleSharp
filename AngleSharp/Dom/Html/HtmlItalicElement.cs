namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The i HTML element.
    /// </summary>
    sealed class HtmlItalicElement : HtmlElement
    {
        public HtmlItalicElement(Document owner)
            : base(owner, Tags.I, NodeFlags.HtmlFormatting)
        {
        }
    }
}
