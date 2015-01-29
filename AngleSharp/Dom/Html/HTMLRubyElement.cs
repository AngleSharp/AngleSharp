namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The ruby HTML element.
    /// </summary>
    sealed class HtmlRubyElement : HtmlElement
    {
        public HtmlRubyElement(Document owner)
            : base(owner, Tags.Ruby)
        {
        }
    }
}
