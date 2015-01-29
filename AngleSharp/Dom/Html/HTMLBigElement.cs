namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The big HTML element.
    /// </summary>
    sealed class HtmlBigElement : HtmlElement
    {
        public HtmlBigElement(Document owner)
            : base(owner, Tags.Big, NodeFlags.HtmlFormatting)
        {
        }
    }
}
