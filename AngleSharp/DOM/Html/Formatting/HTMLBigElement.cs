namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The big HTML element.
    /// </summary>
    sealed class HTMLBigElement : HTMLElement
    {
        public HTMLBigElement(Document owner)
            : base(Tags.Big, NodeFlags.HtmlFormatting)
        {
            Owner = owner;
        }
    }
}
