namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the an HTML heading element (h1, h2, h3, h4, h5, h6).
    /// </summary>
    sealed class HtmlHeadingElement : HtmlElement, IHtmlHeadingElement
    {
        public HtmlHeadingElement(Document owner, String name = null, String prefix = null)
            : base(owner, name ?? TagNames.H1, prefix, NodeFlags.Special)
        {
        }
    }
}
