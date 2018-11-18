namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// The ruby HTML element.
    /// </summary>
    sealed class HtmlRubyElement : HtmlElement
    {
        public HtmlRubyElement(Document owner, String prefix = null)
            : base(owner, TagNames.Ruby, prefix)
        {
        }
    }
}
