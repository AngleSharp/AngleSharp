namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The bold HTML element.
    /// </summary>
    sealed class HtmlBoldElement : HtmlElement
    {
        public HtmlBoldElement(Document owner)
            : base(owner, Tags.B, NodeFlags.HtmlFormatting)
        {
        }
    }
}
