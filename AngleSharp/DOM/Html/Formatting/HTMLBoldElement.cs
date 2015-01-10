namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The bold HTML element.
    /// </summary>
    sealed class HTMLBoldElement : HTMLElement
    {
        public HTMLBoldElement(Document owner)
            : base(Tags.B, NodeFlags.HtmlFormatting)
        {
            Owner = owner;
        }
    }
}
