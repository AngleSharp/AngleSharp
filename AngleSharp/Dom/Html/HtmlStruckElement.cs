namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The s HTML element.
    /// </summary>
    sealed class HtmlStruckElement : HtmlElement
    {
        public HtmlStruckElement(Document owner)
            : base(owner, Tags.S, NodeFlags.HtmlFormatting)
        {
        }
    }
}
