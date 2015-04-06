namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The ruby HTML element.
    /// </summary>
    sealed class HtmlRubyElement : HtmlElement
    {
        public HtmlRubyElement(Document owner, String prefix = null)
            : base(owner, Tags.Ruby, prefix)
        {
        }
    }
}
