namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The code HTML element.
    /// </summary>
    sealed class HTMLCodeElement : HTMLElement
    {
        public HTMLCodeElement(Document owner)
            : base(owner, Tags.Code, NodeFlags.HtmlFormatting)
        {
        }
    }
}
