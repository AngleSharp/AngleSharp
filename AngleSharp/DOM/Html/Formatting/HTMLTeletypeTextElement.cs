namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The tt HTML element.
    /// </summary>
    sealed class HTMLTeletypeTextElement : HTMLElement
    {
        public HTMLTeletypeTextElement(Document owner)
            : base(owner, Tags.Tt, NodeFlags.HtmlFormatting)
        {
        }
    }
}
