namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The bold HTML element.
    /// </summary>
    sealed class HTMLBoldElement : HTMLElement
    {
        public HTMLBoldElement(Document owner)
            : base(owner, Tags.B, NodeFlags.HtmlFormatting)
        {
        }
    }
}
