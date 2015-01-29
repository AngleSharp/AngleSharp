namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The ruby HTML element.
    /// </summary>
    sealed class HTMLRubyElement : HtmlElement
    {
        public HTMLRubyElement(Document owner)
            : base(owner, Tags.Ruby)
        {
        }
    }
}
