namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The code HTML element.
    /// </summary>
    sealed class HtmlCodeElement : HtmlElement
    {
        public HtmlCodeElement(Document owner)
            : base(owner, Tags.Code, NodeFlags.HtmlFormatting)
        {
        }
    }
}
