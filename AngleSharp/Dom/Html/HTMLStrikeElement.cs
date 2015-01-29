namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The strike HTML element.
    /// </summary>
    sealed class HtmlStrikeElement : HtmlElement
    {
        public HtmlStrikeElement(Document owner)
            : base(owner, Tags.Strike, NodeFlags.HtmlFormatting)
        {
        }
    }
}
