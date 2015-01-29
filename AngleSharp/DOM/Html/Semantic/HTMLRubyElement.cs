namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The ruby HTML element.
    /// </summary>
    sealed class HTMLRubyElement : HTMLElement
    {
        public HTMLRubyElement(Document owner)
            : base(owner, Tags.Ruby)
        {
        }
    }
}
