namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The em HTML element.
    /// </summary>
    sealed class HTMLEmphasizeElement : HTMLElement
    {
        public HTMLEmphasizeElement(Document owner)
            : base(owner, Tags.Em, NodeFlags.HtmlFormatting)
        {
        }
    }
}
