namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The small HTML element.
    /// </summary>
    sealed class HtmlSmallElement : HtmlElement
    {
        public HtmlSmallElement(Document owner)
            : base(owner, Tags.Small, NodeFlags.HtmlFormatting)
        {
        }
    }
}
