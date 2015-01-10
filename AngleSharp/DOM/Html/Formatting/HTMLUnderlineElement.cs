namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The u HTML element.
    /// </summary>
    sealed class HTMLUnderlineElement : HTMLElement
    {
        public HTMLUnderlineElement(Document owner)
            : base(Tags.U, NodeFlags.HtmlFormatting)
        {
            Owner = owner;
        }
    }
}
