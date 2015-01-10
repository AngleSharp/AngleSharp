namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The code HTML element.
    /// </summary>
    sealed class HTMLCodeElement : HTMLElement
    {
        public HTMLCodeElement(Document owner)
            : base(Tags.Code, NodeFlags.HtmlFormatting)
        {
            Owner = owner;
        }
    }
}
