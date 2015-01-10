namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The ruby HTML element.
    /// </summary>
    sealed class HTMLRubyElement : HTMLElement
    {
        public HTMLRubyElement(Document owner)
            : base(Tags.Ruby)
        {
            Owner = owner;
        }
    }
}
