namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The nobr HTML element.
    /// </summary>
    sealed class HTMLNoNewlineElement : HTMLElement
    {
        public HTMLNoNewlineElement(Document owner)
            : base(owner, Tags.NoBr, NodeFlags.HtmlFormatting)
        {
        }
    }
}
