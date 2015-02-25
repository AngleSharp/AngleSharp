namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The strong HTML element.
    /// </summary>
    sealed class HtmlStrongElement : HtmlElement
    {
        public HtmlStrongElement(Document owner)
            : base(owner, Tags.Strong, NodeFlags.HtmlFormatting)
        {
        }
    }
}
