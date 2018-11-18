namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents an HTML element with only semantic meaning.
    /// </summary>
    sealed class HtmlSemanticElement : HtmlElement, IHtmlSemanticElement
    {
        public HtmlSemanticElement(Document owner, String name, String prefix = null)
            : base(owner, name, prefix, NodeFlags.Special)
        {
        }
    }
}
